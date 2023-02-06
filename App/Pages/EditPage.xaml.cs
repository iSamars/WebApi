using App.Helpers;
using App.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace App
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
            if (_newObject)
            {
                RequestHelper.Create(DataContext as Product);

            }
            else
            {
                RequestHelper.Update(DataContext as Product);

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
