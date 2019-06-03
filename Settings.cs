using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;

namespace CCTime
{
	public class Settings
	{
		public static bool StartMinimized { get; set; }
		public static int WindowWidth { get; set; }
		public static int WindowHeight { get; set; }
		public static string WindowPosition { get; set; }
		public static bool ClockOnAtStartup { get; set; }
		public static bool CloseButtonMinimizes { get; set; }
		public static string TotalsString { get; set; }
		public static string UnaccountedString { get; set; }
		public static int MinutesOnClockButtons { get; set; }
		public static int MinutesOnTaskButton1 { get; set; }
		public static int MinutesOnTaskButton2 { get; set; }
		public static int MinutesOnTaskButton3 { get; set; }
		public static bool ClockButtonsGoToNearest { get; set; }
		public static bool DontCopyTasksWithoutTime { get; set; }
		public static bool ReturnToTodayWhenRestoring { get; set; }
		public static int MinutesToAddToNewTasks { get; set; }

		public static int ReportWindowWidth { get; set; }
		public static int ReportWindowHeight { get; set; }
		public static int ReportTabSize { get; set; }
		public static bool ReportShowTotalsColumn { get; set; }
		public static bool ReportShowTotalsRow { get; set; }
		public static bool ReportShowUnaccountedRow { get; set; }
		public static bool ReportAutoCopyToClipboard { get; set; }
		public static string ReportZeroIndicator { get; set; }
		public static string ReportTimeFormat { get; set; }
		public static string ReportTotalsString { get; set; }
		public static string ReportUnaccountedString { get; set; }
		public static string ReportWeekString { get; set; }
		public static bool ReportWarnIfUnaccountedHours { get; set; }

		public static void InitSettings()
		{
			try
			{
				var appFolder = Path.GetDirectoryName( Application.ExecutablePath );
				var parser = new FileIniDataParser();
				IniData data = parser.ReadFile( appFolder + @"\Settings.ini" );

				Settings.StartMinimized = bool.Parse( data["General"]["StartMinimized"] );
				Settings.WindowWidth = int.Parse( data["General"]["WindowWidth"] );
				Settings.WindowHeight = int.Parse( data["General"]["WindowHeight"] );
				Settings.WindowPosition = data["General"]["WindowPosition"];
				Settings.CloseButtonMinimizes = bool.Parse( data["General"]["CloseButtonMinimizes"] );
				Settings.TotalsString = data["General"]["TotalsString"];
				Settings.UnaccountedString = data["General"]["UnaccountedString"];
				Settings.ClockOnAtStartup = bool.Parse( data["General"]["ClockOnAtStartup"] );
				Settings.MinutesOnClockButtons = int.Parse( data["General"]["MinutesOnClockButtons"] );
				Settings.MinutesOnTaskButton1 = int.Parse( data["General"]["MinutesOnTaskButton1"] );
				Settings.MinutesOnTaskButton2 = int.Parse( data["General"]["MinutesOnTaskButton2"] );
				Settings.MinutesOnTaskButton3 = int.Parse( data["General"]["MinutesOnTaskButton3"] );
				Settings.ClockButtonsGoToNearest = bool.Parse( data["General"]["ClockButtonsGoToNearest"] );
				Settings.DontCopyTasksWithoutTime = bool.Parse( data["General"]["DontCopyTasksWithoutTime"] );
				Settings.ReturnToTodayWhenRestoring = bool.Parse( data["General"]["ReturnToTodayWhenRestoring"] );
				Settings.MinutesToAddToNewTasks = int.Parse( data["General"]["MinutesToAddToNewTasks"] );

				Settings.ReportWindowWidth = int.Parse( data["Report"]["ReportWindowWidth"] );
				Settings.ReportWindowHeight = int.Parse( data["Report"]["ReportWindowHeight"] );
				Settings.ReportTabSize = int.Parse( data["Report"]["ReportTabSize"] );
				Settings.ReportShowTotalsColumn = bool.Parse( data["Report"]["ReportShowTotalsColumn"] );
				Settings.ReportShowTotalsRow = bool.Parse( data["Report"]["ReportShowTotalsRow"] );
				Settings.ReportShowUnaccountedRow = bool.Parse( data["Report"]["ReportShowUnaccountedRow"] );
				Settings.ReportAutoCopyToClipboard = bool.Parse( data["Report"]["ReportAutoCopyToClipboard"] );
				Settings.ReportZeroIndicator = data["Report"]["ReportZeroIndicator"];
				Settings.ReportTimeFormat = data["Report"]["ReportTimeFormat"];
				Settings.ReportTotalsString = data["Report"]["ReportTotalsString"];
				Settings.ReportUnaccountedString = data["Report"]["ReportUnaccountedString"];
				Settings.ReportWeekString = data["Report"]["ReportWeekString"];
				Settings.ReportWarnIfUnaccountedHours = bool.Parse( data["Report"]["ReportWarnIfUnaccountedHours"] );
			}
			catch( Exception ex )
			{
				System.Windows.Forms.MessageBox.Show( "Error loading settings file. Using default values...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );

				Settings.StartMinimized = false;
				Settings.WindowWidth = 420;
				Settings.WindowHeight = 525;
				Settings.WindowPosition = "BottomRight";
				Settings.CloseButtonMinimizes = false;
				Settings.TotalsString = "Totals";
				Settings.UnaccountedString = "Unaccounted";
				Settings.ClockOnAtStartup = true;

				Settings.MinutesOnClockButtons = 15;
				Settings.MinutesOnTaskButton1 = 15;
				Settings.MinutesOnTaskButton2 = 30;
				Settings.MinutesOnTaskButton3 = 60;
				Settings.ClockButtonsGoToNearest = true;
				Settings.DontCopyTasksWithoutTime = false;
				Settings.ReturnToTodayWhenRestoring = true;
				Settings.MinutesToAddToNewTasks = 0;

				Settings.ReportWindowWidth = 960;
				Settings.ReportWindowHeight = 480;
				Settings.ReportTabSize = 8;
				Settings.ReportShowTotalsColumn = true;
				Settings.ReportShowTotalsRow = true;
				Settings.ReportShowUnaccountedRow = true;
				Settings.ReportAutoCopyToClipboard = false;
				Settings.ReportZeroIndicator = "-";
				Settings.ReportTimeFormat = "fractional";
				Settings.ReportTotalsString = "Totals";
				Settings.ReportUnaccountedString = "Unaccounted";
				Settings.ReportWeekString = "Week";
				Settings.ReportWarnIfUnaccountedHours = false;
			}
		}
	}
}