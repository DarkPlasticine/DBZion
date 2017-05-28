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
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using DBZion.BLL.Services;

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : MetroWindow
    {
        Main main = null;
        public SearchWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            main = this.Owner as Main;

            if (txbNumber.Text.Trim() != "")
            {
                var orders = main.service.GetOrders(p => p.ReceiptId == Convert.ToInt32(txbNumber.Text));
                DataGridOrders.ItemsSource = orders;
                txbNumber.Clear();
            }
            else if (txbPhone.Text[2] != '_' && txbPhone.Text[13] != '_')
            {
                var orders = main.service.GetOrders(p => p.User.PhoneNumber == txbPhone.Text);
                DataGridOrders.ItemsSource = orders;
                txbPhone.Clear();
            }
            else if (txbFullName.Text.Trim() != "")
            {
                var orders = main.service.GetOrders(p => p.User.FullName.ToLower().Contains(txbFullName.Text.ToLower()));
                DataGridOrders.ItemsSource = orders;
                txbFullName.Clear();
            }
            else if (dpStart.Text.Trim() != "" || dpStop.Text.Trim() != "")
            {
                if (dpStart.Text.Trim() != "" && dpStop.Text.Trim() != "")
                {
                    var orders = main.service.GetOrders(p => p.OrderDate.Date >= Convert.ToDateTime(dpStart.Text) && p.OrderDate.Date <= Convert.ToDateTime(dpStop.Text).Date);
                    DataGridOrders.ItemsSource = orders;
                }
                dpStart.Text = dpStop.Text = "";
                

            }
            else
            {
                var orders = main.service.GetOrders();
                DataGridOrders.ItemsSource = orders;
            }

        }

        private void DataGridOrders_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DataGridOrders_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        private void DataGridOrders_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        public int i = 0;
        private void txbFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (txbFullName.Text != null && txbFullName.Text.Length > 0)
            //    txbFullName.Text = txbFullName.Text.Substring(0, 1).ToUpperInvariant() + txbFullName.Text.Substring(1);

            i++;
            if (i == 1)
            {
                txbFullName.Text = txbFullName.Text[0].ToString().ToUpper();
            }
            else
            {
                txbFullName.SelectionStart = txbFullName.Text.Length;
            }
            if (txbFullName.Text.Length == 0)
            {
                i = 0;
            }

        }

        private void menuRestore_Click(object sender, RoutedEventArgs e)
        {
            var _order = (DBZion.DAL.Entities.Order)DataGridOrders.SelectedItem;
            var _fullName = _order.User.FullName.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            //main.service.UpdateOrder(_order.OrderId, _fullName[0], _fullName[1], _fullName[2], _order.User.PhoneNumber, _order.ReceiptId, _order.ReceiptType, _order.ServiceType, _order.Price, _order.Description, _order.Note, true, _order.IsReady, _order.Call, _order.Worker);
        }

        private void menuView_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
