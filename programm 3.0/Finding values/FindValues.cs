using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programm_3._0
{
    class FindValues
    {
        public int polynomialDegree { get; set; } //степень полинома

        private Data3D data3D;
        private Values values;

        public FindValues(int _n, Data3D _data3D, Values _values)
        {
           // this.inputX = _valueX;
            //this.inputY = _valueY;
            this.polynomialDegree = _n;
            this.data3D = _data3D;
            this.values = _values;   
            plotPlane();
            //WriteData writeData = new WriteData(_outputFile, _values);
        }
        public void mainFunctions(int _countColumn, int _n, double[] _arrayX, double[] _arrayY, double[] _findAnswerValues)
        {
            double[] sumPowTemp = new double[_n + 4];

            double[] sumMulTempCp = new double[_n];

            CountSum countSum = new CountSum();

            countSum.sumPowerX(sumPowTemp, _arrayX, _countColumn, _n);

            //Console.WriteLine("sumPowTemp");
            //for (int i = 0; i < sumPowTemp.Length; i++)
            //{
            //    Console.WriteLine(sumPowTemp[i]);
            //}

            countSum.multXY(sumMulTempCp, _arrayX, _arrayY, _countColumn, _n);

            //Console.WriteLine("sumMulTempCp");
            //for (int i = 0; i < sumMulTempCp.Length; i++)
            //{
            //    Console.WriteLine(sumMulTempCp[i]);
            //}

            //=============================================================================

            double[,] mainMassive = new double[_n, _n];

            assignmentMainMassive(mainMassive, sumPowTemp, _n);

            //Console.WriteLine("mainMassive");
            //for (int i = 0; i < _n; i++)
            //{
            //    for (int j = 0; j < _n; j++)
            //    {
            //        Console.Write(mainMassive[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            double[] freeVar = new double[_n];
            for (int i = 0; i < _n; i++)
            {
                freeVar[i] = sumMulTempCp[i];
                //Console.WriteLine(freeVar[i]);
            }

            MethodGaussa methodGaussa = new MethodGaussa();

            methodGaussa.Kramer(_n, mainMassive, freeVar, _findAnswerValues);
            //Console.WriteLine(" _findAnswerValues");
            //for (int i = 0; i < _findAnswerValues.Length; i++)
            //{
            //    Console.WriteLine(_findAnswerValues[i]);
            //}

        }
        public void assignmentMainMassive(double[,] _aMain, double[] _sumPowTemp, int _n)
        {
            int k = _n - 1;

            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    _aMain[j, i] = _sumPowTemp[j + k];
                }
                k--;
            }
        }
        public double findR(double[] _array, double[] _calcArray, int _size)
        {
            double[] sqError = new double[_size];
            int j = 0;
            for (int i = 0; i < _size; i++)
            {
                sqError[j] = (_array[i] - _calcArray[j]) * (_array[i] - _calcArray[j]);
                //Console.WriteLine(sqError[j]);
                j++;
            }
            double sum = 0;
            for (int i = 0; i < _size; i++)
            {
                sum += sqError[i];
            }
            CountSum countSum = new CountSum();
            return (countSum.sumY(_array, _size) - sum) / countSum.sumY(_array, _size);
        }
        public void findingPlane(double[] _findAnswerValues, double[] _arrayX, int _n, double[] _calculationY, int _sizeMassive)
        {
            switch (_n)
            {
                case 2:
                    for (int i = 0; i < _sizeMassive; i++)
                    {
                        _calculationY[i] = _findAnswerValues[0] * _arrayX[i] + _findAnswerValues[1];
                    }
                    break;
                case 3:
                    for (int i = 0; i < _sizeMassive; i++)
                    {
                        _calculationY[i] = _findAnswerValues[0] * Math.Pow(_arrayX[i], 2) + _findAnswerValues[1] * _arrayX[i] + _findAnswerValues[2];
                    }
                    break;
                case 4:
                    for (int i = 0; i < _sizeMassive; i++)
                    {
                        _calculationY[i] = _findAnswerValues[0] * Math.Pow(_arrayX[i], 3) + _findAnswerValues[1] * Math.Pow(_arrayX[i], 2) + _findAnswerValues[2] * _arrayX[i] + _findAnswerValues[3];
                    }
                    break;
                case 5:
                    for (int i = 0; i < _sizeMassive; i++)
                    {
                        _calculationY[i] = _findAnswerValues[0] * Math.Pow(_arrayX[i], 4) + _findAnswerValues[1] * Math.Pow(_arrayX[i], 3) + _findAnswerValues[2] * Math.Pow(_arrayX[i], 2) + _findAnswerValues[3] * _arrayX[i] + _findAnswerValues[4];
                    }
                    break;
                default:
                    break;
            }
        }
        public void plotPlane()
        {
            double[] mainArrayX = new double[values.column];

            for (int i = 0; i < values.column; i++)
            {
                mainArrayX[i] = data3D.arrayX[i];
                values.readArrayX[i] = mainArrayX[i];
            }

            for (int i = 0; i < values.counterY; i++)
            {
                values.readArrayY[i] = data3D.arrayY[i];
            }
            int startMas = 0, endMas = values.column;
            int m = 0;
            for (int n = 0; n < values.counterY; n++)
            {
                double[] mainArrayZ = new double[values.column];
                int j = 0;
                for (int i = startMas; i < endMas; i++)
                {
                    mainArrayZ[j] = data3D.arrayZ[i];            
                    j++;
                }
                mainFunctions(values.column, values.polynomialDegree, mainArrayX, mainArrayZ, values.findAnswerValues);
                double[] calcZ = new double[values.column];
                findingPlane(values.findAnswerValues, mainArrayX, values.polynomialDegree, calcZ, values.column);


                for (int k = 0; k < values.polynomialDegree; k++)
                {
                    values.findCoefficients[m, k] = values.findAnswerValues[k];
                }
                for (int i = 0; i < values.column; i++)
                {
                    values.findMassive[m, i] = calcZ[i];
                }
                m++;
                startMas = endMas;
                endMas += values.column;
            }
        }
        public void findMassiveYwithX(double _inputX, int _n)
        {
            switch (_n)
            {
                case 2:
                    for (int i = 0; i < values.counterY; i++)
                    {
                        values.findYMassiveWithX[i] = values.findCoefficients[i, 0] * _inputX + values.findCoefficients[i, 1];
                    }
                    break;
                case 3:
                    for (int i = 0; i < values.counterY; i++)
                    {
                        values.findYMassiveWithX[i] = values.findCoefficients[i, 0] * Math.Pow(_inputX, 2) + values.findCoefficients[i, 1] * _inputX + values.findCoefficients[i, 2];
                    }
                    break;
                case 4:
                    for (int i = 0; i < values.counterY; i++)
                    {
                        values.findYMassiveWithX[i] = values.findCoefficients[i, 0] * Math.Pow(_inputX, 3) + values.findCoefficients[i, 1] * Math.Pow(_inputX, 2) + values.findCoefficients[i, 2] * _inputX + values.findCoefficients[i, 3];
                    }
                    break;
                case 5:
                    for (int i = 0; i < values.counterY; i++)
                    {
                        values.findYMassiveWithX[i] = values.findCoefficients[i, 0] * Math.Pow(_inputX, 4) + values.findCoefficients[i, 1] * Math.Pow(_inputX, 3) + values.findCoefficients[i, 2] * Math.Pow(_inputX, 2) + values.findCoefficients[i, 3] * _inputX + values.findCoefficients[i, 4];
                    }
                    break;
            }
        }

        public double findRWithList(List<double>_listArray1, List<double> _listArray2)
        {
            double sum1 = 0, sum2 = 0;
            List<double> temp = new List<double>();
            for (int i = 0; i < _listArray1.Count; i++)
            {
                sum1 += _listArray1[i];
                temp.Add(Math.Pow((_listArray1[i] - _listArray2[i]), 2));
            }
            foreach (var item in temp)
            {
                sum2 += item;
            }
            return (sum1 - sum2) / sum1;
        }
    }
}
