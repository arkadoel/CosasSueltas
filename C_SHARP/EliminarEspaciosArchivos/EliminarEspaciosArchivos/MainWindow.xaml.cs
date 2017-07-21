using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace EliminarEspaciosArchivos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnBuscar.Click += btnBuscar_Click;
            btnProcesar.Click += btnProcesar_Click;
        }

        /// <summary>
        /// Muestra el dialogo para buscar la ruta a evaluar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialogo = new FolderBrowserDialog();
            dialogo.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

            var resultado = dialogo.ShowDialog();

            if (resultado == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(dialogo.SelectedPath) == false && System.IO.Directory.Exists(dialogo.SelectedPath))
                {
                    txtRuta.Text = dialogo.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Procesara los archivos dentro del directorio indicado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnProcesar_Click(object sender, RoutedEventArgs e)
        {
            List<string> extensiones = txtExtensiones.Text.ToString().Split(';').ToList();
            Eliminador elim = new Eliminador(txtRuta.Text, extensiones, this);

            elim.Procesar();

            System.Windows.MessageBox.Show("Proceso terminado", "Terminado", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

        }
    }
}
