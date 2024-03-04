using ContextLibrery;
using Microsoft.VisualBasic;
using static System.Reflection.Metadata.BlobBuilder;


public class ReaderMiniApp : MiniApp
{
    private LibreryContext ctx;
    private Reader currentReader;
    public ReaderMiniApp(LibreryContext dbContext, Reader currentReader) : base(dbContext)
    {
        this.currentReader = currentReader;
        ctx = dbContext;
        Start();
    }

    private void Start()
    {
        while (true)
        {
            Console.WriteLine("Выберите пункт меню:");
            Console.WriteLine("  1. Просмотр всех доступных книг");
            Console.WriteLine("  2. Поиск книги по названию");
            Console.WriteLine("  3. Показать взятые книги");
            Console.WriteLine("  4. Взять книгу");
            Console.WriteLine("  5. Предыдущий пункт меню");

            menuNavigator = Selector(5);
            if (menuNavigator == 5)
            {
                Console.Clear();
                break;
            }
            switch (menuNavigator)
            {
                case 1:
                    ShowLib();
                    break;

                case 2:
                    LookForBookByTitle(new Book { Title = Console.ReadLine() });
                    break;

                case 3:
                    ShowTakenBooks();
                    break;

                case 4:
                    TakeBook();
                    break;
            }
        }
    }

    private void ShowLib()
    {
        Console.Clear();
        int i = 0;
        foreach (var item in ctx.Books)
        {
            Console.WriteLine($"  {++i}. {item.Title} - {item.Year} - {item.Country} - {item.City}");
        }
        Console.ReadLine();
    }

    private void ShowTakenBooks()
    {
        Console.Clear();
        var currentRent = ctx.RentBooks.Where(rent => rent.ReturnDate == null && DateTime.Today < rent.DueDate && rent.ReaderId == currentReader.ReaderId).ToList();
        var overdueRent = ctx.RentBooks.Where(rent => rent.ReturnDate == null && DateTime.Today > rent.DueDate && rent.ReaderId == currentReader.ReaderId).ToList();
        var sortedRent = overdueRent.OrderBy(rent => rent.ReturnDate)
                                      .Concat(currentRent.OrderBy(rent => rent.ReturnDate))
                                      .ToList();
        int i = 0;
        foreach (var item in sortedRent)
        {
            if (item.ReturnDate < item.DueDate)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine($"  {++i}. Книга: {item.Book.Title}, Дата до когда нужно вернуть: {item.DueDate}");
            Console.ResetColor();
        }
        Console.ReadLine();
        Console.Clear();
    }

    private void TakeBook()
    {
        RentBook rent = new RentBook();
        //Book book = new Book();
        var temp1 = "Введите название книги, которую хотите взять: ";

        Console.WriteLine(temp1);
        Console.SetCursorPosition(temp1.Length, 0);
        rent.Book = new Book() { Title = Console.ReadLine() };
        DateTime dueDate = DateTime.Today.AddDays(30);
        //if (LookForBookByTitle(book))
        if (LookForBookByTitle(rent.Book))
        {
            Console.Clear();
            try
            {

                foreach (var item in ctx.Books)
                {
                    if (rent.Book.Title == item.Title)
                    {
                        rent.Book = item;
                        break;
                    }
                }
                rent.Reader = currentReader;
                rent.DueDate = dueDate;
                rent.RentDate = DateTime.Today;


                ctx.RentBooks.Add(rent);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    Console.WriteLine($"Книга '{rent.Book.Title}' успешно взята вами до {dueDate.ToShortDateString()}.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
        }
    }
}
