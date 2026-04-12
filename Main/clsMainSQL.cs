using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS_3280_Group_9_Project
{
    /// <summary>
    /// This part of the project was written by Noelle.
    /// </summary>



    internal class clsMainSQL
    {
        public static string UpdateInvoice(string cost, string invnum, string date)
        {
            try
            {
                string sSQL = "UPDATE Invoices " +
                    "SET TotalCost = " + cost + ", InvoiceDate = #" + date +
                    "# WHERE InvoiceNum = " + invnum;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string InsertItem()
        {
            try
            {
                string sSQL = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) " +
                    "Values (?, ?, ?)";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string DeleteInvoice(string invnum) {
            try
            {
                string sSQL = "DELETE FROM LineItems " +
                    "WHERE InvoiceNum = " + invnum;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string DeleteItem(string invnum, string lineitem)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems " +
                    "WHERE InvoiceNum = " + invnum + " AND LineItemNum = " + lineitem;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string InsertInvoice()
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost) " +
                    "Values (?, ?)";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string SelectItems()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost " +
                    "FROM ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string SelectInvoice(string invnum)
        {
            try
            {
                string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost " +
                    "FROM Invoices " +
                    "WHERE InvoiceNum = " + invnum;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string SelectLineItems(string invnum)
        {
            try
            {
                string sSQL = "SELECT  LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                    "FROM LineItems, ItemDesc " +
                    "WHERE LineItems.ItemCode = ItemDesc.ItemCode AND LineItems.InvoiceNum = " + invnum;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string SelectHighestLineItem(string invnum)
        {
            try
            {
                string sSQL = "SELECT MAX(InvoiceNum) " +
                    "FROM LineItems " +
                    "WHERE InvoiceNum = " + invnum;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        public static string SelectHighestInvoice()
        {
            try
            {
                string sSQL = "SELECT MAX(InvoiceNum) " +
                    "FROM Invoices ";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        

    }
}
