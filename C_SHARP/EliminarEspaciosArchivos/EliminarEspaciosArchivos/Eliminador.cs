using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace EliminarEspaciosArchivos
{
    internal class Eliminador
    {
        private string RutaDirectorio;
        private List<string> Extensiones;
        private List<string> extensiones;
        private MainWindow ventana;

        public Eliminador()
        {

        }

        public Eliminador(string ruta, List<string> extensiones, MainWindow mainWindow)
        {
            this.RutaDirectorio = ruta;
            this.Extensiones = extensiones;
            this.ventana = mainWindow;
        }


        public void Procesar()
        {
            List<string> archivos = new List<string>();

            foreach (string ext in Extensiones)
            {
               archivos.AddRange(Directory.GetFiles(RutaDirectorio, ext, SearchOption.TopDirectoryOnly).ToList());
            }

            ventana.prog.Maximum = archivos.Count;
            ventana.prog.Minimum = 0;
            ventana.prog.Value = 0;
            int actual = 0;
            
            foreach (string nombreArchivo in archivos)
            {
                ventana.prog.Value = actual;
                string texto = "";
                Encoding encodingfichero = Encoding.UTF8;

                using (StreamReader Reader = new StreamReader(nombreArchivo))
                {
                    encodingfichero = Reader.CurrentEncoding;
                    texto = Reader.ReadToEnd();                    
                }

                

                texto = ReducirSaltosDeLinea(texto);

                //quitar espacios
                string[] lineas = null;
                DameLineas(ref lineas, texto);

                StringBuilder sb = new StringBuilder();
                string lineaAnterior = "";
                foreach (string linea in lineas)
                {
                    string newlinea = linea;
                    bool lineaIgnorada = false;
                    if (string.IsNullOrWhiteSpace(newlinea.Replace(" ", "")) )// && string.IsNullOrWhiteSpace(lineaAnterior))
                    {
                         lineaIgnorada = true;
                    }

                    if(!lineaIgnorada) sb.AppendLine(newlinea);
                    lineaAnterior = newlinea.Replace(" ", "");
                }

               // File.Copy(nombreArchivo, nombreArchivo + ".bak");

                using (StreamWriter writer = new StreamWriter(nombreArchivo, false, encodingfichero))
                {
                    writer.WriteLine(sb.ToString());
                }
                DoEvents();
                actual++;

            }
        }


        public static string ReducirEspacios(string texto)
        {
            while (texto.Contains("  "))
            {
                texto = texto.Replace("  ", " ");
            }
            return texto.Trim();
        }

        public static void DameLineas(ref string[] lineas, string texto)
        {
            lineas = texto.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string ReducirSaltosDeLinea(string texto)
        {
            texto = texto.Replace("\n", "\r\n");
            texto = texto.Replace("\r", "\r\n");

            for (int i = 0; i < 3; i++)
            {
                texto = texto.Replace("\r\r", "\r");
                texto = texto.Replace("\n\n", "\n");
                texto = texto.Replace("\r\n\r\n\r\n", "\r\n");
            }

            return texto;
        }

        public void DoEvents()
        {
            this.ventana.Dispatcher.Invoke(DispatcherPriority.Background,
                                                  new Action(delegate { }));
        }
               
    }
}
