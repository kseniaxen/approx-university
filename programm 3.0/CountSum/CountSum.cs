using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programm_3._0
{
    class CountSum
    {
        public double sumY(double[] _arrayY, int _Size)
        {
            double sumArrCp = 0;
            for (int i = 0; i < _Size; i++)
            {
                sumArrCp += _arrayY[i];
            }
            return sumArrCp;
        }
        public void sumPowerX(double[] _sumPowTemp, double[] _arrayX, int _Size, int _n)
        {

            if (_n == 3)
            {
                _n += 1;
            }
            else if (_n == 4)
            {
                _n += 2;
            }
            else if (_n == 5)
            {
                _n += 3;
            }
            for (int i = 0; i <= _n; i++)
            {
                _sumPowTemp[i] = 0;
                for (int j = 0; j < _Size; j++)
                {
                    _sumPowTemp[i] += Math.Pow(_arrayX[j], i);
                }
            }
        }
        public void multXY(double[] _sumMulTempCp, double[] _arrayX, double[] _arrayY, int _Size, int _n)
        {
            _sumMulTempCp[0] = sumY(_arrayY, _Size);
            for (int i = 1; i < _n; i++)
            {
                _sumMulTempCp[i] = 0;
                for (int j = 0; j < _Size; j++)
                {
                    _sumMulTempCp[i] += Math.Pow(_arrayX[j], i) * _arrayY[j];

                }
            }
        }
    }
}
