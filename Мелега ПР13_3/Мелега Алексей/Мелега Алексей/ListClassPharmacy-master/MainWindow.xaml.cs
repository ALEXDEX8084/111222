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
using ListClass.Classes;
using System.IO;
using Microsoft.Win32;

namespace ListClass
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
       // List<Pharmacy> pharmacies = new List<Pharmacy>();
        public MainWindow()
        {
            InitializeComponent();
            BtnPrint.IsEnabled = false;
            
            TxtSearch.IsEnabled = false;
            BtnAdd.IsEnabled = false;
            RbUp.IsEnabled = false;
            RbDown.IsEnabled = false;
            CmbFiltr.IsEnabled = false;
            

        }
        /// <summary>
        /// Вывод списка в таблицу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.goods.ToList();
            DtgListPreparate.SelectedIndex = -1;
           
        }
        /// <summary>
        /// сортировка по алфавиту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.goods.OrderBy(x=>x.Name).ToList();
        }
        /// <summary>
        /// сортировка в обратном порядке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.goods.OrderByDescending(x => x.Name).ToList();
        }
        /// <summary>
        /// поиск по названию товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.goods.Where(x => 
                x.Name.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
        }

       /// <summary>
       /// фильтр по названию магазина
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (CmbFiltr.SelectedIndex == 0)
            {
                DtgListPreparate.ItemsSource = ConnectHelper.goods.Where(x =>
                x.Mname.Contains("Чертвёрочка")).ToList();
            }
            else
                if (CmbFiltr.SelectedIndex == 1)
            {
                DtgListPreparate.ItemsSource = ConnectHelper.goods.Where(x =>
                x.Mname.Contains("Диски")).ToList();
            }
            else
                if (CmbFiltr.SelectedIndex == 2)
            {
                DtgListPreparate.ItemsSource = ConnectHelper.goods.Where(x =>
                x.Mname.Contains("Копеечка")).ToList();
            }
            else
            {
                DtgListPreparate.ItemsSource = ConnectHelper.goods.Where(x =>
                x.Mname.Contains("Шашан")).ToList();
            }
        }
        /// <summary>
        /// Кнопка открывающая окно добавления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddPreparate windowAdd = new WindowAddPreparate();
            windowAdd.ShowDialog();
        }

       
        /// <summary>
        /// Кнопка очистки полей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = null;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowAddPreparate windowAdd = new WindowAddPreparate((sender as Button).DataContext as Goods);
            windowAdd.ShowDialog();
            ConnectHelper.SaveListToFile(ConnectHelper.filename);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var resMessage = MessageBox.Show("Удалить запись?", "Подтверждение",
               MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resMessage == MessageBoxResult.Yes)
            {
                int ind = DtgListPreparate.SelectedIndex;
                ConnectHelper.goods.RemoveAt(ind);
                DtgListPreparate.ItemsSource = ConnectHelper.goods.ToList();
                ConnectHelper.SaveListToFile(ConnectHelper.filename);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if ((bool)saveFileDialog.ShowDialog())
                {
                    string file = saveFileDialog.FileName;
                    ConnectHelper.SaveListToFile(file);
                    BtnPrint.IsEnabled = true;

                    TxtSearch.IsEnabled = true;
                    BtnAdd.IsEnabled = true;
                    RbUp.IsEnabled = true;
                    RbDown.IsEnabled = true;
                    CmbFiltr.IsEnabled = true;
                    RbDown.IsEnabled = true;
                }
            }
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            //загрузка данных из файла

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                ConnectHelper.filename = openFileDialog.FileName;
                ConnectHelper.ReadListFromFile(ConnectHelper.filename);
                BtnPrint.IsEnabled = true;
                
                TxtSearch.IsEnabled = true;
                BtnAdd.IsEnabled = true;
                RbUp.IsEnabled = true;
                RbDown.IsEnabled = true;
                CmbFiltr.IsEnabled = true;
                RbDown.IsEnabled = true;

            }
            else
                return;
            DtgListPreparate.ItemsSource = ConnectHelper.goods.ToList();

        }
    }
}
