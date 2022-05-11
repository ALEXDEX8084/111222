using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace ListClass.Classes
{
    /// <summary>
    /// Помощник для соединения
    /// </summary>
    class ConnectHelper
    {
        public static List<Goods> goods = new List<Goods>();
        public static string filename;
        public static void ReadListFromFile(string filename)
        {
            try
            {
                StreamReader streamReader = new StreamReader(filename, Encoding.UTF8);
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                string[] items = line.Split(';');
                Goods good = new Goods()
                {
                    Name = items[0].Trim(),
                    Mname = items[1].Trim(),
                    Price = double.Parse(items[2].Trim()),
                    Count = int.Parse(items[3].Trim())
                };
                goods.Add(good);
            }
        }
            catch (Exception ex)
            {
                MessageBox.Show("Неверный формат данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public static void SaveListToFile(string filename)
        {
            StreamWriter streamWriter = new StreamWriter(filename, false, Encoding.UTF8);
            foreach (Goods ph in goods)
            {
                streamWriter.WriteLine($"{ph.Name};{ph.Mname};{ph.Price};{ph.Count}");
            }
            streamWriter.Close();
        }

    }
}
