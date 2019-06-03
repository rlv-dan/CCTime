using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace CCTime
{
	static class Utilities
	{
		// This presumes that weeks start with Monday.
		// Week 1 is the 1st week of the year with a Thursday in it.
		// Source: https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date
		public static int GetIso8601WeekOfYear( DateTime time )
		{
			// Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
			// be the same week# as whatever Thursday, Friday or Saturday are,
			// and we always get those right
			DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek( time );
			if( day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday )
			{
				time = time.AddDays( 3 );
			}

			// Return the week of our adjusted day
			return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear( time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday );
		}

		// Returns for example 1.5 instead of 90 min
		public static string MinutesToFractionalHours( int minutes )
		{
			var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
			var sign = minutes < 0 ? "-" : "";
			minutes = Math.Abs( minutes );
			int hours = ( minutes - minutes % 60 ) / 60;
			int minutesLeft = minutes - ( hours * 60 );
			var result = hours + decimalSeparator + Math.Round( (decimal)minutesLeft / 60, 2 ).ToString().Replace( "0" + decimalSeparator, "" );
			if( result.EndsWith( decimalSeparator + "0" ) )
			{
				result = result.Replace( decimalSeparator + "0", "" );
			}
			return sign + result;
		}

		// Returns for example 1:30 instead of 90 min
		public static string MinutesToHours( int minutes )
		{
			var sign = minutes < 0 ? "-" : "";
			minutes = Math.Abs( minutes );
			int hours = ( minutes - minutes % 60 ) / 60;
			int minutesLeft = minutes - ( hours * 60 );
			var separator = Thread.CurrentThread.CurrentCulture.DateTimeFormat.TimeSeparator;
			return sign + hours + separator + minutesLeft.ToString("D2");
		}
	}
}
