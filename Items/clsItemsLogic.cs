using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS_3280_Group_9_Project.Items
{
    internal class clsItemsLogic
    {
        /// <summary>
        /// Method to get a list of all items in the database
        /// </summary>
        /// <returns>A list of items</returns>
        /// <exception cref="Exception"></exception>
        public static List<clsItem> GetAllItems()
        {
            try
            {
                // Create a database
                clsDataAccess db = new clsDataAccess();

                // Create a list to hold the passenger information
                List<clsItem> items = new List<clsItem>();

                // Create a DataSet to hold the results of the SQL query
                DataSet ds;
                int iReturnValue = 0;

                // Get the SQL statement for retrieving flight information
                string sSQL = clsItemsSQL.GetItems();

                // Execute the SQL statement and populate the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iReturnValue);

                // Loop through the DataSet and populate the list of passengers
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    clsItem item = new clsItem
                    {
                        iItemCode = Convert.ToInt32(row["Item_Code"]),
                        dItemCost = Convert.ToDecimal(row["Item_Cost"]),
                        sItemDescription = row["Item_Description"].ToString()
                    }
                    ;
                    items.Add(item);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "-> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to add an item to the database
        /// </summary>
        /// <param name="newItem">New item to add to the database</param>
        /// <exception cref="Exception"></exception>
        public static void AddItem(clsItem newItem)
        {
            try
            {
                // Create a database
                clsDataAccess db = new clsDataAccess();

                // Get the SQL statement for adding an item
                string sSQL = clsItemsSQL.AddItem(newItem);

                // Execute the SQL statement
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "-> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to edit an item in the database
        /// </summary>
        /// <param name="oldItem">Old item to update</param>
        /// <param name="newItem">New item to update to</param>
        /// <exception cref="Exception"></exception>
        public static void EditItem(clsItem oldItem, clsItem newItem)
        {
            try
            {
                // Create a database
                clsDataAccess db = new clsDataAccess();

                // Get the SQL statement for editing an item
                string sSQL = clsItemsSQL.UpdateItem(oldItem, newItem);

                // Execute the SQL statement
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "-> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to delete an item from the database
        /// </summary>
        /// <param name="itemToDelete">Item to delet</param>
        /// <exception cref="Exception"></exception>
        public static void DeleteItem(clsItem itemToDelete)
        {
            try
            {
                // Create a database
                clsDataAccess db = new clsDataAccess();

                // Get the SQL statement for deleting an item
                string sSQL = clsItemsSQL.DeleteItem(itemToDelete);

                // Execute the SQL statement
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "-> " + ex.Message);
            }
        }
        
        /// <summary>
        /// Method to determine whether an item is on an existing invoice or not
        /// </summary>
        /// <param name="itemToCheck">Item to check for invoices</param>
        /// <returns>True if the item exists on an invoice, false if otherwise</returns>
        /// <exception cref="Exception"></exception>
        public static bool IsItemOnInvoice(clsItem itemToCheck)
        {
            try
            {
                // Create a database

                clsDataAccess db = new clsDataAccess();
                // Get the SQL statement for checking if an item is on an invoice
                string sSQL = clsItemsSQL.GetInvoicesWithItem(itemToCheck);

                // Execute the SQL statement and get the results
                DataSet ds;
                int iReturnValue = 0;
                ds = db.ExecuteSQLStatement(sSQL, ref iReturnValue);

                // If there are any rows returned, then the item is on an invoice
                return ds.Tables[0].Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "-> " + ex.Message);
            }
        }

        // Create a method that handles refreshing the data grid once the item list has been updated (added, edited, or deleted)
    }
}
