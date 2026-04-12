using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_3280_Group_9_Project
{
    internal class clsInvoice
    {
        /// <summary>
        /// Invoice ID
        /// </summary>
        public string sInvoiceNum;
        /// <summary>
        /// Invoice Date
        /// </summary>
        public string sInvoiceDate;
        /// <summary>
        /// Total Cost
        /// </summary>
        public string sTotalCost;

        /// <summary>
        /// Constructor for clsInvoice
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <param name="sInvoiceDate"></param>
        /// <param name="sTotalCost"></param>
        public clsInvoice(string sInvoiceNum, string sInvoiceDate, string sTotalCost)
        {
            this.sInvoiceNum = sInvoiceNum;
            this.sInvoiceDate = sInvoiceDate;
            this.sTotalCost = sTotalCost;
        }
        //override tostring?
    }
}
