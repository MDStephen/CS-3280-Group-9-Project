using CS_3280_Group_9_Project.Main;
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

namespace CS_3280_Group_9_Project.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {

        /// <summary>
        /// An integer storing the ID of the selected invoice which will be returned to main (parent window)
        /// </summary>
        public int SelectedInvoiceID = -1;

        /// <summary>
        /// Constructor for the seach window
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            warningFlag.Content = "";
        }

        /// <summary>
        /// On click for the select invoice button, the main window will open passing through the selected invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clkSelectInvoice(object sender, RoutedEventArgs e)
        {
            // Call the interface to send to the main window what invoice needs to be opened for viewing/editing
            // Make sure that an invoice has been selected and that it is valid
            if (SelectedInvoiceID == -1)
            {
                // Flag a selection is needed warning
                warningFlag.Content = "Please Select an Invoice First!";
            }
            else
            {
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
