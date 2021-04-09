using System;
using System.Collections.Generic;
using System.Text;
using Visma_task.Interfaces;

namespace Visma_task.Classes
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }
        public Customer BookHolder { get; set; }
        public DateTime? WhenTaken { get; set; }
        public DateTime? EstimatedReturn { get; set; }
    }
}
