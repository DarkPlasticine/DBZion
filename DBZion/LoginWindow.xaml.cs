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
using System.Xml;
using System.Xml.Linq;
using DBZion.BLL.Services;
using System.Configuration;

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        private string conStr = null;
        private string status = null;
        Main mainWindow;
        //OrderService service = null;

        public LoginWindow()
        {
            XDocument settings = XDocument.Load(Environment.CurrentDirectory + @"/Data/Settings.xml");

            InitializeComponent();
            foreach (XElement el in settings.Root.Elements())
            {
                if (el.Name == "connectionString")
                {
                    conStr = el.Attribute("name").Value;
                    SetConnectionString();
                }
                if (el.Name == "status")
                    status = el.Attribute("name").Value;
                if (el.Name == "programVersion")
                {
                    labelVersion.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    el.Attribute("version").Value = labelVersion.Content.ToString();
                }
            }

            settings.Save(Environment.CurrentDirectory + @"/Data/Settings.xml");

            if (GetStatus(status) == true)
                statusServer.Foreground = Brushes.Green;

            //labelVersion.Content = Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (UserComboBox.Text != "")
            {
                try
                {
                    //service = new OrderService(conStr);
                    //service.GetOrders();
                
                    mainWindow = new Main(UserComboBox.Text);
                    mainWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            ImportBD importBDWindow = new ImportBD();
            importBDWindow.Owner = this;
            importBDWindow.Show();
        }

        private bool GetStatus(string Status)
        {
            try
            {
                File.ReadAllText(status);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        private void SetConnectionString()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("connectionStrings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value.Equals("DbConnection"))
                        {
                            node.Attributes[1].Value = conStr;
                        }
                    }
                }
            }

            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
