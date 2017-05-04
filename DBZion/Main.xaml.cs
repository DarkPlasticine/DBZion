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

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        public string CurrentUser { get; set; }
        public Main(string user)
        {
            InitializeComponent();
            this.Title = "ZION [" + user + "]";
            CurrentUser = user;
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateReceiptWindow();
            createWindow.Show();
            
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
    }
}
