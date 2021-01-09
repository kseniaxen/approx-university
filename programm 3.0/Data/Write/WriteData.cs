using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace programm_3._0
{
    class WriteData
    {
        public string fileNameOutputMassiveY { get; set; }
        private Values value;
        public WriteData(string file, Values values)
        {
            fileNameOutputMassiveY = file;
            this.value = values;
            WriteMassiveX();
        }

        public void WriteMassiveX()
        {
            StringBuilder output = new StringBuilder();

            output.Append("T;");
            output.Append("p;");
            output.Append(Environment.NewLine);
            output.Append(";");
            for (int i = 0; i < value.counterY; i++)
            {
                output.Append($"{value.readArrayY[i].ToString("F" + 2)};");
            }
            output.Append(Environment.NewLine);


            for (int i = 0; i < value.column; i++)
            {
                output.Append($"{value.readArrayX[i].ToString("F" + 2)};");
                for (int j = 0; j < value.counterY; j++)
                {
                    output.Append($"{value.findMassive[j, i].ToString("F" + 3)};");
                }
                output.Append(Environment.NewLine);
            }

            try
            {
                System.IO.File.WriteAllText(fileNameOutputMassiveY, output.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"<--Ошибка записи в файл: {ex.Message}");
            }
        }

    }
}
