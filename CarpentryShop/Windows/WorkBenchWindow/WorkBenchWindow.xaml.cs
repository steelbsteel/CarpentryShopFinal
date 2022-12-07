using CarpentryShop.CarpentryShopDB;
using CarpentryShop.Windows.ReceiptsWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для WorkBenchWindow.xaml
    /// </summary>
    public partial class WorkBenchWindow : Window
    {
        public WorkBenchWindow(List<WoodDetails> woodDetail, List<MetalDetails> metalDetail, List<Tools> tool)
        {
            InitializeComponent();

            WoodDetailList.ItemsSource = woodDetail;
            MetalDetailList.ItemsSource = metalDetail;
            ToolList.ItemsSource = tool;
        }

        private void EventSelectPaths(object sender, RoutedEventArgs e)
        {
            var window = new Paths();
            window.Show();
            this.Close();
        }

        private void EventCreateComponent(object sender, RoutedEventArgs e)
        {
            InventoryComponents component = new InventoryComponents();
            bool isExist = false; 

            foreach (ComponentReceipts receipt in App.Connection.ComponentReceipts)
            {
                int idTool = (ToolList.Items[0] as Tools).idTool;
                int idWoodDetail = (WoodDetailList.Items[0] as WoodDetails).idWoodDetail;
                int idMetalDetail = (MetalDetailList.Items[0] as MetalDetails).idMetalDetail;

                if (receipt.idWoodDetail == idWoodDetail &&
                   receipt.idMetalDetail == idMetalDetail &&
                   receipt.idTool == idTool)
                {
                    int idReceipt = receipt.idCompontentReceipt;
                    foreach(Components componentt in App.Connection.Components)
                    {
                        if (componentt.idComponentReceipt == idReceipt)
                        {
                            component.idComponent = componentt.idComponent;
                            isExist = true;
                        }
                    }
                }
            }

            if (isExist)
            {
                MetalDetails md = MetalDetailList.Items[0] as MetalDetails;
                WoodDetails wd = WoodDetailList.Items[0] as WoodDetails;
                Tools t = ToolList.Items[0] as Tools;

                MetalDetails md1 = App.Connection.MetalDetails.FirstOrDefault(x => x.NameMetalDetail == md.NameMetalDetail);
                WoodDetails wd1 = App.Connection.WoodDetails.FirstOrDefault(x => x.NameWoodDetail== wd.NameWoodDetail);
                Tools t1 = App.Connection.Tools.FirstOrDefault(x => x.NameTool == t.NameTool);

                InventoryComponents invComp = new InventoryComponents();
                InventoryMetalDetails invMD = App.Connection.InventoryMetalDetails.FirstOrDefault(x => x.idMetalDetail == md1.idMetalDetail);
                List<ComponentReceipts> Receipts = App.Connection.ComponentReceipts.ToList();
                List<ComponentReceipts> PossibleReceipts = new List<ComponentReceipts>();
                List<Components> cmps = App.Connection.Components.ToList();
                List<Components> PossibleComponents = new List<Components>();

                foreach(ComponentReceipts receipt in Receipts)
                {
                    if (receipt.idMetalDetail == md1.idMetalDetail &&
                        receipt.idWoodDetail == wd1.idWoodDetail &&
                        receipt.idTool == t1.idTool)
                    {
                        PossibleReceipts.Add(receipt);
                    }
                }

                foreach(Components c in cmps)
                {
                    foreach(ComponentReceipts crs in PossibleReceipts)
                    {
                        if(c.idComponentReceipt == crs.idCompontentReceipt)
                        {
                            PossibleComponents.Add(c);
                        }
                    }
                }

                var window = new SelectPossibleComponent(PossibleComponents);
                window.Show();
                this.Close();
            }

            else
            {
                MessageBox.Show("Из этих частей нельзя собрать компонент");
            }
        }
    }
}
