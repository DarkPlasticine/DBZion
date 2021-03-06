﻿using System;
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
using System.Collections.ObjectModel;
using DBZion.DAL.Entities;

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : MetroWindow
    {
        Main main = null;
        private OrderService service = null;

        public SearchWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            main = this.Owner as Main;

            service = new OrderService("DbConnection");

            if (txbNumber.Text.Trim() != "")
            {
                var order = service.GetOrders(p => p.ReceiptId == Convert.ToInt32(txbNumber.Text));
                DataGridOrders.ItemsSource = order;
                txbNumber.Clear();
            }
            else if (txbPhone.Text[2] != '_' && txbPhone.Text[13] != '_')
            {
                var orders = service.GetOrders(p => p.User.PhoneNumber == txbPhone.Text);
                DataGridOrders.ItemsSource = orders;
                txbPhone.Clear();
            }
            else if (txbFullName.Text.Trim() != "")
            {
                var orders = service.GetOrders(p => p.User.FullName.ToLower().Contains(txbFullName.Text.ToLower()))
                    .OrderBy(s => s.User.Surname).ThenBy(f => f.User.FirstName).ThenBy(m => m.User.MiddleName);
                DataGridOrders.ItemsSource = orders;
                txbFullName.Clear();
            }
            else if (dpStart.Text.Trim() != "" || dpStop.Text.Trim() != "")
            {

                ObservableCollection<Order> orders = null;
                
                if (dpStart.Text.Trim() != "")
                {
                    orders = service.GetOrders(p => p.OrderDate.Date == Convert.ToDateTime(dpStart.Text));
                }
                else if (dpStop.Text.Trim() != "")
                {
                    orders = service.GetOrders(p => p.OrderDate.Date <= Convert.ToDateTime(dpStop.Text).Date);
                }
                else
                {
                    orders = service.GetOrders(p => p.OrderDate.Date >= Convert.ToDateTime(dpStart.Text) && p.OrderDate.Date <= Convert.ToDateTime(dpStop.Text).Date);
                }

                DataGridOrders.ItemsSource = orders;
                dpStart.Text = dpStop.Text = "";
            }
            else
            {
                var orders = service.GetOrders();
                DataGridOrders.ItemsSource = orders;
                MessageBox.Show(orders.Count.ToString());
            }

            service.Dispose();
        }

        private void DataGridOrders_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var k = (DataGrid)sender;
            selectedOrderID = ((DAL.Entities.Order)k.SelectedItem).OrderId;

            CreateReceiptWindow crw = new CreateReceiptWindow(selectedOrderID);
            crw.Owner = this;
            crw.Show();
        }

        private void DataGridOrders_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //var _recepeit = (DAL.Entities.Order)e.Row.DataContext;

            //if (_recepeit.IsReady == true)
            //{
            //    e.Row.Background = (SolidColorBrush)Application.Current.Resources["AccentColorBrush3"];
            //}

            //if (_recepeit.Call == true)
            //{
            //    e.Row.Background = new SolidColorBrush(Colors.CornflowerBlue);
            //}
        }

        private void DataGridOrders_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        public int i = 0;
        private int selectedOrderID;

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
            service = new OrderService("DbConnection");
            var _order = ((DBZion.DAL.Entities.Order)DataGridOrders.SelectedItem).OrderId;

            service.UpdateOrder(_order, true);
            main.RefreshOrders();
            //main.service.UpdateOrder(_order.OrderId, _fullName[0], _fullName[1], _fullName[2], _order.User.PhoneNumber, _order.ReceiptId, _order.ReceiptType, _order.ServiceType, _order.Price, _order.Description, _order.Note, true, _order.IsReady, _order.Call, _order.Worker);
            service.Dispose();
        }

        private void menuView_Click(object sender, RoutedEventArgs e)
        {
            int selectedOrderID = ((Order)DataGridOrders.SelectedItem).OrderId;

            main.selectedOrderID = selectedOrderID;

            CreateReceiptWindow crw = new CreateReceiptWindow(selectedOrderID);
            crw.Owner = main;
            crw.Show();
        }

        private void MetroWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnSearch_Click(sender, e);
        }
    }
}
