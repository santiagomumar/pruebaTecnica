using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using PruebaTecnica_MarvelDatabase.Clases;
using PruebaTecnica_MarvelDatabase.Formularios;
using System.Data;

namespace PruebaTecnica_MarvelDatabase
{
    public partial class frmConsultaDatosMarvel : Form
    {
        private clConexionApi miConexionApi;
        private clMySQL miConexionMySQL;

        #region FORMULARIO

        public frmConsultaDatosMarvel()
        {
            InitializeComponent();
            miConexionApi = new clConexionApi();
            miConexionMySQL = new clMySQL();
        }

        private void frmConsultaDatosMarvel_Load(object sender, EventArgs e)
        {
            cbPreguntas .SelectedIndex = 0; 
        }

        #endregion

        #region EVENTOS MOUSE BOTÓN
        private void btnConsultar_MouseDown(object sender, MouseEventArgs e)
        {
            btnConsultar.Image = PruebaTecnica_MarvelDatabase.Properties.Resources.capitan_logo_push;
        }

        private void btnConsultar_MouseLeave(object sender, EventArgs e)
        {
            btnConsultar.Image = PruebaTecnica_MarvelDatabase.Properties.Resources.capitan_logo;
        }

        private void btnConsultar_MouseEnter(object sender, EventArgs e)
        {
            btnConsultar.Image = PruebaTecnica_MarvelDatabase.Properties.Resources.capitan_logo_hover;
        }

        private void btnConsultar_MouseUp(object sender, MouseEventArgs e)
        {
            btnConsultar.Image = PruebaTecnica_MarvelDatabase.Properties.Resources.capitan_logo_hover;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            cargarDatosTabla();
        }

        private void btnCargarDatos_MouseDown(object sender, MouseEventArgs e)
        {
            btnCargarDatos.Image = PruebaTecnica_MarvelDatabase.Properties.Resources.marvel_download;
        }

        private void btnCargarDatos_MouseUp(object sender, MouseEventArgs e)
        {
            btnCargarDatos.Image = PruebaTecnica_MarvelDatabase.Properties.Resources.marvel_nodata;
        }

        private async void btnCargarDatos_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show($"Este proceso borrará todos los datos para insertarlos de nuevo. \n " +
                $"¿Desea seguir adelante? El proceso puede tardar unos minutos.","¡Atención!",MessageBoxButtons.YesNo ,MessageBoxIcon.Question);

            if (r != DialogResult.Yes) return;
            //Primero resetear la base de datos para insertar los datos
            //Se podría hacer más complejo y comprobar qué datos ya están insertados sin tener que borrar los existentes y también actualizar si hubiese alguna modificación            
            if (miConexionMySQL.resetTables())
            {                
                await inicializarDatos();
            }

        }

        #endregion

        #region CARGAR DATOS

        async Task inicializarDatos()
        {
            try
            {
                Cursor = Cursors.AppStarting;
                btnCargarDatos.Enabled = false;
                btnConsultar.Enabled = false;
                
                using (var progressForm = new frmProgressBar())
                {
                    progressForm.StartPosition = FormStartPosition.CenterScreen;
                    progressForm.Show();

                    // Obtener el número total de personajes de la API
                    int totalPersonajes = await miConexionApi.ObtenerTotalElementos("characters");
                    // Procesar e insertar todos los personajes en la base de datos paginando las solicitudes
                    if (totalPersonajes > 0) await miConexionApi.getTodosLosPersonajesAsync(totalPersonajes);

                    int totalEventos = await miConexionApi.ObtenerTotalElementos("events");
                    // Procesar e insertar todos los eventos en la base de datos paginando las solicitudes
                    if (totalEventos > 0) await miConexionApi.getTodosLosEventosAsync(totalEventos);

                    int totalSeries = await miConexionApi.ObtenerTotalElementos("series");
                    // Procesar e insertar todos las series en la base de datos paginando las solicitudes
                    if (totalSeries > 0) await miConexionApi.getTodasLasSeriesAsync(totalEventos);

                    //Añadimos parametros para filtrar por comics y quitar duplicados con las variantes
                    //string parametros = ("format=comic&formatType=comic&noVariants=true");
                    //int totalComics = await miConexionApi.ObtenerTotalElementos("comics", parametros);

                    int totalComics = 1000; //Ponemos 1000 para realizar la prueba ya que el total de cómics es muy amplio
                                            //y la API tardaría mucho tiempo en devolver todos los datos                    
                    if (totalComics > 0) await miConexionApi.getTodosLosComicsAsync(totalComics);
                    
                    progressForm.Hide();
                }

                btnCargarDatos.Enabled = true;
                btnConsultar.Enabled = true;
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar los datos: {ex.Message}");
                Cursor = Cursors.Default;
                btnCargarDatos.Enabled = true;
                btnConsultar.Enabled = true;
            }
        }

        private void cargarDatosTabla()
        {

            Cursor = Cursors.AppStarting;

            DataTable dt = new DataTable();
            int opcionSeleccionada = Convert.ToInt32(cbPreguntas.SelectedIndex);
            switch (opcionSeleccionada)
            {
                case -1:
                    MessageBox.Show("Seleccione una de las opciones.", "¡Atención!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbPreguntas.Focus();
                    break;
                case 0:
                    dt = miConexionMySQL.getPersonajesPopulares();
                    break;
                case 1:
                    dt = miConexionMySQL.getEventosOrderByComics();
                    break;
                case 2:
                    dt = miConexionMySQL.getParejasMásRecurrentes();
                    break;
                case 3:
                    dt = miConexionMySQL.getSeriesOrderByNumPersonajes();
                    break;

            }

            dgvDatos.DataSource = dt;

            Cursor = Cursors.Default;
        }

        #endregion

    }
}
