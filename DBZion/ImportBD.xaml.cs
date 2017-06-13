﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;
using DBZion.BLL.Services;
using DBZion.DAL.Entities;
using MahApps.Metro.Controls;

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для ImportBD.xaml
    /// </summary>
    public partial class ImportBD : MetroWindow
    {
        protected internal OrderService service = null;
        List<OldOrder> oldOrders = new List<OldOrder>();
        int current = 0;
        int full = 0;
        string count = @"Импортированно: {0}/{1}";
        public ImportBD()
        {
            InitializeComponent();
            service = new OrderService(@"Data Source=Shpizpurgen-PC\SQLExpress;Initial Catalog=DataBaseZion;Integrated Security=True");
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            // List<OldOrder> oldOrders = new List<OldOrder>();
            //oldOrders.Clear();
            //try
            //{
            //    XmlSerializer formatter = new XmlSerializer(typeof(List<OldOrder>));

            //    using (FileStream fs = new FileStream(txbPath.Text, FileMode.OpenOrCreate, FileAccess.Read))
            //    {
            //        oldOrders = (List<OldOrder>)formatter.Deserialize(fs);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            foreach (OldOrder o in oldOrders)
            {
                string[] u = o.FullName.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (o.PhoneNumber == null)
                    o.PhoneNumber = "";

                if (o.ServiceType == "")
                    o.ServiceType = o.ReceiptType;

                Order _order = new Order()
                {
                    Call = o.Call,
                    Description = o.Description,
                    IsActive = o.IsActive,
                    IsReady = o.IsReady,
                    Note = o.Note,
                    OrderDate = o.OrderDate,
                    Price = o.Price,
                    ReceiptId = o.ReceiptID,
                    ReceiptType = o.ReceiptType,
                    ServiceType = o.ServiceType,
                    User = new User()
                    {
                        PhoneNumber = o.PhoneNumber
                    },
                    Worker = o.Worker
                };
               
                switch (u.Count())
                {
                    case 1: _order.User.Surname = u[0]; _order.User.FirstName = "Аноним"; _order.User.MiddleName = "Анонимович"; break;
                    case 2: _order.User.FirstName = u[1]; _order.User.Surname = u[0]; _order.User.MiddleName = "Анонимович"; break;
                    case 3: _order.User.FirstName = u[1]; _order.User.Surname = u[0]; _order.User.MiddleName = u[2]; break;
                    default: _order.User.FirstName = "Аноним"; _order.User.Surname = "Анонимов"; _order.User.MiddleName = "Анонимович"; break;
                }

                service.AddOrder(_order);
            }

            MessageBox.Show("OK");
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                txbPath.Text = dialog.FileName;
            }

            oldOrders.Clear();
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<OldOrder>));

                using (FileStream fs = new FileStream(txbPath.Text, FileMode.Open))
                {
                    oldOrders = (List<OldOrder>)formatter.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            pbProress.Maximum = full = oldOrders.Count;
            lblProgress.Content = String.Format(count, current, full);
        }

        [Serializable]
        public class OldOrder
        {
            public string FullName { get; set; }
            public int ReceiptID { get; set; }
            public string ReceiptType { get; set; }
            public string ServiceType { get; set; }
            public int Price { get; set; }
            public DateTime OrderDate { get; set; }
            public string Description { get; set; }
            public string Note { get; set; }
            public bool IsActive { get; set; }
            public bool IsReady { get; set; }
            public bool Call { get; set; }
            public string Worker { get; set; }
            public string PhoneNumber { get; set; }

            public OldOrder()
            {

            }

            public OldOrder(int id, string rType, string sType, int price, string fullname, DateTime orderdate, string description, string note, bool active, bool ready, bool call, string worker, string phone)
            {
                FullName = fullname;
                ReceiptID = id;
                ReceiptType = rType;
                ServiceType = sType;
                Price = price;
                OrderDate = orderdate;
                Description = description;
                Note = note;
                IsActive = active;
                IsReady = ready;
                Call = call;
                Worker = worker;
                PhoneNumber = phone;
            }
        }
    }
}
