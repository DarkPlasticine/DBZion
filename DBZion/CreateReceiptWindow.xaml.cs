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
    /// Логика взаимодействия для CreateReceiptWindow.xaml
    /// </summary>
    public partial class CreateReceiptWindow : MetroWindow
    {
        private static DBZion.DAL.Entities.Order order = null;
        private bool isUpdating = false;
        Main main = null;

        public CreateReceiptWindow()
        {
            InitializeComponent();
           
            
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //this.PopupAddCustom.IsOpen = true;


        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            main = this.Owner as Main;
            if (main != null)
            {
                List<string> receiptTypes = new List<string> { "Услуги", "Тест Б/У", "Картридж" };
                List<string> serviceTypes = main.service.GetFieldValues(p => p.ServiceType);
                isUpdating = false;

                cbReceiptType.ItemsSource = receiptTypes;

                

                if (main.selectedOrderID != 0)
                {
                    isUpdating = true;
                    order = main.service.GetOrders(p => p.OrderId == main.selectedOrderID).FirstOrDefault();
                    if (order != null)
                    {
                        gridOrder.DataContext = order;
                    }
                }
                else
                {
                    txbWorker.Content = main.CurrentUser;
                    txbReceiptId.Text = "???";
                    cbFullName.ItemsSource = main.service.GetUsers().Select(p => p.FullName).ToList();
                }
            }

        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            string[] sb = cbFullName.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (isUpdating == false)
            {
                DateTime dateTime = DateTime.Now;
                main.service.AddOrder(sb[0], sb[1], sb[2], txbPhone.Text, cbReceiptType.Text, txbServiceType.Text, Convert.ToInt32(txbPrice.Text), DateTime.Now, txbDescription.Text, txbNote.Text, true, (bool)chkDone.IsChecked, (bool)chkCall.IsChecked, txbWorker.Content.ToString());

            }
            else
            {
                main.service.UpdateOrder(main.selectedOrderID, sb[0], sb[1], sb[2], txbPhone.Text, cbReceiptType.Text, txbServiceType.Text, Convert.ToInt32(txbPrice.Text), txbDescription.Text, txbNote.Text, true, (bool)chkDone.IsChecked, (bool)chkCall.IsChecked, txbWorker.Content.ToString());
            }
            main.RefreshOrders();
            this.Close();
        }

        private void OnComboboxTextChanged(object sender, RoutedEventArgs e)
        {

            var user = main.service.FindUser(p => p.FullName == cbFullName.Text);
            if (user != null)
                txbPhone.Text = user.PhoneNumber;
            ////cbFullName.IsDropDownOpen = true;

            //var tb = (TextBox)e.OriginalSource;
            //tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            //cbFullName.ItemsSource = main.service.GetUsers().Select(p => p.FullName).ToList();
            //CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(cbFullName.ItemsSource);
            //cv.Filter = s => ((string)s).IndexOf(cbFullName.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private void cbFullName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
                var user = main.service.FindUser(p => p.FullName == cbFullName.Text);
                if (user != null)
                    txbPhone.Text = user.PhoneNumber;
        }

        private void cbFullName_KeyDown(object sender, KeyEventArgs e)
        {
            //if ((e.Key == Key.CapsLock) ||
            //    (e.Key == Key.LeftCtrl) ||
            //    (e.Key == Key.LeftShift) ||
            //    (e.Key == Key.RightShift) ||
            //    (e.Key == Key.LeftAlt) ||
            //    (e.Key == Key.Left) ||
            //    (e.Key == Key.Right) ||
            //    (e.Key == Key.Up) ||
            //    (e.Key == Key.Down) ||
            //    (e.Key == Key.PageUp) ||
            //    (e.Key == Key.PageDown) ||
            //    (e.Key == Key.Home) ||
            //    (e.Key == Key.End))
            //{
            //    return;
            //}

            //cbFullName.IsDropDownOpen = true;
        }

        private void cbFullName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbFullName.IsDropDownOpen = true;
            var users = main.service.GetUsers(p => p.FullName.Contains(e.Text));
            if (users.Count != 0)
                cbFullName.ItemsSource = users;
        }
    }
}
