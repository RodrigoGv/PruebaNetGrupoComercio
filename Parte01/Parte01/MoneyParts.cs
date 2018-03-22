using System;
using System.Collections.Generic;

namespace Parte01
{
    public class MoneyParts
    {
        static List<decimal> denominaciones = new List<decimal>() { 200, 100, 50, 20, 10, 5, 2, 1, (decimal)0.5, (decimal)0.2, (decimal)0.1, (decimal)0.05 };
        public static decimal[][] build(decimal numero)
        {
            if (numero % (decimal)0.05 != 0) return new decimal[0][];
            var soluciones = new List<decimal[]>();
            CambioDeMaquinaDeMonedas(numero, 0, new decimal[0], soluciones);
            return soluciones.ToArray();
        }


        public static void CambioDeMaquinaDeMonedas(decimal numero, int indiceDenominaciones, decimal[] solucionActual, List<decimal[]> soluciones)
        {
            if (numero == 0)
            {
                soluciones.Add(solucionActual);
                return;
            }

            for (int i = indiceDenominaciones; i < denominaciones.Count; i++)
            {
                if (denominaciones[i] > numero) continue;
                var myArray = new decimal[solucionActual.Length + 1];
                myArray[solucionActual.Length] = denominaciones[i];
                Array.Copy(solucionActual, myArray, solucionActual.Length);
                CambioDeMaquinaDeMonedas(numero - denominaciones[i], i, myArray, soluciones);
            }
        }
    }
}
