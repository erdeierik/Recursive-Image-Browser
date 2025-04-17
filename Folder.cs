using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFeladat
{
    internal class Folder
    {

        public string Name;
        public string Path;
        public List<Folder> SubFolders = new List<Folder>();
        public List<string> Files = new List<string>();

        public Folder(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }

    }
}
