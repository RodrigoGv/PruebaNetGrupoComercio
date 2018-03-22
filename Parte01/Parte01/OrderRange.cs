using System.Collections.Generic;

namespace Parte01
{
    class OrderRange
    {
        public void build(List<int> coleccion, out List<int> pares, out List<int> impares)
        {
            pares = new List<int>(); impares = new List<int>();
            foreach(var numero in coleccion) { if (numero % 2 == 0) pares.Add(numero); else impares.Add(numero); }
            pares.Sort();
            impares.Sort();
        }
    }
}
