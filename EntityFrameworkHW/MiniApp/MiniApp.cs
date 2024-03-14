using ContextLibrery;

public class Person
{
    public string? Login { get; set; }
    public string? Password { get; set; }
}

public class MiniApp
{

    internal int menuNavigator = 0;
    private LibreryContext ctx;
    private Reader currentReader;

    public MiniApp(LibreryContext dbContext)
    {
        ctx = dbContext;
    }

    public void StartApp()
    {
        while (true)
        {
            Console.WriteLine("Выберите пункт меню:");
            Console.WriteLine("  1. Войти в аккаунт");
            Console.WriteLine("  2. Закончить работу приложения");

            menuNavigator = Selector(2);

            if (menuNavigator == 1)
            {
                Console.Clear();
                var log = Login();
                if (log == 1)
                {
                    new ReaderMiniApp(ctx, currentReader);
                }
                else if (log == 2)
                {
                    new LibreryMiniApp(ctx);
                }
            }
            else
            {
                Console.WriteLine("Алилуя!");
                break;
            }
        }
    }

    private int Login()
    {
        while (true)
        {
            Person person = new Person();

            Console.WriteLine("Для входа в аккаунт введите свои данные. Для выхода из этого пункта меню введите 1: ");

            string temp1 = "  Логин: ";
            string temp2 = "  Пароль: ";
            Console.WriteLine(temp1);
            Console.WriteLine(temp2);

            Console.SetCursorPosition(temp1.Length, 1);
            person.Login = Console.ReadLine();

            if (person.Login == "1")
            {
                Console.Clear();
                return 0;
            }

            Console.SetCursorPosition(temp2.Length, 2);
            person.Password = Console.ReadLine();

            if (ReaderExistCheck(person))
            {
                foreach (var item in ctx.Readers)
                {
                    if (item.Login == person.Login)
                    {
                        currentReader = item;
                    }
                }
                Console.Clear();
                Console.WriteLine("Доступ читателя успешно получен.");
                Thread.Sleep(1000);
                Console.Clear();
                return 1;
            }
            else if (LibrerianExistCheck(person))
            {
                Console.Clear();
                Console.WriteLine("Доступ библиотекаря успешно получен.");
                Thread.Sleep(1000);
                Console.Clear();
                return 2;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Неверно введены данные или пользователь не существует.");
                Thread.Sleep(1000);
                Console.Clear();
            }
            Console.Clear();
        }
    }

    internal bool LibrerianExistCheck(Person librarian)
    {
        foreach (var item in ctx.Librerians)
        {
            if (item.Login == librarian.Login && item.Password == librarian.Password)
            {
                return true;
            }
        }
        return false;
    }

    internal bool ReaderExistCheck(Person reader)
    {
        foreach (var item in ctx.Readers)
        {
            if (item.Login == reader.Login && item.Password == reader.Password)
            {
                return true;
            }
        }
        return false;
    }

    internal int Selector(int chengesQuantity)
    {
        menuNavigator = int.Parse(Console.ReadLine());
        while (menuNavigator > chengesQuantity || menuNavigator < 1)
        {
            Console.WriteLine("Неверный пункт меню");
            menuNavigator = int.Parse(Console.ReadLine());
        }
        Console.Clear();
        return menuNavigator;
    }

    internal void SearchBook()
    {
        while (true)
        {
            Console.WriteLine("Введите название книги, которую хотите найти: ");
            Book book = new Book() { Title = Console.ReadLine() };
            if (LookForBookByTitle(book))
            {
                int i = 0;
                foreach (var item in ctx.Books)
                {
                    if (book.Title == item.Title)
                    {
                        Console.WriteLine($"  {++i}. {item.Title} - {item.Year} - {item.Country} - {item.City}");
                    }
                }
                Console.ReadLine();
                Console.Clear();
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Книга не найдена");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    internal void SearchAuthor()
    {
        while (true)
        {
            Console.WriteLine("Введите фамилию и имя автора, которого хотите найти: ");
            Author author = new Author();
            string temp1 = "  Фамилия: ";
            string temp2 = "  Имя: ";
            Console.WriteLine(temp1);
            Console.SetCursorPosition(temp1.Length, 1);
            author.LastName = Console.ReadLine();

            Console.WriteLine(temp2);
            Console.SetCursorPosition(temp2.Length, 2);
            author.FirstName = Console.ReadLine();

            if (LookForAuthor(author))
            {
                int i = 0;
                foreach (var item in ctx.Authors)
                {
                    if (author.FirstName == item.FirstName && author.LastName == item.LastName)
                    {
                        Console.WriteLine($"  {++i}. {item.FirstName} - {item.MiddleName} - {item.LastName} - {item.DateOfBirth}");
                    }
                }
                Console.ReadLine();
                Console.Clear();
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Автор не найден");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    internal bool LookForAuthor(Author date)
    {
        foreach (var item in ctx.Authors)
        {
            if (item.LastName == date.LastName && item.FirstName == date.FirstName)
            {
                return true;
            }
        }
        return false;
    }

    internal bool LookForBookByTitle(Book date)
    {
        foreach (var item in ctx.Books)
        {
            if (item.Title == date.Title)
            {
                return true;
            }
        }
        return false;
    }
}
