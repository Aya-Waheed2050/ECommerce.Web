namespace AdminDashBoard.Helpers
{
    public static class PictureSettings
    {
        public static string? Upload(IFormFile file, string folderName)
        {
            List<string> allowedExtensions = new List<string>() { ".png", ".jpg", ".jpeg" };
            const int maxSize = 2 * 1024 * 1024;

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension)) return null;
            
            if (file.Length > maxSize || file.Length == 0) return null;
            
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            //var saveFileName = file.FileName.Length > 10 ? file.FileName.Substring(0, 10) : file.FileName;
            var saveFileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}_{saveFileName}{extension}";

            var filePath = Path.Combine(folderPath, fileName);

            using FileStream fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);

            return $"images/{folderName}/{fileName}"; 
        }

        public static bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}