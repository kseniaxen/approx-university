using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programm_3._0
{
    class Run_xa_yb_c
    {
        public string mainPath { get; set; }
        public Microsoft.Office.Interop.Excel.Application ObjExcel;
        public Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
        public Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

        public double startPhi { get; set; }
        public double endPhi { get; set; }
        public double hPhi { get; set; }

        public string numColX { get; set; }
        public string numColY { get; set; }
        public string numColZ { get; set; }
        public string numColF { get; set; }

        private ReadTransformData readTransformData;
        private Data3D data3D;
        private Values values;
        private FindValues_xa_yb_c findValues;


        private Data3D data3DInverted;
        private Values valuesInverted;
        private FindValues_xa_yb_c findValuesInverted;

        public List<double> fillArrayX;
        public List<double> fillArrayY;

        public List<double> twoDimArrayX1;
        public List<double> twoDimArrayY1;

        public List<double> oneDimArrayX1;
        public List<double> oneDimArrayY1;

        public List<double> resultArray;
        public int Draw { get; set; }
        //public List<double> resultArray_WithoutNull;
        //public List<double> resultArray_Approx;
        //public List<double> invertedArray_WithoutNull; //Перевернутый массив
        //public List<double> invertedArray_Approx;
        public Run_xa_yb_c(string _mainPath, Microsoft.Office.Interop.Excel.Application _ObjExcel, Microsoft.Office.Interop.Excel.Workbook _ObjWorkBook, Microsoft.Office.Interop.Excel.Worksheet _ObjWorkSheet, double _startPhi, double _endPhi, double _hPhi, string numColX, string numColY, string numColZ, string numColF)
        {
            this.mainPath = _mainPath;
            this.ObjExcel = _ObjExcel;
            this.ObjWorkBook = _ObjWorkBook;
            this.ObjWorkSheet = _ObjWorkSheet;

            this.startPhi = _startPhi;
            this.endPhi = _endPhi;
            this.hPhi = _hPhi;

            fillArrayX = new List<double>();
            fillArrayY = new List<double>();
            twoDimArrayX1 = new List<double>();
            twoDimArrayY1 = new List<double>();
            oneDimArrayX1 = new List<double>();
            oneDimArrayY1 = new List<double>();

            resultArray = new List<double>();
            //resultArray_WithoutNull = new List<double>();
            //resultArray_Approx = new List<double>();
            Pre_GlobalInfo();
            GlobalInfo();
        }
        private void Pre_GlobalInfo()
        {
            readTransformData = new ReadTransformData(mainPath, ObjExcel, ObjWorkBook, ObjWorkSheet, startPhi, endPhi, hPhi, numColX, numColY, numColZ, numColF, false);
            fillArrayX = readTransformData.fillArrayX;
            fillArrayY = readTransformData.fillArrayY;
            twoDimArrayX1 = readTransformData.twoDimArrayX1;
            twoDimArrayY1 = readTransformData.twoDimArrayY1;
            oneDimArrayX1 = readTransformData.oneDimArrayX1;
            oneDimArrayY1 = readTransformData.oneDimArrayY1;
            resultArray = readTransformData.resultArray;
            Draw = readTransformData.Draw;
        }

        private void GlobalInfo()
        {
            readTransformData = new ReadTransformData(mainPath, ObjExcel, ObjWorkBook, ObjWorkSheet, startPhi, endPhi, hPhi, numColX, numColY, numColZ, numColF, false);
        }

        public void RunApproximation_resultArray_WithoutNull(string _polynome)
        {
            data3D = new Data3D(readTransformData.oneDimArrayX1, readTransformData.oneDimArrayY1, readTransformData.resultArray_WithoutNull);
            values = new Values(Convert.ToInt32(_polynome), data3D.arrayX.Length, data3D.arrayY.Length);
            findValues = new FindValues_xa_yb_c(Convert.ToInt32(_polynome), data3D, values);
            values.assignListArray(values.listArrayResult);
        }

        public void RunApproximation_resultArray_Approx(string _polynome)
        {
            data3D = new Data3D(readTransformData.oneDimArrayX1, readTransformData.oneDimArrayY1, readTransformData.resultArray_Approx);
            // Console.WriteLine($"{data3D.arrayX.Length} {data3D.arrayY.Length}");
            values = new Values(Convert.ToInt32(_polynome), data3D.arrayX.Length, data3D.arrayY.Length);
            findValues = new FindValues_xa_yb_c(Convert.ToInt32(_polynome), data3D, values);
            values.assignListArray(values.listArrayResult);
        }

        public void RunSwapApproximation(string _polynome, bool key)
        {
            if (key)
            {
                data3DInverted = new Data3D(readTransformData.oneDimArrayY1, readTransformData.oneDimArrayX1, readTransformData.invertedArray_WithoutNull);
                valuesInverted = new Values(Convert.ToInt32(_polynome), data3DInverted.arrayY.Length, data3DInverted.arrayX.Length);
                findValuesInverted = new FindValues_xa_yb_c(Convert.ToInt32(_polynome), data3DInverted, valuesInverted);
                valuesInverted.assignListArray(valuesInverted.listArrayResult);
            }
            else
            {
                data3DInverted = new Data3D(readTransformData.oneDimArrayY1, readTransformData.oneDimArrayX1, readTransformData.invertedArray_Approx);
                valuesInverted = new Values(Convert.ToInt32(_polynome), data3DInverted.arrayX.Length, data3DInverted.arrayY.Length);
                findValuesInverted = new FindValues_xa_yb_c(Convert.ToInt32(_polynome), data3DInverted, valuesInverted);
                valuesInverted.assignListArray(valuesInverted.listArrayResult);
            }

        }

        //public void writeDataZ_resultArray_WithoutNull(string _nameFile)
        //{
        //    //Запись данных в файл scilab
        //    DrawGraph graph = new DrawGraph(readTransformData.resultArray, readTransformData.fillArrayX, readTransformData.fillArrayY, _nameFile, readTransformData.Draw);

        //    List<double> arrSwap = new List<double>();
        //    readTransformData.invertedDataZ(valuesInverted.listArrayResult, arrSwap, readTransformData.oneDimArrayX1.Count, readTransformData.oneDimArrayY1.Count, readTransformData.Draw);
        //    graph.drawGraphInScilab(
        //            findValues.findRWithList(readTransformData.resultArray_WithoutNull, values.listArrayResult),
        //            findValues.findRWithList(readTransformData.resultArray_WithoutNull, arrSwap),
        //            findValues.findRWithList(readTransformData.resultArray_WithoutNull, values.avgArrayResultBetweenTwoList(values.listArrayResult, arrSwap)),
        //            readTransformData.resultArray_WithoutNull,
        //            readTransformData.twoDimArrayX1,
        //            readTransformData.twoDimArrayY1,
        //            readTransformData.oneDimArrayX1,
        //            readTransformData.oneDimArrayY1,
        //            values.listArrayResult,
        //            arrSwap,
        //            values.avgArrayResultBetweenTwoList(values.listArrayResult, arrSwap)
        //            );
        //}

        //public void writeDataZ_resultArray_Approx_Swap(string _nameFile)
        //{
        //    //Запись данных в файл scilab
        //    //List<double> arrSwap_ResultArray = new List<double>();
        //    //readTransformData.invertedDataZ(readTransformData.resultArray, arrSwap_ResultArray, readTransformData.fillArrayX.Count, readTransformData.fillArrayY.Count, (readTransformData.fillArrayY.Count*2));
        //    DrawGraph graph = new DrawGraph(readTransformData.resultArray, readTransformData.fillArrayX, readTransformData.fillArrayY, _nameFile, (readTransformData.oneDimArrayX1.Count * 2));

        //    List<double> arrSwap_ListArrRes = new List<double>();
        //    List<double> arrSwap_ResultArray = new List<double>();
        //    List<double> arrSwap_ResultArray_Approx = new List<double>();
        //    List<double> arrSwap_ResultArray_WithoutNull = new List<double>();
        //    List<double> arrSwap_ResultArray_TDX = new List<double>();
        //    List<double> arrSwap_ResultArray_TDY = new List<double>();

        //    //Console.WriteLine();
        //    //Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!");
        //    //  Console.WriteLine($"X {readTransformData.oneDimArrayX1.Count} Y {readTransformData.oneDimArrayY1.Count}");

        //    readTransformData.invertedDataZ(readTransformData.resultArray_NEW, arrSwap_ResultArray, readTransformData.oneDimArrayY1.Count, readTransformData.oneDimArrayX1.Count, (readTransformData.oneDimArrayX1.Count * 2));
        //    readTransformData.invertedDataZ(valuesInverted.listArrayResult, arrSwap_ListArrRes, readTransformData.oneDimArrayY1.Count, readTransformData.oneDimArrayX1.Count, (readTransformData.oneDimArrayX1.Count * 2));
        //    readTransformData.invertedDataZ(readTransformData.resultArray_Approx, arrSwap_ResultArray_Approx, readTransformData.oneDimArrayY1.Count, readTransformData.oneDimArrayX1.Count, (readTransformData.oneDimArrayX1.Count * 2));
        //    readTransformData.invertedDataZ(readTransformData.resultArray_WithoutNull, arrSwap_ResultArray_WithoutNull, readTransformData.oneDimArrayY1.Count, readTransformData.oneDimArrayX1.Count, (readTransformData.oneDimArrayX1.Count * 2));
        //    readTransformData.invertedDataZ(readTransformData.twoDimArrayX1, arrSwap_ResultArray_TDX, readTransformData.oneDimArrayY1.Count, readTransformData.oneDimArrayX1.Count, (readTransformData.oneDimArrayX1.Count * 2));
        //    readTransformData.invertedDataZ(readTransformData.twoDimArrayY1, arrSwap_ResultArray_TDY, readTransformData.oneDimArrayY1.Count, readTransformData.oneDimArrayX1.Count, (readTransformData.oneDimArrayX1.Count * 2));

        //    //Console.WriteLine($"RA {arrSwap_ResultArray.Count} {arrSwap_ListArrRes.Count}");
        //    graph.drawGraphInScilab_Z_approx_INT(
        //            findValues.findRWithList(arrSwap_ResultArray_Approx, values.listArrayResult),
        //            findValues.findRWithList(arrSwap_ResultArray_Approx, arrSwap_ListArrRes),
        //            findValues.findRWithList(arrSwap_ResultArray_Approx, values.avgArrayResultBetweenTwoList(values.listArrayResult, arrSwap_ListArrRes)),
        //            arrSwap_ResultArray,
        //            arrSwap_ResultArray_Approx,
        //            arrSwap_ResultArray_WithoutNull,
        //            arrSwap_ResultArray_TDX,
        //            arrSwap_ResultArray_TDY,
        //            readTransformData.oneDimArrayX1,
        //            readTransformData.oneDimArrayY1,
        //            arrSwap_ListArrRes,
        //            values.listArrayResult,
        //            values.avgArrayResultBetweenTwoList(values.listArrayResult, arrSwap_ListArrRes)
        //            );
        //}

        public void writeDataZ_resultArray_Approx(string _nameFile)
        {
            //DrawGraph graph = new DrawGraph(readTransformData.resultArray, readTransformData.fillArrayX, readTransformData.fillArrayY, _nameFile, readTransformData.Draw);
            //Console.WriteLine($"X {readTransformData.oneDimArrayX1.Count} Y {readTransformData.oneDimArrayY1.Count}");
            DrawGraph graph = new DrawGraph(resultArray, fillArrayX, fillArrayY, _nameFile);
            List<double> arrSwap = new List<double>();
            readTransformData.invertedDataZ(valuesInverted.listArrayResult, arrSwap, readTransformData.oneDimArrayX1.Count, readTransformData.oneDimArrayY1.Count, (readTransformData.oneDimArrayY1.Count * 2));
            graph.drawGraphInScilab_Z_approx_WITH_START_PARAM_INT(
                    findValues.findRWithList(readTransformData.resultArray_Approx, values.listArrayResult),
                    findValues.findRWithList(readTransformData.resultArray_Approx, valuesInverted.listArrayResult),
                    findValues.findRWithList(readTransformData.resultArray_Approx, values.avgArrayResultBetweenTwoList(values.listArrayResult, valuesInverted.listArrayResult)),
                    Draw,
                    (readTransformData.oneDimArrayX1.Count * 2),
                    twoDimArrayX1,
                    twoDimArrayY1,
                    oneDimArrayX1,
                    oneDimArrayY1,
                    readTransformData.resultArray,
                    readTransformData.resultArray_Approx,
                    readTransformData.resultArray_WithoutNull,
                    readTransformData.twoDimArrayX1,
                    readTransformData.twoDimArrayY1,
                    readTransformData.oneDimArrayX1,
                    readTransformData.oneDimArrayY1,
                    values.listArrayResult,
                    valuesInverted.listArrayResult,
                    values.avgArrayResultBetweenTwoList(values.listArrayResult, valuesInverted.listArrayResult)
                    );
        }
    }
}

