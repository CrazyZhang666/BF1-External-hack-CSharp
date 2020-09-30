using System.Windows.Forms;

namespace BF1_Hack
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HackManager().RenderSurface);
        }
    }
}
