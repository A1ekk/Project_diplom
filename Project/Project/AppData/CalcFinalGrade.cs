using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AppData
{
    public class CalcFinalGrade
    {
        public static double final_grade(double one, double two, double three, double four, double five, double six, double seven, double eight)
        {
            double result;
            result = (one + two + three + four + five + six + seven + eight) / 8;
            result = Math.Round(result, 2);
            return result;
        }
    }
}
