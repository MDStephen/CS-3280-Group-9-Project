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

            // Populate the data grid with the list of items from the database
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
        /// Function to allow user to add an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Populate descriptive title label

            // Clear textboxes and make them so they can be written into

            // Prompt user to click the save button to save the new item to the database

            // Allow Save button to be clicked

        }

        /// <summary>
        /// Function to allow user to edit the current details of a selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Populate descriptive title label

            // Identify current item

            // Keep current data in text boxes and allow them to be written into/changed

            // Allow Save button to be clicked
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
            // The save button should be able to handle both adding and editing an item, so we will need to determine which action the user is trying to perform

            // Determine what action the user is trying to perform (add or edit)

            // Create a new item based on the data in the text boxes

            // If the user is trying to add an item, add the new item to the database

            // If the user is trying to edit an item, update the existing item in the database with the new data

            // Set the boolean variable to true to indicate that a change has been made to the item list
            
        }
    }
}
