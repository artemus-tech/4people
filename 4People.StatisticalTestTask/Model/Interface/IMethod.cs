using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4People.StatisticalTestTask.Model
{
    public interface IMethod
    {
        double Estimate(int N, int currentGuestPoints, int currentHostsPoints , List<OutComesModel>  data, double PriorProbability);
    }
}
