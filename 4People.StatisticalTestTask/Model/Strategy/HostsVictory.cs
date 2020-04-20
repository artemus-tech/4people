using _4People.StatisticalTestTask.Model.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4People.StatisticalTestTask.Model
{
    class HostsVictory :AbstractCase, IMethod
    {
        public double Estimate(int N, int currentGuestPoints, int currentHostsPoints , List<OutComesModel> data,double PriorProbability)
        {
            if (N > 0)
            {
                if (currentHostsPoints + N <= currentGuestPoints)
                {
                    return 0.0;
                }

                if (currentHostsPoints> currentGuestPoints+ N)
                {
                    return 1.0;
                }


                List<OutComesModel> filter = Filter(N, currentGuestPoints, currentHostsPoints, data);

                if (filter.Count == 0)
                {
                    throw (new Exception("Input file doesn't contains specified data for calculation of the Host's vicotory probability. Check Input file."));
                }

                Save(filter, "Host");

                return filter.Sum(x => x.P)/ PriorProbability;

            }
            else {
                return currentHostsPoints>currentGuestPoints ? 1 :0.0;//daWhere(x => x.Guests == currentGuestPoints && x.Hosts==currentHostsPoints).Sum(x => x.P);
            }

        }

        private static List<OutComesModel> Filter(int N, int currentGuestPoints, int currentHostsPoints, List<OutComesModel> data)
        {
            return data.Where(x => 
            x.Guests + x.Hosts <= N + currentGuestPoints + currentHostsPoints && 
            x.Hosts > x.Guests && 
            x.Guests+x.Hosts> currentGuestPoints+currentHostsPoints&&
            x.Guests>=currentGuestPoints

            ).ToList();
        }
    }
}
