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
    /// Логика взаимодействия для ArhiveWindow.xaml
    /// </summary>
    public partial class ArhiveWindow : MetroWindow
    {
        private OrderService service = null;
        public ArhiveWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            service = new OrderService("DbConnection");
            var users = service.GetUsers();
            //List<string> userFullNames = users.Select(p => p.FullName).ToList();
            userList.ItemsSource = users;
            service.Dispose();
        }

        private void userList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            service = new OrderService("DbConnection");
            
            var orders = service.GetUserOrders(userId);
            service.Dispose();
        }
    }
}
