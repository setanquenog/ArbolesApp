using System;
using System.Collections.Generic;

namespace ArbolesApp
{
    public class Arbol
    {
        public Nodo Raiz { get; set; }

        public Arbol()
        {
            Raiz = null;
        }

        public void Insertar(string valor)
        {
            Raiz = InsertarRecursivo(Raiz, valor);
        }

        private Nodo InsertarRecursivo(Nodo raiz, string valor)
        {
            if (raiz == null)
            {
                return new Nodo(valor);
            }

            int comparacion = string.Compare(valor, raiz.Valor, StringComparison.OrdinalIgnoreCase);

            if (comparacion < 0)
            {
                raiz.Izquierdo = InsertarRecursivo(raiz.Izquierdo, valor);
            }
            else if (comparacion > 0)
            {
                raiz.Derecho = InsertarRecursivo(raiz.Derecho, valor);
            }

            return raiz;
        }

        public Nodo Buscar(string valor)
        {
            return BuscarRecursivo(Raiz, valor);
        }

        private Nodo BuscarRecursivo(Nodo raiz, string valor)
        {
            if (raiz == null || string.Equals(raiz.Valor, valor, StringComparison.OrdinalIgnoreCase))
            {
                return raiz;
            }

            if (string.Compare(valor, raiz.Valor, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return BuscarRecursivo(raiz.Izquierdo, valor);
            }

            return BuscarRecursivo(raiz.Derecho, valor);
        }

        public void ImprimirRutaBusqueda(string valor)
        {
            Nodo actual = Raiz;
            Console.Write("Ruta: ");

            while (actual != null)
            {
                Console.Write(actual.Valor);

                int comparacion = string.Compare(valor, actual.Valor, StringComparison.OrdinalIgnoreCase);

                if (comparacion == 0)
                {
                    Console.WriteLine(" -> Encontrado");
                    return;
                }

                Console.Write(" -> ");

                if (comparacion < 0)
                {
                    actual = actual.Izquierdo;
                }
                else
                {
                    actual = actual.Derecho;
                }
            }

            Console.WriteLine("No encontrado");
        }

        public void Eliminar(string valor)
        {
            Raiz = EliminarRecursivo(Raiz, valor);
        }

        private Nodo EliminarRecursivo(Nodo raiz, string valor)
        {
            if (raiz == null) return null;

            int comparacion = string.Compare(valor, raiz.Valor, StringComparison.OrdinalIgnoreCase);

            if (comparacion < 0)
            {
                raiz.Izquierdo = EliminarRecursivo(raiz.Izquierdo, valor);
            }
            else if (comparacion > 0)
            {
                raiz.Derecho = EliminarRecursivo(raiz.Derecho, valor);
            }
            else
            {
                if (raiz.Izquierdo == null) return raiz.Derecho;
                if (raiz.Derecho == null) return raiz.Izquierdo;

                raiz.Valor = ValorMinimo(raiz.Derecho);
                raiz.Derecho = EliminarRecursivo(raiz.Derecho, raiz.Valor);
            }

            return raiz;
        }

        private string ValorMinimo(Nodo raiz)
        {
            string min = raiz.Valor;
            while (raiz.Izquierdo != null)
            {
                min = raiz.Izquierdo.Valor;
                raiz = raiz.Izquierdo;
            }
            return min;
        }

        public void InOrden(Nodo nodo)
        {
            if (nodo != null)
            {
                InOrden(nodo.Izquierdo);
                Console.Write(nodo.Valor + " ");
                InOrden(nodo.Derecho);
            }
        }

        public void PreOrden(Nodo nodo)
        {
            if (nodo != null)
            {
                Console.Write(nodo.Valor + " ");
                PreOrden(nodo.Izquierdo);
                PreOrden(nodo.Derecho);
            }
        }

        public void PostOrden(Nodo nodo)
        {
            if (nodo != null)
            {
                PostOrden(nodo.Izquierdo);
                PostOrden(nodo.Derecho);
                Console.Write(nodo.Valor + " ");
            }
        }

        public int Altura(Nodo nodo)
        {
            if (nodo == null) return -1;
            int altIzq = Altura(nodo.Izquierdo);
            int altDer = Altura(nodo.Derecho);
            return Math.Max(altIzq, altDer) + 1;
        }

        public int ContarNodos(Nodo nodo)
        {
            if (nodo == null) return 0;
            return 1 + ContarNodos(nodo.Izquierdo) + ContarNodos(nodo.Derecho);
        }

        public int ContarHojas(Nodo nodo)
        {
            if (nodo == null) return 0;
            if (nodo.Izquierdo == null && nodo.Derecho == null) return 1;
            return ContarHojas(nodo.Izquierdo) + ContarHojas(nodo.Derecho);
        }

        public string Minimo(Nodo nodo)
        {
            if (nodo == null) return "Vacio";
            while (nodo.Izquierdo != null) nodo = nodo.Izquierdo;
            return nodo.Valor;
        }

        public string Maximo(Nodo nodo)
        {
            if (nodo == null) return "Vacio";
            while (nodo.Derecho != null) nodo = nodo.Derecho;
            return nodo.Valor;
        }

        public bool EsBST(Nodo nodo, string min, string max)
        {
            if (nodo == null) return true;

            if ((min != null && string.Compare(nodo.Valor, min, StringComparison.OrdinalIgnoreCase) <= 0) ||
                (max != null && string.Compare(nodo.Valor, max, StringComparison.OrdinalIgnoreCase) >= 0))
            {
                return false;
            }

            return EsBST(nodo.Izquierdo, min, nodo.Valor) && EsBST(nodo.Derecho, nodo.Valor, max);
        }

        public void Balancear()
        {
            List<Nodo> nodos = new List<Nodo>();
            GuardarNodos(Raiz, nodos);
            Raiz = ConstruirBalanceado(nodos, 0, nodos.Count - 1);
        }

        private void GuardarNodos(Nodo nodo, List<Nodo> nodos)
        {
            if (nodo == null) return;
            GuardarNodos(nodo.Izquierdo, nodos);
            nodos.Add(nodo);
            GuardarNodos(nodo.Derecho, nodos);
        }

        private Nodo ConstruirBalanceado(List<Nodo> nodos, int inicio, int fin)
        {
            if (inicio > fin) return null;

            int medio = (inicio + fin) / 2;
            Nodo nodo = nodos[medio];

            nodo.Izquierdo = ConstruirBalanceado(nodos, inicio, medio - 1);
            nodo.Derecho = ConstruirBalanceado(nodos, medio + 1, fin);

            return nodo;
        }

        public void Imprimir(Nodo nodo, string espacio = "", bool esUltimo = true)
        {
            if (nodo != null)
            {
                Console.Write(espacio);
                if (esUltimo)
                {
                    Console.Write("+-- ");
                    espacio += "    ";
                }
                else
                {
                    Console.Write("|-- ");
                    espacio += "|   ";
                }

                Console.WriteLine(nodo.Valor);

                Imprimir(nodo.Izquierdo, espacio, false);
                Imprimir(nodo.Derecho, espacio, true);
            }
        }
    }
}