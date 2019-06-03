using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CCTime
{
	public partial class FormDateSelect : Form
	{
		private bool ChooseFromAvailable;	// true -> can only select available date, false -> can only select dates without data
		private string FormTitle;

		private bool allowDate( DateTime dt )
		{
			/*if( dt.Date > DateTime.Today )
			{
				// don't allow future dates
				return false;
			}*/

			if(this.ChooseFromAvailable)
			{
				return TaskManager.DayExsists( dt );
			}
			else
			{
				return !TaskManager.DayExsists( dt );
			}
		}

		public FormDateSelect( DateTime dt, bool chooseFromAvailable, string formTitle )
		{
			this.ChooseFromAvailable = chooseFromAvailable;
			this.FormTitle = formTitle;

			InitializeComponent();
			this.SelectedDate = DateTime.MinValue;
			buttonOk.Enabled = allowDate(monthCalendar.SelectionStart);

			// set bolded dates to indicate available dates
			List<DateTime> l = new List<DateTime>();
			foreach (var d in TaskManager.taskDays)
			{
				l.Add( DateTime.Parse( d ) );
			}
			monthCalendar.BoldedDates = l.ToArray();

			monthCalendar.SelectionStart = dt;
		}

		public DateTime SelectedDate { get; set; }

		private void buttonOk_Click( object sender, EventArgs e )
		{
			SelectedDate = monthCalendar.SelectionStart;
			this.Close();
		}

		private void buttonCancel_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		private void monthCalendar_DateChanged( object sender, DateRangeEventArgs e )
		{
			buttonOk.Enabled = allowDate( monthCalendar.SelectionStart );

		}

		private void FormAddDate_Load( object sender, EventArgs e )
		{
			this.Text = this.FormTitle;
		}

		private void FormDateSelect_Shown( object sender, EventArgs e )
		{
			// monthcalendar varies in size depending on operating system, so we need to adapt controls and form size
			buttonOk.Left = monthCalendar.Left + monthCalendar.Width + monthCalendar.Left;
			buttonCancel.Left = monthCalendar.Left + monthCalendar.Width + monthCalendar.Left;
			this.Width = buttonCancel.Left + buttonCancel.Width + monthCalendar.Left;
		}

	}
}
