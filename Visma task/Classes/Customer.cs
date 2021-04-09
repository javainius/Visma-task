using System;
using System.Collections.Generic;
using System.Text;
using Visma_task.Interfaces;

namespace Visma_task.Classes
{
    public class Customer
    {
        public string Name { get; set; }
        public List<int> TakenBookIdsList { get; set; }
    }
}
