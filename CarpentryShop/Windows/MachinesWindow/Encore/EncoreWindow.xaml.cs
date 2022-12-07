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

namespace CarpentryShop.Windows.MachinesWindow.Encore
{
    /// <summary>
    /// Логика взаимодействия для EncoreWindow.xaml
    /// </summary>
    public partial class EncoreWindow : Window
    {
        public EncoreWindow()
        {
            InitializeComponent();
            MachineImage.Source = ByteToImage(App.Connection.Machines.First(x => x.idMachine == 1).ImageMachine);

            List<WoodDetails> woodDetailsList = new List<WoodDetails>();

            foreach (InventoryWoodDetails inventoryWoodDetail in App.Connection.InventoryWoodDetails.ToList())
            {
                WoodDetails currentMaterial = App.Connection.WoodDetails.FirstOrDefault(x => x.idWoodDetail == inventoryWoodDetail.idWoodDetail);
                woodDetailsList.Add(currentMaterial);
            }

            WoodDetailsInventoryList.ItemsSource = woodDetailsList;
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
            WoodDetails selectedMaterial = WoodDetailsInventoryList.SelectedItem as WoodDetails;

            if (selectedMaterial != null)
            {
                if (selectedMaterial.NameWoodDetail == "Доска 100х13")
                {
                    InventoryComponents newDetail = new InventoryComponents();
                    InventoryWoodDetails materialToDelete = App.Connection.InventoryWoodDetails.FirstOrDefault(x => x.idWoodDetail == selectedMaterial.idWoodDetail);
                    newDetail.idInventory = 1;
                    newDetail.idComponent = 1;

                    App.Connection.InventoryComponents.Add(newDetail);
                    App.Connection.InventoryWoodDetails.Remove(materialToDelete);
                    App.Connection.SaveChanges();

                    MessageBox.Show("Закругленная доска 100х13 была создана");
                    var window = new CarpenterWindow();
                    window.Show();
                    this.Close();

                }

                else if (selectedMaterial.NameWoodDetail == "Черенок")
                {
                    InventoryComponents newDetail = new InventoryComponents();
                    InventoryWoodDetails materialToDelete = App.Connection.InventoryWoodDetails.FirstOrDefault(x => x.idWoodDetail == selectedMaterial.idWoodDetail);
                    newDetail.idInventory = 1;
                    newDetail.idComponent = 2;

                    App.Connection.InventoryComponents.Add(newDetail);
                    App.Connection.InventoryWoodDetails.Remove(materialToDelete);
                    App.Connection.SaveChanges();

                    MessageBox.Show("Черенок был обработан");
                    var window = new CarpenterWindow();
                    window.Show();
                    this.Close();

                }

                else
                {
                    MessageBox.Show("Из этой детали ничего не получится на данном станке");
                }
            }

            else
            {
                MessageBox.Show("Выберите материал!");
            }
        }
    }
}
