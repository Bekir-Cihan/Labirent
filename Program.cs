using System;
    using System.Windows.Forms;

        namespace LabirentOyunu
        {
        internal static class Program
{
    /// <summary>
    /// Uygulamanın ana giriş noktasıdır.
    /// </summary>
        [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1()); // Uygulamayı Form1 ile başlat
    }
}
}
