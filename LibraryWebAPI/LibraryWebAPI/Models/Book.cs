using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryWebAPI.Models
{
    public class Book
    {
        public int BorrowerId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Onloan { get; set; }
        public DateTime Created { get; set; }
        public DateTime Returned { get; set; }
        public double Fine { get; set; }
        public bool Reserved { get; set; }
        public DateTime Availabledate { get; set; }


    }

}
