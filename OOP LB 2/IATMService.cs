using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LB_2
{
    internal interface IATMService
    {
        void CashWithDraw(Dictionary<int, int> source, Dictionary<int, int> result, int amount);
    }
}
