using _4People.StatisticalTestTask.Model.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4People.StatisticalTestTask.Model
{
    class GuestsVictory : AbstractCase, IMethod
    {
        public double Estimate(int N, int currentGuestPoints, int currentHostsPoints , List<OutComesModel> data,double PriorProbability)
        {
            if (N > 0)
            {
                if (currentGuestPoints + N <= currentHostsPoints)
                {
                    return 0.0;
                }

                if (currentGuestPoints > currentHostsPoints + N)
                {
                    return 1.0;
                }

                var filter = Filter(N, currentGuestPoints, currentHostsPoints, data);

                //first iteration if it's a sequence
                //var filter = data.Where(x => x.Guests + x.Hosts <= currentGuestPoints + currentHostsPoints + N && x.Guests > x.Hosts && x.Hosts>=currentHostsPoints && x.Guests>=currentGuestPoints).ToList();
                Save(filter,"Guest");

                if (filter.Count == 0)
                {
                    throw (new Exception("Not enough data to estimate guest victory probability"));
                }


                return filter.Sum(x => x.P) / PriorProbability;
            }
            else {
                return currentGuestPoints > currentHostsPoints ? 1 : 0;
            }  



        }

        private static List<OutComesModel> Filter(int N, int currentGuestPoints, int currentHostsPoints, List<OutComesModel> data)
        {
            return data.Where(x =>
            x.Guests + x.Hosts <= currentGuestPoints + currentHostsPoints + N && 
            x.Guests > x.Hosts &&
            x.Guests + x.Hosts > currentHostsPoints+currentGuestPoints &&
            x.Hosts >= currentHostsPoints
            //x.Hosts >= currentHostsPoints && x.Guests > currentGuestPoints
            ).ToList();
        }
    }
}
