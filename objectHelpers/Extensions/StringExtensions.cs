using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Machineghost.ObjectHelpers.Extensions
{
	/// <summary>
	/// Helpful extensions for object type or format conversions.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Determines if a string contains another string.
		/// Returns true if found.
		/// </summary>
		public static bool Contains(this string value, string containsValue, StringComparison comparison)
		{
			value = value.ToNullSafeString();
			return value.IndexOf(containsValue, comparison) >= 0;
		}

		/// <summary>
		/// Convert single comma-separated string into a list of objects of specified type.
		/// </summary>
		public static List<T> FromCsvToListOf<T>(this string csv)
		{
			var results = new List<T>();
			var input = new List<string>(csv.ToListOfString(','));

			if (typeof(T).IsEnum)
				if (input.FirstOrDefault().IsNumeric())
				{
					//Value is Numeric
					var inputIntList = input.Select(x => (int)Convert.ChangeType(x, typeof(int))).ToList();
					results = inputIntList.Cast<T>().ToList();
				}
				else
				{
					foreach (var item in input)
					{
						try
						{
							results.Add((T)Enum.Parse(typeof(T), item));
						}
						catch
						{
							// silent parsing error
						}
					}
				}
			else
				results = input.Select(x => (T)Convert.ChangeType(x, typeof(T))).ToList();

			return results;
		}

		/// <summary>
		/// Determines if a string represents a datetime value.
		/// Returns true if datetime.
		/// </summary>
		public static bool IsDateTime(this string value)
		{
			DateTime hold;
			return DateTime.TryParse(value, out hold);
		}

		/// <summary>
		/// Determines if a string represents a numeric value.
		/// Returns true if numeric.
		/// </summary>
		public static bool IsNumeric(this string value)
		{
			double hold;
			return double.TryParse(value, NumberStyles.Any, null, out hold);
		}

		/// <summary>
		/// Return the left-most substring of a given string for a given length.
		/// </summary>
		public static string Left(this string value, int length)
		{
			value = value.ToNullSafeString();
			return value.Length > length ? value.Substring(0, length) : value;
		}

		/// <summary>
		/// Return the right-most substring of a given string for a given length.
		/// </summary>
		public static string Right(this string value, int length)
		{
			value = value.ToNullSafeString();
			return value.Length > length ? value.Substring(value.Length - length, length) : value;
		}

		/// <summary>
		/// Returns the byte array of a given string.
		/// </summary>
		public static byte[] ToBytes(this string str)
		{
			return Encoding.ASCII.GetBytes(str);
		}

		/// <summary>
		/// Returns the camel-case equivalent of a given string.
		/// Allows for strings which begin with underscores or digits.
		/// </summary>
		public static string ToCamelCase(this string str)
		{
			var chars = str.ToCharArray();

			for (var i = 0; i < chars.Length; i++)
				if (char.IsLetter(chars[i]))
				{
					chars[i] = char.ToLowerInvariant(chars[i]);
					break;
				}

			return new string(chars);
		}

		/// <summary>
		/// Convert a list of strings into a single comma-separated string.
		/// </summary>
		public static string ToCsvFromList(this List<string> value)
		{
			var itemsCsv = "";
			if (value != null)
				foreach (var item in value)
				{
					if (!string.IsNullOrEmpty(itemsCsv))
						itemsCsv += ",";
					itemsCsv += item;
				}
			return itemsCsv;
		}

		/// <summary>
		///  Convert a list of strings into a single comma-separated string.
		/// </summary>
		public static string ToCsvFromList(this IEnumerable<string> value)
		{
			if (value == null) return "";

			var csvList = new List<string>();
			csvList.AddRange(value);
			return ToCsvFromList(csvList);
		}

		/// <summary>
		/// Convert a list of integers into a single comma-separated string.
		/// </summary>
		public static string ToCsvFromList(this List<int> value)
		{
			var itemsCsv = "";
			if (value != null)
				foreach (var item in value)
				{
					if (!string.IsNullOrEmpty(itemsCsv))
						itemsCsv += ",";
					itemsCsv += item;
				}
			return itemsCsv;
		}

		/// <summary>
		/// Convert a list of integers into a single comma-separated string.
		/// </summary>
		public static string ToCsvFromList(this IEnumerable<int> value)
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
		/// Return an html-decoded equivalent of a given string.
		/// </summary>
		public static string ToHtmlDecode(this string value)
		{
			return value != null ? HttpUtility.HtmlDecode(value) : "";
		}

		/// <summary>
		/// Convert a string that represents a comma-separated numeric list into a list of integers.
		/// If any of the items in the comma-separated string are not numeric, return null.
		/// </summary>
		public static List<int> ToListOfInt(this string value)
		{
			var intList = new List<int>();
			if (!string.IsNullOrEmpty(value))
			{
				var list = value.Split(',');
				foreach (var item in list)
				{
					if (!item.IsNumeric())
					{
						intList = null;
						break;
					}

					if (!string.IsNullOrEmpty(item.Trim()))
						intList.Add(item.ToInt(-1));
				}
			}
			return intList;
		}

		/// <summary>
		/// Convert a comma-separated list in a single string into a list of longs.
		/// If any of the items in the comma-separated list are not numeric, return null.
		/// </summary>
		public static List<long> ToListOfLong(this string value)
		{
			var lngList = new List<long>();
			if (!string.IsNullOrEmpty(value))
			{
				var list = value.Split(',');
				foreach (var item in list)
				{
					if (!item.IsNumeric())
					{
						lngList = null;
						break;
					}

					var itemLng = item.ToLong(-1);
					if (itemLng != -1)
						lngList.Add(itemLng);
				}
			}
			return lngList;
		}

		/// <summary>
		/// Convert a character-separated list in a single string into a list of strings.
		/// </summary>
		public static List<string> ToListOfString(this string value, char delineator, bool ignoreQuotes = true)
		{
			return value.ToListOfString(delineator.ToString(), ignoreQuotes);
		}

		/// <summary>
		/// Convert a character-separated list in a single string into a list of strings.
		/// </summary>
		public static List<string> ToListOfString(this string value, string delineator, bool ignoreQuotes = true)
		{
			var stringList = new List<string>();
			if (string.IsNullOrEmpty(value)) return stringList;

			if (ignoreQuotes)
			{
				var list = Regex.Split(value, Regex.Escape(delineator));
				foreach (var item in list)
				{
					var itemString = item.ToNullSafeString().Trim();
					if (!string.IsNullOrEmpty(itemString))
						stringList.Add(itemString);
				}
			}
			else
			{
				// ignore the delineator if within quotes
				var matchPattern = regexDelineateButIgnoreQuotedPhrases(delineator);
				foreach (Match match in Regex.Matches(value, matchPattern))
					stringList.Add(match.Value);
			}

			return stringList;
		}

		/// <summary>
		/// Returns null for an empty string.
		/// If the given string is not empty, the original string is returned.
		/// </summary>
		public static string ToNullFromEmpty(this string str)
		{
			if (str == "")
			{
				return null;
			}

			return str;
		}

		/// <summary>
		/// Returns the right-most trimmed substring of a given string for a given trim length.
		/// </summary>
		public static string TrimEnd(this string value, string trimValue)
		{
			if (string.IsNullOrEmpty(value)) return string.Empty;
			if (string.IsNullOrEmpty(trimValue)) return value;

			return value.TrimEnd(trimValue.ToCharArray());
		}

		/// <summary>
		/// Returns the left-most trimmed substring of a given string for a given trim length.
		/// </summary>
		public static string TrimStart(this string value, string trimValue)
		{
			if (string.IsNullOrEmpty(value)) return string.Empty;
			if (string.IsNullOrEmpty(trimValue)) return value;

			return value.TrimStart(trimValue.ToCharArray());
		}

		/// <summary>
		/// Returns substring of shortened length and appends "..."
		/// Returns original value if length is greater than value char length.
		/// </summary>
		public static string Truncate(this string value, int length)
		{
			value = value.ToNullSafeString();
			return value.Length > length ? value.Left(length) + "..." : value;
		}

		/// <summary>
		/// Returns the enum equivalent of an string, of a given enum type.
		/// </summary>
		public static T ParseEnum<T>(this string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}

		/// <summary>
		/// Return an html-decoded equivalent of a given xml string.
		/// Only decodes reserved XML chars found within the string.
		/// </summary>
		public static string XmlDecode(this string value)
		{
			//decodes: &, ", ', <, >
			var result = value.ToNullSafeString().Trim();
			if (!string.IsNullOrEmpty(result))
			{
				result = result.Replace("&amp;", "&");
				result = result.Replace("&quot;", "\"");
				result = result.Replace("&apos;", "'");
				result = result.Replace("&lt;", "<");
				result = result.Replace("&gt;", ">");
			}
			return result;
		}

		/// <summary>
		/// Return an html-encoded equivalent of a given string for use in XML.
		/// Only encodes reserved XML chars found within the string.
		/// </summary>
		public static string XmlEncode(this string value)
		{
			//encodes: &, ", ', <, >
			var result = value.ToNullSafeString().Trim();
			if (!string.IsNullOrEmpty(result))
			{
				result = result.Replace("&", "&amp;");
				result = result.Replace("\"", "&quot;");
				result = result.Replace("'", "&apos;");
				result = result.Replace("<", "&lt;");
				result = result.Replace(">", "&gt;");
			}
			return result;
		}

		/// <summary>
		/// Builds regex that will delineate on specified string but ignore delineator if inside quotes.
		/// </summary>
		private static string regexDelineateButIgnoreQuotedPhrases(string delineator)
		{
			var encodeDelineator = delineatorIsRegexSpecialChar(delineator);

			// pattern in English:
			// -- matches if a phrase or word within quotes
			// -- or, if not in quotes, words between delineating character
			// -- in either case, ignore leading or trailing whitespace chars
			var matchPattern = $"(\\s*\"[^\"]+\"\\s*|\\s*[^{(encodeDelineator ? "\\" : "")}{delineator}]+\\s*)";
			return matchPattern;
		}

		/// <summary>
		/// Determines if a given delineator is a special character in regex patterns.
		/// </summary>
		private static bool delineatorIsRegexSpecialChar(string delineator)
		{
			var delineatorIsRegexSpecialChar =
				delineator == "\\"
				|| delineator == "^"
				|| delineator == "$"
				|| delineator == "."
				|| delineator == "|"
				|| delineator == "?"
				|| delineator == "*"
				|| delineator == "+"
				|| delineator == "("
				|| delineator == ")"
				|| delineator == "["
				|| delineator == "{";
			return delineatorIsRegexSpecialChar;
		}
	}
}