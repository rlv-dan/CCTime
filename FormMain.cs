using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using System.Timers;
using System.Linq;
using IniParser;
using IniParser.Model;
using System.IO;

namespace CCTime
{
	public partial class FormMain : Form
	{
		private DateTime currentlyViewingDate;

		private System.Timers.Timer minuteTimer;
		private int minutTimerSeconds = 0;

		private bool okToHide = true;

		public FormMain()
		{
			InitializeComponent();
			this.Opacity = 0;	// if hide at startup, this prevents flashing by
		}

		private void Form1_Load( object sender, EventArgs e )
		{
			Settings.InitSettings();

			this.Width = Settings.WindowWidth;
			this.Height = Settings.WindowHeight;

			TaskManager.InitData();

			currentlyViewingDate = DateTime.Now;

			InitTimers();

			ShowDate();

			InitObjectListView();

			PositionForm();

			InitSystemTrayIconMenu();

			this.ShowInTaskbar = false;

			SetTimerState( Settings.ClockOnAtStartup );

			RefreshCurrentView();

			if( Settings.StartMinimized )
				HideForm();
			else
				this.Opacity = 1;
		}

		private void Form1_Deactivate( object sender, EventArgs e )		// lost focus
		{
			if( okToHide )
			{
				HideForm();
			}
		}

		private void Form1_FormClosing( object sender, FormClosingEventArgs e )
		{
			if( Settings.CloseButtonMinimizes )
			{
				HideForm();
				e.Cancel = true;
			}
			else
			{
				QuitApp();
			}
		}

		private void FormMain_KeyDown( object sender, KeyEventArgs e )
		{
			if( objectListView.IsCellEditing )
			{
				return;
			}
			RefreshCurrentView();
		}

		private void FormMain_KeyUp( object sender, KeyEventArgs e )
		{
			if( objectListView.IsCellEditing )
			{
				return;
			}

			if( e.KeyCode == Keys.Left )
			{
				buttonPrevDate_Click( null, null );
			}
			else if( e.KeyCode == Keys.Right )
			{
				buttonNextDate_Click( null, null );
			}

			RefreshCurrentView();
		}


		// --- Form Utilities -----------------------------------------------------------

		private void ShowForm()
		{
			this.Opacity = 1;
			this.Visible = true;
			if( Settings.ReturnToTodayWhenRestoring )
			{
				currentlyViewingDate = DateTime.Now;
				ShowDate();
			}
			PositionForm();
			this.Activate();
			this.BringToFront();
		}

		private void HideForm()
		{
			this.Opacity = 0;
			this.Visible = false;
			TaskManager.PersistData();
		}

		private void PositionForm()
		{
			switch( Settings.WindowPosition.ToLower() )
			{
				case "topleft":
					this.Top = Screen.PrimaryScreen.WorkingArea.Top;
					this.Left = Screen.PrimaryScreen.WorkingArea.Left;
					break;

				case "topright":
					this.Top = Screen.PrimaryScreen.WorkingArea.Top;
					this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
					break;

				case "bottomleft":
					this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
					this.Left = Screen.PrimaryScreen.WorkingArea.Left;
					break;

				case "bottomright":
				default:
					this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
					this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
					break;
			}
		}

		private void QuitApp()
		{
			if( minuteTimer != null)	// Null means application did not initialize. Could be an error, so we should not persist data
			{
				minuteTimer.Enabled = false;
				okToHide = false;
				notifyIcon.Visible = false;
				TaskManager.PersistData();

				Application.Exit();
			}
		}

		private void ShowDate()
		{
			// ReDraws UI

			TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
			labelDate.Text = textInfo.ToTitleCase( currentlyViewingDate.ToString( "dddd d MMMM" ) );

			var currentTasks = TaskManager.GetDay( currentlyViewingDate );

			objectListView.SetObjects( currentTasks );

			RefreshCurrentView();

			buttonNextDate.Enabled = TaskManager.GetNextDate( currentlyViewingDate ) != null;
			buttonPrevDate.Enabled = TaskManager.GetPreviousDate( currentlyViewingDate ) != null;
		}

