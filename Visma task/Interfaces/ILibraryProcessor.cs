using System;
using System.Collections.Generic;
using System.Text;
using Visma_task.Classes;
using Visma_task.Enums;

namespace Visma_task.Interfaces
{
    public interface ILibraryProcessor
    {
        public List<Customer> Customers { get; set; }
        public List<Book> Books { get; set; }

        public BookTakingStatus TakeBook(int id, string takerName, DateTime sinceWhen, DateTime tillWhen);
        public void AddNewBook(Book newBook);
        public BookReturningStatus ReturnBook(int bookId, DateTime returnTime);
        public string DeleteBookById(int id);
    }
}
