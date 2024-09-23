using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_MarvelDatabase
{
    public class clVariables
    {
        // Cadena de conexión a la base de datos MySQL
        public static string connectionString = "server=localhost;database=marvel_database;user=pruebas;password=test123456;";
        //Url a la API
        public static string urlBase = "https://gateway.marvel.com:443/v1/public/";
        //Clave pública
        public static string clavePublica = "610e21f490e8bf2d923998d0da6b22f2";
        //Clave privada
        public static string clavePrivada = "267f81e248cfa1760b64db6efe2ff98389486019";
    }
}
