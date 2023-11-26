using LibraryWebAPI.Models;

namespace LibraryWebAPI.Data
{
    public static class BookStore
    {
        public static List<Book> bookList = new List<Book>
        {
            new Book{Id=1, Title="Book1", Onloan = true,},
            new Book{Id=2, Title="Book2", Onloan = false},
            new Book{Id=3, Title="Book3", Onloan = false},
            new Book{Id=4, Title="Book4", Onloan = true},
            new Book{Id=5, Title="Book5", Onloan = true}
        };

    }
}