		private void RefreshCurrentView()
		{
			// update unaccounted minutes
			var currentTasks = TaskManager.GetDay( currentlyViewingDate );
			var totalMinutes = currentTasks[0].Minutes;
			var totalAccountedFor = 0;
			for( int i = 2; i < currentTasks.Count; i++ )
			{
				totalAccountedFor += currentTasks[i].Minutes;
			}
			currentTasks[1].Minutes = totalMinutes - totalAccountedFor;

			// force view to refresh
			for( int i = 0; i < currentTasks.Count; i++ )
			{
				objectListView.RefreshObject( currentTasks[i] );
			}
		}

		// --- Timers --------------------------------------------------------------------

		private void InitTimers()
		{
			// setup main timer
			minuteTimer = new System.Timers.Timer();
			minuteTimer.Interval = 1000;
			minuteTimer.Elapsed += delegate( object source, ElapsedEventArgs ev )
			{
				// This runs once a second
				minutTimerSeconds++;
				var todayTasks = TaskManager.GetToday();

				if( minutTimerSeconds >= 60 )
				{	// one minute has passed
					minutTimerSeconds = 0;
					foreach( var t in todayTasks )
					{
						if( t.Type == TaskType.Total )
						{
							t.Minutes++;
						}
						else if( t.Type == TaskType.Normal && t.AutoTick )
						{
							t.Minutes++;
						}
					}
					
					RefreshCurrentView();
				}

				try
				{
					if( !this.IsDisposed && !this.Disposing )
					{
						this.Invoke( new Action( () =>
						{
							int hours = ( todayTasks[0].Minutes - todayTasks[0].Minutes % 60 ) / 60;
							this.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "  [" + hours.ToString( "D2" ) + ":" + ( todayTasks[0].Minutes - hours * 60 ).ToString( "D2" ) + ":" + minutTimerSeconds.ToString( "D2" ) + "]";
						} ) );
					}
				}
				catch( Exception ex )
				{
				}
			};

			// save data every 10 minutes to be safe
			var persisTimer = new System.Timers.Timer();
			persisTimer.Interval = 600000;
			persisTimer.Elapsed += delegate( object source, ElapsedEventArgs ev )
			{
				TaskManager.PersistData();
			};
			persisTimer.Enabled = true;

		}

		private void SetTimerState( bool enabled )
		{
			if( enabled )
			{
				minuteTimer.Enabled = true;
				buttonTimerOnOff.Image = CCTime.Properties.Resources.clock;
				this.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
				RefreshCurrentView();
			}
			else
			{
				minuteTimer.Enabled = false;
				buttonTimerOnOff.Image = CCTime.Properties.Resources.clock_gray;
				this.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " [STOPPED]";
			}
		}


		// --- ObjectListView -----------------------------------------------------------

		private void InitObjectListView()
		{
			objectListView.ButtonClick += ClickListviewButton;

			this.olvColumn6.AspectGetter = delegate( object x )
			{
				var task = x as Task;
				var tick = "";
				if(task.Type == TaskType.Normal && task.AutoTick )
				{
					tick = "●";
				}
				return tick;
			};

			// show task title without comment (if any)
			this.olvColumn1.AspectToStringConverter = delegate( object x )
			{
				string[] tmp = ( x as string ).Split( ';' );
				if( tmp.Length > 1 )
					return tmp[0] + "*";
				else if( tmp.Length == 1 )
					return tmp[0];
				else
					return "";
			};


			// show minutes as hours:minutes
			this.olvColumn2.AspectToStringConverter = delegate( object x )
			{
				int minutes = (int)x;

				var neg = "";
				if( minutes < 0 )
				{
					neg = "-";
					minutes = Math.Abs( minutes );
				}

				int hours = ( minutes - minutes % 60 ) / 60;
				return neg + hours.ToString( "D2" ) + ":" + ( minutes - hours * 60 ).ToString( "D2" );
			};

			// button labels
			this.olvColumn3.AspectToStringConverter = delegate( object x )
			{
				return GetButtonLabel( (string)x, 3 );
			};
			this.olvColumn4.AspectToStringConverter = delegate( object x )
			{
				return GetButtonLabel( (string)x, 4 );
			};
			this.olvColumn5.AspectToStringConverter = delegate( object x )
			{
				return GetButtonLabel( (string)x, 5 );
			};

		}

