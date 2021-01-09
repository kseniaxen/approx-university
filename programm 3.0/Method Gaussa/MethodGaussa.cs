using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programm_3._0
{
    class MethodGaussa
    {
       private double determinate(int _n, double[,] _massive)
       {
            double d = 0;
            int c, subi, i, j, subj;

            double[,] submat = new double[_n, _n];

            if (_n == 2)
            {
                return ((_massive[0, 0] * _massive[1, 1]) - (_massive[1, 0] * _massive[0, 1]));
            }
            else
            {
                for (c = 0; c < _n; c++)
                {
                    subi = 0;
                    for (i = 1; i < _n; i++)
                    {
                        subj = 0;
                        for (j = 0; j < _n; j++)
                        {
                            if (j == c)
                            {
                                continue;
                            }
                            submat[subi, subj] = _massive[i, j];
                            subj++;
                        }
                        subi++;
                    }
                    d = d + (Math.Pow(-1, c) * _massive[0, c] * determinate(_n - 1, submat));
                }
                return d;
            }
       }

        public void Kramer(int _n, double[,] _aMain, double[] _freeVar, double[] _findAnswerValues)
        {
            double[] detArray = new double[_n];
            double deterMain;
            double[] temp = new double[_n];
            deterMain = determinate(_n, _aMain);
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    temp[j] = _aMain[j,i];
                    _aMain[j,i] = _freeVar[j];
                }
                detArray[i] = determinate(_n, _aMain);
                for (int k = 0; k < _n; k++)
                {
                    _aMain[k,i] = temp[k];
                }
            }

            for (int i = 0; i < _n; i++)
            {
                _findAnswerValues[i] = detArray[i] / deterMain;
            }
        }
    }
}
