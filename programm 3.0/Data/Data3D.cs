using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace programm_3._0
{
    class Data3D
    {
        public double[] arrayX { get; set; }
        public double[] arrayY { get; set; }
        public double[] arrayZ { get; set; }

        public Data3D(List<double> _arrX, List<double> _arrY, List<double> _arrZ)
        {
            arrayX = new double[_arrX.Count];
            arrayY = new double[_arrY.Count];
            arrayZ = new double[_arrZ.Count];

            _arrX.CopyTo(arrayX);
            _arrY.CopyTo(arrayY);
            _arrZ.CopyTo(arrayZ);
        }

    }
}
