using CS_3280_Group_9_Project.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    using static clsSearchLogic;
    using static System.Runtime.InteropServices.JavaScript.JSType;

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
        /// A list for populating with data from the database
        /// </summary>
        public List<(int ID, string Date, int TotalCost)> invoiceList;

        /// <summary>
        /// Constructor for the seach window, calls the SQL class to get all invoices and display them
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();

            try
            {
                warningFlag.Content = "";
                clsSearchLogic sL = new clsSearchLogic();
                invoiceList = sL.GetInvoiceList();
                invoiceListDisplay.ItemsSource = invoiceList;

                // Initialize the search limiting combo boxes
                invoiceList = invoiceList.OrderBy(x => x.TotalCost).ToList();

                invoice_number_combo_box.Items.Add("All");
                invoice_date_combo_box.Items.Add("All");
                total_cost_combo_box.Items.Add("All");

                invoice_number_combo_box.SelectedValue = "All";
                invoice_date_combo_box.SelectedValue = "All";
                total_cost_combo_box.SelectedValue = "All";

                for (int i = 0; i < invoiceList.Count; i++)
                {
                    invoice_number_combo_box.Items.Add(invoiceList[i].ID.ToString());
                    invoice_date_combo_box.Items.Add(invoiceList[i].Date);
                    total_cost_combo_box.Items.Add(invoiceList[i].TotalCost.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
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

        /// <summary>
        /// A function which limits the list display to the combobox search limiting selected values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void limitSearch(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (invoice_number_combo_box.SelectedItem == null || invoice_date_combo_box.SelectedItem == null || total_cost_combo_box.SelectedItem == null)
                {
                    return;
                }

                int id;
                int total_cost;
                string date;

                if ((string)invoice_number_combo_box.SelectedItem == "All")
                {
                    id = -1;
                }
                else
                {
                    id = int.Parse(invoice_number_combo_box.SelectedItem.ToString());
                }
                if ((string)invoice_date_combo_box.SelectedItem == "All")
                {
                    date = null;
                }
                else
                {
                    date = (string)invoice_date_combo_box.SelectedItem;
                }
                if ((string)total_cost_combo_box.SelectedItem == "All")
                {
                    total_cost = -1;
                }
                else
                {
                    total_cost = int.Parse(total_cost_combo_box.SelectedItem.ToString());
                }

                invoiceListDisplay.ItemsSource = invoiceList
                    .Where(x => (id == -1 || x.ID == id)
                             && (date == null || x.Date == date)
                             && (total_cost == -1 || x.TotalCost == total_cost))
                    .ToList();
            }
            catch (Exception ex)
{
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Sets the internal value to the value selected from the list box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invoiceListDisplaySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = ((int ID, string Date, int TotalCost))invoiceListDisplay.SelectedItem;
                SelectedInvoiceID = item.ID;
            }
            catch (Exception ex)
{
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Sets filters to all essentially 'clearing' them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearFiltersClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                invoice_date_combo_box.SelectedItem = "All";
                invoice_number_combo_box.SelectedItem = "All";
                total_cost_combo_box.SelectedItem = "All";
            }
            catch (Exception ex)
{
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
    }
}
