using System;
using System.Collections.Generic;
using System.Text;
using Visma_task.Classes;

namespace Visma_task.Filterings
{
    public abstract class BooksFilter
    {
        public List<Book> Books { get; set; }

        public BooksFilter(List<Book> books)
        {
            Books = books;
        }
    }
}
