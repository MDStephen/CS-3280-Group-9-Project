using CS_3280_Group_9_Project.Items;
using CS_3280_Group_9_Project.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CS_3280_Group_9_Project.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml. This part of the project was written by Noelle.
    /// </summary>
    public partial class wndMain : Window
    {
        wndSearch wndMainSearch;
        wndItems wndMainItems;
        clsMainLogic clsUIMainLogic;
        bool bCreateMode;
        bool bEditMode;
        static int invoiceID;


        public wndMain()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                //set cmbItems itemsource to clsMainLogic.liItems

                menuWindows.Visibility = Visibility.Visible;
                bCreateMode = false;
                bEditMode = false;
                invoiceID = 0;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        private void MenuSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndMainSearch = new wndSearch();
                wndMainSearch.ShowDialog();
                //once the search window is closed the invoiceID should be set in the search window's closing event.
                //SelectInvoice(ToString(invoiceID))

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void UpdateItems()
        {
            //Updates cmbItems itemsource to clsMainLogic.GetItems() 
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndMainItems = new wndItems();
                wndMainItems.ShowDialog();
                //need to update cmbItems if the items changed in the items window.
                //For now assuming that the wndItems will be coded to update the database then i'll grab the items from there.
                //UpdateItems()

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //when add item is clicked, add the item to the invoice
            //update lblTotalCost
        }

        private void cmbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //update lblItem
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //validate txtDate
            //clsMainSQL.InsertInvoice
            //lock the invoice controls
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            
            //clsMainLogic.DeleteItem(Itemcode);
            //update dgItems
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //unlock invoice controls
            //EditMode = true;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //unlock invoice controls
            //CreateMode = true;
        }
    }
}
