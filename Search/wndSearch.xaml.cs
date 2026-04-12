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
        public wndSearch()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
<<<<<<< Updated upstream
            this.Hide();
            e.Cancel = true;
=======
            // Call the interface to send to the main window what invoice needs to be opened for viewing/editing
            // Make sure that an invoice has been selected and that it is valid
            if (SelectedInvoiceID == -1)
            {
                // Flag a selection is needed warning
                warningFlag.Content = "Please Select an Invoice First!";
            }
            else
            {
                //SelectedInvoiceID = 
                this.DialogResult = true;
                this.Close();
            }
>>>>>>> Stashed changes
        }
    }
}
