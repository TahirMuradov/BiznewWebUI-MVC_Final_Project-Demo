namespace BiznewWebUI.Helper
{
    public static class FileHelper
    {
        public static async Task<string> SaveFileAsync(this IFormFile file, string WebRootPath)
        {
            var filePath = Path.Combine(WebRootPath, "uploads").ToLower();
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var path = string.Empty;
            
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            var checkFile = directoryInfo.GetFiles()
                .Any(x => x.Name.Contains( file.FileName)&& x.Length==file.Length );

          

            if (!checkFile)
            {
              

               path= "/uploads/" + Guid.NewGuid().ToString() + file.FileName;

            using FileStream fileStream = new(WebRootPath + path, FileMode.Create);
            await file.CopyToAsync(fileStream);
               
            }
            else
            {

                for (int i = 0; i < directoryInfo.GetFiles().Length; i++)
                {
                    if (directoryInfo.GetFiles()[i].Name.Contains(file.FileName) && directoryInfo.GetFiles()[i].Length == file.Length)
                    {
                        path = "/uploads/" + directoryInfo.GetFiles()[i].Name;
                        break;
                    }

                }
            }
            
            return path;
        }
        public static bool DeleteFileAsync(string FilePath,string webRoo)
        {
            return true;
        }
    }
}
