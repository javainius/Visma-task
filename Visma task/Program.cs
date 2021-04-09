using System;
using Visma_task.Classes;
using Visma_task.Interfaces;

namespace Visma_task
{
    class Program
    {
        private static IApplication _application;
        static void Main(string[] args)
        {
            _application = new Application();

            _application.Run();
        }
    }
}
