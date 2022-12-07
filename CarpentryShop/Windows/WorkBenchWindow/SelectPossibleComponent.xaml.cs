using CarpentryShop.CarpentryShopDB;
using CarpentryShop.Windows.ReceiptsWindows;
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
    /// Логика взаимодействия для SelectPossibleComponent.xaml
    /// </summary>
    public partial class SelectPossibleComponent : Window
    {
        public SelectPossibleComponent(List<Components> components)
        {
            InitializeComponent();

            PossibleComponents.ItemsSource = components;
        }

        private void EventCreateDetail(object sender, RoutedEventArgs e)
        {
            if (PossibleComponents.SelectedItem != null)
            {


                Components selectedComponent = PossibleComponents.SelectedItem as Components;
                Components createdComponent = App.Connection.Components.FirstOrDefault(x => x.Name == selectedComponent.Name);
                ComponentReceipts cReceipt = App.Connection.ComponentReceipts.FirstOrDefault(x => x.idCompontentReceipt == createdComponent.idComponentReceipt);

                int idMD = App.Connection.MetalDetails.FirstOrDefault(x => x.idMetalDetail == cReceipt.idMetalDetail).idMetalDetail;
                int idWD = App.Connection.WoodDetails.FirstOrDefault(x => x.idWoodDetail == cReceipt.idWoodDetail).idWoodDetail;
                int idT = App.Connection.Tools.FirstOrDefault(x => x.idTool == cReceipt.idTool).idTool;

                InventoryMetalDetails invMD = App.Connection.InventoryMetalDetails.FirstOrDefault(x => x.idMetalDetail == idMD);
                InventoryWoodDetails invWD = App.Connection.InventoryWoodDetails.FirstOrDefault(x => x.idWoodDetail == idWD);
                InventoryTools invT = App.Connection.InventoryTools.FirstOrDefault(x => x.idTool == idT);
                InventoryComponents invComp = new InventoryComponents();
                invComp.idInventory = 1;
                invComp.idComponent = createdComponent.idComponent;


                App.Connection.InventoryComponents.Add(invComp);
                App.Connection.InventoryMetalDetails.Remove(invMD);
                App.Connection.InventoryWoodDetails.Remove(invWD);
                App.Connection.SaveChanges();

                var window = new CarpenterWindow();
                window.Show();
                this.Close();
                MessageBox.Show("Компонент создан");
            }

            else
            {
                MessageBox.Show("Выберите компонент");
            }

        }
    }
}
