namespace Talabat.Dashboard.Helpers
{
	public class PictureSettings
	{
		public static string UploadFile(IFormFile file, string folderName)
		{
			// 1. Get folder path
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);
			// 2. Set a unique filename using GUID
			var fileName = Guid.NewGuid()  + file.FileName;
			// 3. Get the  file path
			var filePath = Path.Combine(folderPath, fileName);
			// 4. Save the file as a stream
			var fs = new FileStream(filePath, FileMode.Create);
			// 5. Copy my file into the FileStream
			file.CopyTo(fs);
			// 6. Return the file name 
			return Path.Combine( "images\\products", fileName);

			/// // Ensure the directory exists
			/// if (!Directory.Exists(folderPath))
			/// {
			/// 	Directory.CreateDirectory(folderPath);
			/// }
		}

		public static void DeleteFile(string folderName, string fileName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);
			if (File.Exists(filePath))
			{ 
				File.Delete(filePath);
			}

		}

	}
}