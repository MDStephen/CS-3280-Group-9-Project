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

namespace CS_3280_Group_9_Project.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        // Initializae ItemsLogic class to access its methods

        public wndItems()
        {

            // Initialize the window and its components
            // TODO: configure properly
            InitializeComponent();

            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                // Populate the data grid with the list of items from the database
                // Set the source of the data grid to the list of items from the database
                dgItems.ItemsSource = clsItemsLogic.GetAllItems();

                // Disable all input text boxes since thye should not be editable until the user clicks either the add or edit button
                txtbxCode.IsEnabled = false;
                txtbxCost.IsEnabled = false;
                txtbxDescription.IsEnabled = false;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Private boolean variable to track whether any changes to the list has occurred
        /// </summary>
        private bool bHasItemListChanged;

        /// <summary>
        /// Public boolean variable to send to main UI to update whether any changes have occurred so that visuals/data can be updated
        /// </summary>
        public bool HasItemListChanged;

        /// <summary>
        /// Variable to help determine whether the user is trying to add or edit an item so that the save button can perform the correct action
        /// </summary>
        private enum ItemMode { None, Add, Edit }
        private ItemMode currentMode = ItemMode.None;

        /// <summary>
        /// Variable to track the currently selected item in the data grid so that we know which item to edit when the user clicks the edit button and to populate the text boxes with the current details of the selected item
        /// </summary>
        private clsItem selectedItem;


        /// <summary>
        /// Function to allow user to add an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            // Set the current mode to add so that the save button knows to add a new item to the database instead of editing an existing item
            currentMode = ItemMode.Add;

            // Populate descriptive title label
            lblActionTitle.Content = "Add New Item";

            // Disable all buttons except for the save button to force user to either save or cancel the add action before doing anything else
            btnAdd.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            dgItems.IsEnabled = false;
            btnCancel.IsEnabled = true;

            // Code textbox should not be able to be written into since it is an auto-generated primary key, so we will leave it as is and just make the other textboxes writable
            txtbxCode.IsEnabled = false;
            txtbxDescription.IsEnabled = true;
            txtbxCost.IsEnabled = true;
            // Clear textboxes and make them so they can be written into
            txtbxCode.Text = "";
            txtbxCost.Text = "";
            txtbxDescription.Text = "";

            // Prompt user to click the save button to save the new item to the database
            txtBlckActionPrompt.Text = "Please enter the details of the new item and click the Save button to add it to the database.";
        }

        /// <summary>
        /// Function to allow user to edit the current details of a selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Populate descriptive title label

            lblActionTitle.Content = "Edit Item";

            // Set the current mode to edit so that the save button knows to edit the existing item in the database instead of adding a new item
            currentMode = ItemMode.Edit;

            // Lock UI
            btnAdd.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            dgItems.IsEnabled = false;
            btnCancel.IsEnabled = true;
            txtbxCode.IsEnabled = false;
            txtbxDescription.IsEnabled = true;
            txtbxCost.IsEnabled = true;
            txtBlckActionPrompt.Text = "Update the details of the selected item and click the Save button to apply the changes.";

            // Identify current item
            selectedItem = (clsItem)dgItems.SelectedItem;
        }

        /// <summary>
        /// Function to save the current data in the text boxes to the database
        /// </summary>
        /// <remarks>
        /// This button should become available and should be clicked AFTER either the add or save button
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // Instatiate a new item object to be added to the database with the data from the text boxes
                clsItem newItem = new clsItem
                {
                    sItemCode = clsItemsLogic.GetNextItemCode(),
                    sItemDescription = txtbxDescription.Text,
                    dItemCost = decimal.Parse(txtbxCost.Text)
                };

                // Determine what action the user is trying to perform (add or edit)
                switch (currentMode)
                {
                    case ItemMode.Add:
                        // Add the new item to the database
                        clsItemsLogic.AddItem(newItem);
                        break;
                    
                    case ItemMode.Edit:
                        // Identify the current item being edited (this will require a way to track which item is being edited, such as a private variable that is set when the edit button is clicked)
                        clsItem oldItem = (clsItem)dgItems.SelectedItem;
                        // Update the existing item in the database with the new data
                        clsItemsLogic.EditItem(oldItem, newItem);
                        break;

                    default:
                        MessageBox.Show("No action selected.");
                        return;
                }

                HasItemListChanged = true;

                // Reset UI
                currentMode = ItemMode.None;
                btnAdd.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                txtbxCost.Text = "";
                txtbxDescription.Text = "";
                lblActionTitle.Content = "";
                txtBlckActionPrompt.Text = "";
                dgItems.IsEnabled = true;

                // Refresh the data grid to show the updated list of items from the database
                dgItems.ItemsSource = clsItemsLogic.GetAllItems();

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Funtion to help with error handling
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Function to handle selection changes in the data grid.  This will be used to populate the text boxes with the current details of the selected item when the edit button is clicked and to identify which item is being edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgItems.SelectedItem is clsItem selected)
            {
                txtbxCode.Text = selected.sItemCode;
                txtbxDescription.Text = selected.sItemDescription;
                txtbxCost.Text = selected.dItemCost.ToString();
            }
        }

        /// <summary>
        /// Button to cancel the current add or edit action and reset the UI to its default state.  This will not save any changes to the database and will just reset the text boxes and buttons to their default state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Set current mode back to none so that the save button does not perform any action if it is clicked
            currentMode = ItemMode.None;

            // Reset UI
            btnAdd.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnSave.IsEnabled = false;
            txtbxCost.IsEnabled = false;
            txtbxDescription.IsEnabled = false;
            txtbxCode.Text = "";
            txtbxDescription.Text = "";
            txtbxCost.Text = "";
            lblActionTitle.Content = "";
            txtBlckActionPrompt.Text = "";
            dgItems.IsEnabled = true;
        }

        /// <summary>
        /// Button to handle delete, should check to make sure item is not on an invoice before allowing deletion and should prompt user to confirm deletion since this action cannot be undone.  If the item is on an invoice, a message should pop up to the user letting them know that they cannot delete the item until it is removed from all invoices.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Identify the selected item to be deleted
                clsItem itemToDelete = (clsItem)dgItems.SelectedItem;

                if (itemToDelete != null)
                {
                    // Check if the item is on any invoices
                    bool itemOnInvoice = clsItemsLogic.IsItemOnInvoice(itemToDelete);

                    if (itemOnInvoice)
                    {
                        MessageBox.Show("This item cannot be deleted because it is on an invoice. Please remove it from all invoices before deleting.");
                    }
                    else
                    {
                        // Confirm deletion with the user
                        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item? This action cannot be undone.", "Confirm Deletion", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            clsItemsLogic.DeleteItem(itemToDelete);
                            dgItems.ItemsSource = clsItemsLogic.GetAllItems();
                        }
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