		private string GetButtonLabel(string source, int column)
		{
			// Converts original button label depending on modifier keys

			if( objectListView.IsCellEditing )
			{
				return source;
			}


			if( source == "" ) return "";

			var label = source;

			if( column == 3 )	// 15 min
			{
				if( ModifierKeys.HasFlag( Keys.Shift ) )
				{
					label = label.Replace( "+", "- " );
					label = label.Replace( "< ", "- " );
				}
				if( ModifierKeys.HasFlag( Keys.Control ) )
				{
					label = label = "< " + Settings.MinutesOnClockButtons;
				}
			}

			if( column == 4 )	// 30 min
			{
				if( ModifierKeys.HasFlag( Keys.Shift ) )
				{
					label = label.Replace( "+", "- " );
				}
				if( ModifierKeys.HasFlag( Keys.Control ) )
				{
					label = "";
				}
			}

			if( column == 5 )	// 60 min
			{
				if( ModifierKeys.HasFlag( Keys.Shift ) )
				{
					label = label.Replace( "+", "- " );
					if( label.Contains( " >" ) )
					{
						label = "+" + label.Replace( " >", "" );
					}
				}
				if( ModifierKeys.HasFlag( Keys.Control ) )
				{
					label = label = Settings.MinutesOnClockButtons + " >";
				}
			}
			return label;
		}

		private void ClickListviewButton( object senderObject, CellClickEventArgs ev )
		{
			// Processes time based on buttons labels 
			
			var label = GetButtonLabel( ev.SubItem.ModelValue.ToString(), ev.ColumnIndex );
			var timerState = minuteTimer.Enabled;
			minuteTimer.Enabled = false;
			var m = ev.Model as Task;
			var modifier = Regex.Replace( label, "[0-9 -]", "" );
			var minutes = Convert.ToInt32( Regex.Replace( label, "[^0-9-]", "" ) );
			if( modifier == "<" ) 
			{
				if( minutes == 15 )
				{
					// round down to nearest 15 min
					if( m.Minutes == 0 || m.Minutes % 15 == 0 || m.Minutes % 30 == 0 || m.Minutes % 45 == 0 || m.Minutes % 60 == 0 ) m.Minutes--;	// if already at nearest 15 min, allow skipping past
					while( m.Minutes != 0 && m.Minutes % 15 != 0 && m.Minutes % 30 != 0 && m.Minutes % 45 != 0 && m.Minutes % 60 != 0 ) m.Minutes--;
				}
				else if( minutes == 30 )
				{
					// round down to nearest 30 min
					if( m.Minutes == 0 || m.Minutes % 30 == 0 || m.Minutes % 60 == 0 ) m.Minutes--;	// if already at nearest 30 min, allow skipping past
					while( m.Minutes != 0 && m.Minutes % 30 != 0 && m.Minutes % 60 != 0 ) m.Minutes--;
				}
				else if( minutes == 60 )
				{
					// round down to nearest 60 min
					if( m.Minutes == 0 || m.Minutes % 60 == 0 ) m.Minutes--;	// if already at nearest 60 min, allow skipping past
					while( m.Minutes != 0 && m.Minutes % 60 != 0 ) m.Minutes--;
				}
				else 
				{
					// skip rounding, just subtract
					m.Minutes -= minutes;
				}
			}
			else if( modifier == ">" )
			{
				if( minutes == 15 )
				{
					// round up to nearest 15 min
					if( m.Minutes == 0 || m.Minutes % 15 == 0 || m.Minutes % 30 == 0 || m.Minutes % 45 == 0 || m.Minutes % 60 == 0 ) m.Minutes++;	// if already at nearest 15 min, allow skipping past
					while( m.Minutes != 0 && m.Minutes % 15 != 0 && m.Minutes % 30 != 0 && m.Minutes % 45 != 0 && m.Minutes % 60 != 0 ) m.Minutes++;
				}
				else if( minutes == 30 )
				{
					// round up to nearest 30 min
					if( m.Minutes == 0 || m.Minutes % 30 == 0 || m.Minutes % 60 == 0 ) m.Minutes++;	// if already at nearest 30 min, allow skipping past
					while( m.Minutes != 0 && m.Minutes % 30 != 0 && m.Minutes % 60 != 0 ) m.Minutes++;
				}
				else if( minutes == 60 )
				{
					// round up to nearest 6+ min
					if( m.Minutes == 0 || m.Minutes % 60 == 0 ) m.Minutes++;	// if already at nearest 60 min, allow skipping past
					while( m.Minutes != 0 && m.Minutes % 60 != 0 ) m.Minutes++;
				}
				else
				{
					// skip rounding, just add
					m.Minutes += minutes;
				}
			}
			else 
			{
				// normal
				m.Minutes += minutes; 
			}

			minuteTimer.Enabled = timerState;
			RefreshCurrentView();
		}

