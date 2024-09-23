using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using PruebaTecnica_MarvelDatabase.Objetos;
using System.Diagnostics;
using System.Text;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace PruebaTecnica_MarvelDatabase.Clases
{
    internal class clConexionApi
    {
        private static readonly int limit = 100;

        #region MÉTODOS GENÉRICOS

        //Método para generar el hash necesario en la llamada
        private static string GenerateMd5Hash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        // Método para obtener el número total de elementos de cada llamada, para controlar la paginación ya que el máximo es 100
        public async Task<int> ObtenerTotalElementos(string operacion, string parametros = "")
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string timestamp = DateTime.Now.Ticks.ToString();
                    string hash = GenerateMd5Hash(timestamp + clVariables.clavePrivada + clVariables.clavePublica);
                    string url = $"{clVariables.urlBase}{operacion}?{parametros}&ts={timestamp}&apikey={clVariables.clavePublica}&hash={hash}&limit=1";
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Verificamos si la solicitud fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(jsonResponse))
                        {
                            JObject json = JObject.Parse(jsonResponse);

                            // Verificamos que "code" y "data" no sean nulos
                            if (json["code"] != null && json["data"] != null)
                            {
                                int code = json["code"].ToObject<int>();

                                // Comprobamos que el código sea 200 (éxito)
                                if (code == 200)
                                {
                                    // Verificamos que "total" no sea nulo
                                    if (json["data"]["total"] != null)
                                    {
                                        int total = json["data"]["total"].ToObject<int>();
                                        return total;
                                    }
                                    else
                                    {
                                        MessageBox.Show("El campo 'total' es nulo en la respuesta.");
                                        return -1; // Indicamos error en caso de total nulo
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"Error en la respuesta de la API. Código: {code}");
                                    return -1;
                                }
                            }
                            else
                            {
                                MessageBox.Show("El campo 'code' o 'data' es nulo en la respuesta.");
                                return -1;
                            }
                        }
                        else
                        {
                            MessageBox.Show("La respuesta de la API está vacía.");
                            return -1;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Error al conectar con la API. Código de estado: {response.StatusCode}");
                        return -1;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Error en la solicitud HTTP: {e.Message}");
                return -1;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error inesperado: {e.Message}");
                return -1;
            }
        }

        // Método para obtener datos de la API con paginación
        static async Task<JObject> ObtenerDatosAPIAsync(string operacion, int offset, string parametros = "")
        {
            using (HttpClient client = new HttpClient())
            {
                string timestamp = DateTime.Now.Ticks.ToString();
                string hash = GenerateMd5Hash(timestamp + clVariables.clavePrivada + clVariables.clavePublica);
                string url = $"{clVariables.urlBase}{operacion}?ts={timestamp}&apikey={clVariables.clavePublica}&hash={hash}&limit={limit}&offset={offset}";
                if (parametros != "")
                {
                    url = $"{clVariables.urlBase}{operacion}?{parametros}&ts={timestamp}&apikey={clVariables.clavePublica}&hash={hash}&limit={limit}&offset={offset}";
                }
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(jsonResponse);

                    if (json["code"].ToObject<int>() == 200)
                    {
                        return json;
                    }
                    else
                    {
                        throw new Exception("Error en la respuesta de la API");
                    }
                }
                else
                {
                    throw new Exception("Error al conectar con la API");
                }
            }
        }

        #endregion

        #region MÉTODOS OBTENER E INSERTAR DATOS

        // Método para obtener todos los personajes paginando y almacenarlos en la base de datos
        public async Task getTodosLosPersonajesAsync(int totalPersonajes)
        {
            using (MySqlConnection conn = new MySqlConnection(clVariables.connectionString))
            {
                clMySQL miSQL = new clMySQL();
                await conn.OpenAsync();

                using (var transaction = await conn.BeginTransactionAsync())
                {
                    try
                    {
                        int offset = 0;

                        // Realizar solicitudes paginadas hasta obtener todos los personajes
                        while (offset < totalPersonajes)
                        {
                            var personajesJson = await ObtenerDatosAPIAsync("characters", offset);
                            var personajes = personajesJson["data"]["results"];

                            foreach (var personaje in personajes)
                            {
                                oCharacter _character = new oCharacter();
                                _character.Id = personaje["id"] != null ? personaje["id"].ToObject<int>() : 0;
                                _character.Name = personaje["name"] != null ? personaje["name"].ToString() : string.Empty;
                                _character.Description = personaje["description"] != null ? personaje["description"].ToString() : string.Empty;
                                _character.Total_Comics = personaje["comics"] != null && personaje["comics"]["available"] != null
                                                          ? personaje["comics"]["available"].ToObject<int>() : 0;

                                if (_character.Id > 0)
                                {
                                    await miSQL.insertCharacter(_character, conn, transaction);
                                }
                            }

                            // Incrementar el offset para la siguiente llamada
                            offset += limit;
                        }

                        await transaction.CommitAsync(); // Confirmar transacción
                        Console.WriteLine("Datos de personajes insertados correctamente.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(); // Revertir transacción en caso de error
                        MessageBox.Show($"Error durante la inserción de personajes: {ex.Message}");
                    }
                }
            }
        }

        // Método para obtener todos los eventos paginando (si fuera necesario) y almacenarlos en la base de datos
        public async Task getTodosLosEventosAsync(int totalEventos)
        {
            using (MySqlConnection conn = new MySqlConnection(clVariables.connectionString))
            {
                clMySQL miSQL = new clMySQL();
                await conn.OpenAsync();

                using (var transaction = await conn.BeginTransactionAsync())
                {
                    try
                    {
                        int offset = 0;

                        // Realizar solicitudes paginadas hasta obtener todos los eventos
                        while (offset < totalEventos)
                        {
                            var eventosJson = await ObtenerDatosAPIAsync("events", offset);
                            var eventos = eventosJson["data"]["results"];

                            foreach (var evento in eventos)
                            {
                                oEvent _event = new oEvent();
                                _event.Id = evento["id"] != null ? evento["id"].ToObject<int>() : 0;
                                _event.Title = evento["title"] != null ? evento["title"].ToString() : string.Empty;
                                _event.Description = evento["description"] != null ? evento["description"].ToString() : string.Empty;
                                _event.Start = evento["start"] != null ? evento["start"].ToObject<DateTime?>() : null;
                                _event.End = evento["end"] != null ? evento["end"].ToObject<DateTime?>() : null;
                                _event.Total_Comics = evento["comics"]?["available"] != null ? evento["comics"]["available"].ToObject<int>() : 0;

                                if (_event.Id > 0)
                                {
                                    await miSQL.insertEvent(_event, conn, transaction);
                                }
                            }

                            // Incrementar el offset para la siguiente llamada
                            offset += limit;
                        }

                        await transaction.CommitAsync(); // Confirmar transacción
                        Console.WriteLine("Datos de eventos insertados correctamente.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(); // Revertir transacción en caso de error
                        MessageBox.Show($"Error durante la inserción de los eventos: {ex.Message}");
                    }
                }
            }
        }

        // Método para obtener todos las series paginando (si fuera necesario) y almacenarlos en la base de datos
        public async Task getTodasLasSeriesAsync(int totalSeries)
        {
            using (MySqlConnection conn = new MySqlConnection(clVariables.connectionString))
            {
                clMySQL miSQL = new clMySQL();
                await conn.OpenAsync();

                using (var transaction = await conn.BeginTransactionAsync())
                {
                    try
                    {
                        int offset = 0;

                        // Realizar solicitudes paginadas hasta obtener todos las series
                        while (offset < totalSeries)
                        {
                            var seriesJson = await ObtenerDatosAPIAsync("series", offset);
                            var series = seriesJson["data"]["results"];

                            foreach (var serie in series)
                            {
                                oSerie _serie = new oSerie();
                                _serie.Id = serie["id"] != null ? serie["id"].ToObject<int>() : 0;
                                _serie.Title = serie["title"] != null ? serie["title"].ToString() : string.Empty;
                                _serie.Description = serie["description"] != null ? serie["description"].ToString() : string.Empty;
                                _serie.Total_Characters = serie["characters"]?["available"] != null ? serie["characters"]["available"].ToObject<int>() : 0;

                                if (_serie.Id > 0)
                                {
                                    await miSQL.insertSerie(_serie, conn, transaction);
                                }
                            }

                            // Incrementar el offset para la siguiente llamada
                            offset += limit;
                        }

                        await transaction.CommitAsync(); // Confirmar transacción
                        Console.WriteLine("Datos de series insertados correctamente.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(); // Revertir transacción en caso de error
                        MessageBox.Show($"Error durante la inserción de las series: {ex.Message}");
                    }
                }
            }
        }

        //Método para obtener todos los cómics paginando (si fuera necesario) y almacenarlos en la base de datos 
        public async Task getTodosLosComicsAsync(int totalComics)
        {
            using (MySqlConnection conn = new MySqlConnection(clVariables.connectionString))
            {
                clMySQL miSQL = new clMySQL();
                await conn.OpenAsync();

                using (var transaction = await conn.BeginTransactionAsync())
                {
                    try
                    {
                        int offset = 0;

                        // Realizar solicitudes paginadas hasta obtener todos los eventos
                        while (offset < totalComics)
                        {
                            //Añadimos parametros para filtrar por comics y quitar duplicados con las variantes
                            string parametros = ("format=comic&formatType=comic&noVariants=true");
                            var comicsJson = await ObtenerDatosAPIAsync("comics", offset, parametros);
                            var comics = comicsJson["data"]["results"];

                            foreach (var comic in comics)
                            {
                                //Comprobar el total de personajes que hay en cada cómic
                                var totalPersonajes = comic["characters"]?["available"]?.ToObject<int>() ?? 0;

                                // Solo procesar comics que tienen al menos 2 personajes
                                if (totalPersonajes >= 2)
                                {
                                    oComic _comic = new oComic();
                                    _comic.Id = comic["id"] != null ? comic["id"].ToObject<int>() : 0;
                                    _comic.Title = comic["title"] != null ? comic["title"].ToString() : string.Empty;

                                    await miSQL.insertComic(_comic, conn, transaction);

                                    // Insertar la relación entre cómic y personaje para poder consultar las parejas que más se repiten
                                    var characters = comic["characters"]["items"];
                                    foreach (var character in characters)
                                    {
                                        //Extraer el nombre del personaje del json  
                                        string CharacterName = character["name"] != null ? character["name"].ToString() : string.Empty;
                                        if (CharacterName != "")
                                        {
                                            await miSQL.insertComicCharacters(_comic.Id, CharacterName, conn, transaction);
                                        }

                                    }

                                }
                            }

                            offset += limit; // Incrementa el offset para la siguiente página
                        }

                        await transaction.CommitAsync(); // Confirmar transacción
                        Console.WriteLine("Datos de cómics insertados correctamente.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync(); // Revertir transacción en caso de error
                        MessageBox.Show($"Error durante la inserción de los cómics: {ex.Message}");
                    }
                }
            }
        }

        #endregion
    }
}
