using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaodeAmapDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var l = GeographyHelper.GetAddress("116.481488", "39.990464");
            l = GeographyHelper.GetLonAndLat("郑州市国际会展中心");
        }
    }
}
