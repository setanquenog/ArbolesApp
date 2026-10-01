namespace ArbolesApp
{
    public class Nodo
    {
        public string Valor { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        public Nodo(string valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }
}