using CarpentryShop.CarpentryShopDB;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CarpentryShop.Windows.WorkBenchWindow
{
    /// <summary>
    /// Логика взаимодействия для Paths.xaml
    /// </summary>
    public partial class Paths : Window
    {
        public Paths()
        {
            InitializeComponent();
            List<Tools> tools = new List<Tools>();
            List<WoodDetails> woodDetails = new List<WoodDetails>();
            List<MetalDetails> metalDetails = new List<MetalDetails>();

            foreach (InventoryTools invTools in App.Connection.InventoryTools)
            {
                Tools tool = App.Connection.Tools.FirstOrDefault(x => x.idTool == invTools.idTool);
                tools.Add(tool);
            }
            foreach (InventoryWoodDetails invWoodDetails in App.Connection.InventoryWoodDetails)
            {
                WoodDetails woodDetail = App.Connection.WoodDetails.FirstOrDefault(x => x.idWoodDetail == invWoodDetails.idWoodDetail);
                woodDetails.Add(woodDetail);
            }
            foreach (InventoryMetalDetails invMetalDetail in App.Connection.InventoryMetalDetails)
            {
                MetalDetails metalDetail = App.Connection.MetalDetails.FirstOrDefault(x => x.idMetalDetail == invMetalDetail.idMetalDetail);
                metalDetails.Add(metalDetail);
            }

            ToolsList.ItemsSource = tools;
            WoodDetailsList.ItemsSource = woodDetails;
            MetalDetailsList.ItemsSource = metalDetails;
        }

        private void EventAddSelectedItems(object sender, RoutedEventArgs e)
        {
            if (ToolsList.SelectedItem != null &&
                WoodDetailsList.SelectedItem != null &&
                MetalDetailsList.SelectedItem != null)
            {
                List<Tools> tools = new List<Tools>();
                List<WoodDetails> woodDetails = new List<WoodDetails>();
                List<MetalDetails> metalDetails = new List<MetalDetails>();
                Tools tool = ToolsList.SelectedItem as Tools;
                MetalDetails mDetail = MetalDetailsList.SelectedItem as MetalDetails;
                WoodDetails wDetail = WoodDetailsList.SelectedItem as WoodDetails;
                tools.Add(tool);
                woodDetails.Add(wDetail);
                metalDetails.Add(mDetail);

                var window = new WorkBenchWindow(woodDetails, metalDetails, tools);
                window.Show();
                this.Close();
            }
        }
    }
}
