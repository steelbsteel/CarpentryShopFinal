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
    /// Логика взаимодействия для ProductWorkBench.xaml
    /// </summary>
    public partial class ProductWorkBench : Window
    {
        public ProductWorkBench()
        {
            InitializeComponent();
            List<Components> components = App.Connection.Components.ToList();

            List<Components> equippedComponents = new List<Components>();

            List<InventoryComponents> inventoryComponents= App.Connection.InventoryComponents.ToList();

            foreach(Components c in components) 
            {
                foreach(InventoryComponents ic in inventoryComponents)
                {
                    if (c.idComponent == ic.idComponent)
                    {
                        equippedComponents.Add(c);
                    }
                }
            }

            ComponentsListBox1.ItemsSource = equippedComponents;
            ComponentsListBox2.ItemsSource = equippedComponents;
        }

        private void EventCreateProduct(object sender, RoutedEventArgs e)
        {

            bool isFirstComp = false;

            if (ComponentsListBox1.SelectedItem != null && ComponentsListBox2.SelectedItem != null)
            {
                Components Comp1 = ComponentsListBox1.SelectedItem as Components;
                Components Comp2 = ComponentsListBox2.SelectedItem as Components;
                Components selectedComponent1 = App.Connection.Components.FirstOrDefault(x => x.Name == Comp1.Name);
                Components selectedComponent2 = App.Connection.Components.FirstOrDefault(x => x.Name == Comp2.Name);
                InventoryComponents invComp1 = App.Connection.InventoryComponents.FirstOrDefault(x => x.idComponent == selectedComponent1.idComponent);
                InventoryComponents invComp2 = App.Connection.InventoryComponents.FirstOrDefault(x => x.idComponent == selectedComponent2.idComponent);

                if (selectedComponent1 != selectedComponent2)
                {
                    foreach(ProductReceipts receipt in App.Connection.ProductReceipts)
                    {
                        if(receipt.idFirstComponent == selectedComponent1.idComponent)
                        {
                            isFirstComp = true;
                        }
                    }

                    if (isFirstComp)
                    {
                        ProductReceipts pReceipt = new ProductReceipts();

                        foreach (ProductReceipts receipt in App.Connection.ProductReceipts)
                        {
                            if (receipt.idFirstComponent == selectedComponent1.idComponent &&
                               receipt.idSecondComponent == selectedComponent2.idComponent)
                            {
                                pReceipt = receipt;
                            }
                        }
                        if (pReceipt.idFirstComponent != null && pReceipt.idSecondComponent != null)
                        {
                            Products product = App.Connection.Products.FirstOrDefault(x => x.idProductReceipt == pReceipt.idProductReceipt);

                            StorageProduct productStorage = new StorageProduct();
                            productStorage.idStorage = 1;
                            productStorage.idProduct = product.idProduct;

                            App.Connection.StorageProduct.Add(productStorage);
                            App.Connection.InventoryComponents.Remove(invComp1);
                            App.Connection.InventoryComponents.Remove(invComp2);
                            App.Connection.SaveChanges();

                            var window = new CarpenterWindow();
                            window.Show();
                            this.Close();
                            MessageBox.Show("Изделие было создано и добавлен на склад");
                        }

                        else
                        {
                            MessageBox.Show("Нельзя создать изделие из этих компонентов");
                        }
                    }

                    else
                    {
                        ProductReceipts pReceipt = new ProductReceipts();

                        foreach (ProductReceipts receipt in App.Connection.ProductReceipts)
                        {
                            if (receipt.idFirstComponent == selectedComponent2.idComponent &&
                               receipt.idSecondComponent == selectedComponent1.idComponent)
                            {
                                pReceipt = receipt;
                            }
                        }

                        if (pReceipt.idFirstComponent != null && pReceipt.idSecondComponent != null)
                        {
                            Products product = App.Connection.Products.FirstOrDefault(x => x.idProductReceipt == pReceipt.idProductReceipt);

                            StorageProduct productStorage = new StorageProduct();
                            productStorage.idStorage = 1;
                            productStorage.idProduct = product.idProduct;

                            App.Connection.StorageProduct.Add(productStorage);
                            App.Connection.InventoryComponents.Remove(invComp1);
                            App.Connection.InventoryComponents.Remove(invComp2);
                            App.Connection.SaveChanges();

                            var window = new CarpenterWindow();
                            window.Show();
                            this.Close();
                            MessageBox.Show("Изделие было создано и добавлен на склад");
                        }

                        else
                        {
                            MessageBox.Show("Нельзя создать изделие из этих компонентов");
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Вы выбрали одинаковые компоненты...");
                }
            }


            else
            {
                MessageBox.Show("Выберите компоненты!");
            }
        }
    }
}
