using System;
using System.Globalization;
using Machineghost.ObjectHelpers.Extensions;

namespace Machineghost.ObjectHelpers.Utilities
{
	/// <summary>
	/// Helper class for interacting with DateTime objects.
	/// </summary>
	public static class DateTimeUtility
	{
		/// <summary>
		/// Sets standard end time of 11:59:59.998 PM to a DateTime value
		/// Milliseconds are 998 because http://stackoverflow.com/questions/8153963/sql-server-2008-and-milliseconds
		/// </summary>
		public static DateTime AddEndTime(DateTime dateTime)
		{
			if (dateTime != DateTime.MinValue)
			{
				DateTime newDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 998, DateTimeKind.Unspecified);
				dateTime = newDate;
			}
			return dateTime;
		}

		/// <summary>
		/// Add 998 milliseconds to an End Date
		/// Milliseconds are 998 because http://stackoverflow.com/questions/8153963/sql-server-2008-and-milliseconds
		/// </summary>
		public static DateTime AddMillisecondsToEndDate(DateTime date)
		{
			if (date != DateTime.MinValue)
			{
				DateTime newDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 998, DateTimeKind.Unspecified);
				date = newDate;
			}
			return date;
		}

		/// <summary>
		/// Sets standard start time of 12:00 AM to a DateTime value.
		/// </summary>
		public static DateTime AddStartTime(DateTime dateTime)
		{
			DateTime outDateTime = dateTime.Date + new TimeSpan(0, 0, 0);
			return outDateTime;
		}

		/// <summary>
		/// Convert an Eastern Standard date to a UTC.
		/// </summary>
		public static DateTime ConvertFromEasternToUTC(DateTime easternDateTime)
		{
			if (easternDateTime != DateTime.MinValue)
			{
				var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
				DateTime utc = TimeZoneInfo.ConvertTimeToUtc(
					DateTime.SpecifyKind(easternDateTime, DateTimeKind.Unspecified), timeZoneInfo);
				return utc;
			}
			else
			{
				// invalid datetime value, simply return value passed in
				return easternDateTime;
			}
		}

		/// <summary>
		/// Convert a UTC date to Eastern Standard Time.
		/// </summary>
		public static DateTime ConvertFromUTCToEastern(DateTime utcDateTime)
		{
			if (utcDateTime != null && utcDateTime != DateTime.MinValue)
			{
				DateTime eastern = TimeZoneInfo.ConvertTimeFromUtc(
					utcDateTime, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
				return eastern;
			}
			else
			{
				return utcDateTime;
			}
		}

		/// <summary>
		/// Returns the AM PM designator for a given date.
		/// Example: AM
		/// </summary>
		public static string GetAmPmDesignator(DateTime dateTime)
		{
			return dateTime.ToString("tt", CultureInfo.CreateSpecificCulture("en-US"));
		}

		/// <summary>
		/// Returns the abbreviated name of the day of the week for a given date, in US English.
		/// Example: Tue
		/// </summary>
		public static string GetAbbreviatedDay(DateTime dateTime)
		{
			return dateTime.ToString("ddd", CultureInfo.CreateSpecificCulture("en-US"));
		}

		/// <summary>
		/// Returns a simple date in the format: M/d/yyyy
		/// Returns an empty string if date is DateTime.MinValue.
		/// </summary>
		public static string GetSimpleDate(DateTime dateValue)
		{
			if (dateValue != DateTime.MinValue)
			{
				return dateValue.ToString("M/d/yyyy");
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// Returns a simple datetime in the format: M/d/yyyy h:mm tt.
		/// Returns an empty string if date/time is DateTime.MinValue.
		/// </summary>
		public static string GetSimpleDateTime(DateTime dateValue)
		{
			if (dateValue != DateTime.MinValue)
			{
				return dateValue.ToString("M/d/yyyy h:mm tt");
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// Converts a string value to a formatted datetime string as: MM/dd/yyyy HH:mm:ss:fff
		/// </summary>
		public static string GetSimpleDateTime(string dateTimeString)
		{
			return GetSimpleDateTime(dateTimeString.ToDateTime(DateTime.MinValue));
		}

		/// <summary>
		/// This method return a specific DateTime to display for messages (i.e. Aug 14, 2013 5:41pm)
		/// </summary>
		public static string GetDisplayFriendlyDateTime(DateTime messageDate)
		{
			return messageDate.ToString("MMM dd, yyyy h:mm") + messageDate.ToString("tt").ToLower();
		}

		/// <summary>
		/// This method return a specific DateTime to display for messages (i.e. Aug 14, 2013 5:41pm ET)
		/// </summary>
		public static string GetDisplayFriendlyDateTimeEt(DateTime messageDate)
		{
			return messageDate.ToString("MMM dd, yyyy h:mm") + messageDate.ToString("tt").ToLower() + " ET";
		}

		/// <summary>
		/// Return the first day of the month for a given date.
		/// </summary>
		public static DateTime GetFirstDayOfMonth(DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		/// <summary>
		/// Returns formatted date string with day of the week, abbreviated name of the month, the day of the month, from 01 through 31, and the year as a four-digit number.
		/// Example: Tuesday, June 4, 1999
		/// </summary>
		public static string GetFullyNamedMonthDateYearWithDayOfWeek(DateTime dateTime)
		{
			return dateTime.ToString("dddd, MMMM dd, yyyy");
		}

		/// <summary>
		/// Gets the named month from an int.
		/// </summary>
		public static string GetFullMonthNameFromInt(int monthDesignation)
		{
			if (monthDesignation > 12 || monthDesignation < 1)
				return "Invalid Month";
			return new DateTime(1900, monthDesignation, 1)
				.ToString("MMMM", CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Return the last day of the month for a given date.
		/// </summary>
		public static DateTime GetLastDayOfMonth(DateTime date)
		{
			var firstDayOfMonth = GetFirstDayOfMonth(date);
			var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
			return lastDayOfMonth;
		}

		/// <summary>
		/// Returns formatted date string with abbreviated name of the month, the day of the month, from 01 through 31, and the year as a four-digit number.
		/// Example: Jun 4, 1999
		/// </summary>
		public static string GetNamedMonthDateWithAbbreviatedMonthName(DateTime dateTime)
		{
			return dateTime.ToString("MMM dd, yyyy");
		}

		/// <summary>
		/// Returns formatted date string with the abbreviated name of the day of the week, abbreviated name of the month,
		/// the day of the month, from 01 through 31, the year as a four-digit number, and the AM/PM designator
		/// Example: Tue Jun 4, 1999 4:13:45pm ET
		/// </summary>
		public static string GetNamedMonthDateTimeSecondsWithAbbreviatedMonthName(DateTime dateTime)
		{
			return dateTime.ToString("MMM d, yyyy h:mm:ss", CultureInfo.CreateSpecificCulture("en-US"))
						+ GetAmPmDesignator(dateTime).ToLower() + " ET";
		}

		/// <summary>
		/// Returns the time portion of a date.
		/// The hour, using a 12-hour clock from 1 to 12 and the minute, from 00 through 59.
		/// Example: 4:13
		/// </summary>
		public static string GetTime(DateTime dateTime)
		{
			return dateTime.ToString("h:mm", CultureInfo.CreateSpecificCulture("en-US"));
		}

		/// <summary>
		/// Test a string for a valid date
		/// </summary>
		public static bool IsValidDate(string dateTime)
		{
			DateTime outputDate;
			return DateTime.TryParse(dateTime, out outputDate);
		}
	}
}