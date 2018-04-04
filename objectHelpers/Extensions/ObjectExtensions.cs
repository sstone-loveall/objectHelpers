using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using Machineghost.ObjectHelpers.Constants;
using Newtonsoft.Json;

namespace Machineghost.ObjectHelpers.Extensions
{
	/// <summary>
	/// Helpful extensions for object type or format conversions.
	/// </summary>
	public static class ObjectExtensions
	{
		/// <summary>
		/// Convert any object to its bool equivalent, if exists.
		/// For example, "1" or 1 to true or "true" to true.
		/// Returns the provided defaultValue if conversion fails.
		/// </summary>
		public static bool ToBool(this object value, bool defaultValue)
		{
			bool boolValue;
			var strValue = (value ?? "").ToString().ToLower().Trim();
			if (strValue == "1" || strValue == "yes" || strValue == "on" || strValue == "true")
				boolValue = true;
			else if (strValue == "0" || strValue == "no" || strValue == "false")
				boolValue = false;
			else if (!bool.TryParse(strValue, out boolValue))
				boolValue = defaultValue;
			return boolValue;
		}

		/// <summary>
		/// Converts a list of string objects into a single comma-separated string equivalent.
		/// </summary>
		public static string ToCsvFromList<String>(this List<String> value)
		{
			if (value == null) return "";

			var itemsCsv = "";
			foreach (var item in value)
			{
				if (!string.IsNullOrEmpty(itemsCsv))
					itemsCsv += ",";
				itemsCsv += item;
			}
			return itemsCsv;
		}

		/// <summary>
		/// Convert an object to a DateTime.
		/// Returns DateTime.MinDate if conversion fails.
		/// </summary>
		public static DateTime ToDateTime(this object value)
		{
			return ToDateTime(value, DateTime.MinValue);
		}

		/// <summary>
		/// Convert an object to a DateTime.
		/// Returns the provided defaultValue if conversion fails.
		/// </summary>
		public static DateTime ToDateTime(this object value, DateTime defaultValue)
		{
			DateTime outDateTime;
			if (value is DateTime)
			{
				// if value is already typed DateTime, no conversion necessary
				outDateTime = (DateTime)value;
			}
			else
			{
				// if value is not already typed DateTime, convert to string and attempt to parse
				var strValue = value.ToNullSafeString();
				if (!DateTime.TryParseExact(strValue, DateTimeConstants.DATE_TIME_WITH_MILLISECONDS_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out outDateTime)
					&& !DateTime.TryParse(strValue, out outDateTime))
					outDateTime = defaultValue;
			}
			return outDateTime;
		}

		/// <summary>
		/// Converts any object to its DB null-safe equivalent.
		/// If value is null, return DBNull value.
		/// </summary>

		public static object ToDBSafeValue(this object value)
		{
			return value ?? DBNull.Value;
		}

		/// <summary>
		/// Convert any object to its decimal equivalent, if exists.
		/// For example, "1" or 1 to 1.00.
		/// Returns the provided defaultValue if conversion fails.
		/// </summary>
		public static decimal ToDecimal(this object value, decimal defaultValue)
		{
			decimal decimalValue;
			return decimal.TryParse(value + "", out decimalValue) ? decimalValue : defaultValue;
		}

		/// <summary>
		/// Convert any object to its double equivalent, if exists.
		/// For example, "1" or 1 to 1.00, or true to 1.00 or false to 0.00.
		/// Returns the provided defaultValue if conversion fails.
		/// </summary>
		public static double ToDouble(this object value, double defaultValue)
		{
			double doubleValue;

			return value is bool
				? (bool)value
					? 1
					: 0
				: double.TryParse(value + "", out doubleValue)
					? doubleValue
					: defaultValue;
		}

		/// <summary>
		/// Returns an html-encoded string equivalent of an object's value.
		/// </summary>
		public static string ToHtmlEncode(this object value)
		{
			return !string.IsNullOrEmpty(value.ToNullSafeString()) ? HttpUtility.HtmlEncode(value.ToString()) : "";
		}

		/// <summary>
		/// Convert any object to its int equivalent, if exists.
		/// For example, "1" or 1.00 to 1 or true to 1, false to 0.
		/// Returns the provided defaultValue if conversion fails.
		/// </summary>
		public static int ToInt(this object value, int defaultValue)
		{
			int intValue;

			return value is bool
				? (bool)value
					? 1
					: 0
				: int.TryParse(value + "", out intValue)
					? intValue
					: defaultValue;
		}

		/// <summary>
		/// Returns the serialized json equivalent of an object.
		/// </summary>
		public static string ToJSON(this object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		/// <summary>
		/// Convert any object to its long equivalent, if exists.
		/// For example, "1" to 1.
		/// Returns the provided defaultValue if conversion fails.
		/// </summary>
		public static long ToLong(this object value, long defaultValue)
		{
			long longValue;
			return long.TryParse(value + "", out longValue) ? longValue : defaultValue;
		}

		/// <summary>
		/// Convert any object to its null-safe string equivalent.
		/// For example, 1 "1" or null to empty string.
		/// Returns an empty string by default.
		/// </summary>
		public static string ToNullSafeString(this object value)
		{
			return value.ToNullSafeString("");
		}

		/// <summary>
		/// Convert any object to its null-safe string equivalent.
		/// For example, 1 "1" or null to empty string.
		/// Returns the provided defaultValue if conversion fails.
		/// </summary>
		public static string ToNullSafeString(this object value, string defaultValue)
		{
			var strValue = (value ?? (defaultValue ?? "")).ToString();
			return strValue;
		}

		/// <summary>
		/// Returns the url-decoded string equivalent of an object's value.
		/// </summary>
		public static string ToUrlDecode(this object value)
		{
			return !string.IsNullOrEmpty(value.ToNullSafeString()) ? HttpUtility.UrlDecode(value.ToString()) : "";
		}

		/// <summary>
		/// Returns an html-encoded string equivalent of an object's value.
		/// </summary>
		public static string ToUrlEncode(this object value)
		{
			return !string.IsNullOrEmpty(value.ToNullSafeString()) ? HttpUtility.UrlEncode(value.ToString()) : "";
		}

		/// <summary>
		/// Returns a url path-encoded equivalent of an object's value.
		/// </summary>
		public static string ToUrlPathEncode(this object value)
		{
			return !string.IsNullOrEmpty(value.ToNullSafeString()) ? HttpUtility.UrlPathEncode(value.ToString()) : "";
		}
	}
}