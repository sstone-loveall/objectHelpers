namespace Machineghost.ObjectHelpers.Extensions
{
	/// <summary>
	/// Helpful extensions for object type or format conversions.
	/// </summary>
	public static class BoolExtensions
	{
		/// <summary>
		/// Returns a string representation of the boolean value.
		/// </summary>
		public static string ToLowerCase(this bool value)
		{
			return value ? "true" : "false";
		}
	}
}