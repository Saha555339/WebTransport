using System;
using System.IO;

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
            Encoding srcEncoding = Encoding.GetEncoding(1251);
            if (_path != null)
            {
                if (!(_path.EndsWith(".csv") || _path.EndsWith(".txt")))
                    throw new Exception("Некорректный тип файла");
                else
                {
                    if (file.Length > file_length_byte)
                        throw new Exception("Некорректный размер файла");
                    else
                    {
                        _arr = File.ReadAllLines(_path, encoding: srcEncoding);
                        if (_arr[0] == "")
                            throw new Exception("Файл пуст");
                    }
                }
            }
            else
                throw new Exception("Путь файла не загружен");
        }
    }
}
