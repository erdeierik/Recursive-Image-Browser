using ProjektFeladat;

class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0 || !Directory.Exists(args[0]))
        {
            Console.WriteLine("Error: provide the path of a directory");
            Environment.Exit(1);
        }

        var directory = args[0];
        Console.WriteLine($"INFO: {DateTime.Now} | Base directory: {directory}");
        Folder rootFolder = TraverseDirectory(directory);

        Generator.GenerateHtml(rootFolder,directory);
        Console.WriteLine($"INFO: HTMLs are generated succesfully!");
    }

    private static Folder TraverseDirectory(string path)
    {
        Folder folder = new Folder(Path.GetFileName(path), path);
        Console.WriteLine($"DEBUG: {DateTime.Now} | Current directory: {folder.Path}");
        try
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file).ToLower();
                if (extension == ".png" || extension == ".jpg" || extension == ".gif")
                {
                    folder.Files.Add(file);
                }
            }
            
            var directories = Directory.GetDirectories(path);
            foreach(var directory in directories)
            {
                Folder subFolder = TraverseDirectory(directory);
                folder.SubFolders.Add(subFolder);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return folder;
    }

}