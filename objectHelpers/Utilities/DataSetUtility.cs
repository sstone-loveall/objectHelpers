using System.Data;

namespace Machineghost.ObjectHelpers.Utilities
{
	/// <summary>
	/// Helper class for interactions with DataSet objects.
	/// </summary>
	public static class DataSetUtility
	{
		/// <summary>
		/// Determines if a given column is null or empty.
		/// Returns true if null or empty.
		/// </summary>
		public static bool DataRowColumnIsNullOrEmpty(DataRow row, string columnName)
		{
			return row.IsNull(columnName) || string.IsNullOrEmpty(row[columnName].ToString());
		}
	}
}