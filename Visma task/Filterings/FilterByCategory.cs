using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visma_task.Classes;
using Visma_task.Interfaces;

namespace Visma_task.Filterings
{
    class FilterByCategory : BooksFilter, IUserFilterCommand
    {
        public FilterByCategory(List<Book> books) : base(books) { }

        public List<Book> GetFilteredBooks(string filterArgument) => Books.Where(book => book.Category
                                                                    .IndexOf(filterArgument, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }
}
