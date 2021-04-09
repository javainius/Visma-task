using System;
using System.Collections.Generic;
using System.Text;
using Visma_task.Classes;

namespace Visma_task.Interfaces
{
    interface IUserFilterCommand
    {
        public List<Book> GetFilteredBooks(string filterArgument);
    }
}
