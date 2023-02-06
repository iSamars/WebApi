using UserApp.Helpers;
using UserApp.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using UserApp.Helpers.Extensions;
using System.Windows.Controls;

namespace UserApp
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        private Frame _frame;
        private bool _newObject = false;

        public EditPage(Frame frame, Product product)
        {
            _frame = frame;

            if (product == null)
            {
                _newObject = true;
                product = new Product();
            }

            DataContext = product;
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Product product = DataContext as Product;

            if(product.Name.IsNullOrEmpty() || product.Description.IsNullOrEmpty())
            {
                MessageBox.Show("Fields cannot be empty!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_newObject)
            {
                RequestHelper.Create(product);

            }
            else
            {
                RequestHelper.Update(product);

            }

            MessageBox.Show("Success!", "", MessageBoxButton.OK, MessageBoxImage.Information);

            _frame.Navigate(new MainPage(_frame));
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            _frame.GoBack();
        }
    }
}