		private void objectListView1_CellEditStarting( object sender, CellEditEventArgs e )
		{
			// prevent editing totals & unaccounted
			var task = e.RowObject as Task;
			if( task.Type != TaskType.Normal )
			{
				if( e.Column.Index == 0 )	// name column
				{
					e.Cancel = true;
				}
				if( e.Column.Index == 2 )	// time column
				{
					// unaccounted time is calculated, so don't allow editing it either
					if( task.Type != TaskType.Total )
					{
						e.Cancel = true;
					}
				}
			}
		}

		private void objectListView1_CellEditFinished( object sender, CellEditEventArgs e )
		{
			RefreshCurrentView();
		}


		private void objectListView1_KeyUp( object sender, KeyEventArgs e )
		{
			if(e.KeyCode == Keys.Delete)
			{
				buttonTaskDel_Click( new object(), new EventArgs() );
			}
		}


		private void objectListView1_CellRightClick( object sender, CellRightClickEventArgs e )
		{
			var task = e.Item.RowObject as Task;
			task.AutoTick = !task.AutoTick;
			//e.Item.Selected = false;
			RefreshCurrentView();
		}

		// --- GUI Buttons --------------------------------------------------------------

		private void buttonPrevDate_Click( object sender, EventArgs e )
		{
			var prevDay = TaskManager.GetPreviousDate( currentlyViewingDate );
			if( prevDay != null )
			{
				currentlyViewingDate = DateTime.Parse( prevDay );
				ShowDate();
			}
		}

		private void buttonNextDate_Click( object sender, EventArgs e )
		{
			var nextDay = TaskManager.GetNextDate( currentlyViewingDate );
			if ( nextDay != null )
			{
				currentlyViewingDate = DateTime.Parse(nextDay);
				ShowDate();
			}
		}

		private void labelDate_Click( object sender, EventArgs e )
		{
			// go to date when clicking the date label
			var hideState = okToHide;
			okToHide = false;

			var dateSelectForm = new FormDateSelect( currentlyViewingDate, true, "Select a Date to View" );
			dateSelectForm.ShowDialog();
			if( dateSelectForm.SelectedDate != DateTime.MinValue )
			{
				currentlyViewingDate = dateSelectForm.SelectedDate;
				ShowDate();
			}

			okToHide = hideState;
		}

