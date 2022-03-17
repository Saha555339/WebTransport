using WebTransport.Dto;
using System.Collections.Generic;
using WebTransport.DataBase;
using System;
using System.IO;
using System.Globalization;
using System.Linq;

namespace WebTransport.DataParse
{
    public class StopsFileParser : FileParser
    {
        public StopsFileParser(string path)
            : base(path)
        {

        }
        private List<string> _stopNames = new List<string>();
        public List<string> StopNames
        {
            get { return _stopNames; }
        }
        private List<double> _longitudes = new List<double>();
        public List<double> Longitudes
        {
            get { return _longitudes; }
        }
        private List<double> _latitudes = new List<double>();
        public List<double> Latitudes
        {
            get { return _latitudes; }
        }
        private List<string> _districtNames = new List<string>();
        public List<string> DistrictNames
        {
            get { return _districtNames; }
        }
        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public void ParseStops()
        {
            Parse();
            //Проверка на названия
            for (int i = 1; i < _arr.Length; i++)
            {
                string[] s = _arr[i].Split(";");
                s[1] = s[1].Replace("\"", "");
                _stopNames.Add(s[1]);
                bool check = true;
                double longitude = 0;
                double latitude = 0;
                try
                {
                    s[2] = s[2].Replace("\"", "");
                    s[3] = s[3].Replace("\"", "");
                    longitude = double.Parse(s[2], formatter);
                    latitude = double.Parse(s[3], formatter);
                }
                catch (Exception)
                {
                    check = false;
                    throw new Exception("Неверный формат координат");
                }
                if (check)
                {
                    _longitudes.Add(longitude);
                    _latitudes.Add(latitude);
                }
                s[6] = s[6].Replace("\"", "");
                _districtNames.Add(s[6]);
            }
        }


    }
}
