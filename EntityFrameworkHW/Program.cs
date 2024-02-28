using ContextLibrery;

namespace EntityFrameworkHW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibreryContext lib = new LibreryContext();
            
            LibreryMiniApp App = new LibreryMiniApp(lib);

            App.Start();

        }
    }

    
}
