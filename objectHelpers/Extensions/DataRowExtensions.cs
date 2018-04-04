using System.Data;

namespace Machineghost.ObjectHelpers.Extensions
{
	/// <summary>
	/// Helpful extensions for DataRow objects.
	/// </summary>
	public static class DataRowExtensions
	{
		/// <summary>
		/// Returns the value in a column within the given DataRow.
		/// </summary>
		public static object GetValue(this DataRow row, string column)
		{
			return row.Table.Columns.Contains(column) ? row[column] : null;
		}

		/// <summary>
		/// Returns the first value that matches a given column name for a given DataRow.
		/// </summary>
		public static object GetValue(this DataRow row, string[] columns)
		{
			foreach (string column in columns)
			{
				if (row.Table.Columns.Contains(column))
				{
					return row[column];
				}
			}
			return null;
		}
	}
}