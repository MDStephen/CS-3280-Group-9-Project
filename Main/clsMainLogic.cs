using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CS_3280_Group_9_Project
{
    /// <summary>
    /// This part of the project was written by Noelle.
    /// </summary>
    /// 

    internal class clsMainLogic
    {
        
        /// <summary>
        /// Database access
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// Items pulled from database
        /// </summary>
        DataSet dsResult;

        /// <summary>
        /// List containing all Items in the database
        /// </summary>
        private List<clsItem> liAllItems;

        /// <summary>
        /// List containing the items on the current invoice.
        /// </summary>
        private BindingList<clsItem> liInvoiceItems;

        /// <summary>
        /// Number of items that exist.
        /// </summary>
        private int iNumofItems;

        /// <summary>
        /// Number of items on the current invoice
        /// </summary>
        private int iNumofInvoiceItems;

        
        /// <summary>
        /// Current invoice from database.
        /// </summary>
        public clsInvoice clsCurrInvoice;

        /// <summary>
        /// Total cost of the invoice
        /// </summary>
        public float fTotalCost;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <exception cref="Exception"></exception>
        public clsMainLogic()
        {
            try
            {
                db = new clsDataAccess();

                dsResult = db.ExecuteSQLStatement(clsMainSQL.SelectItems(), ref iNumofItems);
                liAllItems = new List<clsItem>();
                fTotalCost = 0;

                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++) {
                    float.TryParse(dsResult.Tables[0].Rows[i][2].ToString().Replace("$", ""), out float cost);

                    liAllItems.Add(new clsItem(dsResult.Tables[0].Rows[i][0].ToString(),
                        dsResult.Tables[0].Rows[i][1].ToString(),
                        cost));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        

        #region Selecting existing invoice

        /// <summary>
        /// Select Invoice from the database
        /// </summary>
        /// <param name="InvoiceID">Invoice to find</param>
        /// <exception cref="Exception"></exception>
        public void SelectInvoice(string InvoiceID)
        {

            try
            {
                fTotalCost = 0;
                int result = 0;
                dsResult = db.ExecuteSQLStatement(clsMainSQL.SelectInvoice(InvoiceID), ref result);

                clsCurrInvoice = new clsInvoice(dsResult.Tables[0].Rows[0][0].ToString(),
                            dsResult.Tables[0].Rows[0][1].ToString(),
                            dsResult.Tables[0].Rows[0][2].ToString()); 

                UpdateInvoiceItems(InvoiceID);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }

        }

        /// <summary>
        /// Gets the items on the invoice from the database.
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateInvoiceItems(string InvoiceNum)
        {
            try
            {
                dsResult = db.ExecuteSQLStatement(clsMainSQL.SelectLineItems(InvoiceNum), ref iNumofInvoiceItems);
                
                liInvoiceItems = new BindingList<clsItem>();
                fTotalCost = 0;
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    float.TryParse(dsResult.Tables[0].Rows[i][2].ToString().Replace("$", ""), out float cost);
                    liInvoiceItems.Add(new clsItem(dsResult.Tables[0].Rows[i][0].ToString(),
                        dsResult.Tables[0].Rows[i][1].ToString(),
                        cost));
                    fTotalCost += cost;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Returns liInvoiceItems
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BindingList<clsItem> GetInvoiceItems()
        {
            try
            {
                
                return liInvoiceItems;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        

        /// <summary>
        /// Update existing invoice in the database
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <param name="date"></param>
        /// <exception cref="Exception"></exception>
        public void EditInvoice(string InvoiceID, string date)
        {
            
            try
            {
                //delete lineitems
                db.ExecuteNonQuery(clsMainSQL.DeleteInvoice(InvoiceID));
                //reinsert all items
                int i = 0;

                foreach (var item in liInvoiceItems)
                {
                    db.InsertLineItem(clsMainSQL.InsertItem(), InvoiceID, i.ToString(), item.sItemCode);
                    i++;
                }

                //update cost and date

                db.ExecuteNonQuery(clsMainSQL.UpdateInvoice(fTotalCost.ToString(), InvoiceID, date));
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        #endregion


        #region New Invoice

        /// <summary>
        /// Sets liInvoiceItems to an empty list and cost to 0.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void NewInvoice()
        {
            try
            {
                liInvoiceItems = new BindingList<clsItem>();
                clsCurrInvoice = null;
                fTotalCost = 0;
                iNumofInvoiceItems = 0;
                
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Inserts a new invoice into database.
        /// </summary>
        /// <param name="date">Invoice due date</param>
        public void InsertNewInvoice(string date)
        {
            
            db.InsertInvoice(clsMainSQL.InsertInvoice(), date, fTotalCost);
            //get invoice id
            string InvoiceID = db.ExecuteScalarSQL(clsMainSQL.SelectHighestInvoice());
            //insert all liInvoiceItems into lineitems
            int i = 0;
            foreach (var item in liInvoiceItems)
            {
                db.InsertLineItem(clsMainSQL.InsertItem(), InvoiceID, i.ToString(), item.sItemCode);
                //db.ExecuteNonQuery(clsMainSQL.InsertItem(InvoiceID, i.ToString(), item.sItemCode));
                i++;
            }

            //select invoice from database
            int result = 0;
            dsResult = db.ExecuteSQLStatement(clsMainSQL.SelectInvoice(InvoiceID), ref result);

            clsCurrInvoice = new clsInvoice(dsResult.Tables[0].Rows[0][0].ToString(),
                        dsResult.Tables[0].Rows[0][1].ToString(),
                        dsResult.Tables[0].Rows[0][2].ToString());
        }

        #endregion

        #region Item Functions

        /// <summary>
        /// Updates list of all items in the database.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void UpdateItems()
        {
            try
            {
                dsResult = db.ExecuteSQLStatement(clsMainSQL.SelectItems(), ref iNumofItems);
                liAllItems = new List<clsItem>();

                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    float.TryParse(dsResult.Tables[0].Rows[i][2].ToString().Replace("$", ""), out float cost);

                    liAllItems.Add(new clsItem(dsResult.Tables[0].Rows[i][0].ToString(),
                        dsResult.Tables[0].Rows[i][1].ToString(),
                        cost));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        

        /// <summary>
        /// Returns liAllItems.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsItem> GetAllItems()
        {
            try
            {
                return liAllItems;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }

        /// <summary>
        /// Inserts an item into liInvoiceItems and updates cost
        /// </summary>
        /// <param name="Item"></param>
        /// <exception cref="Exception"></exception>
        public void InsertItem(clsItem Item)
        {
            try
            {
                liInvoiceItems.Add(Item);
                fTotalCost += Item.fItemCost;
                iNumofInvoiceItems++;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }
        }
        
        /// <summary>
        /// Deletes an item from the invoice
        /// </summary>
        /// <param name="ItemCode">Item to delete</param>
        /// <returns>True if deleted, false otherwise</returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteItem(string ItemCode)
        {
            
            try
            {
                foreach (var item in liInvoiceItems) //determine if item is in liItems.
                {
                    if (item.sItemCode == ItemCode) //if so, delete it liItems, delete all lineItems in database then  return true
                    {
                        liInvoiceItems.Remove(item);
                        fTotalCost -= item.fItemCost;
                        iNumofInvoiceItems--;
                        return true;
                    }
                }

                return false; //if not, return false.
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);

            }

        }

        #endregion

    }
}
