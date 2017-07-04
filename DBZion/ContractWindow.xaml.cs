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
using System.IO;
using System.Xml.Linq;

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для ContractWindow.xaml
    /// </summary>
    public partial class ContractWindow : MetroWindow
    {
        private string contract;

        public ContractWindow()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            TextRange txtRange = null;
            GetPath();

            byte[] file = File.ReadAllBytes(contract);

            using (MemoryStream stream = new MemoryStream(file))
            {
                // create a TextRange around the entire document
                txtRange = new TextRange(document.ContentStart, document.ContentEnd);
                txtRange.Load(stream, DataFormats.Rtf);
            }

            if (expanderPassport.IsExpanded == true)
            {
                string str = String.Format("Паспорт серия {0} номер {1}, дата выдачи {2} {3}", txbPassportSerial.Text, txbPassportNumber.Text, dpPassportDate.Text, txbPassportIssued.Text);
                txtRange.Text.Replace("#001", str);
            }
        }

        private void expanderPassport_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (expanderPassport.IsExpanded == true)
                {
                    expanderDrivers.IsExpanded = false;
                    expanderMilitary.IsExpanded = false;
                    expanderOther.IsExpanded = false;
                }
            }
            catch { }
        }

        private void expanderDrivers_Expanded(object sender, RoutedEventArgs e)
        {
            if (expanderDrivers.IsExpanded)
            {
                expanderPassport.IsExpanded = false;
                expanderMilitary.IsExpanded = false;
                expanderOther.IsExpanded = false;
            }
        }

        private void expanderMilitary_Expanded(object sender, RoutedEventArgs e)
        {
            if (expanderMilitary.IsExpanded)
            {
                expanderDrivers.IsExpanded = false;
                expanderPassport.IsExpanded = false;
                expanderOther.IsExpanded = false;
            }
        }

        private void expanderOther_Expanded(object sender, RoutedEventArgs e)
        {
            if (expanderOther.IsExpanded)
            {
                expanderDrivers.IsExpanded = false;
                expanderMilitary.IsExpanded = false;
                expanderPassport.IsExpanded = false;
            }
        }

        private void GetPath()
        {
            XDocument settings = XDocument.Load(Environment.CurrentDirectory + @"/Data/Settings.xml");
            foreach (XElement el in settings.Root.Elements())
            {
                if (el.Name == "contract")
                    contract = el.Attribute("path").Value;
            }
        }

    }
}
