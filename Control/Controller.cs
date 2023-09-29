using proyectoMaquina.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoMaquina.Control
{
    public sealed class Controller
    {
        public static Controller _instance;
        public List<IProduct> ListaProductos { get; set; }
        private Controller()
        {
            ListaProductos = new List<IProduct>(); //nombre, precio, cantidad.

            ListaProductos.Add(new Consumable("CocaCola", 3200, 5));
            ListaProductos.Add(new Consumable("Snacks", 2500, 10));
            ListaProductos.Add(new Consumable("Chocolatina", 2800, 8));
        }

        public static Controller GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Controller();
            }
            return _instance;
        }

        public string DisplayProductList()
        {
            string value = "";

            foreach (IProduct product in ListaProductos)
            {
                value += product.DisplayProduct() + '\n';
            }
            return value;
        }

        public bool ProductExists(string product_name)
        {
            bool product_exists = false;

            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == product_name)
                {
                    product_exists = true;
                }
            }
            return product_exists;
        }

        public bool ProductHasInvenrory(string product_name)
        {
            bool has_inventory = false;
            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == product_name && product.Quantity > 0)
                {
                    has_inventory = true;
                }
            }
            return has_inventory;
        }

        public int getPriceByName(string nameProduct)
        {
            int value = 0;
            foreach (IProduct product in ListaProductos)
            {
                if(product.Name == nameProduct) {
                    value = product.Price;
                }
            }
            return value;
        }

        public void updateInventary(string product_name)
        {
            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == product_name)
                {
                    product.Quantity--;
                }
            }
        }

        public void updateInventary(string product_name, int count, int price)
        {
            foreach (IProduct product in ListaProductos)
            {
                if (product.Name == product_name)
                {
                    product.Quantity = product.Quantity + count;
                    product.Price = price;
                }
            }
        }

        public void addProduct(string product_name, int price,  int quantity)
        {
            ListaProductos.Add(new Consumable(product_name, quantity, price));
        }
    }
}