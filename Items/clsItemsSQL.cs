using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS_3280_Group_9_Project.Items
{
    internal class clsItemsSQL
    {
        /// <summary>
        /// Method to return SQL query to get all items in the database
        /// </summary>
        /// <returns>SQL query to get all items in the database</returns>
        /// <exception cref="Exception"></exception>
        public static string GetItems()
        {
            try
            {
                string sSQL = "select ItemCode, ItemDesc, Cost from ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "-> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to return SQL query to get all invoices that have a specific item in them
        /// </summary>
        /// <param name="itemToSearchFor">Item to find invoices for</param>
        /// <returns>SQL query to get all invoices that have a specific item in them</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoicesWithItem(clsItem itemToSearchFor)
        {
            try
            {
                string sSQL = "select distinct(InvoiceNum) from LineItems where ItemCode = " + itemToSearchFor.iItemCode;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "-> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to return SQL query to update an existing item in the database
        /// </summary>
        /// <param name="oldItem">Old item to update</param>
        /// <param name="newItem">New item to update to</param>
        /// <returns>SQL query to update an existing item in the database</returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateItem(clsItem oldItem, clsItem newItem)
        {
            try
            {
                string sSQL = "Update ItemDesc Set ItemDesc = '" + newItem.sItemDescription + "', Cost = " + newItem.dItemCost + " where ItemCode = '" + oldItem.iItemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "-> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to return SQL query to insert a new item into the database
        /// </summary>
        /// <param name="newItem">New item to insert</param>
        /// <returns>SQL query to insert a new item into the database</returns>
        /// <exception cref="Exception"></exception>
        public static string AddItem(clsItem newItem)
        {
            try
            {
                string sSQL = "Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values('" + newItem.iItemCode + "', '" + newItem.sItemDescription + "', " + newItem.dItemCost + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "-> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to return SQL query to delete an existing item from the database
        /// </summary>
        /// <param name="itemToDelete">Item to delete</param>
        /// <returns>SQL query to delete an existing item from the database</returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteItem(clsItem itemToDelete)
        {
            try
            {
                string sSQL = "Delete from ItemDesc Where ItemCode = '" + itemToDelete.iItemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + "-> " + ex.Message);
            }
        }
    }
}