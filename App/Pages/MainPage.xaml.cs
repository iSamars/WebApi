using App.Helpers;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace App
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Frame _frame;
        public MainPage(Frame frame)
        {
            _frame = frame;
            InitializeComponent();
            GetProducts();
        }

        public async void GetProducts()
        {
            DGridProducts.ItemsSource = await RequestHelper.Get();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new EditPage(_frame, (sender as Button).DataContext as Product));
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new EditPage(_frame, null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            List<Product> products = DGridProducts.SelectedItems.Cast<Product>().ToList();

            if(MessageBox.Show("Are you sure?", "Attention!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                foreach(Product product in products)
                {
                    RequestHelper.Delete(product.Id);
                }

                MessageBox.Show("Success!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                GetProducts();
            }
        }
    }
}
