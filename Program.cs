using System;
using System.Windows.Forms;

namespace AccountAdmin
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для автономной оснастки администрирования учётных записей.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Нативный, прямой запуск пульта управления группами MSA / MSU
            // Строка 18 полностью выровнена, скобки и точки с запятой на своих секторах!
            Application.Run(new MainForm());
        }
    }
}
