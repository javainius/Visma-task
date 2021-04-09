using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visma_task.Enums;

namespace Visma_task.Classes
{
    public class LibraryProcessorLogic
    {
        public List<Book> Books { get; set; }
        public List<Customer> Customers { get; set; }

        public LibraryProcessorLogic(List<Book> books, List<Customer> customers)
        {
            Books = books;
            Customers = customers;
        }

        public void AssignCustomerToBook(Book book, Customer customer, DateTime sinceWhen, DateTime tillWhen)
        {
            book.BookHolder = customer;
            book.WhenTaken = sinceWhen;
            book.EstimatedReturn = tillWhen;
        }

        public void AssignBookToCustomer(Book book, Customer customer)
        {
            customer.TakenBookIdsList.Add(book.Id);
        }

        public void UnsignCustomerFromBook(Book book)
        {
            book.BookHolder = null;
            book.WhenTaken = null;
            book.EstimatedReturn = null;
        }

        public void UnsignBookFromCustomer(Book book) => book.BookHolder.TakenBookIdsList.Remove(book.Id);

        public Customer GetCustomerByName(string customerName)
        {
            var customer = Customers.Where(customer => customer.Name == customerName).FirstOrDefault();

            return customer != null ? customer : new Customer() { Name = customerName, TakenBookIdsList = new List<int>() };
        }

        public Book GetBookById(int id) => Books.Find(book => book.Id == id);

        public bool IsTimePeriodValid(DateTime sinceWhen, DateTime tillWhen) =>
                                                (tillWhen - sinceWhen).TotalDays > BookTakingConstants.AllowedNumberOfDays();
        public bool IsAmountOfTakenBooksValid(Customer customer) => customer.TakenBookIdsList.Count >= BookTakingConstants.AllowedNumberOFBooks();

        public BookReturningStatus GetReturningStatus(Book book, DateTime returnTime) => book.EstimatedReturn < returnTime ?
                                                                           BookReturningStatus.ReturnedAfterEstimatedTime : BookReturningStatus.ReturnedOnTime;

        public bool IsBookAlradyTaken(Book book) => book.BookHolder != null;
    }
}
