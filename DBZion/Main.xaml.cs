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
            this.MouseRightButtonUp += new MouseButtonEventHandler(DataGridOrders_MouseRightButtonDown);
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
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Owner = this;
            searchWindow.Show();
        }

        private void buttonArchive_Click(object sender, RoutedEventArgs e)
        {
            ArhiveWindow archivWindow = new ArhiveWindow();
            archivWindow.Owner = this;
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

        private void txbSearch_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string filter = txbSearch.Text;

            System.ComponentModel.ICollectionView cv = CollectionViewSource.GetDefaultView(DataGridOrders.ItemsSource);

            if (filter == "") cv.Filter = null;
            else
            {
                cv.Filter = o => 
                {
                    DAL.Entities.Order p = o as DAL.Entities.Order;
                    return p.ReceiptId.ToString().ToUpper().Contains(filter.ToUpper()) || 
                           p.User.FullName.ToUpper().Contains(filter.ToUpper()) ||
                           p.User.PhoneNumber.ToUpper().Contains(filter.ToUpper()) ||
                           p.Worker.ToUpper().Contains(filter.ToUpper());
                };
            }
        }

        private void menuEdit_Click(object sender, RoutedEventArgs e)
        {
            var k = (DataGrid)e.OriginalSource;
            selectedOrderID = ((DBZion.DAL.Entities.Order)k.SelectedItem).OrderId;

            CreateReceiptWindow crw = new CreateReceiptWindow();
            crw.Owner = this;
            crw.Show();
        }

        private void DataGridOrders_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void DataGridOrders_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is DataGridCell))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null) return;

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;
                cell.Focus();

                while ((dep != null) && !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }
                DataGridRow row = dep as DataGridRow;
                DataGridOrders.SelectedItem = row.DataContext;
            }
        }
      
        private void menuEdit_Click_1(object sender, RoutedEventArgs e)
        {
            selectedOrderID = ((DBZion.DAL.Entities.Order)DataGridOrders.SelectedItem).OrderId;

            CreateReceiptWindow crw = new CreateReceiptWindow();
            crw.Owner = this;
            crw.Show();
        }

        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            selectedOrderID = ((DAL.Entities.Order)DataGridOrders.SelectedItem).OrderId;
            service.UpdateOrder(selectedOrderID, false);
            RefreshOrders();
        }
    }
}
