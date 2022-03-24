using System;
using System.IO;
using WebTransport.ProjectExceptions;

namespace WebTransport.DataParse
{
    public class FileParser: IFileParser
    {
        protected string[] _arr;
        private string _path;
        public FileParser(string path)
        {
            _path = path;
        }
        public void Parse()
        {
            const int file_length_byte = 1024 * 1024 * 10;
            System.IO.FileInfo file = new System.IO.FileInfo(_path);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            System.Text.Encoding srcEncoding = System.Text.Encoding.GetEncoding(1251);
            if (_path != null)
            {
                if (!(_path.EndsWith(".csv") || _path.EndsWith(".txt")))
                    throw new TransportParseException("Некорректный тип файла");
                else
                {
                    if (file.Length > file_length_byte)
                        throw new TransportParseException("Некорректный размер файла");
                    else
                    {
                        _arr = File.ReadAllLines(_path, encoding: srcEncoding);
                        if (_arr[0] == "")
                            throw new TransportParseException("Файл пуст");
                    }
                }
            }
            else
                throw new TransportParseException("Путь файла не загружен");
        }
    }
}
