using System.IO;

namespace Machineghost.ObjectHelpers.Utilities
{
	/// <summary>
	/// Helper class for interacting with FileStreams.
	/// </summary>
	public static class FileStreamUtility
	{
		/// <summary>
		/// Return a memory stream for a given file.
		/// Returns empty if the file is not found.
		/// </summary>
		public static MemoryStream MemoryStreamFromFileName(string fileName)
		{
			MemoryStream ms = new MemoryStream();
			using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				byte[] bytes = new byte[file.Length];
				file.Read(bytes, 0, (int)file.Length);
				ms.Write(bytes, 0, (int)file.Length);
			}
			ms.Position = 0;
			return ms;
		}
	}
}