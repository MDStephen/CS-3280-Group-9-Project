using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS_3280_Group_9_Project
{
    internal class clsItem
    {

        /// <summary>
        /// Item code from the database
        /// </summary>
        public string sItemCode { get; set; }
        /// <summary>
        /// Item description
        /// </summary>
        public string sItemDesc { get; set; }
        /// <summary>
        /// Item cost
        /// </summary>
        public float fItemCost { get; set; }

        //make constructor for clsItem

        /// <summary>
        /// clsItem constructor
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <param name="ItemDesc"></param>
        /// <param name="ItemCost"></param>
        /// <exception cref="Exception"></exception>
        public clsItem(string ItemCode, string ItemDesc, float ItemCost)
        {
            try
            {
                sItemCode = ItemCode;
                sItemDesc = ItemDesc;
                fItemCost = ItemCost;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        
        /// <summary>
        /// Overrides ToString to return sItemDesc
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return sItemDesc;
        }
    }
}
