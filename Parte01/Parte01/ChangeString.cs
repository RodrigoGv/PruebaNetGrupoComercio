namespace Parte01
{
    class ChangeString
    {
        public string build(string texto)
        {
            var resultado = ""; var abc = "abcdefghijklmnñopqrstuvwxyza"; int i;
            foreach (var letra in texto)
            {
                i = abc.IndexOf(letra);
                resultado += i > 0 ? char.IsUpper(letra) ? char.ToUpper(abc[i]) : abc[i] : letra;
            }
            return resultado;
        }
    }
}
