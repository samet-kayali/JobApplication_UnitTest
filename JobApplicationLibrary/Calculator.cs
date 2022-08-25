using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary
{
    public class Calculator
    {

        //İşe yeni başlayan personelin işletmeye olan maaliyetini hesaplama metotu.
        public double CalculateJobCost(double net_ucret, double sigorta_maaliyet, double issizlik_fonu_maaliyet)
        {
            return net_ucret + net_ucret * 24 / 100 + net_ucret * 3 / 100;
        }
    }
}