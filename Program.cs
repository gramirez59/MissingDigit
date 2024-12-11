namespace MissingDigit
{
    public class Program
    {
        public static string MissingDigit(string str)
        {
            string response = string.Empty;

            str = str.Replace(" ", "");
            str = str.ToLower();

            double valorX = EncontrarNumeros(str);
            if (!ValidarXMedio(str))
                response = "El valor de X es: " + valorX.ToString();
            else
                response = "El valor de X es: " + DeterminarNumeroFaltante(str, valorX).ToString();

            return response;
        }

        public static double EncontrarNumeros(string str)
        {
            int positionOfX = str.IndexOf('x');
            int positionOfEqual = str.IndexOf('=');
            int positionOfOperator = GetPositionOperator(str);
            int resultado;
            double valorX = 0;

            if (positionOfX < positionOfOperator)
            {
                string resulString = str.Substring(positionOfEqual + 1);
                resultado = int.Parse(resulString);
                string segundoNumeroString = str.Substring(positionOfOperator + 1, (positionOfEqual - 1 - positionOfOperator));
                int segundoNumero = int.Parse(segundoNumeroString);
                valorX = OperarNumeros(str[positionOfOperator].ToString(), null, segundoNumero, resultado);
            }

            if ((positionOfX > positionOfOperator) && (positionOfX < positionOfEqual))
            {
                string resulString = str.Substring(positionOfEqual + 1);
                resultado = int.Parse(resulString);
                string primerNumeroString = str.Substring(0, (positionOfOperator));
                int primerNumero = int.Parse(primerNumeroString);
                valorX = OperarNumeros(str[positionOfOperator].ToString(), primerNumero, null, resultado);
            }

            if (positionOfX > positionOfEqual)
            {
                string primerNumeroString = str.Substring(0, (positionOfOperator));
                int primerNumero = int.Parse(primerNumeroString);
                string segundoNumeroString = str.Substring(positionOfOperator + 1, (positionOfEqual - 1 - positionOfOperator));
                int segundoNumero = int.Parse(segundoNumeroString);
                valorX = OperarNumeros(str[positionOfOperator].ToString(), primerNumero, segundoNumero, null);
            }

            return valorX;
        }

        public static double OperarNumeros(string operacion, int? primerNumero = null, int? segundoNumero = null, int? resultado = null)
        {
            double resultadoOperacion = 0;
            if (operacion.Equals("+"))
            {
                if (resultado != null && segundoNumero != null)
                {
                    resultadoOperacion = resultado.Value - segundoNumero.Value;
                }
                if (resultado != null && primerNumero != null)
                {
                    resultadoOperacion = resultado.Value - primerNumero.Value;
                }
                if (primerNumero != null && segundoNumero != null)
                {
                    resultadoOperacion = primerNumero.Value + segundoNumero.Value;
                }
            }

            if (operacion.Equals("-"))
            {
                if (resultado != null && segundoNumero != null)
                {
                    resultadoOperacion = resultado.Value + segundoNumero.Value;
                }
                if (resultado != null && primerNumero != null)
                {
                    resultadoOperacion = primerNumero.Value - resultado.Value;
                }
                if (primerNumero != null && segundoNumero != null)
                {
                    if (primerNumero >= segundoNumero)
                        resultadoOperacion = primerNumero.Value - segundoNumero.Value;
                    else
                        resultadoOperacion = segundoNumero.Value - primerNumero.Value;
                }
            }

            if (operacion.Equals("*"))
            {
                if (resultado != null && segundoNumero != null)
                {
                    resultadoOperacion = resultado.Value / segundoNumero.Value;
                }
                if (resultado != null && primerNumero != null)
                {
                    resultadoOperacion = resultado.Value / primerNumero.Value;
                }
                if (primerNumero != null && segundoNumero != null)
                {
                    resultadoOperacion = primerNumero.Value * segundoNumero.Value;
                }
            }

            if (operacion.Equals("/"))
            {
                if (resultado != null && segundoNumero != null)
                {
                    resultadoOperacion = resultado.Value * segundoNumero.Value;
                }
                if (resultado != null && primerNumero != null)
                {
                    resultadoOperacion = primerNumero.Value / resultado.Value;
                }
                if (primerNumero != null && segundoNumero != null)
                {
                    resultadoOperacion = primerNumero.Value / segundoNumero.Value;
                }
            }

            return resultadoOperacion;
        }

        public static bool ValidarXMedio(string str)
        {
            int positionOfX = str.IndexOf('x');
            int positionOfEqual = str.IndexOf('=');
            int positionOfOperator = GetPositionOperator(str);

            if ((positionOfX < positionOfOperator) && positionOfOperator > 1)//Quiere decir que el número donde está X es de más de un digito
                return true;
            
            if ((positionOfX > positionOfOperator) && (positionOfX < positionOfEqual) && (positionOfEqual - positionOfOperator > 2))
                return true;
            
            if (positionOfX > positionOfEqual)
            {
                int posicionFinal = str.Length - 1;
                if (posicionFinal - positionOfEqual > 1)
                    return true;
            }

            return false;
        }

        public static int DeterminarNumeroFaltante(string str, double valorX)
        {
            int positionOfX = str.IndexOf('x');
            int positionOfEqual = str.IndexOf('=');
            int positionOfOperator = GetPositionOperator(str);
            string valorXTexto = valorX.ToString();
            int respuesta = 0;

            if (positionOfX < positionOfOperator && positionOfOperator > 1)
            {                
                string textoInmersoX = str.Substring(0, positionOfOperator);                    

                for (int i = 0; i < textoInmersoX.Length; i++)
                {
                    if (textoInmersoX[i].ToString().Equals("x"))
                    {
                        respuesta = int.Parse(valorXTexto[i].ToString());
                        break;
                    }                            
                }                
            }

            if ((positionOfX > positionOfOperator) && (positionOfX < positionOfEqual) && (positionOfEqual - positionOfOperator > 2))
            {                
                string textoInmersoX = str.Substring(positionOfOperator + 1, (positionOfEqual - 1 - positionOfOperator));

                for (int i = 0; i < textoInmersoX.Length; i++)
                {
                    if (textoInmersoX[i].ToString().Equals("x"))
                    {
                        respuesta = int.Parse(valorXTexto[i].ToString());
                        break;
                    }                            
                }                
            }

            if (positionOfX > positionOfEqual)
            {
                int posicionFinal = str.Length - 1;
                if (posicionFinal - positionOfEqual > 1)
                {
                    string textoInmersoX = str.Substring(positionOfEqual + 1, (posicionFinal - positionOfEqual));

                    for (int i = 0; i < textoInmersoX.Length; i++)
                    {
                        if (textoInmersoX[i].ToString().Equals("x"))
                        {
                            respuesta = int.Parse(valorXTexto[i].ToString());
                            break;
                        }                            
                    }
                }
            }

            return respuesta;
        }

        public static int GetPositionOperator(string str)
        {
            int positionOfOperator = 0;

            if (str.Contains('+'))
                positionOfOperator = str.IndexOf('+');

            if (str.Contains('-'))
                positionOfOperator = str.IndexOf('-');

            if (str.Contains('*'))
                positionOfOperator = str.IndexOf('*');

            if (str.Contains('/'))
                positionOfOperator = str.IndexOf('/');

            return positionOfOperator;
        }

        static void Main(string[] args)
        {
            ///Console.WriteLine(MissingDigit(Console.ReadLine()));
            var texto = string.Empty;
            
            while (!texto.Equals("salir"))
            {
                texto = Console.ReadLine();
                if (texto.ToLower().Equals("salir"))
                    break;
                texto = MissingDigit(texto);
                Console.WriteLine(texto);
            }
        }
    }
}