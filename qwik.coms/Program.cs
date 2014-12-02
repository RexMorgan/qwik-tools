using qwik.coms.Configuration;
using StructureMap;
using System;
using System.Windows.Forms;

namespace qwik.coms
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var container = new Container(new CoreRegistry());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.GetInstance<Main>());
        }
    }
}