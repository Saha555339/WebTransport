using System.Collections.Generic;
using LibraryProjectExceptions;
using System;
using System.Globalization;

namespace LibraryDataParse
{
    public class StopsFileParser : FileParser
    {
        public StopsFileParser(string path)
            : base(path)
        {

        }
        private List<StopForParse> _stops = new List<StopForParse>();
        public List<StopForParse> Stops
        {
            get { return _stops; }
        }
        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public void ParseStops()
        {
            bool check = true;
            try
            {
                Parse();
            }
            catch(TransportParseException ex)
            {
                Console.WriteLine(ex.Message);
                check = false;
            }
            //Проверка на названия
            if (check)
            {
                for (int i = 1; i < _arr.Length; i++)
                {
                    string[] s = _arr[i].Split(";");
                    s[1] = s[1].Replace("\"", "");
                    s[6] = s[6].Replace("\"", "");
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
                        throw new TransportParseException("Неверный формат координат");
                    }
                    if (check)
                    {
                        _stops.Add(new StopForParse()
                        {
                            Name = s[1],
                            Latitude = latitude,
                            Longitude = longitude,
                            DistcrictName = s[6]
                        });
                    }
                }
            }
        }


    }
}
