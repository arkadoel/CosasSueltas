using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GuardarUTF_8
{
    class Program
    {
        #region Constantes y variables
        private static string ACTUAL_DIR = System.IO.Directory.GetCurrentDirectory();
        private static List<string> extensiones = new List<string>()
        {
            ".txt", ".js", ".aspx", ".ascx", ".cs", ".css", ".Master"
        };
        private static ConsoleColor colorOriginal = Console.ForegroundColor;
        private const string MENU_ENCODING = @"
Seleccione una codificacion de destino:
    1.- utf-8
    2.- ascii

    v.- Ver la codificacion actual

Opcion:";
        private static Dictionary<int, string> Codificaciones = new Dictionary<int, string>()
        {
            { 1, "utf-8"},
            { 2, "ascii"}
        };
        #endregion

        
        static void Main(string[] args)
        {
            string directorioDestino = "";
            string encodingDestino = "";
            string proceder = "";


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Por favor, seleccione el directorio que desea convertir a un formato de codificacion.");
            while (string.IsNullOrWhiteSpace(directorioDestino) || Directory.Exists(directorioDestino) == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Directorio: ");
                Console.ForegroundColor = colorOriginal;
                directorioDestino = Console.ReadLine();
                if (Directory.Exists(directorioDestino) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El directorio de destino no existe.");
                }
                Console.WriteLine("");
            }

            while (encodingDestino != "1" && encodingDestino != "2" && encodingDestino.ToUpper() != "V")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(MENU_ENCODING);
                Console.ForegroundColor = colorOriginal;
                encodingDestino = Console.ReadLine();
            }

            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Se pasara el directorio ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(directorioDestino);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" al formato [");
            Console.ForegroundColor = ConsoleColor.White;
            if (encodingDestino.ToUpper() != "V")
            {
                Console.Write(Codificaciones[Convert.ToInt32(encodingDestino)]);
            }
            else Console.Write(" ver encoding actual");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("].");
            do
            {
                Console.Write("¿Desea proceder [S/N]?");
                proceder = Console.ReadLine();
                proceder = proceder.ToUpper();
            } while (proceder != "S" && proceder != "N");

            switch (proceder)
            {
                case "S":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Procediendo...");

                    if (encodingDestino == "1")
                    {   //utf-8
                        ChangeEncodingForDirectory(directorioDestino, true, Encoding.UTF8, true);
                    }
                    else if (encodingDestino == "2")
                    {   //ascii
                        ChangeEncodingForDirectory(directorioDestino, true, Encoding.ASCII, true);
                    }
                    else if (encodingDestino.ToUpper() == "V")
                    {   //ver codificacion actual
                        ChangeEncodingForDirectory(directorioDestino, true, Encoding.ASCII, false);
                    }
                    
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Proceso terminado");
                    break;
                case "N":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Adios");
                    break;
            }

            Console.ReadLine();
        }

        #region Metodos
        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetFileEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            return Encoding.ASCII;
        }

         /// <summary>
        /// Permite copiar directorios y hacerlo recursivamente
        /// </summary>
        /// <param name="dirOrigen"></param>
        /// <param name="dirDestino"></param>
        /// <param name="recursivo">¿hacer copia recursiva?</param>
        public static void ChangeEncodingForDirectory(string dirOrigen, bool recursivo, Encoding encodingSalida, bool hacer)
		{
			DirectoryInfo dir = new DirectoryInfo(dirOrigen);
			DirectoryInfo[] dirs = dir.GetDirectories();
			
			if(dir.Exists==true){

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("" + dir.FullName.ToString() + "/");
				FileInfo[] files = dir.GetFiles();
				foreach(FileInfo file in files){
                    if (extensiones.Contains(file.Extension))
                    {
                        string textoFichero = string.Empty;
                        Console.ForegroundColor = ConsoleColor.White;
                        string encodingAntes = GetFileEncoding(file.FullName).WebName;

                        if (hacer == false)
                        {
                            Console.WriteLine("\t" + file.Name.ToString() + "  [{0}] ", encodingAntes);
                        }
                        else if (encodingSalida.WebName != encodingAntes )
                        {
                            StreamReader fichlectura = new StreamReader(file.FullName);
                            textoFichero = fichlectura.ReadToEnd();
                            fichlectura.Close();

                            StreamWriter fichWrite = new StreamWriter(file.FullName, false, encodingSalida);
                            fichWrite.Write(textoFichero);
                            fichWrite.Close();

                            string encodingDespues = GetFileEncoding(file.FullName).WebName;

                            Console.WriteLine("\t" + file.Name.ToString() + "  [{0}] a [{1}] ", encodingAntes, encodingDespues);
                        }
                    }
				}
				//copy subdirs
				if(recursivo==true){
					foreach(DirectoryInfo subdir in dirs){
						//procesar recursivamente
                        ChangeEncodingForDirectory(subdir.FullName, recursivo, encodingSalida, hacer);
					}
				}
			}
		}


        #endregion
    }
}
