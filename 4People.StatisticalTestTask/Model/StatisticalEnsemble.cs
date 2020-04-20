using _4People.StatisticalTestTask.Model.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4People.StatisticalTestTask.Model
{
    public class StatisticalEnsemble
    {
        private IMethod cases;
        private List<OutComesModel> StatisticsData;
        private double PriorProbability;

        public StatisticalEnsemble(IMethod method, List<OutComesModel> input, int N, int CurGuestScore, int CurHostsScore)
        {
            cases = method;
            StatisticsData = input;
            var allOutComes = StatisticsData.Where(x => 
                x.Guests+x.Hosts > CurGuestScore+CurHostsScore &&
                x.Guests >= CurGuestScore && 
                x.Guests <= CurGuestScore + N && 
                x.Guests-CurGuestScore+x.Hosts-CurHostsScore<=N && 
                x.Hosts >= CurHostsScore &&
                x.Hosts <= CurHostsScore + N)
                .ToList();
            AbstractCase.Save(allOutComes, "All");
            PriorProbability = allOutComes.Sum(x => x.P);



        }
        

        public double Estimate(int N, int CurGuestScore, int CurHostsScore)
        {
            //just flag for me
            double res = 0.0;
            try
            {
                res = cases.Estimate(N, CurGuestScore, CurHostsScore, StatisticsData, PriorProbability);

            }
            catch (Exception ex)
            {
                
                throw (new Exception(ex.Message));
            }
            return res;
        }
        



    }
}
