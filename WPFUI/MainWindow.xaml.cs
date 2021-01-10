using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Excel;
using System.IO;
using programm_3._0;

namespace WPFUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        Microsoft.Office.Interop.Excel.Application ObjExcel;
        Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
        public string excelFile { get; set; }
        public string polynome { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                pathFile.Text = openFileDialog.FileName;
                excelFile = openFileDialog.FileName;
            }
        }

        private void Calcul_Click(object sender, RoutedEventArgs e)
        {
            //sheets "VN1_общ_граф"
            ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            ObjWorkBook = ObjExcel.Workbooks.Open(excelFile);
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[sheet.Text];

            programm_3._0.Run run = new programm_3._0.Run(excelFile, ObjExcel, ObjWorkBook, ObjWorkSheet, Convert.ToDouble(start.Text), Convert.ToDouble(end.Text), Convert.ToDouble(step.Text), colX.Text, colY.Text, colZ.Text, colF.Text );
            ObjWorkBook.Close(false);
            ObjExcel.Quit();
            ObjWorkSheet = null;
            ObjWorkBook = null;
            ObjExcel = null;
            GC.Collect();

            if (PolynomeComboBox.SelectedItem != null)
            {
                switch (PolynomeComboBox.Text)
                {
                    case "y=ax+b":
                        polynome = "2";
                        break;
                    case "y=ax2+bx+c":
                        polynome = "3";
                        break;
                    case "y=ax3+bx2+cx+d":
                        polynome = "4";
                        break;
                    case "y=ax4+bx3+cx2+dx+e":
                        polynome = "5";
                        break;
                    default:
                        break;
                }
                run.RunApproximation_resultArray_Approx(polynome);
                run.RunSwapApproximation(polynome, false);
                run.writeDataZ_resultArray_Approx("graphic end.sce");
            }
                
        }
    }
}
