using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4People.StatisticalTestTask.Model.Strategy
{
    public class DeadHeat : AbstractCase, IMethod
    {


        public double Estimate(int N, int currentGuestPoints, int currentHostsPoints , List<OutComesModel> data, double PriorProbability)
        {
            if (N > 0)
            {
                if (currentGuestPoints + N < currentHostsPoints || currentHostsPoints + N < currentGuestPoints)
                {
                    return 0.0;
                }
                if (N==1 && currentHostsPoints==currentGuestPoints)
                {
                    return 0.0;
                }

                var filter = Filter(N, currentGuestPoints, currentHostsPoints, data);

                var sumPinRange = filter.Sum(x => x.P);

                Save(filter, "DeadHeat");

                if (filter.Count == 0)
                {
                    throw (new Exception("Not enough data to estimate  DeadHeat probability. Check Input file."));
                }

                return sumPinRange / PriorProbability;
            }
            else
            {
                return currentHostsPoints == currentGuestPoints ? 1 : 0;
                //return data.Where(x => x.Guests == currentGuestPoints && x.Hosts == currentHostsPoints).Sum(x => x.P);
            }
        }

        private static List<OutComesModel> Filter(int N, int currentGuestPoints, int currentHostsPoints, List<OutComesModel> data)
        {
            return data.Where(x =>
            x.Guests + x.Hosts <= N + currentGuestPoints + currentHostsPoints &&
            x.Guests == x.Hosts &&
            x.Guests + x.Hosts > currentHostsPoints + currentGuestPoints && 
            x.GetLeader() >= Math.Max(currentGuestPoints, currentHostsPoints)
            ).ToList();
        }
    }
}
