using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visma_task.Enums;
using Visma_task.Interfaces;

namespace Visma_task.Classes
{
    public class LibraryProcessor : ILibraryProcessor
    {
        private LibraryProcessorLogic _libararyProcessorLogic;

        public List<Customer> Customers { get; set; }
        public List<Book> Books { get; set; }

        public LibraryProcessor(List<Book> books)
        {
            Books = books;
            Customers = new List<Customer>();

            _libararyProcessorLogic = new LibraryProcessorLogic(books, Customers);
        }

        public LibraryProcessor(List<Book> books, List<Customer> customers)
        {
            Books = books;
            Customers = customers;

            _libararyProcessorLogic = new LibraryProcessorLogic(books, Customers);
        }

        public void AddNewBook(Book newBook) 
        {
            newBook.Id = Books.LastOrDefault().Id + 1;
            Books.Add(newBook);
        } 

        public BookTakingStatus TakeBook(int id, string takerName, DateTime sinceWhen, DateTime tillWhen)
        {
            var book = _libararyProcessorLogic.GetBookById(id);

            if (_libararyProcessorLogic.IsBookAlradyTaken(book))
                return BookTakingStatus.BookAlradyTaken;

            var bookTaker = _libararyProcessorLogic.GetCustomerByName(takerName);

            if (_libararyProcessorLogic.IsTimePeriodValid(sinceWhen, tillWhen))
                return BookTakingStatus.SpecifiedTimeTooLong;

            if (_libararyProcessorLogic.IsAmountOfTakenBooksValid(bookTaker))
                return BookTakingStatus.ReachedBookLimit;

            _libararyProcessorLogic.AssignCustomerToBook(book, bookTaker, sinceWhen, tillWhen);
            _libararyProcessorLogic.AssignBookToCustomer(book, bookTaker);

            return BookTakingStatus.SuccessfullyTaken;
        }

        public BookReturningStatus ReturnBook(int bookId, DateTime returnTime)
        {
            Book book = _libararyProcessorLogic.GetBookById(bookId);

            BookReturningStatus returnStatus = _libararyProcessorLogic.GetReturningStatus(book, returnTime);

            _libararyProcessorLogic.UnsignBookFromCustomer(book);
            _libararyProcessorLogic.UnsignCustomerFromBook(book);

            return returnStatus;
        }

        public string DeleteBookById(int id)
        {
            Books.Remove(_libararyProcessorLogic.GetBookById(id));

            return "Book is removed successfully";
        } 
    }
}
