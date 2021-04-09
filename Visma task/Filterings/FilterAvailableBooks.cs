using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visma_task.Classes;
using Visma_task.Interfaces;

namespace Visma_task.Filterings
{
    public class FilterAvailableBooks : BooksFilter, IUserFilterCommand
    {
        public FilterAvailableBooks(List<Book> books) : base(books) { }

        public List<Book> GetFilteredBooks(string filterArgument) => Books.Where(book => book.BookHolder == null).ToList();
    }
}
