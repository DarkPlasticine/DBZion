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
    /// Логика взаимодействия для CreateReceiptWindow.xaml
    /// </summary>
    public partial class CreateReceiptWindow : MetroWindow
    {
        private static Order order = null;
        private bool isUpdating = false;
        Main main = null;
        private OrderService service = null;

        public CreateReceiptWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //var sampleMessageDialog = new SampleMessageDialog
            //{
            //    Message = { Text = ((ButtonBase)sender).Content.ToString() }
            //};
            //this.PopupAddCustom.IsOpen = true;
            //DialogHost.Show(sender, closingEventHandler);
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            main = this.Owner as Main ;
            if (main != null)
            {
                service = new OrderService("DbConnection");
                List<string> receiptTypes = new List<string> { "Услуги", "Тест Б/У", "Картридж" };
                List<string> serviceTypes = service.GetFieldValues(p => p.ServiceType);
                isUpdating = false;

                cbReceiptType.ItemsSource = receiptTypes;

                if (main.selectedOrderID != 0)
                {
                    isUpdating = true;
                    order = service.FindOrder(main.selectedOrderID); //GetOrders(p => p.OrderId == main.selectedOrderID).FirstOrDefault();
                    if (order != null)
                    {
                        gridOrder.DataContext = order;
                        btnAgreement.IsEnabled = true;
                    }
                }
                else
                {
                    cbFullName.ItemsSource = service.GetUsers().Select(p => p.FullName).ToList();
                    txbDate.Text = DateTime.Now.ToString();
                    txbReceiptId.Text = GetReceiptID(service.GetOrders(k => k.IsActive == true).ToDictionary(p => p.ReceiptId)).ToString();
                    txbWorker.Text = main.CurrentUser;
                }

                service.Dispose();
            }
        }

        private int GetReceiptID (Dictionary<int, DAL.Entities.Order> dic)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            bool flag = true;
            int i = 0;
            while (flag)
            {
                i = rnd.Next(1, 100);
                if (!dic.ContainsKey(i)) flag = false;
            }

            return i;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            string[] sb = cbFullName.Text.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                Order _order = new Order()
                {
                    Call = (bool)chkCall.IsChecked,
                    Description = txbDescription.Text,
                    IsActive = true,
                    IsReady = (bool)chkDone.IsChecked,
                    Note = txbNote.Text,
                    OrderDate = Convert.ToDateTime(txbDate.Text),
                    Price = Convert.ToInt32(txbPrice.Text),
                    ReceiptId = Convert.ToInt32(txbReceiptId.Text),
                    ReceiptType = cbReceiptType.Text,
                    ServiceType = txbServiceType.Text,
                    Worker = txbWorker.Text,
                    User = new User
                    {
                        Surname = sb[0],
                        FirstName = sb[1],
                        MiddleName = sb[2],
                        PhoneNumber = txbPhone.Text
                    }

                };

                service = new OrderService("DbConnection");
                if (isUpdating == false)
                {
                    //Добавление квитанции
                    service.AddOrder(_order);
                }
                else
                {
                    // Обновление уже созданной квитанции
                    service.UpdateOrder(main.selectedOrderID, _order);
                }
                main.RefreshOrders();
                service.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnComboboxTextChanged(object sender, RoutedEventArgs e)
        {
            service = new OrderService("DbConnection");
            var user = service.FindUser(p => p.FullName == cbFullName.Text);
            if (user != null)
                txbPhone.Text = user.PhoneNumber;

            service.Dispose();
            ////cbFullName.IsDropDownOpen = true;

            //var tb = (TextBox)e.OriginalSource;
            //tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            //cbFullName.ItemsSource = main.service.GetUsers().Select(p => p.FullName).ToList();
            //CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(cbFullName.ItemsSource);
            //cv.Filter = s => ((string)s).IndexOf(cbFullName.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private void cbFullName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            service = new OrderService("DbConnection");
            var user = service.FindUser(p => p.FullName == cbFullName.Text);
            if (user != null)
                txbPhone.Text = user.PhoneNumber;
            service.Dispose();
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
            service = new OrderService("DbConnection");
            cbFullName.IsDropDownOpen = true;
            var users = service.GetUsers(p => p.FullName.Contains(e.Text));
            if (users.Count != 0)
                cbFullName.ItemsSource = users;
            service.Dispose();
        }

        private void btnAgreement_Click(object sender, RoutedEventArgs e)
        {
            var agreementWindow = new ContractWindow();

            agreementWindow.Show();

        }

        private void cbReceiptType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbReceiptType.SelectedIndex != -1)
            {
                txbServiceType.Text = cbReceiptType.Items[cbReceiptType.SelectedIndex].ToString();
            }
        }
    }
}
