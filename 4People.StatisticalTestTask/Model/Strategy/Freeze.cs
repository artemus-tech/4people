using _4People.StatisticalTestTask.Model.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4People.StatisticalTestTask.Model
{
    public class Freeze : AbstractCase, IMethod
    {
        public double Estimate(int N, int currentGuestPoints, int currentHostsPoints, List<OutComesModel> data,double PriorProbability)
        {

            /*
            var filter = data.Where(x => x.Guests == currentGuestPoints && x.Hosts == currentHostsPoints).ToList();

            Save(filter,"Stuck");
            if (filter.Count == 0)
            {
                throw (new Exception("Out of data to estimate Stuck. Check input file."));
            }
            return filter.Sum(x => x.P)/data.Where(x => x.Guests >= currentGuestPoints && x.Hosts >= currentHostsPoints).Sum(x=>x.P)  ;
            */
            var filter = data.Where(x => x.Guests >= currentGuestPoints && x.Hosts >= currentHostsPoints && x.Guests+ x.Hosts!= currentGuestPoints+ currentHostsPoints && x.Guests + x.Hosts <= N + currentGuestPoints + currentHostsPoints).ToList();

            Save(filter, "Stuck");
            if (filter.Count == 0)
            {
                throw (new Exception("Input file doesn't contains specified data for Freeze-probability calculation. Check Input file."));
            }
            return 1 - filter.Sum(x => x.P)/100;


        }
    }
}
