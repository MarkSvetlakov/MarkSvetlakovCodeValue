using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace FileFinder
{
    class FileSearcher
    {
        public string StartDirectoryForSearch { get; private set; }
        public string FileToSearch { get; private set; }
        private List<string> _searchResultList;


        public FileSearcher(string[] arguments)
        {
                if (arguments?.Length == 2)
                {
                    this._searchResultList = new List<string>();
                    this.StartDirectoryForSearch = arguments[0];
                    this.FileToSearch = arguments[1];
                    this._searchResultList = _searchFile(StartDirectoryForSearch);
                }
        }


        private List<string> _searchFile(string dirToSearch)
        {
            if (Directory.Exists(dirToSearch))
            {
                try
                {
                    foreach (string file in Directory.GetFiles(dirToSearch, "*" + FileToSearch + "*"))
                    {
                            this._searchResultList.Add(file);
                        
                    }
                    foreach (string directory in Directory.GetDirectories(dirToSearch))
                    {
                        _searchFile(directory);
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    Trace.TraceError(ex.Message);
                    return null;
                }
            }
            else
            {
                this._searchResultList = null;
            }
            return this._searchResultList;
        }


        public StringBuilder SearchResult()
        {
            StringBuilder strBuilder = new StringBuilder();

            if (this._searchResultList == null)
            {
                strBuilder.Append("Please enter new arguments!");
            }
            else if (!this._searchResultList.Any())
            {
                strBuilder.Append("No files found!");
            }
            else
            {
                foreach (string file in this._searchResultList)
                {
                    try
                    {
                        strBuilder.AppendLine(file);
                        strBuilder.AppendLine("Length in bytes = " + new FileInfo(file).Length);
                    }
                    catch (PathTooLongException ex)
                    {
                        Trace.TraceError(ex.Message);
                    }
                }
            }
            return strBuilder;
        }
    }
}
