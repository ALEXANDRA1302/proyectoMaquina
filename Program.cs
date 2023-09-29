using proyectoMaquina.Control;
using proyectoMaquina.Model;
using System;

namespace ProyectoMaquina.View
{
    internal class View
    {
        static void Main(string[] args)
        {
            //Aqui empieza el Programa
            Controller controller = Controller.GetInstance();

            string texto_bienvenida = "Bienvenido a la Maquina Expendora...";
            Console.WriteLine(texto_bienvenida);
            string input_cliente = "";

            string input_producto = "";

            while (true)
            {
                do
                {
                    Console.WriteLine("Escoja Tipo de Cliente: [C] o [P]");
                    input_cliente = Console.ReadLine();

                } while (input_cliente != "C" && input_cliente != "P"); //

                Console.WriteLine("La Lista de Productos es: "); //TO-DO: Lista de Productos

                //FOR Para imprimir Productos con Controller
                Console.WriteLine(controller.DisplayProductList());


                if (input_cliente == "C")
                {
                    Console.WriteLine("Escoja un Producto de la Lista...");

                    bool valid_product = false;
                    do
                    {
                        input_producto = Console.ReadLine();
                        valid_product = controller.ProductExists(input_producto) && controller.ProductHasInvenrory(input_producto);

                        if (valid_product == false)
                        {
                            Console.WriteLine("Escoja un Producto Valido: ");
                        }
                    } while (!valid_product);


                    Console.WriteLine("Ingrese los Billetes para el pago del Producto: ");
                    int suma_billete = 0;
                    bool moneyValid = true;
                    while (moneyValid)
                    {

                        Console.WriteLine("Ingrese el Billete...:");

                        try
                        {

                            suma_billete += Convert.ToInt32(Console.ReadLine());
                            int price = controller.getPriceByName(input_producto);
                            int vuelto = suma_billete - price;
                            if (vuelto > 0)
                            {
                                Console.WriteLine($"Tu cambio es {vuelto}");
                                controller.updateInventary(input_producto);
                                moneyValid = false;

                            }
                            else
                            {
                                Console.WriteLine($"Tu saldo actual es: {suma_billete}, por lo tanto, debes ingresar más billetes");
                            }

                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine($"Por favor, ingrese un dato con valor numerico: {e.Message}");
                        }

                        /*
                        Console.WriteLine("Para Dejar de Ingresar Billetes, escriba: [STOP] de lo contario presione ENTER");
                        string input_cash = Console.ReadLine();
                        if (input_cash == "STOP")
                        {
                            break;
                        }
                        */
                    }


                    //Comparar precio producto vs suma_billete. Hacer Calculo

                }
                else if (input_cliente == "P")
                {
                    Console.WriteLine("Ingrese el nombre del producto?");
                    input_producto = Console.ReadLine();
                    bool cantidadValida = true;
                    while (cantidadValida)
                    {
                        Console.WriteLine("Ingrese la cantidad del producto?");

                        try
                        {

                            int cantidad = Convert.ToInt32(Console.ReadLine());
                            cantidadValida = false;

                            bool precioValida = true;
                            while (precioValida)
                            {
                                Console.WriteLine("Ingrese el precio del producto?");

                                try
                                {

                                    int precio = Convert.ToInt32(Console.ReadLine());
                                    precioValida = false;

                                    if (controller.ProductExists(input_producto))
                                    {
                                        controller.updateInventary(input_producto, cantidad, precio);
                                    }
                                    else
                                    {
                                        controller.addProduct(input_producto, cantidad, precio);
                                    }
                                }
                                catch (FormatException e)
                                {
                                    Console.WriteLine($"Por favor, ingrese un dato con valor numerico: {e.Message}");
                                }
                            }



                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine($"Por favor, ingrese un dato con valor numerico: {e.Message}");
                        }
                    }
                }
            }
        }
    }
}