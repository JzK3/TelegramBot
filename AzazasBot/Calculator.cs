using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzazasBot
{
    public  class Calculator
    {
        public static int Calc(string message)
        {

                string numbers = "123456789";

                int summ = 0;

            foreach (var i in message)
            {
                foreach (var j in numbers)
                {
                    if (i == j)
                    {
                        summ += (int)char.GetNumericValue(i);
                    }
                }
            }
            return summ;
        }
    }
}
