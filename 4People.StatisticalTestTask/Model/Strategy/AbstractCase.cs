using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4People.StatisticalTestTask.Model.Strategy
{
    public class AbstractCase
    {
        /// <summary>
        /// just for me
        /// </summary>
        public static void Save(List<OutComesModel> data, string key)
        {

            bool exists = System.IO.Directory.Exists(Constants.LogDir);

            if (!exists)
                System.IO.Directory.CreateDirectory(Constants.LogDir);

            using (TextWriter tw = new StreamWriter(Path.Combine(Constants.LogDir, $"{key}_{Constants.LogFile}")))
            {
                foreach (var s in data)
                {
                    tw.WriteLine(s.GetRow());
                }
            }
        }
    }
}
