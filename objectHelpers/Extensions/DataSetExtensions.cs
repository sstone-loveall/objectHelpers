using System.Data;

namespace Machineghost.ObjectHelpers.Extensions
{
	/// <summary>
	/// Helpful extensions for DataSet objects.
	/// </summary>
	public static class DataSetExtensions
	{
		/// <summary>
		/// Returns the value of a given output parameter, by name.
		/// </summary>
		public static object GetOutputParameterValue(this DataSet dataSetObject, string parameterName)
		{
			var dataTable = dataSetObject.Tables["OutputParameters"];
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow row = dataTable.Rows[i];
				if (row.Table.Columns.Contains(parameterName))
				{
					return row.Table.Columns[parameterName];
				}
			}
			return null;
		}

		/// <summary>
		/// Determines if a given DataSet contains any data.
		/// </summary>
		public static bool HasData(this DataSet dataSetObject)
		{
			return (dataSetObject != null && dataSetObject.Tables.Count > 0 && dataSetObject.Tables[0].Rows.Count > 0);
		}

		/// <summary>
		/// Determines if a given DataSet contains any output parameters.
		/// </summary>
		public static bool HasOutputParameters(this DataSet dataSetObject)
		{
			return (dataSetObject.Tables.Contains("OutputParameters") && dataSetObject.Tables["OutputParameters"].Rows.Count > 0);
		}
	}
}