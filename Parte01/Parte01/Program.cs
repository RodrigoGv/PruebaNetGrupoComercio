using System;
using System.Collections.Generic;

namespace Parte01
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numeros = new List<int> { 5,4,7,9,2,0,1,4,7,5,3,6,1,0,2,1,0 }, pares, impares;
            new OrderRange().build(numeros, out pares, out impares);
            MoneyParts.build((decimal)0.51);
            Console.WriteLine(new ChangeString().build("*1aB"));
        }
    }
}
