using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace CS_3280_Group_9_Project.Search
{

    using static clsDataAccess;

    /// <summary>
    /// The class which holds methods for calling sql and handling return values
    /// </summary>
    internal class clsSearchSQL
    {
        /// <summary>
        /// This method returns a list of invoices as a tuple for each value
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllInvoices(ref int rowsReturned)
        {
            try
            {
                // The SQL statement selecting all invoices
                string selectAllInvoices = "SELECT * FROM Invoices;";

                /// Extract each invoice from the database
                clsDataAccess db = new clsDataAccess();
                DataSet data = db.ExecuteSQLStatement(selectAllInvoices, ref rowsReturned);

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
