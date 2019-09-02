using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace D_Library.Models.Plugins
{
    public class FileManagement
    {
        public FileManagement()
        {

        }

        public void DeleteFileWithPath(string path)
        {
            File.Delete(path);
        }

        public void Dir_Empty(string root)
        {
            int x = Directory.GetFiles(root).Count();

            if ( x == 0)
            {
                Directory.Delete(root);
            }
        }
    }
}