using MySql.Data.MySqlClient;
using PruebaTecnica_MarvelDatabase.Objetos;
using System.Data;

namespace PruebaTecnica_MarvelDatabase.Clases
{
    internal class clMySQL
    {        
        //Función para eliminar los datos existentes de todas las tablas
        public bool resetTables()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(clVariables.connectionString))
                {
                    conn.Open();

                    // Comando para borrar datos de las tablas
                    var deleteCommand = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 0; " +
                                                          "DELETE FROM characters; " +
                                                          "DELETE FROM comics; " +
                                                          "DELETE FROM events; " +
                                                          "DELETE FROM charactercomics; " +
                                                          "DELETE FROM comics; " +
                                                          "SET FOREIGN_KEY_CHECKS = 1;", conn);

                    int rowsAffected = deleteCommand.ExecuteNonQuery(); 
                    
                    return rowsAffected >= 0; // Si es 0 es porque no hay datos previos
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reseteando las tablas: {ex.Message}");
                return false; // Devuelve false en caso de error
            }
        }

        #region PERSONAJES

        //Método asíncrono para insertar cada personaje en la base de datos
        public async Task insertCharacter(oCharacter personaje, MySqlConnection conn, MySqlTransaction transaction)
        {               
                // Consulta SQL para insertar los datos
                string query = "INSERT IGNORE INTO characters (id, name, description, total_comics) " +
                               "VALUES (@id, @name, @description, @total_comics)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", personaje.Id);
                    cmd.Parameters.AddWithValue("@name", personaje.Name);
                    cmd.Parameters.AddWithValue("@description", personaje.Description);
                    cmd.Parameters.AddWithValue("@total_comics", personaje.Total_Comics);
                    await cmd.ExecuteNonQueryAsync();
                }                       
        }

        // Método para realizar el SELECT y obtener el top 10 de personajes
        public DataTable getPersonajesPopulares()
        {
            string query = "SELECT id as 'Id', name as 'Personaje', description as 'Descripción', total_comics as 'Total Cómics' FROM characters ORDER BY total_comics DESC LIMIT 10";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(clVariables.connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                            if (ds.Tables.Count > 0) dt = ds.Tables[0]; 
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            return dt;
        }

        #endregion

        #region EVENTOS

        //Método asíncrono para insertar cada evento en la base de datos
        public async Task insertEvent(oEvent evento, MySqlConnection conn, MySqlTransaction transaction)
        {
            // Consulta SQL para insertar los datos
            string query = "INSERT IGNORE INTO events (id, title, description, start, end, total_comics) " +
                           "VALUES (@id, @title, @description, @start, @end, @total_comics)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@id", evento.Id);
                cmd.Parameters.AddWithValue("@title", evento.Title);
                cmd.Parameters.AddWithValue("@description", evento.Description);
                cmd.Parameters.AddWithValue("@start", evento.Start);
                cmd.Parameters.AddWithValue("@end", evento.End);
                cmd.Parameters.AddWithValue("@total_comics", evento.Total_Comics);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        // Método para realizar el SELECT y obtener loes eventos ordenados por el total de cómics de mayor a menor
        public DataTable getEventosOrderByComics()
        {
            string query = "select id as 'Id', title as 'Evento', description as 'Descripción', total_comics as 'Total Cómics', date(start) as 'Inicio', date(end) as 'Fin' " +
                "from events order by total_comics DESC";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(clVariables.connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                            if (ds.Tables.Count > 0) dt = ds.Tables[0];
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            return dt;
        }

        #endregion

        #region SERIES

        //Método asíncrono para insertar cada serie en la base de datos
        public async Task insertSerie(oSerie serie, MySqlConnection conn, MySqlTransaction transaction)
        {
            // Consulta SQL para insertar los datos
            string query = "INSERT IGNORE INTO series (id, title, description, total_characters) " +
                           "VALUES (@id, @title, @description, @total_characters)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@id", serie.Id);
                cmd.Parameters.AddWithValue("@title", serie.Title);
                cmd.Parameters.AddWithValue("@description", serie.Description);
                cmd.Parameters.AddWithValue("@total_characters", serie.Total_Characters);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        // Método para realizar el SELECT y obtener el top 3 de series con más personakes
        public DataTable getSeriesOrderByNumPersonajes ()
        {
            string query = "select id as 'Id', title as 'Serie', description as 'Descripción', total_characters as 'Total Personajes' from series order by total_characters DESC LIMIT 3";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(clVariables.connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                            if (ds.Tables.Count > 0) dt = ds.Tables[0];
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            return dt;
        }

        #endregion

        #region COMICS

        //Método asíncrono para insertar cada comic en la base de datos
        public async Task insertComic(oComic comic, MySqlConnection conn, MySqlTransaction transaction)
        {
            // Consulta SQL para insertar los datos
            string query = "INSERT IGNORE INTO comics (id, title) " +
                           "VALUES (@id, @title)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@id", comic.Id);
                cmd.Parameters.AddWithValue("@title", comic.Title);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        //Método asíncrono para insertar los personajes que aparecen en cada comic en la base de datos
        public async Task insertComicCharacters(int ComicId, string CharacterName, MySqlConnection conn, MySqlTransaction transaction)
        {
            // Consulta SQL para insertar los datos
            string query = "INSERT INTO CharacterComics (ComicId, CharacterName) " +
                           "VALUES (@ComicId, @CharacterName)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@ComicId", ComicId);                
                cmd.Parameters.AddWithValue("@CharacterName", CharacterName);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        // Método para realizar el SELECT y obtener las 5 parejas más recurrentes de los cómics
        public DataTable getParejasMásRecurrentes()
        {            
            string query = "SELECT c1.CharacterName AS 'Personaje nº1', c2.CharacterName AS 'Personaje nº2',COUNT(*) AS 'Coincidencias' " +
            "FROM CharacterComics c1 " +
            "JOIN CharacterComics c2 ON c1.ComicId = c2.ComicId " +
            "WHERE c1.CharacterName<c2.CharacterName " +
            "GROUP BY c1.CharacterName, c2.CharacterName " +
            "ORDER BY Coincidencias DESC " +
            "LIMIT 5";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(clVariables.connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                            if (ds.Tables.Count > 0) dt = ds.Tables[0];
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            return dt;
        }

        #endregion

    }
}
