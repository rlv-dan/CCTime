using System;
using System.Collections.Generic;
using System.Text;

namespace CCTime
{
	public enum TaskType
	{
		Normal,
		Total,
		Unaccounted
	}

	class Task
	{
		public Task( string title, int minutes = 0, TaskType type = TaskType.Normal)
		{
			this.Title = title;
			this.Minutes = minutes;
			this.Type = type;
			this.IsDirty = false;
			this.AutoTick = false;
		}

		public TaskType Type { get; set; }
		public bool IsDirty { get; set; }
		public bool AutoTick { get; set; }

		private string _title;
		public string Title
		{
			get 
			{
				return this._title; 
			}
			set 
			{ 
				this._title = value; 
				this.IsDirty = true; 
			}
		}

		private int _minutes;
		public int Minutes
		{
			get 
			{ 
				return this._minutes; 
			}
			set 
			{ 
				this._minutes = value;
				if( this.Type == TaskType.Normal || this.Type == TaskType.Total )
				{
					this._minutes = Math.Max( 0, this._minutes );	// don't allow negative values, except for unaccounted field
				}
				this.IsDirty = true; 
			}
		}


		// Properties corresponding to buttons in ObjectListView

		public string Plus15
		{
			get {
				if( this.Type == TaskType.Total )
				{
					if ( Settings.ClockButtonsGoToNearest )
					{
						return "< " + Settings.MinutesOnClockButtons;
					}
					return "-" + Settings.MinutesOnTaskButton1;
				}

				if( this.Type == TaskType.Normal )
					return "+" + Settings.MinutesOnTaskButton1;
				else
					return "";
			}
		}
		public string Plus30
		{
			get
			{
				if( this.Type == TaskType.Normal )
					return "+" + Settings.MinutesOnTaskButton2;
				else
					return "";
			}
		}
		public string Plus60
		{
			get {
				if( this.Type == TaskType.Total )
				{
					if( Settings.ClockButtonsGoToNearest )
					{
						return Settings.MinutesOnClockButtons + " >";
					}
					return "+" + +Settings.MinutesOnClockButtons; 
				}

				if( this.Type == TaskType.Normal )
					return "+" + Settings.MinutesOnTaskButton3;
				else
					return "";
			}
		}

	}
}
