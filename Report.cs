using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CCTime
{
	class Report
	{
		public Report( DateTime currentDate)
		{
			var d = currentDate;
			var currentWeek = Utilities.GetIso8601WeekOfYear( currentDate );
			var dayNames = new string[7];

			// Find start of week
			while( currentWeek == Utilities.GetIso8601WeekOfYear( d.AddDays( -1 ) ) )
			{
				d = d.AddDays( -1 );
			}

			var chart = new Dictionary<string, int[]>();

			// Collect data from all days
			var satTotals = 0;
			var sunTotals = 0;
			var foundUnallocatedHours = false;
			for( int i = 0; i < 7; i++ )
			{
				dayNames[i] = d.ToString( "ddd" );		// Gets the localized day name

				var data = TaskManager.GetDay( d );
				if( data != null )
				{
					foreach( var task in data )
					{
						if(task.Title == Settings.UnaccountedString && task.Minutes != 0)
						{
							foundUnallocatedHours = true;
						}

						if( !chart.ContainsKey( task.Title ) )
						{
							chart.Add( task.Title, new int[7] );
						}
						chart[task.Title][i] += task.Minutes;
						if( i == 5 ) satTotals += task.Minutes;
						if( i == 6 ) sunTotals += task.Minutes;
					}
				}
				d = d.AddDays( 1 );
			}

			if( chart.Count <= 2 )
			{
				MessageBox.Show( "Nothing to show yet...", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name );
				return;
			}

			if( foundUnallocatedHours && Settings.ReportWarnIfUnaccountedHours )
			{
				MessageBox.Show( "Warning: There are still hours left unaccounted for this week...", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
			}

			var totals = chart[Settings.TotalsString];
			chart.Remove( Settings.TotalsString );
			var unaccounted = chart[Settings.UnaccountedString];
			chart.Remove( Settings.UnaccountedString );

			var onlyTitles = chart.Select( x => x.Key.Split(';')[0] );
			var longestTaskName = onlyTitles.Max( s => s.Length );
			var startIndent = Math.Max( longestTaskName, 16 );

			// Remove sat/sun if empty
			var lastDayIndex = 7;
			if( sunTotals == 0 )
			{
				lastDayIndex = 6;
				if( satTotals == 0 )
				{
					lastDayIndex = 5;
				}
			}


			// Generate table

			// Header: Week + Day names
			var report = ( Settings.ReportWeekString + " " + currentWeek.ToString() ).PadRight( startIndent );
			for( int i = 0; i < lastDayIndex; i++ )
			{
				report += dayNames[i].PadLeft( Settings.ReportTabSize );
			}
			report += System.Environment.NewLine + System.Environment.NewLine;

			// Each task
			foreach( var task in chart )
			{
				if( task.Value.Sum() > 0 )	// skip task with no hours whole week
				{
					string[] tmp = task.Key.Split( ';' );
					var title = tmp[0];
					var comment = tmp.Length > 1 ? tmp[1] : "";

					report += title.PadRight( startIndent );
					for( int i = 0; i < lastDayIndex; i++ )
					{
						var hours = FormatTime( task.Value[i] );
						if( hours == "0" )
							report += Settings.ReportZeroIndicator.PadLeft( Settings.ReportTabSize );
						else
							report += hours.PadLeft( Settings.ReportTabSize );
					}

					if( Settings.ReportShowTotalsColumn )
					{
						var sum = FormatTime( task.Value.Sum() );
						sum = sum.PadRight( 4 );	// helps aligning comment below correctly
						report += ( "".PadLeft( Settings.ReportTabSize ) + "= " + sum );
					}

					if( comment != "" )
					{
						report += ( "".PadLeft( Settings.ReportTabSize ) + comment );
					}

					report += System.Environment.NewLine;
				}
			}

			// Add totals below all tasks
			if( Settings.ReportShowTotalsRow )
			{
				report += System.Environment.NewLine;
				report += Settings.ReportTotalsString.PadRight( startIndent );
				for( int i = 0; i < lastDayIndex; i++ )
				{
					var hours = FormatTime( totals[i] );
					report += hours.PadLeft( Settings.ReportTabSize );
				}
				if( Settings.ReportShowTotalsColumn )
				{
					report += ( "".PadLeft( Settings.ReportTabSize ) + "= " + FormatTime( totals.Sum() ) );
				}
			}
			report += System.Environment.NewLine;

			// Finally unaccounted minutes
			if( Settings.ReportShowUnaccountedRow && foundUnallocatedHours )
			{
				report += Settings.ReportUnaccountedString.PadRight( startIndent );
				for( int i = 0; i < lastDayIndex; i++ )
				{
					var hours = FormatTime( unaccounted[i] );
					report += hours.PadLeft( Settings.ReportTabSize );
				}
			}
			report += System.Environment.NewLine;

			// Show the report
			ShowReportForm( "Report for week " + currentWeek, report );
			if( Settings.ReportAutoCopyToClipboard )
			{
				Clipboard.SetText( report );
			}
		}

		private void ShowReportForm( string title, string data)
		{
			Form form = new Form();
			TextBox textBox = new TextBox();

			textBox.Multiline = true;
			textBox.WordWrap = false;
			textBox.ScrollBars = ScrollBars.Both;
			textBox.Font = new Font( "Consolas", 12, FontStyle.Regular );
			textBox.Anchor = textBox.Anchor | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;

			form.ClientSize = new System.Drawing.Size( Settings.ReportWindowWidth, Settings.ReportWindowHeight );
			textBox.SetBounds( 0, 0, Settings.ReportWindowWidth, Settings.ReportWindowHeight );

			form.Controls.AddRange( new Control[] { textBox } );
			form.StartPosition = FormStartPosition.CenterScreen;

			textBox.Text = data;
			form.Text = title;
			textBox.Focus();

			form.KeyPreview = true;
			form.KeyUp += new KeyEventHandler( delegate( object sender, KeyEventArgs e ) { if( e.KeyCode == Keys.Escape ) ( sender as Form ).Close(); });
			textBox.KeyDown += new KeyEventHandler( delegate( object sender, KeyEventArgs e ) { if( e.Control && e.KeyCode == Keys.A ) { e.SuppressKeyPress = true; textBox.SelectAll(); } } );

			form.Show();
			textBox.SelectionLength = 0;
		}

		private string FormatTime(int minutes)
		{
			switch( Settings.ReportTimeFormat.ToLower() )
			{
				case "minutes":
					return minutes.ToString();

				case "hours":
					return Utilities.MinutesToHours( minutes );

				case "fractional":
				default:
					return Utilities.MinutesToFractionalHours( minutes );
			}
			
		}

	}
}
