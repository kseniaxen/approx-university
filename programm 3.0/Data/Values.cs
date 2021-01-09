using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programm_3._0
{
    class Values
    {
        public int polynomialDegree { get; set; } //степень полинома
        public int column { get; set; } //столбик 
        public int counterY { get; set; }
        public double[] findAnswerValues { get; set; } //найденные коэффициенты с метода гаусса 
        public double[,] findMassive { get; set; } 
        public double[,] findCoefficients { get; set; } //coefficients все коэффициенты в массиве
        public double[] findYMassiveWithX  { get; set; } //массив полученные с введенной температуры на каждую плотность

        public List<double> listArrayResult; //найденный массив в виде листа
        public double[] findCoefWithZ { get; set; }
        public double[] readArrayX { get; set; }//считанные данные с текстового документа массив Х
        public double[] readArrayY { get; set; }//считанные данные с текстового документа массив Z
        public Values(int _n, int _counterX, int _counterY) //степень, количество X(T,Cp), количество Z(P)
        {
            polynomialDegree = _n;
            this.counterY = _counterY;
            this.column = _counterX;
            findAnswerValues = new double[polynomialDegree];
            //Console.WriteLine($"Y {_counterY} X {_counterX}");
            findMassive = new double[_counterY,_counterX];
            findCoefficients = new double[_counterY, _n];
            findYMassiveWithX = new double[_counterY];
            findCoefWithZ = new double[_n];
            readArrayX = new double[_counterX];
            readArrayY = new double[_counterY];
            listArrayResult = new List<double>();
        }
        public void assignListArray(List <double> _listArrayResult) //запись главного двумерного массива в лист
        {
            for (int i = 0; i < counterY; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    //Console.Write($"{findMassive[i, j]}  "); 
                    _listArrayResult.Add(findMassive[i, j]);
                }
                //Console.WriteLine();
            }
        }
        public List<double> avgArrayResultBetweenTwoList(List<double> _listArray1, List<double> _listArray2)
        {
            List<double> listRes = new List<double>();
            for (int i = 0; i < _listArray1.Count; i++)
            {
                listRes.Add((_listArray1[i] + _listArray2[i]) / 2);
            }
            return listRes;
        }
    }
}
