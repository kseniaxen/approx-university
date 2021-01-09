using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programm_3._0
{
    class DrawGraph
    {
        List<double> listZ;
        List<double> listX;
        List<double> listY;
        StringBuilder output;
        public string fileNameOutput { get; set; }
        public DrawGraph(List<double> _resArr, List<double> _xArr, List<double> _yArr, string _fileName)
        {
            listZ = new List<double>();
            listX = new List<double>();
            listY = new List<double>();
            output = new StringBuilder();
            this.listZ = _resArr;
            this.listX = _xArr;
            this.listY = _yArr;
            this.fileNameOutput = _fileName;
        }
        //public void drawGraphInScilab(double _R2_1, double _R2_2, double _R2_avg, params List<double>[] _array)
        //{
        //    output.Append("clc");output.Append(Environment.NewLine);
        //    output.Append("clear");output.Append(Environment.NewLine);
        //    //fillArrayX
        //    output.Append("//X начальное");
        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(listX, "X_start");

        //    //fillArrayY
        //    output.Append("//Y начальное");
        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(listY, "Y_start");

        //    //resultArray Z
        //    output.Append("//Z c нулями");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(listZ, "Z_null");

        //    //resultArray_WithoutNull Z
        //    output.Append("//Z заменили нули");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[0], "Z_withoutNull");

        //    //двумерные данные X twoDimArrayX1
        //    output.Append("//X двумерный");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[1], "X_twoDim");

        //    //двумерные данные Y twoDimArrayY1
        //    output.Append("//Y двумерный");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[2], "Y_twoDim");

        //    // преобразование X_twoDim и Y_twoDim в одномерные X_oneDim и Y_oneDim
        //    output.Append("//преобразование X_twoDim и Y_twoDim в одномерные X_oneDim и Y_oneDim");
        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(_array[3], "X_oneDim");

        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(_array[4], "Y_oneDim");

        //    output.Append("scf(1);");output.Append(Environment.NewLine);
        //    output.Append("clf(1);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z_null);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_null', 'X_oneDim', 'Y_oneDim', 'Z_null');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(1);"); output.Append(Environment.NewLine);
        //    output.Append("scf(2);"); output.Append(Environment.NewLine);
        //    output.Append("clf(2);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z_withoutNull);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_withoutNull','X_oneDim', 'Y_oneDim', 'Z_withoutNull');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(2);"); output.Append(Environment.NewLine);

        //    //Аппроксимация 1 listArrayResult
        //    output.Append("//Аппроксимация 1");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[5], "Z1_approx");

        //    output.Append("scf(3);"); output.Append(Environment.NewLine);
        //    output.Append("clf(3);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z1_approx);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z1_approx','X_oneDim', 'Y_oneDim', 'Z1_approx');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(3);"); output.Append(Environment.NewLine);

        //    //Аппроксимация 2 listArrayResult
        //    output.Append("//Аппроксимация 2");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[6], "Z2_approx");

        //    output.Append("scf(4);"); output.Append(Environment.NewLine);
        //    output.Append("clf(4);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim,Y_oneDim, Z2_approx);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z2_approx','X_oneDim', 'Y_oneDim', 'Z2_approx');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(4);"); output.Append(Environment.NewLine);

        //    // две аппроксимации в одном графическом окне
        //    output.Append(" // две аппроксимации в одном графическом окне");
        //    output.Append(Environment.NewLine);

        //    output.Append("scf(5);"); output.Append(Environment.NewLine);
        //    output.Append("clf(5);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z1_approx);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z2_approx);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z1_approx+Z2_approx','X_oneDim', 'Y_oneDim', 'Z');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(5);"); output.Append(Environment.NewLine);

        //    //вывод листа середних между вумя аппроксимациями
        //    output.Append("//Аппроксимация среднее");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[7], "Z_approx_avg");

        //    output.Append("scf(6);"); output.Append(Environment.NewLine);
        //    output.Append("clf(6);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z_approx_avg);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_approx_avg','X_oneDim', 'Y_oneDim', 'Z');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(6);"); output.Append(Environment.NewLine);

        //    //вывод R2
        //    output.Append("//R2_1 R2_2 R2_avg"); output.Append(Environment.NewLine);
        //    output.Append($"R2_1 = {_R2_1.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
        //    output.Append($"R2_2 = {_R2_2.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
        //    output.Append($"R2_avg = {_R2_avg.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
        //    output.Append("disp(R2_1,'R2_1= ',R2_2,'R2_2= ',R2_avg,'R2_avg= ');"); output.Append(Environment.NewLine);

        //    try
        //    {
        //        System.IO.File.WriteAllText(fileNameOutput, output.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"<--Ошибка записи в файл: {ex.Message}");
        //    }

        //}

        //public void drawGraphInScilab_Z_approx(double _R2_1, double _R2_2, double _R2_avg, params List<double>[] _array)
        //{
        //    output.Append("clc"); output.Append(Environment.NewLine);
        //    output.Append("clear"); output.Append(Environment.NewLine);
        //    //fillArrayX
        //    output.Append("//X начальное");
        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(listX, "X_start");

        //    //fillArrayY
        //    output.Append("//Y начальное");
        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(listY, "Y_start");

        //    //resultArray Z
        //    output.Append("//Z c нулями");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(listZ, "Z_null");

        //    //resultArray_Approx Z
        //    output.Append("//Z с аппроксимируемыми значения вместо нуля");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[0], "Z_approximation");

        //    //resultArray_WithoutNull Z
        //    output.Append("//Z заменили нули");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[1], "Z_withoutNull");

        //    //двумерные данные X twoDimArrayX1
        //    output.Append("//X двумерный");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[2], "X_twoDim");

        //    //двумерные данные Y twoDimArrayY1
        //    output.Append("//Y двумерный");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[3], "Y_twoDim");

        //    // преобразование X_twoDim и Y_twoDim в одномерные X_oneDim и Y_oneDim
        //    output.Append("//преобразование X_twoDim и Y_twoDim в одномерные X_oneDim и Y_oneDim");
        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(_array[4], "X_oneDim");

        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(_array[5], "Y_oneDim");

        //    output.Append("scf(1);"); output.Append(Environment.NewLine);
        //    output.Append("clf(1);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z_null);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_null', 'X_oneDim', 'Y_oneDim', 'Z_null');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(1);"); output.Append(Environment.NewLine);
        //    output.Append("scf(2);"); output.Append(Environment.NewLine);
        //    output.Append("clf(2);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z_withoutNull);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_withoutNull','X_oneDim', 'Y_oneDim', 'Z_withoutNull');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(2);"); output.Append(Environment.NewLine);

        //    output.Append("scf(3);"); output.Append(Environment.NewLine);
        //    output.Append("clf(3);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z_approximation);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_approximation','X_oneDim', 'Y_oneDim', 'Z_approximation');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(3);"); output.Append(Environment.NewLine);

        //    //Аппроксимация 1 listArrayResult
        //    output.Append("//Аппроксимация 1");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[6], "Z1_approx");

        //    output.Append("scf(4);"); output.Append(Environment.NewLine);
        //    output.Append("clf(4);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z1_approx);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z1_approx','X_oneDim', 'Y_oneDim', 'Z1_approx');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(4);"); output.Append(Environment.NewLine);

        //    //Аппроксимация 2 listArrayResult
        //    output.Append("//Аппроксимация 2");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[7], "Z2_approx");

        //    output.Append("scf(5);"); output.Append(Environment.NewLine);
        //    output.Append("clf(5);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim,Y_oneDim, Z2_approx);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z2_approx','X_oneDim', 'Y_oneDim', 'Z2_approx');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(5);"); output.Append(Environment.NewLine);

        //    // две аппроксимации в одном графическом окне
        //    output.Append(" // две аппроксимации в одном графическом окне");
        //    output.Append(Environment.NewLine);

        //    output.Append("scf(6);"); output.Append(Environment.NewLine);
        //    output.Append("clf(6);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z1_approx);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z2_approx);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z1_approx+Z2_approx','X_oneDim', 'Y_oneDim', 'Z');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(6);"); output.Append(Environment.NewLine);

        //    //вывод листа середних между вумя аппроксимациями
        //    output.Append("//Аппроксимация среднее");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[8], "Z_approx_avg");

        //    output.Append("scf(7);"); output.Append(Environment.NewLine);
        //    output.Append("clf(7);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z_approx_avg);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_approx_avg','X_oneDim', 'Y_oneDim', 'Z');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(7);"); output.Append(Environment.NewLine);

        //    //вывод R2
        //    output.Append("//R2_1 R2_2 R2_avg"); output.Append(Environment.NewLine);
        //    output.Append($"R2_1 = {_R2_1.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
        //    output.Append($"R2_2 = {_R2_2.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
        //    output.Append($"R2_avg = {_R2_avg.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
        //    output.Append("disp(R2_1,'R2_1= ',R2_2,'R2_2= ',R2_avg,'R2_avg= ');"); output.Append(Environment.NewLine);

        //    try
        //    {
        //        System.IO.File.WriteAllText(fileNameOutput, output.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"<--Ошибка записи в файл: {ex.Message}");
        //    }

        //}

        //public void drawGraphInScilab_Z_approx_INT(double _R2_1, double _R2_2, double _R2_avg, params List<double>[] _array)
        //{
        //    output.Append("clc"); output.Append(Environment.NewLine);
        //    output.Append("clear"); output.Append(Environment.NewLine);
        //    //fillArrayX
        //    output.Append("//X начальное");
        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(listX.Select(i => (int)i).ToList(), "X_start");

        //    //fillArrayY
        //    output.Append("//Y начальное");
        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(listY.Select(i => (int)i).ToList(), "Y_start");

        //    //resultArray Z
        //    output.Append("//Z c нулями");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(listZ.Select(i => (int)i).ToList(), "Z_null",listX.Count*2);


        //    //resultArray_Approx Z
        //    output.Append("//Z с аппроксимируемыми значения вместо нуля");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[0].Select(i => (int)i).ToList(), "Z_approximation");

        //    //resultArray_WithoutNull Z
        //    output.Append("//Z заменили нули");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[1].Select(i => (int)i).ToList(), "Z_withoutNull");

        //    //двумерные данные X twoDimArrayX1
        //    output.Append("//X двумерный");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[2].Select(i => (int)i).ToList(), "X_twoDim");

        //    //двумерные данные Y twoDimArrayY1
        //    output.Append("//Y двумерный");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[3].Select(i => (int)i).ToList(), "Y_twoDim");

        //    // преобразование X_twoDim и Y_twoDim в одномерные X_oneDim и Y_oneDim
        //    output.Append("//преобразование X_twoDim и Y_twoDim в одномерные X_oneDim и Y_oneDim");
        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(_array[4].Select(i => (int)i).ToList(), "X_oneDim");

        //    output.Append(Environment.NewLine);
        //    writeDataOneDim(_array[5].Select(i => (int)i).ToList(), "Y_oneDim");

        //    output.Append("scf(1);"); output.Append(Environment.NewLine);
        //    output.Append("clf(1);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_start, Y_start, Z_null);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_null', 'X_start', 'Y_start', 'Z_null');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(1);"); output.Append(Environment.NewLine);
        //    output.Append("scf(2);"); output.Append(Environment.NewLine);
        //    output.Append("clf(2);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z_withoutNull);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_withoutNull','X_oneDim', 'Y_oneDim', 'Z_withoutNull');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(2);"); output.Append(Environment.NewLine);

        //    output.Append("scf(3);"); output.Append(Environment.NewLine);
        //    output.Append("clf(3);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z_approximation);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_approximation','X_oneDim', 'Y_oneDim', 'Z_approximation');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(3);"); output.Append(Environment.NewLine);

        //    //Аппроксимация 1 listArrayResult
        //    output.Append("//Аппроксимация 1");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[6].Select(i => (int)i).ToList(), "Z1_approx");

        //    output.Append("scf(4);"); output.Append(Environment.NewLine);
        //    output.Append("clf(4);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z1_approx);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z1_approx','X_oneDim', 'Y_oneDim', 'Z1_approx');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(4);"); output.Append(Environment.NewLine);

        //    //Аппроксимация 2 listArrayResult
        //    output.Append("//Аппроксимация 2");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[7].Select(i => (int)i).ToList(), "Z2_approx");

        //    output.Append("scf(5);"); output.Append(Environment.NewLine);
        //    output.Append("clf(5);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim,Y_oneDim, Z2_approx);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z2_approx','X_oneDim', 'Y_oneDim', 'Z2_approx');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(5);"); output.Append(Environment.NewLine);

        //    // две аппроксимации в одном графическом окне
        //    output.Append(" // две аппроксимации в одном графическом окне");
        //    output.Append(Environment.NewLine);

        //    output.Append("scf(6);"); output.Append(Environment.NewLine);
        //    output.Append("clf(6);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z1_approx);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z2_approx);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z1_approx+Z2_approx','X_oneDim', 'Y_oneDim', 'Z');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(6);"); output.Append(Environment.NewLine);

        //    //вывод листа середних между вумя аппроксимациями
        //    output.Append("//Аппроксимация среднее");
        //    output.Append(Environment.NewLine);
        //    writeDataTwoDim(_array[8].Select(i => (int)i).ToList(), "Z_approx_avg");

        //    output.Append("scf(7);"); output.Append(Environment.NewLine);
        //    output.Append("clf(7);"); output.Append(Environment.NewLine);
        //    output.Append("mesh(X_oneDim, Y_oneDim, Z_approx_avg);"); output.Append(Environment.NewLine);
        //    output.Append("xtitle('Z_approx_avg','X_oneDim', 'Y_oneDim', 'Z');"); output.Append(Environment.NewLine);
        //    output.Append("xgrid(7);"); output.Append(Environment.NewLine);

        //    //вывод R2
        //    output.Append("//R2_1 R2_2 R2_avg"); output.Append(Environment.NewLine);
        //    output.Append($"R2_1 = {_R2_1.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
        //    output.Append($"R2_2 = {_R2_2.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
        //    output.Append($"R2_avg = {_R2_avg.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
        //    output.Append("disp(R2_1,'R2_1= ',R2_2,'R2_2= ',R2_avg,'R2_avg= ');"); output.Append(Environment.NewLine);

        //    try
        //    {
        //        System.IO.File.WriteAllText(fileNameOutput, output.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"<--Ошибка записи в файл: {ex.Message}");
        //    }

        //}

        public void drawGraphInScilab_Z_approx_WITH_START_PARAM_INT(double _R2_1, double _R2_2, double _R2_avg, int _draw_START, int _draw, params List<double>[] _array)
        {
            output.Append("clc"); output.Append(Environment.NewLine);
            output.Append("clear"); output.Append(Environment.NewLine);
            //fillArrayX
            output.Append("//X начальное");
            output.Append(Environment.NewLine);
            writeDataOneDim(listX.Select(i => (int)i).ToList(), "X_start");

            //fillArrayY
            output.Append("//Y начальное");
            output.Append(Environment.NewLine);
            writeDataOneDim(listY.Select(i => (int)i).ToList(), "Y_start");

            //resultArray Z
            output.Append("//Z начальное");
            output.Append(Environment.NewLine);
            writeDataTwoDim(listZ.Select(i => (int)i).ToList(), "Z_null_START", _draw_START);

            //двумерные данные X twoDimArrayX1
            output.Append("//X двумерный начальный");
            output.Append(Environment.NewLine);
            writeDataTwoDim(_array[0].Select(i => (int)i).ToList(), "X_twoDim_START", _draw_START);

            //двумерные данные Y twoDimArrayY1
            output.Append("//Y двумерный начальный");
            output.Append(Environment.NewLine);
            writeDataTwoDim(_array[1].Select(i => (int)i).ToList(), "Y_twoDim_START", _draw_START);

            // преобразование X_twoDim и Y_twoDim в одномерные X_oneDim и Y_oneDim
            output.Append("//преобразование X_twoDim_START и Y_twoDim_START в одномерные X_oneDim_START и Y_oneDim_START");
            output.Append(Environment.NewLine);
            writeDataOneDim(_array[2].Select(i => (int)i).ToList(), "X_oneDim_START");

            output.Append(Environment.NewLine);
            writeDataOneDim(_array[3].Select(i => (int)i).ToList(), "Y_oneDim_START");

            //resultArray Z
            output.Append("//Z с преобразованное с нулями");
            output.Append(Environment.NewLine);
            writeDataTwoDim(_array[4].Select(i => (int)i).ToList(), "Z_null", _draw);

            //resultArray_Approx Z
            output.Append("//Z с аппроксимируемыми значения вместо нуля");
            output.Append(Environment.NewLine);
            writeDataTwoDim(_array[5].Select(i => (int)i).ToList(), "Z_approximation", _draw);

            //resultArray_WithoutNull Z
            output.Append("//Z заменили нули");
            output.Append(Environment.NewLine);
            writeDataTwoDim(_array[6].Select(i => (int)i).ToList(), "Z_withoutNull", _draw);

            //двумерные данные X twoDimArrayX1
            output.Append("//X двумерный");
            output.Append(Environment.NewLine);
            writeDataTwoDim(_array[7].Select(i => (int)i).ToList(), "X_twoDim", _draw);

            //двумерные данные Y twoDimArrayY1
            output.Append("//Y двумерный");
            output.Append(Environment.NewLine);
            writeDataTwoDim(_array[8].Select(i => (int)i).ToList(), "Y_twoDim", _draw);

            // преобразование X_twoDim и Y_twoDim в одномерные X_oneDim и Y_oneDim
            output.Append("//преобразование X_twoDim и Y_twoDim в одномерные X_oneDim и Y_oneDim");
            output.Append(Environment.NewLine);
            writeDataOneDim(_array[9].Select(i => (int)i).ToList(), "X_oneDim");

            output.Append(Environment.NewLine);
            writeDataOneDim(_array[10].Select(i => (int)i).ToList(), "Y_oneDim");

            output.Append("scf(1);"); output.Append(Environment.NewLine);
            output.Append("clf(1);"); output.Append(Environment.NewLine);
            output.Append("mesh(X_oneDim_START, Y_oneDim_START, Z_null_START);"); output.Append(Environment.NewLine);
            output.Append("mesh(X_oneDim, Y_oneDim, Z_null);"); output.Append(Environment.NewLine);
            output.Append("e = gce(); e.color_mode = 7;"); output.Append(Environment.NewLine);
            output.Append("xtitle('Z_null', 'X_start', 'Y_start', 'Z_null');"); output.Append(Environment.NewLine);
            output.Append("xgrid(1);"); output.Append(Environment.NewLine);
            output.Append("scf(2);"); output.Append(Environment.NewLine);
            output.Append("clf(2);"); output.Append(Environment.NewLine);
            output.Append("mesh(X_oneDim, Y_oneDim, Z_withoutNull);"); output.Append(Environment.NewLine);
            output.Append("xtitle('Z_withoutNull','X_oneDim', 'Y_oneDim', 'Z_withoutNull');"); output.Append(Environment.NewLine);
            output.Append("xgrid(2);"); output.Append(Environment.NewLine);

            output.Append("scf(3);"); output.Append(Environment.NewLine);
            output.Append("clf(3);"); output.Append(Environment.NewLine);
            output.Append("mesh(X_oneDim, Y_oneDim, Z_approximation);"); output.Append(Environment.NewLine);
            output.Append("xtitle('Z_approximation','X_oneDim', 'Y_oneDim', 'Z_approximation');"); output.Append(Environment.NewLine);
            output.Append("xgrid(3);"); output.Append(Environment.NewLine);

            //Аппроксимация 1 listArrayResult
            output.Append("//Аппроксимация 1");
            output.Append(Environment.NewLine);
            writeDataTwoDim(_array[11].Select(i => (int)i).ToList(), "Z1_approx", _draw);

            output.Append("scf(4);"); output.Append(Environment.NewLine);
            output.Append("clf(4);"); output.Append(Environment.NewLine);
            output.Append("mesh(X_oneDim, Y_oneDim, Z1_approx);"); output.Append(Environment.NewLine);
            output.Append("xtitle('Z1_approx','X_oneDim', 'Y_oneDim', 'Z1_approx');"); output.Append(Environment.NewLine);
            output.Append("xgrid(4);"); output.Append(Environment.NewLine);

            //Аппроксимация 2 listArrayResult
            output.Append("//Аппроксимация 2");
            output.Append(Environment.NewLine);
            writeDataTwoDim(_array[12].Select(i => (int)i).ToList(), "Z2_approx", _draw);

            output.Append("scf(5);"); output.Append(Environment.NewLine);
            output.Append("clf(5);"); output.Append(Environment.NewLine);
            output.Append("mesh(X_oneDim,Y_oneDim, Z2_approx);"); output.Append(Environment.NewLine);
            output.Append("xtitle('Z2_approx','X_oneDim', 'Y_oneDim', 'Z2_approx');"); output.Append(Environment.NewLine);
            output.Append("xgrid(5);"); output.Append(Environment.NewLine);

            // две аппроксимации в одном графическом окне
            output.Append(" // две аппроксимации в одном графическом окне");
            output.Append(Environment.NewLine);

            output.Append("scf(6);"); output.Append(Environment.NewLine);
            output.Append("clf(6);"); output.Append(Environment.NewLine);
            output.Append("mesh(X_oneDim, Y_oneDim, Z1_approx);"); output.Append(Environment.NewLine);
            output.Append("mesh(X_oneDim, Y_oneDim, Z2_approx);"); output.Append(Environment.NewLine);
            output.Append("xtitle('Z1_approx+Z2_approx','X_oneDim', 'Y_oneDim', 'Z');"); output.Append(Environment.NewLine);
            output.Append("xgrid(6);"); output.Append(Environment.NewLine);

            //вывод листа середних между вумя аппроксимациями
            output.Append("//Аппроксимация среднее");
            output.Append(Environment.NewLine);
            writeDataTwoDim(_array[13].Select(i => (int)i).ToList(), "Z_approx_avg", _draw);

            output.Append("scf(7);"); output.Append(Environment.NewLine);
            output.Append("clf(7);"); output.Append(Environment.NewLine);
            output.Append("mesh(X_oneDim, Y_oneDim, Z_approx_avg);"); output.Append(Environment.NewLine);
            output.Append("xtitle('Z_approx_avg','X_oneDim', 'Y_oneDim', 'Z');"); output.Append(Environment.NewLine);
            output.Append("xgrid(7);"); output.Append(Environment.NewLine);

            //вывод R2
            output.Append("//R2_1 R2_2 R2_avg"); output.Append(Environment.NewLine);
            output.Append($"R2_1 = {_R2_1.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
            output.Append($"R2_2 = {_R2_2.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
            output.Append($"R2_avg = {_R2_avg.ToString().Replace(',', '.')}"); output.Append(Environment.NewLine);
            output.Append("disp(R2_1,'R2_1= ',R2_2,'R2_2= ',R2_avg,'R2_avg= ');"); output.Append(Environment.NewLine);

            try
            {
                System.IO.File.WriteAllText(fileNameOutput, output.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<--Ошибка записи в файл: {ex.Message}");
            }

        }

        private void writeDataOneDim<T>(List<T> _listArray, string _name)
        {
            output.Append($"{_name} = [");
            for (int i = 0; i < _listArray.Count; i++)
            {
                output.Append($"{_listArray[i].ToString().Replace(',', '.')} ");
            }
            output.Append("];");
            output.Append(Environment.NewLine);

        }

        private void writeDataTwoDim<T>(List<T> _listArray, string _name, int _draw)
        {
            //Y
            output.Append($"{_name} = [....");
            output.Append(Environment.NewLine);
            int l = 0;
            for (int i = 0; i < _listArray.Count; i++)
            {
                if (l == _draw / 2)
                {
                    output.Append($";....");
                    output.Append(Environment.NewLine);
                    l = 0;
                }
                output.Append($"{_listArray[i].ToString().Replace(',', '.')} ");
                l++;

            }
            output.Append("]");
            output.Append(Environment.NewLine);
        }
    }
}
