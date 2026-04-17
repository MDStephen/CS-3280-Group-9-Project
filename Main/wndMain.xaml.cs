using CS_3280_Group_9_Project.Items;
using CS_3280_Group_9_Project.Search;
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
using System.ComponentModel;

namespace CS_3280_Group_9_Project.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml. This part of the project was written by Noelle.
    /// </summary>
    public partial class wndMain : Window
    {

        wndSearch wndMainSearch;
        wndItems wndMainItems;
        clsMainLogic clsUIMainLogic;
        bool bCreateMode;
        bool bEditMode;

        /// <summary>
        /// Current invoice ID.
        /// </summary>
        static int invoiceID;

        /// <summary>
        /// Business logic for the window
        /// </summary>
        clsMainLogic UIMainLogic;



        /// <summary>
        /// Used to determine the behavior of the save button.
        /// </summary>
        public enum InvoiceMode
        {
            None, //not editing or creating an invoice
            Edit, //editing an invoice that exists in the database
            NewInvoice //new invoice
        }

        /// <summary>
        /// instance of InvoiceMode
        /// </summary>
        InvoiceMode eCurrentMode;

        public wndMain()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                

                menuWindows.Visibility = Visibility.Visible;
                menuWindows.IsEnabled = true;
                

                invoiceID = 0;
                //wndMainSearch = new wndSearch();

                //wndMainItems = new wndItems();

                eCurrentMode = InvoiceMode.None;
                btnEdit.IsEnabled = false;
                btnSave.IsEnabled = false;
                btnAdd.IsEnabled = false;
                btnRemove.IsEnabled = false;
                cmbItems.IsEnabled = false;
                txtDate.IsEnabled = false;

                UIMainLogic = new clsMainLogic();
                cmbItems.ItemsSource = UIMainLogic.GetAllItems();


            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Opens the invoice search menu when Search is clicked in the menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //wndMainSearch.ShowDialog();

                /* if (window.ShowDialog() == true)
                {
                    int invoiceID = window.SelectedInvoiceID;
                }*/
                //once the search window is closed the invoiceID should be set in the search window's closing event.
                //SelectInvoice(ToString(invoiceID))

                wndSearch wndMainSearch = new wndSearch();
                if (wndMainSearch.ShowDialog() == true) { 
                    invoiceID = wndMainSearch.SelectedInvoiceID;

                    UIMainLogic.SelectInvoice(invoiceID.ToString());
                    dgItems.DataContext = UIMainLogic.GetInvoiceItems();
                    dgItems.ItemsSource = UIMainLogic.GetInvoiceItems();
                    lblTotalCost.Content = "Total Cost: $" + UIMainLogic.fTotalCost;
                    lblInvoice.Content = "Invoice Number: " + invoiceID.ToString();
                    txtDate.Text = UIMainLogic.clsCurrInvoice.sInvoiceDate;
                    btnEdit.IsEnabled = true;
                    DisableControls();
                }
                

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Opens the item edit window when Edit Items is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //wndMainItems.ShowDialog();
                //need to update cmbItems if the items changed in the items window.
                //For now assuming that the wndItems will be coded to update the database then i'll grab the items from there.
                //UpdateItems()

                wndItems itemsWindow = new wndItems();
                if (itemsWindow.ShowDialog() == true)
                {

                }

                UIMainLogic.UpdateItems();

                cmbItems.ItemsSource = UIMainLogic.GetAllItems();


            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Enables Invoice editing controls
        /// </summary>
        private void EnableControls()
        {
            try
            {
                btnAdd.IsEnabled = true;
                btnRemove.IsEnabled = true;
                cmbItems.IsEnabled = true;
                txtDate.IsEnabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
            
        }

        /// <summary>
        /// Disables Invoice editing controls
        /// </summary>
        private void DisableControls()
        {
            try
            {
                btnAdd.IsEnabled = false;
                btnRemove.IsEnabled = false;
                cmbItems.IsEnabled = false;
                txtDate.IsEnabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
            
        }

        
        /// <summary>
        /// Saves the invoice to the database once clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                //validate txtDate

                if (DateTime.TryParse(txtDate.Text, out DateTime dt))
                {
                    lblDateError.Visibility = Visibility.Collapsed;

                    menuWindows.IsEnabled = true;
                    btnCreate.IsEnabled = true;
                    btnEdit.IsEnabled = true;
                    btnSave.IsEnabled = false;

                    if (eCurrentMode == InvoiceMode.NewInvoice) //If its a new invoice, insert it
                    {
                        UIMainLogic.InsertNewInvoice(txtDate.Text);
                        invoiceID = Int32.Parse(UIMainLogic.clsCurrInvoice.sInvoiceNum);
                        lblInvoice.Content = "Invoice Number: " + invoiceID;

                    }
                    else if (eCurrentMode == InvoiceMode.Edit) //if its an existing invoice, update
                    {

                        UIMainLogic.EditInvoice(invoiceID.ToString(), txtDate.Text);
                    }
                    DisableControls();
                    eCurrentMode = InvoiceMode.None;
                }
                else
                {
                    lblDateError.Visibility = Visibility.Visible;
                }

                

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Lets the user edit an invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                menuWindows.IsEnabled = false; //Dont allow user to open other windows while editing invoice
                eCurrentMode = InvoiceMode.Edit;
                btnEdit.IsEnabled = false;
                btnCreate.IsEnabled = false;
                btnSave.IsEnabled = true;
                EnableControls();

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
            
        }

        /// <summary>
        /// Makes a blank invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                menuWindows.IsEnabled = false;
                eCurrentMode = InvoiceMode.NewInvoice;
                btnEdit.IsEnabled = false;
                btnCreate.IsEnabled = false;
                btnSave.IsEnabled = true;
                EnableControls();
                UIMainLogic.NewInvoice();
                lblTotalCost.Content = "Total Cost: $0";
                lblInvoice.Content = "Invoice Number: TBD";
                dgItems.DataContext = UIMainLogic.GetInvoiceItems();

                dgItems.ItemsSource = UIMainLogic.GetInvoiceItems();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
        }

        /// <summary>
        /// Updates lblCost with item cost.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void cmbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbItems.SelectedItem != null)
                {
                    clsItem SelectedItem = (clsItem)cmbItems.SelectedItem;

                    lblCost.Content = "Item Cost: $" + SelectedItem.fItemCost.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
            
        }

        /// <summary>
        /// Adds selected item in cmbItems to the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (cmbItems.SelectedItem != null)
                {
                    UIMainLogic.InsertItem((clsItem)cmbItems.SelectedItem);
                    lblTotalCost.Content = "Total Cost: $" + UIMainLogic.fTotalCost.ToString();

                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Removes the selected item in cmbItems from the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (cmbItems.SelectedItem != null)
                {
                    clsItem SelectedItem = (clsItem)cmbItems.SelectedItem;
                    if (UIMainLogic.DeleteItem(SelectedItem.sItemCode))
                    {
                        lblTotalCost.Content = "Total Cost: $" + UIMainLogic.fTotalCost.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
        }

        

        
    }
}
