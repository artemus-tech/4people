using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4People.StatisticalTestTask.Model
{
    public class OutComesModel
    {
        public int Guests { get; set; }
        public int Hosts { get; set; }
        public double P { get; set; }

        public int GetLeader()
        {
            return Math.Max(Guests, Hosts);
        }

        public OutComesModel(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var s = input.Trim().Replace(",", ".").Split(new string[] { "-", " " }, StringSplitOptions.None);
                Hosts = Convert.ToInt32(s[0]);
                Guests = Convert.ToInt32(s[1]);
                //P = CustomMath.GetDouble(s[2],0);
                P = s[2].DoubleParseAdvanced();
            }
            else {
                throw (new ArgumentException("Incorrect Format"));
            }

        }
        public string GetRow()
        { 
            return $"{Hosts.ToStringSafe()}-{Guests.ToStringSafe()} {P.ToStringSafe()}";
        }


    }
}
