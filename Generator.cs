using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFeladat
{
    internal class Generator
    {
        private Generator() { }

        public static void GenerateHtml(Folder folder, string rootfolder)
        {
            foreach (var file in folder.Files)
            {
                var htmlContent = buildHtml(folder, file, rootfolder);
                makeHtml(file, htmlContent);
            }

            foreach (var subfolder in folder.SubFolders)
            {
                GenerateHtml(subfolder, rootfolder);
            }

            makeIndex(folder, rootfolder);

            if (folder.Path == rootfolder)
            {
                generateCss(rootfolder);
            }
        }

        private static void generateCss(string rootfolder) 
        {
            try
            {
                File.Copy("styles.css", rootfolder + "/styles.css", overwrite: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static string buildHtml(Folder folder, string file, string rootfolder)
        {
            var path = file.Split("\\");
            var fileName = path[path.Length - 1];

            var nextIndex = folder.Files.IndexOf(file) + 1;
            var nextImg = nextIndex < folder.Files.Count ? folder.Files[nextIndex].Replace(Path.GetExtension(file), ".html") : "#";

            var prevIndex = folder.Files.IndexOf(file) - 1;
            var prevImg = prevIndex >= 0 ? folder.Files[prevIndex].Replace(Path.GetExtension(file), ".html") : "#";

            var htmlContent = @$"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Projekt feladat</title>
                    <link rel='stylesheet' href='{rootfolder}/styles.css'>
                </head>
                <body>
                    <h1><a href='{rootfolder}/index.html'>Start Page</a></h1>
                    <hr>
                    <a href='{folder.Path}/index.html'>^^</a>
                    <div class='navigator'>
                    <a href='{prevImg}'> &lt;&lt; </a>
                    <h2>{fileName}</h2>
                    <a href='{nextImg}'> &gt;&gt; </a>
                    </div>
                    <a href='{nextImg}'><img src='{file}'></a>
                </body>
                </html>";

            return htmlContent;
        }

        private static void makeHtml(string file, string htmlContent)
        {
            try
            {
                var filePath = file.Replace(Path.GetExtension(file), ".html");
                File.WriteAllText(filePath, htmlContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void makeIndex(Folder folder, string rootfolder)
        {
            try
            {
                var filePath = folder.Path + "/index.html";
                var htmlContent = buildIndex(folder, rootfolder);
                File.WriteAllText(filePath, htmlContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static string buildIndex(Folder folder, string rootfolder)
        {
            var directories = buildDirUl(folder, rootfolder);
            var images = buildImgUl(folder);

            var htmlContent = @$"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Projekt feladat</title>
                    <link rel='stylesheet' href='{rootfolder}/styles.css'>
                </head>
                <body>
                    <h1><a href='{rootfolder}/index.html'>Start Page</a></h1>
                    <hr>
                    <h2>Directories:</h2>
                    {directories}
                    <hr>
                    <h2>Images:</h2>
                    {images}
                </body>
                </html>";

            return htmlContent;
        }

        private static string buildDirUl(Folder folder, string rootfolder)
        {
            StringBuilder dirs = new StringBuilder();
            dirs.AppendLine("<ul>");

            if (folder.Path != rootfolder)
            {
                var prevIndex = folder.Path.Replace($"\\{folder.Name}", "") + "/index.html";
                dirs.AppendLine($"<li><a href='{prevIndex}'> &lt;&lt; </a></li>");
            }

            foreach (var subfolder in folder.SubFolders)
            {
                dirs.AppendLine($"<li><a href='{subfolder.Path}/index.html'>{subfolder.Name}</a></li>");
            }

            dirs.AppendLine("</ul>");
            return dirs.ToString();
        }

        private static string buildImgUl(Folder folder)
        {
            StringBuilder imgs = new StringBuilder();
            imgs.AppendLine("<ul>");
            foreach (var file in folder.Files)
            {
                var path = file.Split("\\");
                var htmlPath = file.Replace(Path.GetExtension(file), ".html");
                imgs.AppendLine($"<li><a href='{htmlPath}'>{path[path.Length-1]}</a></li>");
            }
            imgs.AppendLine("</ul>");
            return imgs.ToString();
        }
    }
}
