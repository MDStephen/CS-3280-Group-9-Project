using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace CS_3280_Group_9_Project.Search
{

    using static clsSearchSQL;

    /// <summary>
    /// The Logic class for the search window
    /// </summary>
    internal class clsSearchLogic
    {
        /// <summary>
        /// A method for converting the dataset output from the sql method SelectAllInvoices into a tuple list of each row and it's respective values
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<(int ID, string Date, int TotalCost)> GetInvoiceList()
        {
            try
            {
                List<(int ID, string Date, int TotalCost)> invoiceList = new List<(int ID, string Date, int TotalCost)>();

                int rowsReturned = 0;

                clsSearchSQL ss = new clsSearchSQL();
                DataSet data = ss.SelectAllInvoices(ref rowsReturned);

                // Turn the dataset into the tuple list
                for (int i = 0; i < rowsReturned; i++)
                {
                    invoiceList.Add((Convert.ToInt32(data.Tables[0].Rows[i]["InvoiceNum"]), data.Tables[0].Rows[i]["InvoiceDate"].ToString(), Convert.ToInt32(data.Tables[0].Rows[i]["TotalCost"])));
                }

                return invoiceList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
