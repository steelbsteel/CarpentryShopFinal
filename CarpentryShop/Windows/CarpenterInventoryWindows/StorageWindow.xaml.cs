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

namespace CarpentryShop.Windows.CarpenterInventoryWindows
{
    /// <summary>
    /// Логика взаимодействия для Storage.xaml
    /// </summary>
    public partial class StorageWindow : Window
    {
        public StorageWindow()
        {
            InitializeComponent();

            List<Products> products = App.Connection.Products.ToList();
            List<StorageProduct> storageProducts = App.Connection.StorageProduct.ToList();
            List<Products> createdProducts = new List<Products>();

            foreach (Products product in products)
            {
                foreach(StorageProduct storageProduct in storageProducts)
                {
                    if (product.idProduct == storageProduct.idProduct)
                    {
                        createdProducts.Add(product);
                    }
                }
            }

            StorageProductsList.ItemsSource = createdProducts;
        }
    }
}
