using System;
using System.Collections.Generic;
using System.IO;
namespace Test
{
    class Program
    {


        static void Main(string[] args)
        {
            employeer employeer = new employeer();
            List<employeer> list = new List<employeer>();
            char[] separator = { ',' };
            string[] result = new string[3];
            string[] metadate = new string[3];

            string path = "D:\\acme_worksheet.csv";   //УКАЖИТЕ АДРЕС ВХОДНОГО ФАЙЛА

            //Считывание построчно входного файла и запись в массив list
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                int counter = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (counter == 0)
                    {
                        metadate = line.Split(separator);
                        counter++;
                    }
                    else
                    {
                        result = line.Split(separator);
                        employeer.employee_name = result[0];
                        employeer.date = DateTime.Parse(result[1]);
                        employeer.work_hours = result[2];
                        list.Add(employeer);
                    }
                }
            }


            //Узнаём все возможные даты и записываем их в массив date
            List<DateTime> date = new List<DateTime>();

            DateTime tim = list[0].date;
            date.Add(tim);
            for (int i = 1; i < list.Count; i++)
            {
                if (tim != list[i].date)
                {
                    date.Add(list[i].date);
                    tim = list[i].date;
                }
                else
                {
                    continue;
                }
            }
            //Добавление даты




            //Записываем имена всех рабочих без повторений в массив employee_names

            List<string> employee_names = new List<string>();
            employee_names.Add(list[0].employee_name);
            for (int i = 1; i < list.Count; i++)
            {
                for (int j = 0; j < employee_names.Count; j++)
                {
                    if (j == employee_names.Count - 1 && list[i].employee_name != employee_names[j])
                    {
                        employee_names.Add(list[i].employee_name);
                    }
                    else if (list[i].employee_name == employee_names[j])
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }


            }




            //В строку res добавляем заголовок и даты 
            DateTime curdate = list[0].date;
            string res = "NAME/DATE" + ',' + list[0].date.ToString("d") + ',';
            for (int i = 0; i < list.Count; i++)
            {
                if (curdate != list[i].date)
                {
                    res += list[i].date.ToString("d") + ',';
                    curdate = list[i].date;
                }
                else
                {
                    continue;
                }
            }
            res += '\n';

            //временный обьект структуры employeer нужен для того что-бы после произведения вычислений 
            //заменять в массиве list обьекты на tmp(что-бы не попадать 2 раза на одного и того же рабочего)
            employeer tmp = new employeer();
            tmp.employee_name = "0";
            tmp.date = DateTime.Parse("25.02.2020");
            tmp.work_hours = 0.ToString();


            bool eps = false;


            for (int j = 0; j < employee_names.Count; j++)//Цыкл для прохода по рабочим
            {
                res += '\n' + employee_names[j] + ',';
                for (int i = 0; i < date.Count; i++)//Цикл для прохода по датам
                {

                    for (int k = 0; k < list.Count; k++)//Цикл для прохода по массиву list
                    {

                        if (list[k].date == date[i] && list[k].employee_name == employee_names[j])//Если такой рабочий существует и был на работе в определённый день,
                                                                                                  //То записываем его количество часов
                        {
                            eps = true;
                            res += list[k].work_hours + ',';
                            list[k] = tmp;
                            break;
                        }
                    }
                    if (eps == false)
                    {
                        res += "0" + ",";
                    }
                    else
                    {
                        eps = false;
                    }
                }

            }

            StreamWriter streamWriter = new StreamWriter(@"D:\\fff.csv"); //Выводим информацию в выходной файл(не забудьте указать адрес)
            streamWriter.Write(res);
            streamWriter.Close();
            Console.WriteLine("Done! The file is saved.");
            Console.ReadLine();
        }

    }
    public struct employeer
    {
        public string employee_name;
        public DateTime date;
        public string work_hours;
    }
}
