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

namespace CarpentryShop.Windows.MachinesWindow.Trimming
{
    /// <summary>
    /// Логика взаимодействия для TrimmingWindow.xaml
    /// </summary>
    public partial class TrimmingWindow : Window
    {
        public TrimmingWindow()
        {
            InitializeComponent();

            MachineImage.Source = ByteToImage(App.Connection.Machines.FirstOrDefault(x => x.NameMachine == "Торцовка").ImageMachine);

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
            if (MaterialsInventoryList.SelectedItem != null)
            {
                List<DetailReceipts> TrimmingReceipts = App.Connection.DetailReceipts.Where(x => x.idMachine == 2).ToList();
                List<DetailReceipts> possibleReceipts = new List<DetailReceipts>();
                List<WoodDetails> woodDetails = App.Connection.WoodDetails.ToList();
                List<WoodDetails> possibleWoodDetails = new List<WoodDetails>();

                Materials selectedMaterial = MaterialsInventoryList.SelectedItem as Materials;

                foreach (DetailReceipts currentReceipt in TrimmingReceipts) //возможные рецептов с выбранным материалом
                {
                    if (currentReceipt.idMaterial == selectedMaterial.idMaterial)
                    {
                        possibleReceipts.Add(currentReceipt);
                    }
                }

                foreach (WoodDetails currentDetail in woodDetails) //возможные металлических деталей с возможными рецептами
                {
                    foreach (DetailReceipts currentReceipt in possibleReceipts)
                    {
                        if (currentDetail.idDetailReceipt == currentReceipt.idDetailReceipt)
                        {
                            possibleWoodDetails.Add(currentDetail);
                        }
                    }
                }

                if (possibleWoodDetails.Count != 0)
                {
                    var window = new PossibleWoodDetailChoose(possibleWoodDetails);
                    window.Show();
                    this.Close();
                }

                else
                {
                    MessageBox.Show("С этим материалом не получится создать деталь на этом станке");
                }

            }

            else
            {
                MessageBox.Show("Выберите материал");
            }
        }
    }
}
