using ContextLibrery;

public class LibreryMiniApp
{
    private int menuNavigator = 0;
    private LibreryContext ctx;
    public LibreryMiniApp(LibreryContext dbContext)
    {
        ctx = dbContext;
    }

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Выберите пункт меню:");
            Console.WriteLine("  1. Авторизоваться");
            Console.WriteLine("  2. Зарегестрироваться");
            Console.WriteLine("  3. Закончить работу приложения");

            menuNavigator = Selector(3);

            if (menuNavigator == 1)
            {
                if (Login())
                {
                    AuthorizedMenu();
                }
            }
            else if (menuNavigator == 2)
            {
                CreateNewLibrerian();
            }
            else
            {
                Console.WriteLine("Алилуя");
                break;
            }
        }
    }
    
    private void AuthorizedMenu()
    {
        while (true)
        {
            Console.WriteLine("Выберите пункт меню:");
            Console.WriteLine("  1. Создать нового библиотекоря");
            Console.WriteLine("  2. Создать нового читателя");
            Console.WriteLine("  3. Выйти из аккаунта");

            menuNavigator = Selector(3);

            if (menuNavigator == 1)
            {
                CreateNewLibrerian();
            }
            else if (menuNavigator == 2)
            {
                CreateNewReader();
            }
            else
            {
                break;
            }
        }
    }

    private bool Login()
    {
        while (true)
        {
            Librarian librarian = new Librarian();

            Console.WriteLine("Для входа в аккаунт введите свои данные. Для выхода из этого пункта меню введите 1: ");

            string temp1 = "  Логин: ";
            string temp2 = "  Пароль: ";
            Console.WriteLine(temp1);
            Console.WriteLine(temp2);
            Console.SetCursorPosition(temp1.Length, 1);
            librarian.Login = Console.ReadLine();
            if (librarian.Login == "1")
            {
                Console.Clear();
                return false;
            }
            Console.SetCursorPosition(temp2.Length, 2);
            librarian.Password = Console.ReadLine();

            if (LibrerianExistCheck(librarian))
            {
                Console.Clear();
                Console.WriteLine("Доступ успешно получен.");
                Thread.Sleep(1000);
                Console.Clear();
                return true;
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

    private bool LibrerianExistCheck(Librarian librarian)
    {
        foreach (var item in ctx.Librarians)
        {
            if (item.Login == librarian.Login && item.Password == librarian.Password)
            {
                return true;
            }
        }
        return false;
    }
   
    private void CreateNewLibrerian()
    {
        while (true)
        {
            Librarian librarian = new Librarian();
            Console.WriteLine("Для создания аккаунта введите свои данные: ");

            string temp1 = "  Логин: ";
            string temp2 = "  Пароль: ";
            string temp3 = "  Почта: ";
            Console.WriteLine(temp1);
            Console.WriteLine(temp2);
            Console.WriteLine(temp3);
            Console.SetCursorPosition(temp1.Length, 1);
            librarian.Login = Console.ReadLine();

            Console.SetCursorPosition(temp2.Length, 2);
            librarian.Password = Console.ReadLine();

            Console.SetCursorPosition(temp3.Length, 3);
            librarian.Email = Console.ReadLine();
            
            if (LibrerianExistCheck(librarian))
            {
                Console.Clear();
                Console.WriteLine("Такой библиотекарь уже существует");
                Thread.Sleep(1000);
            }
            else
            {
                try
                {
                    ctx.Librarians.Add(librarian);
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
                    Console.Clear();
                    Console.WriteLine("Аккаунт успешно создан. Можете авторизоваться.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                break;
            }
        }

    }

    private bool ReaderExistCheck(Reader reader)
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

    private int Selector(int chengesQuantity)
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

    private void CreateNewReader()
    {
        while (true)
        {

            Reader reader = new Reader();

            Console.WriteLine("Для создания аккаунта данные читателя: ");

            string temp1 = "  Логин: ";
            string temp2 = "  Пароль: ";
            string temp3 = "  Имя: ";
            string temp4 = "  Фамилия: ";
            string temp5 = "  Почта: ";
            string temp6 = "  Тип документа: ";
            string temp7 = "  Номер документа: ";

            Console.WriteLine(temp1);
            Console.WriteLine(temp2);
            Console.WriteLine(temp3);
            Console.WriteLine(temp4);
            Console.WriteLine(temp5);
            Console.WriteLine(temp6);
            Console.WriteLine(temp7);

            Console.SetCursorPosition(temp1.Length, 1);
            reader.Login = Console.ReadLine();

            Console.SetCursorPosition(temp2.Length, 2);
            reader.Password = Console.ReadLine();

            Console.SetCursorPosition(temp3.Length, 3);
            reader.FirstName = Console.ReadLine();

            Console.SetCursorPosition(temp4.Length, 4);
            reader.LastName = Console.ReadLine();

            Console.SetCursorPosition(temp5.Length, 5);
            reader.Email = Console.ReadLine();

            Console.SetCursorPosition(temp5.Length, 6);

            var typeDocument = new DocumentType();
            typeDocument.TypeName = Console.ReadLine();
            reader.DocumentType = typeDocument;
            
            Console.SetCursorPosition(temp5.Length, 7);
            reader.Email = Console.ReadLine();


            if (ReaderExistCheck(reader))
            {
                Console.Clear();
                Console.WriteLine("Такой пользователь уже существует");
                Thread.Sleep(1000);
            }
            else
            {
                try
                {
                    ctx.Readers.Add(reader);
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
                    Console.Clear();
                    Console.WriteLine("Аккаунт успешно создан. Можете авторизоваться.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                break;
            }

        }
        
    }
}