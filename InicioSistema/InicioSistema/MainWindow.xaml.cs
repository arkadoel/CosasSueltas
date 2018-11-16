using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace InicioSistema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Programa> programas = new List<Programa>();

        #region TOOLS CLASSES
        private class Programa
        {
            public string Name { get; set; }
            public string Priority { get; set; }
            public string Number_Of_CPUS { get; set; }
        }

        private class CPU_Affinity
        {
            /// <summary>
            /// cpu 1
            /// </summary>
            public const long a_0x0001 = 0x0001; 
            /// <summary>
            /// cpu 2
            /// </summary>
            public const long a_0x0002 = 0x0002; 
            /// <summary>
            /// cpu 1 or 2
            /// </summary>
            public const long a_0x0003 = 0x0003;
            /// <summary>
            /// cpu 3
            /// </summary>
            public const long a_0x0004 = 0x0004;
            /// <summary>
            /// cpus 1 or 3 
            /// </summary>
            public const long a_0x0005 = 0x0005;
            /// <summary>
            /// cpus 1, 2, or 3
            /// </summary>
            public const long a_0x0007 = 0x0007;
            /// <summary>
            /// cpus 1, 2, 3, or 4 
            /// </summary>
            public const long a_0x000F = 0x000F;
        }

        public class Priority
        {
            public const string High = "HIGH";
            public const string AboveNormal = "ABOVE";
            public const string Normal = "NORMAL";
            public const string BelowNormal = "BELOW";
            public const string Low = "LOW";
            public const string Kill = "KILL";
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            LoadConfigXML();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Programa programa in programas)
            {
                List<Process> proc = Process.GetProcesses().Where(x => x.ProcessName.ToLower().Contains(programa.Name.ToLower())).ToList();

                if (proc.Count > 0)
                {
                    foreach (var proceso in proc)
                    {
                        try
                        {
                            programa.Priority = programa.Priority.ToUpper();

                            if (programa.Priority == Priority.High) proceso.PriorityClass = ProcessPriorityClass.High;
                            else if (programa.Priority == Priority.AboveNormal) proceso.PriorityClass = ProcessPriorityClass.AboveNormal;
                            else if (programa.Priority == Priority.Normal) proceso.PriorityClass = ProcessPriorityClass.Normal;
                            else if (programa.Priority == Priority.BelowNormal) proceso.PriorityClass = ProcessPriorityClass.BelowNormal;
                            else if (programa.Priority == Priority.Low) proceso.PriorityClass = ProcessPriorityClass.Idle;
                            else if (programa.Priority == Priority.Kill) proceso.Kill();
                            else proceso.PriorityClass = ProcessPriorityClass.Normal;

                            if (programa.Number_Of_CPUS == "1") proceso.ProcessorAffinity = (IntPtr)CPU_Affinity.a_0x0001;
                            else if (programa.Number_Of_CPUS == "2") proceso.ProcessorAffinity = (IntPtr)CPU_Affinity.a_0x0002;
                            else if (programa.Number_Of_CPUS == "3") proceso.ProcessorAffinity = (IntPtr)CPU_Affinity.a_0x0004;
                            else if (programa.Number_Of_CPUS == "1or2") proceso.ProcessorAffinity = (IntPtr)CPU_Affinity.a_0x0003;
                            else if (programa.Number_Of_CPUS == "1or3") proceso.ProcessorAffinity = (IntPtr)CPU_Affinity.a_0x0005;
                            else if (programa.Number_Of_CPUS == "1to3") proceso.ProcessorAffinity = (IntPtr)CPU_Affinity.a_0x0007;
                            else if (programa.Number_Of_CPUS == "all") proceso.ProcessorAffinity = (IntPtr)CPU_Affinity.a_0x000F;
                            else proceso.ProcessorAffinity = (IntPtr)CPU_Affinity.a_0x000F;
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.Print(ex.Message);
                        }

                    }

                }
            }

            this.Close();
        }

        private void LoadConfigXML()
        {
            XDocument xdoc = XDocument.Load("ProcessConfig.xml");
            foreach (var elProcess in xdoc.Root.Elements())
            {
                programas.Add(new Programa()
                {
                    Name = elProcess.Attributes().Where(x => x.Name.LocalName.ToLower() == "name").First().Value,
                    Priority = elProcess.Attributes().Where(x => x.Name.LocalName.ToLower() == "priority").First().Value.ToUpper(),
                    Number_Of_CPUS = elProcess.Attributes().Where(x => x.Name.LocalName.ToLower() == "number_of_cpus").First().Value
                });
            }

            xdoc = null;
            GC.Collect();
        }
    }
}
