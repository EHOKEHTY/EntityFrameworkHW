using ContextLibrery;

namespace EntityFrameworkHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibreryContext lib = new LibreryContext();

            MiniApp App = new MiniApp(lib);
            App.StartApp();
        }
    }

    
}
