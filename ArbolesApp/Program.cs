using System;
using System.Diagnostics;
using System.IO;

namespace ArbolesApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Arbol arbol = new Arbol();
            string archivoActual = "ninguno";
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine();
                Console.WriteLine("Gestion de Arboles Binarios de Busqueda");
                Console.WriteLine("Archivo cargado: " + archivoActual);
                Console.WriteLine("1. Cargar ciudades.txt");
                Console.WriteLine("2. Cargar personas.txt");
                Console.WriteLine("3. Ver grafica del arbol");
                Console.WriteLine("4. Reporte y metricas");
                Console.WriteLine("5. Recorridos");
                Console.WriteLine("6. Buscar valor");
                Console.WriteLine("7. Insertar valor");
                Console.WriteLine("8. Eliminar valor");
                Console.WriteLine("9. Balancear arbol");
                Console.WriteLine("0. Salir");
                Console.Write("Opcion: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        archivoActual = "ciudades.txt";
                        arbol = LeerArchivo(archivoActual);
                        Console.WriteLine("Estructura cargada:");
                        arbol.Imprimir(arbol.Raiz);
                        break;

                    case "2":
                        archivoActual = "personas.txt";
                        arbol = LeerArchivo(archivoActual);
                        Console.WriteLine("Estructura cargada:");
                        arbol.Imprimir(arbol.Raiz);
                        break;

                    case "3":
                        if (arbol.Raiz == null)
                        {
                            Console.WriteLine("Arbol vacio.");
                        }
                        else
                        {
                            arbol.Imprimir(arbol.Raiz);
                        }
                        break;

                    case "4":
                        GenerarReporte(arbol);
                        break;

                    case "5":
                        if (arbol.Raiz == null)
                        {
                            Console.WriteLine("Arbol vacio.");
                            break;
                        }
                        Console.Write("In-Orden: ");
                        arbol.InOrden(arbol.Raiz);
                        Console.WriteLine();

                        Console.Write("Pre-Orden: ");
                        arbol.PreOrden(arbol.Raiz);
                        Console.WriteLine();

                        Console.Write("Post-Orden: ");
                        arbol.PostOrden(arbol.Raiz);
                        Console.WriteLine();
                        break;

                    case "6":
                        if (arbol.Raiz == null)
                        {
                            Console.WriteLine("Arbol vacio.");
                            break;
                        }
                        Console.Write("Nombre a buscar: ");
                        string buscado = Console.ReadLine();

                        Stopwatch swBusqueda = Stopwatch.StartNew();
                        arbol.ImprimirRutaBusqueda(buscado);
                        swBusqueda.Stop();
                        Console.WriteLine("Tiempo de busqueda: " + swBusqueda.ElapsedTicks + " ticks (" + swBusqueda.Elapsed.TotalMilliseconds + " ms)");
                        break;

                    case "7":
                        Console.Write("Nombre a insertar: ");
                        string nuevo = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nuevo))
                        {
                            arbol.Insertar(nuevo.Trim());
                            Console.WriteLine("Insertado.");
                        }
                        break;

                    case "8":
                        if (arbol.Raiz == null)
                        {
                            Console.WriteLine("Arbol vacio.");
                            break;
                        }
                        Console.Write("Nombre a eliminar: ");
                        string borrar = Console.ReadLine();
                        arbol.Eliminar(borrar);
                        Console.WriteLine("Eliminado.");
                        break;

                    case "9":
                        if (arbol.Raiz == null)
                        {
                            Console.WriteLine("Arbol vacio.");
                            break;
                        }
                        Console.WriteLine("Altura antes: " + arbol.Altura(arbol.Raiz));
                        Stopwatch swBalance = Stopwatch.StartNew();
                        arbol.Balancear();
                        swBalance.Stop();
                        Console.WriteLine("Altura despues: " + arbol.Altura(arbol.Raiz));
                        Console.WriteLine("Tiempo de balanceo: " + swBalance.ElapsedTicks + " ticks (" + swBalance.Elapsed.TotalMilliseconds + " ms)");
                        Console.WriteLine("Nueva grafica:");
                        arbol.Imprimir(arbol.Raiz);
                        break;

                    case "0":
                        continuar = false;
                        Console.WriteLine("Saliendo del programa.");
                        break;

                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }
            }
        }

        static Arbol LeerArchivo(string nombreArchivo)
        {
            Arbol nuevoArbol = new Arbol();

            if (!File.Exists(nombreArchivo))
            {
                Console.WriteLine("Archivo no encontrado: " + nombreArchivo);
                return nuevoArbol;
            }

            Stopwatch sw = Stopwatch.StartNew();
            string[] lineas = File.ReadAllLines(nombreArchivo);
            for (int i = 0; i < lineas.Length; i++)
            {
                string linea = lineas[i].Trim();
                if (!string.IsNullOrEmpty(linea))
                {
                    nuevoArbol.Insertar(linea);
                }
            }
            sw.Stop();

            Console.WriteLine("Nodos cargados: " + nuevoArbol.ContarNodos(nuevoArbol.Raiz));
            Console.WriteLine("Tiempo de carga: " + sw.ElapsedTicks + " ticks (" + sw.Elapsed.TotalMilliseconds + " ms)");
            return nuevoArbol;
        }

        static void GenerarReporte(Arbol arbol)
        {
            if (arbol.Raiz == null)
            {
                Console.WriteLine("El arbol no tiene datos.");
                return;
            }

            Console.WriteLine("Metricas del arbol:");
            Console.WriteLine("- Total nodos: " + arbol.ContarNodos(arbol.Raiz));
            Console.WriteLine("- Hojas: " + arbol.ContarHojas(arbol.Raiz));
            Console.WriteLine("- Altura: " + arbol.Altura(arbol.Raiz));
            Console.WriteLine("- Minimo alfabetico: " + arbol.Minimo(arbol.Raiz));
            Console.WriteLine("- Maximo alfabetico: " + arbol.Maximo(arbol.Raiz));
            Console.WriteLine("- Es BST valido: " + (arbol.EsBST(arbol.Raiz, null, null) ? "Si" : "No"));
        }
    }
}