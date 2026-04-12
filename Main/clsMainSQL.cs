using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS_3280_Group_9_Project.Main
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
                    "SET TotalCost = " + cost + ", InvoiceDate = " + date +
                    "WHERE InvoiceNum = " + invnum;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string InsertItem(string invnum, string linenum, string itemcode)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) " +
                    "Values (" + invnum + ", " + linenum + ", " + itemcode + ")";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string DeleteItem(string invnum) {
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

        public static string InsertInvoice(string invdate, string totalcost)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost) " +
                    "Values (" + invdate + ", " + totalcost + ")";
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
                    "FROM Invoices" +
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
                    "FROM LineItems, ItemDesc" +
                    "WHERE LineItems.ItemCode = ItemDesc.ItemCode AND LineItems.InvoiceNum = " + invnum;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string LastInsert()
        {
            try
            {
                string sSQL = "SELECT LAST_INSERT_ID()";
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
