using CS_3280_Group_9_Project.Search;
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

namespace CS_3280_Group_9_Project.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        public wndMain()
        {
            InitializeComponent();

            // For testing purposes only, remove before merging
            wndSearch window = new wndSearch();
            if (window.ShowDialog() == true) // if the user force closes the child window showDialog will return false
            {
                // this retrieves the value stored in the search window object's parameter named SelectedInvoiceID
                int invoiceID = window.SelectedInvoiceID;
            }
        }
    }
}
