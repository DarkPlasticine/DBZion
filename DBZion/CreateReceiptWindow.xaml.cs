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

namespace DBZion
{
    /// <summary>
    /// Логика взаимодействия для CreateReceiptWindow.xaml
    /// </summary>
    public partial class CreateReceiptWindow : MetroWindow
    {
        public CreateReceiptWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.PopupAddCustom.IsOpen = true;
            
            
        }
    }
}
