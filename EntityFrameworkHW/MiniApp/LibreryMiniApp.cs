using ContextLibrery;
using Microsoft.Identity.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class LibreryMiniApp : MiniApp
{
    private LibreryContext ctx;

    public LibreryMiniApp(LibreryContext dbContext) : base(dbContext)
    {
        ctx = dbContext;
        AuthorizedMenu();
    }

    private void AuthorizedMenu()
    {
        while (true)
        {
            Console.WriteLine("Выберите пункт меню:");
            Console.WriteLine("  1. Добавить нового библиотекоря");
            Console.WriteLine("  2. Добавить нового читателя");
            Console.WriteLine("  3. Добавить нового Автора");
            Console.WriteLine("  4. Добавить книгу");
            Console.WriteLine("  5. Вывести всех авторов на экран");
            Console.WriteLine("  6. Вывести все книги на экран");
            Console.WriteLine("  7. Найти книгу");
            Console.WriteLine("  8. Найти автора");
            Console.WriteLine("  9. Изменить книгу");
            Console.WriteLine("  10. Изменить профиль читателя");
            Console.WriteLine("  11. Изменить Автора");
            Console.WriteLine("  12. Удалить профиль читателя");
            Console.WriteLine("  13. Должники");
            Console.WriteLine("  14. Выйти из аккаунта");

            menuNavigator = Selector(14);

            if (menuNavigator == 14)
            {
                break;
            }

            switch (menuNavigator)
            {
                case 1:
                    CreateNewLibrerian();
                    break;

                case 2:
                    CreateNewReader();
                    break;

                case 3:
                    CreateAuthor();
                    break;

                case 4:
                    CreateBook();
                    break;

                case 5:
                    ShowAuthors();
                    break;

                case 6:
                    ShowLib();
                    break;

                case 7:
                    SearchBook();
                    break;

                case 8:
                    SearchAuthor();
                    break;

                case 9:
                    UpdateBook();
                    break;

                case 10:
                    UpdateReader();
                    break;

                case 11:
                    UpdateAuthor();
                    break;

                case 12:
                    DeleteReader();
                    break;

                case 13:
                    ShowDebtor();
                    break;
            }
        }
    }

    private void ShowLib()
    {
        int i = 0;
        foreach (var item in ctx.Books)
        {
            Console.WriteLine($"  {++i}. {item.Title} - {item.Year} - {item.Country} - {item.City}");
        }
        Console.ReadLine();
        Console.Clear();
    }

    private void ShowDebtor()
    {
        Console.Clear();
        var overdueRent = ctx.RentBooks.Where(rent => rent.ReturnDate == null).ToList();
        var sortedRent = overdueRent.OrderBy(rent => rent.ReturnDate)
                                      .ToList();

        foreach (var item in sortedRent)
        {
            if (item.ReturnDate < item.DueDate)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine($"Книга: {item.Book.Title} - Дата возвращения: {item.ReturnDate} - Должник: {item.Reader.LastName} {item.Reader.FirstName}");
            Console.ResetColor();
        }
        Console.ReadLine();
    }

    private void ShowAuthors()
    {
        int i = 0;
        foreach (var item in ctx.Authors)
        {
            Console.WriteLine($"  {++i}. {item}");
        }
        Console.ReadLine();
        Console.Clear();
    }

    private void CreateBook()
    {
        while (true)
        {
            Book book = new Book();
            string temp1 = "  Название: ";
            string temp2 = "  Год выпуска: ";
            string temp3 = "  Тип издания: ";
            string temp4 = "  Страна: ";
            string temp5 = "  Город: ";

            Console.WriteLine("Введите данные книги, которую хотите добавить:");

            Console.WriteLine(temp1);
            Console.WriteLine(temp2);
            Console.WriteLine(temp3);
            Console.WriteLine(temp4);
            Console.WriteLine(temp5);

            Console.SetCursorPosition(temp1.Length, 1);
            book.Title = Console.ReadLine();

            Console.SetCursorPosition(temp2.Length, 2);
            book.Year = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(temp3.Length, 3);
            temp3 = Console.ReadLine();
            
            bool exist = false;
            foreach (var item in ctx.PublisherTypes)
            {
                if (temp3 == item.TypeName)
                {
                    book.PublisherType = item;
                    exist = true;
                }
            }
            if (!exist)
            {
                book.PublisherType = new PublisherType { TypeName =  temp3};
            }

            Console.SetCursorPosition(temp4.Length, 4);
            book.Country = Console.ReadLine();

            Console.SetCursorPosition(temp5.Length, 5);
            book.City = Console.ReadLine();
            if (LookForBookByTitle(book))
            {
                Console.WriteLine("Уже существует");
            }
            else
            {
                try
                {
                    ctx.Books.Add(book);
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
                    Console.WriteLine("Книга успешно добавлена.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                break;
            }

        }
    }

    private void UpdateBook()
    {
        while (true)
        {
            Console.WriteLine("Введите название книги, которую хотите изменить");
            Book book = new Book { Title = Console.ReadLine() };
            if (LookForBookByTitle(book))
            {
                Book toUpdate = new Book();
                Console.Clear();
                Console.WriteLine("Введите данные, которые будут изменены");

                string temp1 = "  Название: ";
                string temp2 = "  Год выпуска: ";
                string temp3 = "  Тип издания: ";
                string temp4 = "  Страна: ";
                string temp5 = "  Город: ";

                Console.WriteLine(temp1);
                Console.WriteLine(temp2);
                Console.WriteLine(temp3);
                Console.WriteLine(temp4);
                Console.WriteLine(temp5);

                Console.SetCursorPosition(temp1.Length, 1);
                toUpdate.Title = Console.ReadLine();

                Console.SetCursorPosition(temp2.Length, 2);
                toUpdate.Year = int.Parse(Console.ReadLine());

                Console.SetCursorPosition(temp3.Length, 3);
                temp3 = Console.ReadLine();
                bool exist = false;
                foreach (var item in ctx.PublisherTypes)
                {
                    if (temp3 == item.TypeName)
                    {
                        toUpdate.PublisherType = item;
                        exist = true;
                    }
                }
                if (!exist)
                {
                    toUpdate.PublisherType = new PublisherType { TypeName = temp3 };
                }
                
                Console.SetCursorPosition(temp4.Length, 4);
                toUpdate.Country = Console.ReadLine();

                Console.SetCursorPosition(temp5.Length, 5);
                toUpdate.City = Console.ReadLine();

                try
                {
                    foreach (var item in ctx.Books)
                    {
                        if (item.Title == book.Title)
                        {
                            book = item;
                            break;
                        }
                    }
                    book.Title = toUpdate.Title;
                    book.Year = toUpdate.Year;
                    book.City = toUpdate.City;
                    book.Country = toUpdate.Country;
                    book.PublisherType = toUpdate.PublisherType;
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
                    Console.WriteLine("Книга успешно обновлена.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Такой книги не существует");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    private void CreateAuthor()
    {
        while (true)
        {
            Author author = new Author();

            string temp1 = "  Имя: ";
            string temp2 = "  Фамилия: ";
            string temp3 = "  Отчество: ";
            string temp4 = "  Год рождения: ";

            Console.WriteLine("Введите данные автора, которого хотите добавить:");

            Console.WriteLine(temp1);
            Console.WriteLine(temp2);
            Console.WriteLine(temp3);
            Console.WriteLine(temp4);

            Console.SetCursorPosition(temp1.Length, 1);
            author.FirstName = Console.ReadLine();

            Console.SetCursorPosition(temp2.Length, 2);
            author.LastName = Console.ReadLine();

            Console.SetCursorPosition(temp3.Length, 3);
            author.MiddleName = Console.ReadLine();

            Console.SetCursorPosition(temp4.Length, 4);
            var year = int.Parse(Console.ReadLine());
            author.DateOfBirth = new DateTime(year, 1, 1);

            if (LookForAuthor(author))
            {
                Console.Clear();
                Console.WriteLine("Уже существует");
                Thread.Sleep(1000);
                Console.Clear();
            }
            else
            {
                try
                {
                    ctx.Authors.Add(author);
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
                    Console.WriteLine("Автор успешно добавлен.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                break;
            }

        }
    }

    private void UpdateAuthor()
    {
        while (true)
        {
            Console.WriteLine("Введите фамилию и имя автора, которого хотите изменить");
            Author author = new Author();
            var temp1 = "  Фамилия: ";
            var temp2 = "  Имя: ";
            Console.WriteLine(temp1);
            Console.WriteLine(temp2);

            Console.SetCursorPosition(temp1.Length, 1);
            author.LastName = Console.ReadLine();

            Console.SetCursorPosition(temp2.Length, 2);
            author.FirstName = Console.ReadLine();



            if (LookForAuthor(author))
            {
                Author toUpdate = new Author();
                Console.Clear();
                temp1 = "  Имя: ";
                temp2 = "  Фамилия: ";
                var temp3 = "  Отчество: ";
                var temp4 = "  Год рождения: ";

                Console.WriteLine("Введите новые данные автора:");

                Console.WriteLine(temp1);
                Console.WriteLine(temp2);
                Console.WriteLine(temp3);
                Console.WriteLine(temp4);

                Console.SetCursorPosition(temp1.Length, 1);
                toUpdate.FirstName = Console.ReadLine();

                Console.SetCursorPosition(temp2.Length, 2);
                toUpdate.LastName = Console.ReadLine();

                Console.SetCursorPosition(temp3.Length, 3);
                toUpdate.MiddleName = Console.ReadLine();

                Console.SetCursorPosition(temp4.Length, 4);
                var year = int.Parse(Console.ReadLine());
                toUpdate.DateOfBirth = new DateTime(year, 1, 1);


                try
                {
                    foreach (var item in ctx.Authors)
                    {
                        if (item.FirstName == author.FirstName && item.LastName == author.LastName)
                        {
                            author = item;
                            break;
                        }
                    }
                    author.DateOfBirth = toUpdate.DateOfBirth;
                    author.FirstName = toUpdate.FirstName;
                    author.LastName = toUpdate.LastName;
                    author.MiddleName = toUpdate.MiddleName;

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
                    Console.WriteLine("Автор успешно обновлен.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Такого автора не существует");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
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

            Console.SetCursorPosition(temp6.Length, 6);
            temp6 = Console.ReadLine();
            
            bool exist = false;
            foreach (var item in ctx.DocumentTypes)
            {
                if (temp6 == item.TypeName)
                {
                    reader.DocumentType = item;
                    exist = true;
                }
            }
            if (!exist)
            {
                reader.DocumentType = new DocumentType { TypeName = temp6 };
            }

            Console.SetCursorPosition(temp7.Length, 7);
            reader.DocumentNumber = Console.ReadLine();


            if (LookForReader(reader))
            {
                Console.Clear();
                Console.WriteLine("Такой пользователь уже существует");
                Thread.Sleep(1000);
                Console.Clear();
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

    private void UpdateReader()
    {
        while (true)
        {
            Console.WriteLine("Введите логин пользователя, профиль которого хотите изменить");
            Reader toUpdate = new Reader();
            Reader reader = new Reader() { Login = Console.ReadLine() };
            if (LookForReader(reader))
            {
                Console.Clear();
                string temp1 = "  Логин: ";
                string temp2 = "  Пароль: ";
                string temp3 = "  Почта: ";
                string temp4 = "  Имя: ";
                string temp5 = "  Фамилия: ";
                string temp6 = "  Тип документа: ";
                string temp7 = "  Номер документа: ";

                Console.WriteLine("Введите данные автора, которого хотите добавить:");

                Console.WriteLine(temp1);
                Console.WriteLine(temp2);
                Console.WriteLine(temp3);
                Console.WriteLine(temp4);
                Console.WriteLine(temp5);
                Console.WriteLine(temp6);
                Console.WriteLine(temp7);

                Console.SetCursorPosition(temp1.Length, 1);
                toUpdate.Login = Console.ReadLine();

                Console.SetCursorPosition(temp2.Length, 2);
                toUpdate.Password = Console.ReadLine();

                Console.SetCursorPosition(temp3.Length, 3);
                toUpdate.Email = Console.ReadLine();

                Console.SetCursorPosition(temp4.Length, 4);
                toUpdate.FirstName = Console.ReadLine();

                Console.SetCursorPosition(temp5.Length, 5);
                toUpdate.LastName = Console.ReadLine();

                Console.SetCursorPosition(temp6.Length, 6);
                temp6 = Console.ReadLine();

                bool exist = false;
                foreach (var item in ctx.DocumentTypes)
                {
                    if (temp6 == item.TypeName)
                    {
                        toUpdate.DocumentType = item;
                        exist = true;
                    }
                }
                if (!exist)
                {
                    toUpdate.DocumentType = new DocumentType { TypeName = temp6 };
                }

                Console.SetCursorPosition(temp7.Length, 7);
                toUpdate.DocumentNumber = Console.ReadLine();

                try
                {
                    foreach (var item in ctx.Readers)
                    {
                        if (item.Login == reader.Login)
                        {
                            reader = item;
                            break;
                        }
                    }

                    reader.Login = toUpdate.Login;
                    reader.LastName = toUpdate.LastName;
                    reader.FirstName = toUpdate.FirstName;
                    reader.DocumentType = toUpdate.DocumentType;
                    reader.DocumentNumber = toUpdate.DocumentNumber;
                    reader.Email = toUpdate.Email;
                    reader.Password = toUpdate.Password;
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
                    Console.WriteLine("Читатель успешно обновлен.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Такого читателя не существует");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    private void DeleteReader()
    {
        while (true)
        {
            Console.WriteLine("Введите логин пользователя, которого хотите удалить");
            Reader reader = new Reader() { Login = Console.ReadLine() };
            if (LookForReader(reader))
            {
                try
                {
                    Reader toDelete = new Reader();
                    foreach (var item in ctx.Readers)
                    {
                        if (reader.Login == item.Login)
                        {
                            toDelete = item;
                            break;
                        }
                    }
                    ctx.Readers.Remove(toDelete);
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
                    Console.WriteLine("Читатель успешно удалён.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Такого читателя не существует");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    private bool LookForReader(Reader date)
    {
        foreach (var item in ctx.Readers)
        {
            if (item.Login == date.Login)
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
            Librerian librarian = new Librerian();
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

            if (!LibrerianTryCreate(librarian))
            {
                Console.Clear();
                Console.WriteLine("Такой библиотекарь уже существует");
                Thread.Sleep(1000);
                Console.Clear();
            }
            else
            {
                try
                {
                    ctx.Librerians.Add(librarian);
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

    private bool LibrerianTryCreate(Librerian librarian)
    {
        foreach (var item in ctx.Librerians)
        {
            if (item.Login == librarian.Login)
            {
                return false;
            }
        }
        return true;
    }
}