internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Save is starting when a key is pressed...");
        Console.ReadLine();
        string sourceDirectory = @"D:\World of Warcraft 3.3.5a\Data\patch-W.MPQ";

        string destinationRootPath = @"D:\Noggitsaves\";

        // Format the current date and time to a valid folder name
        string formattedDateTime = DateTime.Now.ToString("yyyy_MM_dd_HH_mm");
        string destinationDirectory = Path.Combine(destinationRootPath,"save_"+  formattedDateTime + "_good");

        try
        {
            CopyDirectory(sourceDirectory, destinationDirectory, true);
            Console.WriteLine($"Folder copied successfully to {destinationDirectory}");
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }

        Console.WriteLine("Press any key to exit.");
        System.Threading.Thread.Sleep(1000);
    }

    private static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDir);

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDir}");
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        // If the destination directory doesn't exist, create it.
        Directory.CreateDirectory(destinationDir);

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string tempPath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(tempPath, false);
        }

        // If copying subdirectories, copy them and their contents to new location.
        if (recursive)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destinationDir, subdir.Name);
                CopyDirectory(subdir.FullName, tempPath, recursive);
            }
        }
    }
}