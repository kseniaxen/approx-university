using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programm_3._0
{
    class ReadTransformData
    {
        public struct sortedArray<T>
        {
            public T value;
            public int indLast;
            public int indNew;
        }

        public struct sortedList<T>
        {
            public List<T> list;
            public double R;
            public int polynome;
        }

        //переписать на лист
        public double[] transDataX;  //Объем израсходованного доменого газа за период
        public double[] transDataZ; //Продолжительность периода
        public double[] transDataY;  //Средняя температуры купола

        public List<double> dataFDim; //Четвертый массив для группировки данных

        public List<double> fillArrayX;
        public List<double> fillArrayY;

        public List<double> twoDimArrayX1;
        public List<double> twoDimArrayY1;

        public List<double> oneDimArrayX1;
        public List<double> oneDimArrayY1;

        public List<double> resultArray;
        public List<double> resultArray_WithoutNull;
        public List<double> resultArray_Approx;
        public List<double> invertedArray_WithoutNull; //Перевернутый массив
        public List<double> invertedArray_Approx;

        //=============================================================================================

        public string fileName { get; set; } //Имя файла для считывания
        public double start { get; set; }
        public double end { get; set; }
        public double h { get; set; }

        public string numColX { get; set; } //"S"
        public string numColY { get; set; } //"D";
        public string numColZ { get; set; } //"H";
        public string numColF { get; set; }// "Q";

        public double minF { get; set; } 
        public double maxF { get; set; } 

        private Microsoft.Office.Interop.Excel.Application ObjExcel;
        private Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
        private Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

        public int Draw;//Переменная для отображения двумерного массива

        public ReadTransformData(string _pathToFile, Microsoft.Office.Interop.Excel.Application _ObjExcel, Microsoft.Office.Interop.Excel.Workbook _ObjWorkBook, Microsoft.Office.Interop.Excel.Worksheet _ObjWorkSheet, double _start, double _end, double _h, string numColX, string numColY, string numColZ, string numColF, double minF, double maxF, bool _key)
        {
            this.fileName = _pathToFile;

            this.ObjExcel = _ObjExcel;
            this.ObjWorkBook = _ObjWorkBook;
            this.ObjWorkSheet = _ObjWorkSheet;

            this.start = _start;
            this.end = _end;
            this.h = _h;

            this.numColX = numColX;
            this.numColY = numColY;
            this.numColZ = numColZ;
            this.numColF = numColF;

            this.minF = minF;
            this.maxF = maxF;

            dataFDim = new List<double>();

            //Считывание с Excel файла
            assignMainParameters();

            sortedArray<double>[] sortArr = new sortedArray<double>[dataFDim.Count];
            //Сортировка элементов 
            sortByParamMainParameters(dataFDim, sortArr);
            //Выборка элементов
            convertArrayMainParameters(minF, maxF, sortArr, dataFDim);

            //Инициализация главных массивов X и Y
            fillArrayX = new List<double>();
            fillArrayY = new List<double>();

            //Инициализация двумерных массивов X и Y
            twoDimArrayX1 = new List<double>();
            twoDimArrayY1 = new List<double>();

            oneDimArrayX1 = new List<double>();
            oneDimArrayY1 = new List<double>();

            //Инициализация результирующего массива Z
            resultArray = new List<double>();
            //Инициализация результирующего массива Z без нулей
            resultArray_WithoutNull = new List<double>();
            //Инициализация перевернутого массива
            invertedArray_WithoutNull = new List<double>();
            invertedArray_Approx = new List<double>();

            //Запись массивов X и Y с помощью сигмы 
            fillArray(fillArrayY, transDataY, start, end, h);
            fillArray(fillArrayX, transDataX, start, end, h);

            findElementsThrDimensArray(resultArray, h, twoDimArrayX1, twoDimArrayY1);
            numbersColumn_WithNull(resultArray, fillArrayX.Count, fillArrayY.Count, 2, Draw);
            if (numbersColumn_WithNull(resultArray, fillArrayX.Count, fillArrayY.Count, 2, Draw).Count > 0 && _key)
            {
               
            }
            else
            {
                writeListWithoutNull_AvgNum(resultArray, resultArray_WithoutNull, Draw);

                changeMassiveFromTwoDimToOneDim(twoDimArrayY1, oneDimArrayY1, Draw, fillArrayX.Count, fillArrayY.Count, false);
                changeMassiveFromTwoDimToOneDim(twoDimArrayX1, oneDimArrayX1, Draw, fillArrayX.Count, fillArrayY.Count, true);

                ////Замена нулями аппроксимируемыми значениями в resultArray
                resultArray_Approx = new List<double>();
                resultArray_Approx = resultArray;
                //searchBestApproximationsResultArray(resultArray, ref resultArray_Approx, oneDimArrayX1, oneDimArrayY1, Draw);

                //Перевертывание массива Z
                invertedDataZ(resultArray_WithoutNull, invertedArray_WithoutNull, fillArrayX.Count, fillArrayY.Count, Draw);
                invertedDataZ(resultArray_Approx, invertedArray_Approx, fillArrayX.Count, fillArrayY.Count, Draw);
            }
        }

        public void assignMainParameters()
        {
            

            Range usedColumnX = ObjWorkSheet.UsedRange.Columns[numColX];
            System.Array myvaluesX = (System.Array)usedColumnX.Cells.Value2;
            string[] strArrayX = myvaluesX.OfType<object>().Select(o => o.ToString()).ToArray();

            Range usedColumnZ = ObjWorkSheet.UsedRange.Columns[numColZ];
            System.Array myvaluesZ = (System.Array)usedColumnZ.Cells.Value2;
            string[] strArrayY = myvaluesZ.OfType<object>().Select(o => o.ToString()).ToArray();

            Range usedColumnY = ObjWorkSheet.UsedRange.Columns[numColY];
            System.Array myvaluesY = (System.Array)usedColumnY.Cells.Value2;
            string[] strArrayZ = myvaluesY.OfType<object>().Select(o => o.ToString()).ToArray();

            Range usedColumnF = ObjWorkSheet.UsedRange.Columns[numColF];
            System.Array myvaluesF = (System.Array)usedColumnF.Cells.Value2;
            string[] strArrayF = myvaluesF.OfType<object>().Select(o => o.ToString()).ToArray();

            int countXvalues = 0;

            foreach (var item in strArrayX)
            {
                countXvalues++;
            }

            int numStart = 7;

            transDataX = new double[countXvalues - 8 + 1];

            transDataY = new double[countXvalues - 8 + 1];

            transDataZ = new double[countXvalues - 8 + 1];

            int j = 0;

            for (int i = numStart; i < countXvalues; i++)
            {
                transDataX[j] = Convert.ToDouble(strArrayX[i]);
                transDataY[j] = Convert.ToDouble(strArrayY[i]);
                transDataZ[j] = Convert.ToDouble(strArrayZ[i]);
                dataFDim.Add(Convert.ToDouble(strArrayF[i]));
                //Console.WriteLine($"{transDataX[j]}  {transDataY[j]}  {transDataZ[j]}");
                j++;
            }
        }

        private void sortByParamMainParameters(List<double> _data, sortedArray<double>[] _sortArr)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                _sortArr[i].value = _data[i];
                _sortArr[i].indLast = i;
            }

            _data.Sort();

            int n = 0;
            do
            {
                for (int i = 0; i < _data.Count; i++)
                {
                    if (_data[i] == _sortArr[n].value)
                    {
                        _sortArr[n].indNew = i;
                    }
                }
                n++;
            } while (n != _sortArr.Length);
        }

        private void convertArrayMainParameters(double _startNum, double _endNum, sortedArray<double>[] _sortArr, List<double> _dataFDim)
        {
            List<int> tempInd = new List<int>();
            List<double> dataF = new List<double>();
            for (int i = 0; i < _sortArr.Length; i++)
            {
                if (_sortArr[i].value >= _startNum && _sortArr[i].value <= _endNum)
                {
                    dataF.Add(_sortArr[i].value);
                    tempInd.Add(_sortArr[i].indLast);
                }
            }
            List<double> tempX = new List<double>();
            List<double> tempY = new List<double>();
            List<double> tempZ = new List<double>();

            foreach (var item in tempInd)
            {
                tempX.Add(transDataX[item]);
                tempY.Add(transDataZ[item]);
                tempZ.Add(transDataY[item]);
            }

            _dataFDim.Clear();
            foreach (var item in dataF)
            {
                _dataFDim.Add(item);
            }
            Array.Clear(transDataX, 0, transDataX.Length);
            Array.Clear(transDataZ, 0, transDataZ.Length);
            Array.Clear(transDataY, 0, transDataY.Length);

            Array.Resize(ref transDataX, dataF.Count);
            Array.Resize(ref transDataZ, dataF.Count);
            Array.Resize(ref transDataY, dataF.Count);

            tempX.CopyTo(transDataX);
            tempY.CopyTo(transDataZ);
            tempZ.CopyTo(transDataY);

        }

        private double standardDeviation(double[] _mas) //СКО
        {
            double avg = _mas.Average();
            double variance = 0;
            for (int i = 0; i < _mas.Length; i++)
            {
                variance += (_mas[i] - avg) * (_mas[i] - avg);
            }
            variance /= _mas.Length;
            return Math.Sqrt(variance);
        }

        private void fillArray(List<double> _array, double[] _initArray, double _start, double _end, double _h) // заполнение массива tкуп, VДГорб
        {

            double Num = _initArray.Average() + (_start * standardDeviation(_initArray));

            double dev = standardDeviation(_initArray) * _h;
           // Console.WriteLine($"start {Num} end {_initArray.Average() + (_end * standardDeviation(_initArray))}");
            for (double i = _start; i < _end; i += _h)
            {
                _array.Add(Num);
                Num += dev;
               //Console.WriteLine(Num);
                Draw++;
            }
        }

        public void findElementsThrDimensArray(List<double> _resultArray, double _h, List<double> _arrX1, List<double> _arrY1) //нахождение трехмерного массива
        {
            List<double> tempArray = new List<double>();

            List<double> tempArrayX1 = new List<double>();
            List<double> tempArrayY1 = new List<double>();

            for (int i = 0; i < fillArrayX.Count; i++)
            {
                for (int j = 0; j < fillArrayY.Count; j++)
                {
                    for (int k = 0; k < transDataX.Length; k++)
                    {
                        if (((transDataY[k] > fillArrayY[j] - (standardDeviation(transDataY) * _h)) && (transDataY[k] < fillArrayY[j] + (standardDeviation(transDataY) * _h))) && ((transDataX[k] > fillArrayX[i] - (standardDeviation(transDataX) * _h)) && (transDataX[k] < fillArrayX[i] + (standardDeviation(transDataX) * _h))))
                        {
                            tempArray.Add(transDataZ[k]);
                            tempArrayX1.Add(transDataX[k]);
                            tempArrayY1.Add(transDataY[k]);
                        }
                    }
                    if ((tempArray.Count < 1) || (tempArrayX1.Count < 1) || (tempArrayY1.Count < 1))
                    {
                        _resultArray.Add(0);
                        _arrX1.Add(0);
                        _arrY1.Add(0);
                    }
                    else
                    {
                        _resultArray.Add(tempArray.Average());
                        _arrX1.Add(tempArrayX1.Average());
                        _arrY1.Add(tempArrayY1.Average());
                    }
                    tempArray.Clear();
                    tempArrayX1.Clear();
                    tempArrayY1.Clear();
                }
            }

        }

        private double avgNum(List<double> _list)
        {
            List<double> tempList = new List<double>();
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i] > 0)
                {
                    tempList.Add(_list[i]);
                }
            }
            return tempList.Average();
        }

        public void writeListWithoutNull_AvgNum(List<double> _resultArray, List<double> _resultWithoutNull, int _Draw) //переобразование результирующего массива без нулей с помощью среднего по всей строке
        {
            int l = 0;
            List<double> tempList = new List<double>();
            double avg;
            for (int i = 0; i < _resultArray.Count; i++)
            {
                if (l == _Draw / 2)
                {
                    avg = avgNum(tempList);

                    for (int j = 0; j < tempList.Count; j++)
                    {
                        if (tempList[j] <= 0)
                        {
                            _resultWithoutNull.Add(avg);
                        }
                        else
                        {
                            _resultWithoutNull.Add(tempList[j]);
                        }
                    }
                    tempList.Clear();
                    l = 0;
                }
              //  Console.WriteLine(_resultArray[i]);
                tempList.Add(_resultArray[i]);
                l++;
            }

            avg = avgNum(tempList);

            for (int j = 0; j < tempList.Count; j++)
            {
                if (tempList[j] <= 0)
                {
                    _resultWithoutNull.Add(avg);
                }
                else
                {
                    _resultWithoutNull.Add(tempList[j]);
                }

            }

            //int k = 0;
            //for (int i = 0; i < resultWithoutNull.Count; i++)
            //{
            //    if (k == Draw / 2)
            //    {
            //        Console.WriteLine();
            //        k = 0;
            //    }
            //    Console.Write($"{resultWithoutNull[i]}  ");
            //    k++;
            //}

            //Console.ReadLine();

        }

        private double[,] writeTwoDimMassive(List<double> _resultArray, int _colX, int _colY, int _Draw)
        {
            double[,] twoDemMassive = new double[_colX, _colY];
            int l = 0, n = 0;
            for (int i = 0; i < _resultArray.Count; i++)
            {
                if (l == _Draw/2)
                {
                    l = 0;
                    n++; //Console.WriteLine();
                }
                twoDemMassive[n, l] = _resultArray[i];
                //Console.Write($"{n} {l}  ");
                l++;
            }
            return twoDemMassive;
        }

        public void invertedDataZ(List<double> _resultArray, List<double> _invertedArray, int _colX, int _colY, int _Draw)
        {
           // Console.WriteLine($"X {_colX} Y {_colY}");
            double[,] arr = writeTwoDimMassive(_resultArray, _colX, _colY, _Draw);
            
            for (int i = 0; i < _colY; i++)
            {
                for (int j = 0; j < _colX; j++)
                {
                    _invertedArray.Add(arr[j, i]);
                  //  Console.Write($"{arr[j,i]} ");
                }
               // Console.WriteLine();
            }
        }

        private void changeMassiveFromTwoDimToOneDim(List<double> _twoDimArr, List<double> _oneDimArr, int _draw, int _colX, int _colY, bool key)
        {
            double[,] twoDemMassive = writeTwoDimMassive(_twoDimArr, _colX, _colY, Draw);
            double sum = 0;
            int k = 0;
            if (key)
            {
                for (int i = 0; i < _colX; i++)
                {
                    for (int j = 0; j < _colY; j++)
                    {
                        if (twoDemMassive[i, j] > 0)
                        {
                            // Console.Write(twoDemMassive[j,i] + " ");
                            sum += twoDemMassive[i, j];
                            k++;
                        }
                    }
                    _oneDimArr.Add(sum / k);
                    sum = 0;
                    k = 0;
                    //Console.WriteLine();
                }
            }
            else
            {
                for (int i = 0; i < _colY; i++)
                {
                    for (int j = 0; j < _colX; j++)
                    {
                        if (twoDemMassive[j, i] > 0)
                        {
                            // Console.Write(twoDemMassive[j,i] + " ");
                            sum += twoDemMassive[j, i];
                            k++;
                        }
                    }
                    _oneDimArr.Add(sum / k);
                    sum = 0;
                    k = 0;
                    //Console.WriteLine();
                }
            }
        }

        private List<int> numbersColumn_WithNull(List<double> _twoDimArr, int _colX, int _colY, double procent, int _Draw)
        {
            double[,] twoDemMassive = writeTwoDimMassive(_twoDimArr, _colX, _colY, _Draw);
            int counterNULL = 0;
            List<int> numberColumnNull = new List<int>();
            for (int i = 0; i < _colY; i++)
            {
                for (int j = 0; j < _colX; j++)
                {
                    if (twoDemMassive[j, i] <= 0)
                    {
                        counterNULL++;
                    }
                   // Console.Write(twoDemMassive[j, i] + " ");
                }
                //Console.WriteLine();
                if (counterNULL > _colX / procent)
                {
                    numberColumnNull.Add(i);
                    //Console.WriteLine($"i {i}");
                }
                counterNULL = 0;
            }
            return numberColumnNull;
        }

        private List<int> numbersLines_WithNull(List<double> _twoDimArr, int _colY, int _colX, double procent, int _Draw)
        {
            double[,] twoDemMassive = writeTwoDimMassive(_twoDimArr, _colY, _colX, _Draw);
            int counterNULL = 0;
            List<int> numberLinesNull = new List<int>();
            for (int i = 0; i < _colX; i++)
            {
                for (int j = 0; j < _colY; j++)
                {
                    if (twoDemMassive[j, i] <= 0)
                    {
                        counterNULL++;
                    } 
                }
                if (counterNULL < _colY / procent)
                {
                    numberLinesNull.Add(i);
                }
                counterNULL = 0;
            }
            return numberLinesNull;
        }

        private void deleteInArraysColumns_And_Lines_WithNull(ref List<double> _resultArray, ref List<double> _twoDimArrayX1, ref List<double> _twoDimArrayY1, ref List<double> _fillArrayX, ref List<double> _fillArrayY, ref int _draw)
        {
            List<int> numColumn = numbersColumn_WithNull(_resultArray, _fillArrayX.Count, _fillArrayY.Count, 2, _draw);
            int tempCountX = _fillArrayX.Count;
            int tempCountY = _fillArrayY.Count;
            List<double> tempODY = new List<double>();
            List<int> tempCountYNew = new List<int>();

            tempODY.AddRange(_fillArrayY);

            _fillArrayY.Clear();

            foreach (var item in numColumn)
            {
                _fillArrayY.Add(tempODY[item]);
                tempCountYNew.Add(item);
               // Console.WriteLine(item);
            }

            double[,] resArr = writeTwoDimMassive(_resultArray, tempCountX, tempCountY, _draw);
            double[,] resTDX1 = writeTwoDimMassive(_twoDimArrayX1, tempCountX, tempCountY, _draw);
            double[,] resTDY1 = writeTwoDimMassive(_twoDimArrayY1, tempCountX, tempCountY, _draw);

            List<double> tempResArr = new List<double>();
            List<double> tempResTDX1 = new List<double>();
            List<double> tempResTDY1 = new List<double>();


            for (int i = 0; i < tempCountYNew.Count; i++)
            {
                for (int j = 0; j < tempCountX; j++)
                {
                    tempResArr.Add(resArr[j, tempCountYNew[i]]);
                    tempResTDX1.Add(resTDX1[j, tempCountYNew[i]]);
                    tempResTDY1.Add(resTDY1[j, tempCountYNew[i]]);
                }
            }
            List<int> numLines = numbersLines_WithNull(tempResArr, _fillArrayY.Count, tempCountX, 2, _draw);
            List<double> tempODX = new List<double>();
            List<int> tempCountXNew = new List<int>();

            tempODX.AddRange(_fillArrayX);

            _fillArrayX.Clear();

            foreach (var item in numLines)
            {
                _fillArrayX.Add(tempODX[item]);
                tempCountXNew.Add(item);
               // Console.WriteLine(item);
            }

            //tempResTDY1.Clear();
            double[,] resArrTemp = writeTwoDimMassive(tempResArr, _fillArrayY.Count, tempCountX, _draw);
            double[,] resArrTDX1Temp = writeTwoDimMassive(tempResTDX1, _fillArrayY.Count, tempCountX, _draw);
            double[,] resArrTDY1Temp = writeTwoDimMassive(tempResTDY1, _fillArrayY.Count, tempCountX, _draw);
            
            tempResArr.Clear();
            tempResTDX1.Clear();
            tempResTDY1.Clear();
            //Console.WriteLine($"Y {tempCountYNew.Count} X {tempCountXNew.Count}");
            for (int i = 0; i < tempCountXNew.Count; i++)
            {
                for (int j = 0; j < tempCountYNew.Count; j++)
                {
                    tempResArr.Add(resArrTemp[j, tempCountXNew[i]]);
                    tempResTDX1.Add(resArrTDX1Temp[j, tempCountXNew[i]]);
                    tempResTDY1.Add(resArrTDY1Temp[j, tempCountXNew[i]]);
                    //Console.Write($"{tempCountYNew[j]} {tempCountXNew[i]}   ");

                }
               // Console.WriteLine();
            }

            _draw = (numColumn.Count * 2);

            _resultArray.Clear();
            _twoDimArrayX1.Clear();
            _twoDimArrayY1.Clear();

            _resultArray = tempResArr;
            _twoDimArrayX1 = tempResTDX1;
            _twoDimArrayY1 = tempResTDY1;

        }

        List<double> approximationResultArray(List<double> _resultArray, List<double> _oneDimArrayX1, List<double> _oneDimArrayY1, int _Draw, int _n, ref double _R)
        {
            int l = 0;
            List<double> resApprox = new List<double>();
            List<double> tempList = new List<double>();
            List<double> listResTemp = new List<double>();
            List<double> listArrX1Temp = new List<double>();
            List<double> listArrY1Temp = new List<double>();
            Data3D data3D;
            Values values;
            FindValues_xa_yb_c findValues;
            int a = 0;
            listArrY1Temp.Add(_oneDimArrayY1[a]);
            for (int i = 0; i < _resultArray.Count; i++)
            {
                if (l == _Draw / 2)
                {
                    data3D = new Data3D(listArrX1Temp, listArrY1Temp, tempList);
                    values = new Values(_n, data3D.arrayX.Length, data3D.arrayY.Length);
                    findValues = new FindValues_xa_yb_c(_n, data3D, values);
                    int j = 0;
                    foreach (var item in listResTemp)
                    {
                        if(item<=0)
                        {
                            findValues.findMassiveYwithX(_oneDimArrayX1[j], _n);
                            resApprox.Add(values.findYMassiveWithX[0]);
                        }
                        else
                        {
                            resApprox.Add(item);
                        }
                        j++;
                    }

                    l = 0;
                    tempList.Clear();
                    listResTemp.Clear();
                    listArrX1Temp.Clear();
                    listArrY1Temp.Clear();
                    a++;
                    listArrY1Temp.Add(_oneDimArrayY1[a]);
                }

                if(_resultArray[i]>0)
                {
                    tempList.Add(_resultArray[i]);
                    listArrX1Temp.Add(_oneDimArrayX1[l]);
                }
                listResTemp.Add(_resultArray[i]);
                l++;
            }

            data3D = new Data3D(listArrX1Temp, listArrY1Temp, tempList);
            values = new Values(_n, data3D.arrayX.Length, data3D.arrayY.Length);
            findValues = new FindValues_xa_yb_c(_n, data3D, values);
            int k = 0;
            foreach (var item in listResTemp)
            {
                if (item <= 0)
                {
                    findValues.findMassiveYwithX(_oneDimArrayX1[k], _n);
                    resApprox.Add(values.findYMassiveWithX[0]);
                }
                else
                {
                    resApprox.Add(item);
                }
                k++;
            }

            data3D = new Data3D(_oneDimArrayX1, _oneDimArrayY1, _resultArray);
            values = new Values(_n, data3D.arrayX.Length, data3D.arrayY.Length);
            findValues = new FindValues_xa_yb_c(_n, data3D, values);
            _R = findValues.findRWithList(resultArray_WithoutNull, resApprox);
            return resApprox;
        }
      
        public void searchBestApproximationsResultArray(List<double> _resultArray, ref List<double> _resultArray_Approx, List<double> _oneDimArrayX1, List<double> _oneDimArrayY1, int _Draw)
        {
            int[] polynomes = new int[] { 2, 3, 4, 5 };
            //int[] polynomes = new int[] { 3 };
            sortedList<double>[] sortList = new sortedList<double>[polynomes.Length];
            List<double> RList = new List<double>();
            double tempR = 0;
            for (int i = 0; i < polynomes.Length; i++)
            {
                sortList[i].list = approximationResultArray(
                    _resultArray,
                    _oneDimArrayY1,
                    _oneDimArrayX1,
                    _Draw,
                    polynomes[i],
                     ref tempR
                );
                RList.Add(tempR);
                sortList[i].R = tempR;
                sortList[i].polynome = polynomes[i];
            }
            RList.Sort((a,b) => b.CompareTo(a));

            for (int i = 0; i < sortList.Length; i++)
            {        
                if(RList[0] == sortList[i].R)
                {
                   _resultArray_Approx = sortList[i].list;
                    Console.WriteLine($"R: {sortList[i].R}");
                    Console.WriteLine($"Степень полинома аппроксимации resultMassive: {sortList[i].polynome}");
                }
            }
        }

    }
}
