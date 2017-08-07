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
using DBZion.DAL.Entities;

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для ArhiveWindow.xaml
    /// </summary>
    public partial class ArhiveWindow : MetroWindow
    {
        private OrderService service = null;
        private int UnionUserID;
        public ArhiveWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            service = new OrderService("DbConnection");
            var users = service.GetUsers().OrderBy(p => p.Surname).ThenBy(p => p.FirstName).ThenBy(p => p.MiddleName);
            userList.ItemsSource = users;
            service.Dispose();
        }

        private void userList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                service = new OrderService("DbConnection");

                User o = (User)userList.SelectedItem;

                dataGridOrders.ItemsSource = service.GetUserOrders(o.UserID);
                userInfo.DataContext = o;
                service.Dispose();
            }
            catch
            { }
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string filter = txbSearch.Text;

            System.ComponentModel.ICollectionView cv = CollectionViewSource.GetDefaultView(userList.ItemsSource);

            if (filter == "") cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    User u = o as User;
                    return u.FullName.ToString().ToUpper().Contains(filter.ToUpper());
                };
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txbSearch.Clear();
        }

        private void userList_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListBoxItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null) return;

            if (dep is ListBoxItem)
            {
                ListBoxItem cell = dep as ListBoxItem;
                cell.Focus();

                while ((dep != null) && !(dep is ListBoxItem))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }
                ListBoxItem row = dep as ListBoxItem;
                userList.SelectedItem = row.DataContext;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            UnionUserID = ((User)userList.SelectedItem).UserID;
            userList.SelectionMode = SelectionMode.Multiple;
            btnUnion.Visibility = Visibility.Visible;
            separetorUnion.Visibility = Visibility.Visible;
            btnCancelUnion.Visibility = Visibility.Visible;
            txbSearch.Text = ((User)userList.SelectedItem).Surname;
            //MessageBox.Show(((User)userList.SelectedItem).UserID.ToString());
        }

        private void btnCancelUnion_Click(object sender, RoutedEventArgs e)
        {
            userList.SelectionMode = SelectionMode.Single;
            btnUnion.Visibility = Visibility.Collapsed;
            separetorUnion.Visibility = Visibility.Collapsed;
            txbSearch.Clear();
            btnCancelUnion.Visibility = Visibility.Collapsed;
        }

        private void btnUnion_Click(object sender, RoutedEventArgs e)
        {
            OrderService service = new OrderService("DbConnection");
            var users = userList.SelectedItems;

            foreach (User u in users)
            {
                if (u.UserID != UnionUserID)
                {
                    User user = service.FindUser(u.UserID);
                    List<Order> orders = user.Orders.ToList();
                    for (int i = 0; i < orders.Count; i++)
                    {
                        Order o = orders[i];
                        service.UpdateOrder(orders[i].OrderId, UnionUserID);
                        orders.Remove(o);
                        i = -1;
                    }
                }
            }
            int k = service.RemoveInactiveUsers();
            userList.SelectionMode = SelectionMode.Single;
            userList.SelectedIndex = -1;
            userList.ItemsSource = service.GetUsers().OrderBy(p => p.Surname).ThenBy(p => p.FirstName).ThenBy(p => p.MiddleName); 
            service.Dispose();

            btnUnion.Visibility = Visibility.Collapsed;
            separetorUnion.Visibility = Visibility.Collapsed;
            txbSearch.Clear();
            btnCancelUnion.Visibility = Visibility.Collapsed;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            OrderService service = new OrderService("DbConnection");

            int ID = ((User)userList.SelectedItem).UserID;

            string[] userName = txbFullName.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            User user = new User()
            {
                Surname = userName[0],
                FirstName = userName[1],
                MiddleName = userName[2],
                PhoneNumber = txbPhone.Text
            };

            service.UpdateUser(ID, user);

            userList.ItemsSource = service.GetUsers().OrderBy(p => p.Surname).ThenBy(p => p.FirstName).ThenBy(p => p.MiddleName);

            service.Dispose();
        }
    }
}
