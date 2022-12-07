using CarpentryShop.CarpentryShopDB;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CarpentryShop.Windows.MachinesWindow.Cnc
{
    /// <summary>
    /// Логика взаимодействия для CncWindow.xaml
    /// </summary>
    public partial class CncWindow : Window
    {
        public CncWindow()
        {
            InitializeComponent();
            MachineImage.Source = ByteToImage(App.Connection.Machines.First(x => x.idMachine == 1).ImageMachine);

            List<Materials> materialsList = new List<Materials>();

            foreach (InventoryMaterials inventoryMaterial in App.Connection.InventoryMaterials.ToList())
            {
                Materials currentMaterial = App.Connection.Materials.FirstOrDefault(x => x.idMaterial == inventoryMaterial.idMaterial);
                materialsList.Add(currentMaterial);
            }

            MaterialsInventoryList.ItemsSource = materialsList;
        }

        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        private void EventCreateDetail(object sender, RoutedEventArgs e)
        {
            Materials selectedMaterial = MaterialsInventoryList.SelectedItem as Materials;

            if (selectedMaterial != null)
            {
                if (selectedMaterial.NameMaterial == "Фанера")
                {
                    InventoryWoodDetails newDetail = new InventoryWoodDetails();
                    InventoryMaterials materialToDelete = App.Connection.InventoryMaterials.FirstOrDefault(x => x.idMaterial == selectedMaterial.idMaterial);
                    newDetail.idInventory = 1;
                    newDetail.idWoodDetail = 8;

                    App.Connection.InventoryWoodDetails.Add(newDetail);
                    App.Connection.InventoryMaterials.Remove(materialToDelete);
                    App.Connection.SaveChanges();

                    MessageBox.Show("Дно для изделия было создано");
                    var window = new CarpenterWindow();
                    window.Show();
                    this.Close();

                }
            }

            else
            {
                MessageBox.Show("Выберите материал!");
            }
        }
    }
}
