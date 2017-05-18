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

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для ContractWindow.xaml
    /// </summary>
    public partial class ContractWindow : MetroWindow
    {
        public ContractWindow()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
