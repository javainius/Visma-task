using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Visma_task.Classes;
using Visma_task.Interfaces;

namespace Visma_task.Filterings
{
    public class FilterByLanguage : BooksFilter, IUserFilterCommand
    {
        public FilterByLanguage(List<Book> books) : base(books) { }

        public List<Book> GetFilteredBooks(string filterArgument) => Books.Where(book => book.Language
                                                                    .IndexOf(filterArgument, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }
}
