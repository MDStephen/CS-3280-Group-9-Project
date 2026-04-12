using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_3280_Group_9_Project.Items
{
    internal class clsItem
    {
        /// <summary>
        /// Integer property representing the item's code
        /// </summary>
        public int iItemCode { get; set; }

        /// <summary>
        /// Decimal property representing the item's cost
        /// </summary>
        public decimal dItemCost { get; set; }

        /// <summary>
        /// String property representing the item's description
        /// </summary>
        public string sItemDescription{ get; set; }

        /// <summary>
        /// Method to overrired the string display of the class to show the item's description
        /// </summary>
        /// <returns>A string of the item's description</returns>
        public override string ToString()
        {
            return $"{sItemDescription}";
        }
    }
}
