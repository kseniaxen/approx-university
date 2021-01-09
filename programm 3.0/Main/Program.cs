using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programm_3._0
{
    class Program
    {
        static void Main(string[] args)
        { 
            var mainPath = Path.Combine(Directory.GetCurrentDirectory(), "2020 — описательная статистика.xlsx");
            Microsoft.Office.Interop.Excel.Application ObjExcel;
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;

            ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            ObjWorkBook = ObjExcel.Workbooks.Open(mainPath);
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets["VN1_общ_граф"];

            //Run_xa_yb_c run = new Run_xa_yb_c(mainPath, ObjExcel, ObjWorkBook, ObjWorkSheet,-2.0,2.0,1);
            Run run = new Run(mainPath, ObjExcel, ObjWorkBook, ObjWorkSheet, -2.0, 2.0, 1);
            ObjWorkBook.Close(false);
            ObjExcel.Quit();
            ObjWorkSheet = null;
            ObjWorkBook = null;
            ObjExcel = null;
            GC.Collect();

            string polynome;
            Console.WriteLine("Степень полинома:");
            polynome = Console.ReadLine();

            run.RunApproximation_resultArray_Approx(polynome);
            run.RunSwapApproximation(polynome, false);
            run.writeDataZ_resultArray_Approx("graphic end.sce");

            Console.WriteLine("End");
            Console.ReadLine();
        }

    }
}
