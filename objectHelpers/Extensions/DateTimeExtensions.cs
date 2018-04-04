using System;
using System.Globalization;
using Machineghost.ObjectHelpers.Constants;
using Machineghost.ObjectHelpers.Utilities;

namespace Machineghost.ObjectHelpers.Extensions
{
	/// <summary>
	/// Helpful extensions for object type or format conversions.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Converts an ISO 8601 Date String ( 2016-04-08T07:00:00.000Z ) to a DateTime object, with an end time of 23:59:59.
		/// </summary>
		public static DateTime Iso8601ToDateTimeEnd(this string dateString)
		{
			var dateTime = DateTime.Parse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
			return DateTimeUtility.AddEndTime(dateTime);
		}

		/// <summary>
		/// Converts an ISO 8601 Date String ( 2016-04-08T07:00:00.000Z ) to a DateTime object, with a start time of 0:00:00.
		/// </summary>
		public static DateTime Iso8601ToDateTimeStart(this string dateString)
		{
			var dateTime = DateTime.Parse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
			return DateTimeUtility.AddStartTime(dateTime);
		}

		/// <summary>
		/// Add the end of day time ( 23:59:59 ) to a given date.
		/// </summary>
		public static DateTime ToEndOfDayTime(this DateTime dateTime)
		{
			return DateTimeUtility.AddEndTime(dateTime);
		}

		/// <summary>
		/// Add the start of day time ( 00:00:00 ) to a given date.
		/// </summary>
		public static DateTime ToStartOfDayTime(this DateTime dateTime)
		{
			return DateTimeUtility.AddStartTime(dateTime);
		}

		/// <summary>
		/// Return the date as a string, including milliseconds in the format "MM/dd/yyyy HH:mm:ss:fff".
		/// </summary>
		public static string ToStringWithMilliseconds(this DateTime dateTimeObject)
		{
			return dateTimeObject.ToString(DateTimeConstants.SQL_DATE_TIME_FORMAT);
		}

		/// <summary>
		/// Return the date as a string formatted in ISO8601 standard: "YYYY-MM-DD'T'HH:MM:SS'Z'".
		/// </summary>
		public static string UTCDateToStringInISO8601Format(this DateTime dateTime)
		{
			// s = "sortable" pattern, which reflects the defined ISO8601 standard
			// Z indicates UTC timezone
			return dateTime.ToString("s") + "Z";
		}
	}
}