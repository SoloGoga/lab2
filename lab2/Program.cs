using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            String input, input1, output;
            //чтение
            using (StreamReader sr = new StreamReader(@"D:\input.txt"))
            {
                input = sr.ReadLine();
                input1 = sr.ReadLine();
                sr.Close();
            }
            //добавление нулей до нужной разрядности
            input = Convert(input);
            input1 = Convert(input1);
            //сложение чисел
            output = Slojenie(input, input1);
            //проверка результата
            if (!ProverkaNaMinus(Convert(input), Convert(input1))) output = Perevernut(output);
            //запись в файл
            using (StreamWriter sw = new StreamWriter(@"D:\output.txt"))
            {
                sw.Write(output);
                sw.Close();
            }
        }
        
        //сложение
        static String Slojenie(String n1, String n2)
        {
            String output = "";
            bool translation = false;
            for (int i = 15; i >= 0; i--)
            {
                bool a = n1[i] == '1';
                bool b = n2[i] == '1';

                String temp = "0";

                if (translation)
                {
                    translation = a || b;
                    if (a == b) temp = "1";
                }
                else
                {
                    translation = a && b;
                    if (a != b) temp = "1";
                }
                output = output.Insert(0, temp);
            }
            return output;
        }

        //перевод в 16 разряд.
        static String Convert(String input)
        {
            int d = input[0] == '-' ? 1 : 0;
            if (input.Length > 16 + d) input = input.Substring(0, 16 + d);
            else
            {
                String zeros = "";
                while ((zeros + input).Length < 16 + d)
                {
                    zeros += "0";
                }
                input = input.Insert(d, zeros);
            }
            return input;
        }

        //метод инвертирования
        static String Perevernut(String input)
        {
            String output = "";
            for (int i = 0; i < input.Length; i++)
            {
                output += input[i] == '0' ? '1' : '0';
            }
            return output;
        }

        //проверка отрицательности
        static bool ProverkaNaMinus(String n1, String n2)
        {
            bool m1 = n1[0] == '-';
            bool m2 = n2[0] == '-';
            if (!(m1 || m2)) return true;
            if (m1 && m2) return false;
            else
            {
                if (m1) n1 = n1.Substring(1, 16);
                if (m2) n2 = n2.Substring(1, 16);

                for (int i = 0; i < 16; i++)
                {
                    if (n1[i] == n2[i]) continue;
                    else if (n1[i] == '1' && !m1) return true;
                    else if (n2[i] == '1' && !m2) return true;
                }
                return false;
            }
        }
    }
}
