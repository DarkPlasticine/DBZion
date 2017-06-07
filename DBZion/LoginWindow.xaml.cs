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
using System.Xml.Serialization;
using System.IO;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System.Reflection;

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            labelVersion.Content = Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (UserComboBox.Text != "")
            {
                var mainWindow = new Main(UserComboBox.Text);
                mainWindow.Show();
                this.Close();
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            ImportBD importBDWindow = new ImportBD();
            importBDWindow.Owner = this;
            importBDWindow.Show();
        }
    }
}
