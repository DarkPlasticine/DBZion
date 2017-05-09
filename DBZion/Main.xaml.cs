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
using System.Xml.Serialization;
using System.IO;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using DBZion.BLL.Services;

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        public string CurrentUser { get; set; }

        protected internal OrderService service = null;
        protected internal int selectedOrderID = 0;


        public Main(string user)
        {
            InitializeComponent();
            service = new OrderService(@"Data Source=Shpizpurgen-PC\SQLExpress;Initial Catalog=DataBaseZion;Integrated Security=True");
            this.Title = "ZION [" + user + "]";
            CurrentUser = user;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            selectedOrderID = 0;
            CreateReceiptWindow crw = new CreateReceiptWindow();
            crw.Owner = this;
            crw.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow();
            searchWindow.Show();
        }

        private void buttonArchive_Click(object sender, RoutedEventArgs e)
        {
            var archivWindow = new ArhiveWindow();
            archivWindow.Show();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshOrders();
        }

        private void DataGridOrders_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //int id = Convert.ToInt32(orderID);
            var k = (DataGrid)sender;
            selectedOrderID = ((DBZion.DAL.Entities.Order)k.SelectedItem).OrderId;

            CreateReceiptWindow crw = new CreateReceiptWindow();
            crw.Owner = this;
            crw.Show();
        }

        public void RefreshOrders()
        {
            var orders = service.GetOrders(p => p.IsActive == true);
            DataGridOrders.ItemsSource = orders;
        }

        private void DataGridOrders_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var _recepeit = (DAL.Entities.Order)e.Row.DataContext;

            if (_recepeit.IsReady == true)
            {
                e.Row.Background = (SolidColorBrush)Application.Current.Resources["AccentColorBrush3"];
            }

            if (_recepeit.Call == true)
            {
                e.Row.Background = new SolidColorBrush(Colors.CornflowerBlue);
            }
        }
    }
}