		private void buttonTaskAdd_Click( object sender, EventArgs e )
		{
			var hideState = okToHide;
			var timerState = minuteTimer.Enabled;
			okToHide = false;
			minuteTimer.Enabled = false;

			if( Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Shift )
			{
				// Add day
				var dateSelectForm = new FormDateSelect( currentlyViewingDate, false, "Select a Date to Add" );
				dateSelectForm.ShowDialog();
				if( dateSelectForm.SelectedDate != DateTime.MinValue )
				{
					var copyTasks = MessageBox.Show( "Copy tasks from currently viewing day?", "Copy Tasks?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question );

					if( copyTasks != System.Windows.Forms.DialogResult.Cancel )
					{
						if( copyTasks == System.Windows.Forms.DialogResult.Yes )
						{
							TaskManager.AddDay( dateSelectForm.SelectedDate, currentlyViewingDate.ToShortDateString() );
						}
						else
						{
							TaskManager.AddDay( dateSelectForm.SelectedDate );
						}

						currentlyViewingDate = dateSelectForm.SelectedDate;
					}
				}
			}
			else
			{
				// Add task
				var title = Microsoft.VisualBasic.Interaction.InputBox( "Add Task", "Name the task:", "New Task" );
				if( title.Trim() != "" )
				{
					TaskManager.AddTaskToDay( currentlyViewingDate, title );
				}
			}

			ShowDate();
			minuteTimer.Enabled = timerState;
			okToHide = hideState;
		}

		private void buttonTaskDel_Click( object sender, EventArgs e )
		{
			var hideState = okToHide;
			okToHide = false;

			if( Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Shift )
			{
				// Delete day
				if( currentlyViewingDate.ToShortDateString() == DateTime.Now.ToShortDateString() )
				{
					MessageBox.Show( "Can't remove todays date...", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
				else
				{
					var confirm = MessageBox.Show( "Remove " + currentlyViewingDate.ToString( "dddd d MMMM" ) + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question );

					if( confirm == System.Windows.Forms.DialogResult.Yes )
					{
						var nextDay = TaskManager.GetNextDate( currentlyViewingDate );
						var prevDay = TaskManager.GetPreviousDate( currentlyViewingDate );
						if( nextDay != null || prevDay != null )	// prevent removing all dates...
						{
							TaskManager.DeleteDay( currentlyViewingDate );
							if( nextDay != null )
								currentlyViewingDate = DateTime.Parse( nextDay );
							else
								currentlyViewingDate = DateTime.Parse( prevDay );
							ShowDate();
						}
					}
				}
			}
			else if( Control.ModifierKeys == Keys.Alt )
			{
				// Delete all tasks with 0 hours
				var confirm = MessageBox.Show( "Remove all task with 0 time?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
				if( confirm == System.Windows.Forms.DialogResult.Yes )
				{
					var timerState = minuteTimer.Enabled;
					minuteTimer.Enabled = false;

					TaskManager.RemoveTasksWithZeroTime( currentlyViewingDate );

					ShowDate();
					minuteTimer.Enabled = timerState;
				}
			}
			else
			{
				// Delete task
				if( objectListView.SelectedIndex > 1 )
				{
					var confirm = System.Windows.Forms.DialogResult.Yes;
					if( ( objectListView.SelectedObject as Task ).Minutes > 0 )
					{
						confirm = MessageBox.Show( "Remove task, are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
					}

					if( confirm == System.Windows.Forms.DialogResult.Yes )
					{
						var timerState = minuteTimer.Enabled;
						minuteTimer.Enabled = false;
						
						var previouslySelected = objectListView.SelectedIndex;

						TaskManager.RemoveTask( currentlyViewingDate, objectListView.SelectedIndex );
						ShowDate();

						// restore selection to "next" item
						if( previouslySelected > objectListView.Items.Count - 1) previouslySelected = objectListView.Items.Count - 1;
						objectListView.SelectedIndex = previouslySelected;
						objectListView.FocusedItem = objectListView.SelectedItems[0];
						objectListView.Focus();

						minuteTimer.Enabled = timerState;
					}
				}

			}

			okToHide = hideState;
		}

		private void buttonTaskUp_Click( object sender, EventArgs e )
		{
			var index = objectListView.SelectedIndex;
			if( index <= 2 ) return;

			var timerState = minuteTimer.Enabled;
			minuteTimer.Enabled = false;

			if( Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Shift )
			{
				// move to top
				while( index > 2 )
				{
					TaskManager.MoveTask( currentlyViewingDate, index, index - 1 );
					index--;
				}
			}
			else
			{
				// move one step up
				TaskManager.MoveTask( currentlyViewingDate, index, index - 1 );
				index--;
			}

			ShowDate();
			objectListView.SelectedIndex = index;
			objectListView.FocusedItem = objectListView.SelectedItems[0];
			objectListView.Focus();
			minuteTimer.Enabled = timerState;
		}

		private void buttonTaskDown_Click( object sender, EventArgs e )
		{
			var index = objectListView.SelectedIndex;
			if( index >= objectListView.GetItemCount() ) return;
			if( index <= 1 ) return;

			var timerState = minuteTimer.Enabled;
			minuteTimer.Enabled = false;
			
			if( Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Shift )
			{
				// move to bottom
				while( index < objectListView.GetItemCount() )
				{
					TaskManager.MoveTask( currentlyViewingDate, index, index + 1 );
					index++;
				}
				index--;
			}
			else
			{
				// move one step down
				TaskManager.MoveTask( currentlyViewingDate, index, index + 1 );
				index++;
			}

			ShowDate();
			objectListView.SelectedIndex = index;
			objectListView.FocusedItem = objectListView.SelectedItems[0];
			objectListView.Focus();
			minuteTimer.Enabled = timerState;
		}

		private void buttonWeeklyReport_Click( object sender, EventArgs e )
		{
			new Report( currentlyViewingDate );
		}

		private void buttonTimerOnOff_Click( object sender, EventArgs e )
		{
			SetTimerState( !minuteTimer.Enabled );
		}

		private void buttonAlwaysOnTop_Click( object sender, EventArgs e )
		{
			var b = sender as Button;
			if( this.TopMost )
			{
				this.TopMost = false;
				okToHide = true;
				b.Image = CCTime.Properties.Resources.pin_gray;
			}
			else
			{
				this.TopMost = true;
				okToHide = false;
				b.Image = CCTime.Properties.Resources.pin;
			}
		}


		// --- System Tray Icon ---------------------------------------------------------

		private void InitSystemTrayIconMenu()
		{
			this.notifyIcon.Text = this.Text;
			this.notifyIcon.ContextMenu = new ContextMenu();
			this.notifyIcon.ContextMenu.MenuItems.Add( new MenuItem( "Show Window", new EventHandler( notifyIconHandler_Show ) ) );
			this.notifyIcon.ContextMenu.MenuItems.Add( new MenuItem( "-" ) );
			var help = new MenuItem( "Help" );
			this.notifyIcon.ContextMenu.MenuItems.Add( help );
			help.MenuItems.Add( new MenuItem( "About", new EventHandler( notifyIconHandler_About ) ) );
			help.MenuItems.Add( new MenuItem( "Open ReadMe file", new EventHandler( notifyIconHandler_ReadMe ) ) );
			help.MenuItems.Add( new MenuItem( "Go to Homepage", new EventHandler( notifyIconHandler_Homepage ) ) );
			this.notifyIcon.ContextMenu.MenuItems.Add( new MenuItem( "Settings", new EventHandler( notifyIconHandler_Settings ) ) );
			this.notifyIcon.ContextMenu.MenuItems.Add( new MenuItem( "-" ) );
			this.notifyIcon.ContextMenu.MenuItems.Add( new MenuItem( "Exit", new EventHandler( notifyIconHandler_Exit ) ) );
			this.notifyIcon.Visible = true;  // Shows the notify icon in the system tray
		}

		private void notifyIcon_Click( object sender, EventArgs e )
		{
			ShowForm();
		}

		private void notifyIconHandler_Show( object sender, EventArgs e )
		{
			ShowForm();
		}

		private void notifyIconHandler_About( object sender, EventArgs e )
		{
			okToHide = false;
			new FormAbout().ShowDialog();
			okToHide = true;
		}

		private void notifyIconHandler_ReadMe( object sender, EventArgs e )
		{
			var appFolder = Path.GetDirectoryName( Application.ExecutablePath );
			System.Diagnostics.Process.Start( appFolder + @"\ReadMe.txt" );
		}

		private void notifyIconHandler_Homepage( object sender, EventArgs e )
		{
			System.Diagnostics.Process.Start( "https://www.rlvision.com" );
		}

		private void notifyIconHandler_Settings( object sender, EventArgs e )
		{
			var appFolder = Path.GetDirectoryName( Application.ExecutablePath );
			System.Diagnostics.Process.Start( "notepad.exe", appFolder + @"\Settings.ini" );
		}

		private void notifyIconHandler_Exit( object sender, EventArgs e )
		{
			QuitApp();
		}

	}
}
