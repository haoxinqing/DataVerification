using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilForDataCheck
{
    public class DataModel : DBase
    {
        public static PinSolution Solution = new PinSolution();

        public static List<PinProject> Projects = new List<PinProject>();

        public static void Reset()
        {
            Projects.Clear();
        }
    }
}
