using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI
{
    public class ListZ
    {
        public List<double>listApproxZ1  { get; set; }
        public List<double> listApproxZ2 { get; set; }
        public List<double> listApproxZavg { get; set; }

        public ListZ(List<double> listApproxZ1, List<double> listApproxZ2, List<double> listApproxZavg)
        {
            this.listApproxZ1 = new List<double>();
            this.listApproxZ2 = new List<double>();
            this.listApproxZavg = new List<double>();
            foreach (var item in listApproxZ1)
            {
                this.listApproxZ1.Add(item);
            }
            foreach (var item in listApproxZ2)
            {
                this.listApproxZ2.Add(item);
            }
            foreach (var item in listApproxZavg)
            {
                this.listApproxZavg.Add(item);
            }
        }
    }
}
