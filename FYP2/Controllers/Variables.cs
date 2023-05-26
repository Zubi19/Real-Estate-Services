using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP2.Controllers
{
    public static class Variables
    {
        public static int agentarea = 0;
        public static int id = 0;
        public static string type = "";
        public static bool showotheragent=false;
        public static SortedDictionary<string, double> Plikelihood=new SortedDictionary<string,double>();
        public static SortedDictionary<string, double> Nlikelihood = new SortedDictionary<string, double>();
        public static double PositivePriorProbability = 0.0;
        public static double NegativePriorProbability = 0.0;
        public static bool naivebayesrun = false;
    }
}