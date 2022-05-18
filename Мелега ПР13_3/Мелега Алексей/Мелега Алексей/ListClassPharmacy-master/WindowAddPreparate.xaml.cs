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
using System.Windows.Shapes;
using ListClass.Classes;

namespace ListClass
{
    /// <summary>
    /// Логика взаимодействия для WindowAddPreparate.xaml
    /// </summary>
    public partial class WindowAddPreparate : Window
    {
        int mode;
        public WindowAddPreparate()
        {
            InitializeComponent();
            mode = 0;
        }
        public WindowAddPreparate(Goods goods)
        {
            InitializeComponent();
            TxbName.Text = goods.Name;
            TxbCount.Text = goods.Mname;
            TxbPrice.Text = goods.Price.ToString();
            TxbMonth.Text = goods.Count.ToString();
            mode = 1;
            BtnAddPreparate.Content = "Сохранить";
        }
        /// <summary>
        /// Привязка полей для добавления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddPreparate_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(TxbCount.Text) < 0)
            {
                MessageBox.Show("Количество не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxbCount.Clear();
                TxbCount.Focus();
                return;
            }
            if (double.Parse(TxbPrice.Text) < 0)
            {
                MessageBox.Show("Цена не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxbPrice.Clear();
                TxbPrice.Focus();
                return;
            }
            if (int.Parse(TxbMonth.Text) < 0)
            {
                MessageBox.Show("Месяц не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxbMonth.Clear();
                TxbMonth.Focus();
                return;
            }
            if (mode == 0)
            {
                try
                {
                    Goods good = new Goods()
                    {
                        Name = TxbName.Text,
                        Mname = TxbCount.Text,
                        Price = double.Parse(TxbPrice.Text),
                        Count = int.Parse(TxbMonth.Text)
                    };
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Проверьте входные данные: {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    for (int i = 0; i < ConnectHelper.goods.Count; i++)
                    {
                        if (ConnectHelper.goods[i].Name == TxbName.Text)
                        {
                            ConnectHelper.goods[i].Count = int.Parse(TxbMonth.Text);
                            ConnectHelper.goods[i].Price = double.Parse(TxbPrice.Text);
                            ConnectHelper.goods[i].Mname = TxbCount.Text;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Проверьте входные данные: {ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            ConnectHelper.SaveListToFile(ConnectHelper.filename);
            this.Close();
        }
    }
}
