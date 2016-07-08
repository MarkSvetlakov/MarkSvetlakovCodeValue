using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    class FileManager
    {
        private FileStream _fileStream;
        private StreamReader _streamReader;
        public List<string> ReadData(string file)
        {
            List<string> dataList = new List<string>();

            try
            {
                _fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                _streamReader = new StreamReader(_fileStream);
                    while (!_streamReader.EndOfStream)
                    {
                        dataList.Add(_streamReader.ReadLine());
                    }
            }
            catch (FileNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return null;
            }
            finally
            {
                if (_fileStream!= null)
                {
                    _fileStream.Close();
                }
                if (_streamReader!=null)
                {
                    _streamReader.Close();
                }
            }
            
            return dataList;
        }
    }
}
