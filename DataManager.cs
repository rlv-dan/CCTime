using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;

namespace CCTime
{
	class TaskManager
	{
		public static string[] taskDays;	// keeps track of the days we have, since they do not have to be consecutive
		public static Dictionary<string, List<Task>> taskData = new Dictionary<string, List<Task>>();


		// --- Load and Save data -------------------------------------------------------

		public static void InitData()
		{
			// Get all data files (or create folder if not exists)
			var appFolder = Path.GetDirectoryName( Application.ExecutablePath );
			Directory.CreateDirectory(appFolder + @"\data");
			var files = new List<string>( Directory.GetFiles(appFolder + @"\data", "*.csv") );
			files.Sort();

			var taskDaysList = new List<string>();

			// Load all data
			foreach(var f in files)
			{
				try
				{
					var data = File.ReadAllLines(f);
					Task t;

					var totalMinutes = 0;
					var accountedFor = 0;

					// Data format:
					//		each line: title|minutes
					//		1st line is always totals, then all tasks one per line
					List<Task> tasks = new List<Task>();
					for( int i = 0; i < data.Length; i++)
					{
						var d = data[i].Split( '|' );

						var taskTitle = d[0];
						var taskMinutes = int.Parse(d[1]);

						if( i == 0 )
						{
							// first line is total
							totalMinutes = taskMinutes;
							t = new Task( Settings.TotalsString, taskMinutes, TaskType.Total );
							tasks.Add( t );
						}
						else
						{
							accountedFor += taskMinutes;
							t = new Task( taskTitle, taskMinutes );
							tasks.Add( t );
						}
					}

					// add unaccounted "task" to each day too
					t = new Task( Settings.UnaccountedString, totalMinutes - accountedFor, TaskType.Unaccounted );
					tasks.Insert( 1, t );

					var dateToAdd = Path.GetFileNameWithoutExtension( f );
					taskDaysList.Add( dateToAdd );
					taskData.Add( dateToAdd, tasks );
				}
				catch( Exception ex )
				{
					var ret = MessageBox.Show( "Failed to load data for " + System.IO.Path.GetFileNameWithoutExtension( f ) + ". Do you want to continue? (No to quit)", "Load Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error );
					if( ret == DialogResult.No )
					{
						Application.Exit();
					}
				}
			}

			taskDays = taskDaysList.ToArray();

			// ensure today is present
			TaskManager.GetToday();
		}

		public static void PersistData()
		{
			foreach( var item in TaskManager.taskData )
			{
				string csvData = "";
				var date = item.Key;
				var isDirty = false;

				csvData += "Totals|" + item.Value[0].Minutes + "\r\n";	// first line is always totals
				if( item.Value[0].IsDirty ) isDirty = true;
				item.Value[0].IsDirty = false;
				for( int i = 2; i < item.Value.Count; i++ )
				{
					csvData += item.Value[i].Title.Replace("|","") + "|" + item.Value[i].Minutes + "\r\n";
					if( item.Value[i].IsDirty ) isDirty = true;
					item.Value[i].IsDirty = false;
				}

				if( isDirty )
				{
					var appFolder = Path.GetDirectoryName( Application.ExecutablePath );
					var filename = appFolder + @"\data\" + date + ".csv";
					File.WriteAllText( filename, csvData );
				}
			}
		}


		// --- Days ---------------------------------------------------------------------

		public static List<Task> AddDay( DateTime dt, string dateToClone = null )
		{
			var toAdd = dt.ToString( "yyyy-MM-dd" );

			if( !taskData.ContainsKey( toAdd ) )
			{
				var newDay = new List<Task>();
				newDay.Add( new Task( Settings.TotalsString, 0, TaskType.Total ) );	// total
				newDay.Add( new Task( Settings.UnaccountedString, 0, TaskType.Unaccounted ) );	// unaccounted
				if( dateToClone != null && taskData.ContainsKey( dateToClone ) )
				{
					for( int i = 2; i < taskData[dateToClone].Count; i++ )
					{
						if( Settings.DontCopyTasksWithoutTime == true && taskData[dateToClone][i].Minutes == 0 ) continue;
						newDay.Add( new Task( taskData[dateToClone][i].Title, 0 ) );
					}
				}
				newDay[0].IsDirty = true;	// ensure it is getting save

				taskData.Add( toAdd, newDay );

				var tmpList = new List<string>( taskData.Keys.ToArray() );
				tmpList.Sort();
				taskDays = tmpList.ToArray();
			}
			
			return taskData[toAdd];
		}

		public static void DeleteDay( DateTime dt )
		{
			var toDel = dt.ToString( "yyyy-MM-dd" );

			if( taskData.ContainsKey( toDel ) )
			{
				taskData.Remove( toDel );

				var tmpList = new List<string>( taskData.Keys.ToArray() );
				tmpList.Sort();
				taskDays = tmpList.ToArray();

				var appFolder = Path.GetDirectoryName( Application.ExecutablePath );
				File.Delete( appFolder + @"\data\" + toDel + ".csv" );
			}
		}

		public static bool DayExsists( DateTime dt )
		{
			return TaskManager.taskData.ContainsKey( dt.ToString( "yyyy-MM-dd" ) );
		}

		public static List<Task> GetDay( DateTime dt )
		{
			if( !TaskManager.taskData.ContainsKey( dt.ToString( "yyyy-MM-dd" ) ) ) return null;
			return TaskManager.taskData[dt.ToString( "yyyy-MM-dd" )];
		}

		public static List<Task> GetToday()
		{
			var today = DateTime.Now;
			if( DayExsists( today ) )
			{
				return TaskManager.taskData[today.ToString( "yyyy-MM-dd" )];
			}
			else
			{
				// add today, if not present, by cloning previous date
				return AddDay( today, GetPreviousDate( today, true ) );
			}
		}

		public static string GetNextDate( DateTime dt )
		{
			var toFind = dt.ToString( "yyyy-MM-dd" );
			int foundIndex = Array.FindIndex( TaskManager.taskDays, d => d == toFind );
			if( TaskManager.taskDays.Length > foundIndex + 1 )
			{
				return TaskManager.taskDays[foundIndex + 1];
			}
			else
			{
				return null;
			}
		}

		public static string GetPreviousDate( DateTime dt, bool returnLastDateIfNotFound = false )
		{
			var toFind = dt.ToString( "yyyy-MM-dd" );
			int foundIndex = Array.FindIndex( TaskManager.taskDays, d => d == toFind );
			if( foundIndex > 0 )
			{
				return TaskManager.taskDays[foundIndex - 1];	// previous date is not necessarily yesterday
			}
			else
			{
				if( returnLastDateIfNotFound && TaskManager.taskDays.Length > 0 )
					return TaskManager.taskDays[TaskManager.taskDays.Length - 1];
				else
					return null;
			}
		}


		// --- Tasks --------------------------------------------------------------------

		public static void AddTaskToDay( DateTime dt, string title )
		{
			var tasks = TaskManager.taskData[dt.ToString( "yyyy-MM-dd" )];
			var t = new Task( title, Settings.MinutesToAddToNewTasks );
			t.IsDirty = true;
			tasks.Add( t );
		}

		public static void RemoveTask( DateTime dt, int index )
		{
			if( index <= 1 )
			{
				return;
			}

			var tasks = TaskManager.taskData[dt.ToString( "yyyy-MM-dd" )];
			tasks.RemoveAt(index);
		}

		public static void RemoveTasksWithZeroTime( DateTime dt)
		{
			var tasks = TaskManager.taskData[dt.ToString( "yyyy-MM-dd" )];
			for( int i = tasks.Count - 1; i >= 2; i-- )
			{
				if( tasks[i].Type == TaskType.Normal && tasks[i].Minutes == 0 )
				{
					tasks.RemoveAt( i );
				}
			}
		}

		public static void MoveTask( DateTime dt, int oldIndex, int newIndex )
		{
			if( oldIndex <= 1 )
			{
				return;
			}
			if( newIndex <= 1 )
			{
				return;
			}
			
			var tasks = TaskManager.taskData[dt.ToString( "yyyy-MM-dd" )];

			if( newIndex >= tasks.Count )
			{
				return;
			}

			var item = tasks[oldIndex];
			tasks.RemoveAt(oldIndex);
			tasks.Insert(newIndex, item);
		}
	}
}
