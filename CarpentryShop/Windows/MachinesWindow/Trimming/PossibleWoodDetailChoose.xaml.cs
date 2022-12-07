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

namespace CarpentryShop.Windows.MachinesWindow.Trimming
{
    /// <summary>
    /// Логика взаимодействия для PossibleWoodDetailChoose.xaml
    /// </summary>
    public partial class PossibleWoodDetailChoose : Window
    {
        public PossibleWoodDetailChoose(List<WoodDetails> possibleDetails)
        {
            InitializeComponent();
            PossibleDetails.ItemsSource = possibleDetails;
        }

        private void EventCreateDetail(object sender, RoutedEventArgs e)
        {
            if (PossibleDetails.SelectedItem != null)
            {
                WoodDetails detail = PossibleDetails.SelectedItem as WoodDetails;
                WoodDetails neededDetail = App.Connection.WoodDetails.FirstOrDefault(x => x.NameWoodDetail == detail.NameWoodDetail);
                DetailReceipts receipt = App.Connection.DetailReceipts.FirstOrDefault(x => x.idDetailReceipt == neededDetail.idDetailReceipt);
                InventoryMaterials materialToDelete = App.Connection.InventoryMaterials.FirstOrDefault(x => x.idMaterial == receipt.idMaterial);

                InventoryWoodDetails inventoryDetail = new InventoryWoodDetails();
                inventoryDetail.idWoodDetail = neededDetail.idWoodDetail;
                inventoryDetail.idInventory = 1;

                App.Connection.InventoryMaterials.Remove(materialToDelete);
                App.Connection.InventoryWoodDetails.Add(inventoryDetail);
                App.Connection.SaveChanges();
                MessageBox.Show("Деталь успешно создана!");
                var window = new CarpenterWindow();
                window.Show();
                this.Close();
            }
        }
    }
}
