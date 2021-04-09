using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Visma_task.Enums;
using Visma_task.Filterings;
using Visma_task.Interfaces;

namespace Visma_task.Classes
{
    public class Application : IApplication
    {
        private readonly string RepoFileName = "Books.json";
        private readonly IUserInteractor _userInteractor;
        private readonly ILibraryProcessor _libraryProcessor;

        public Application()
        {
            _libraryProcessor = new LibraryProcessor(GetBooksFromFile(RepoFileName));
            _userInteractor = new UserInteractor(_libraryProcessor);
        }

        public void Run()
        {
            while(true)
            {
                Console.WriteLine("Enter '1' if you want to filter books or enter any other value if you want to make actions with books: ");
                Console.WriteLine("Add new book, delete book, take or return book to the library");

                if (Console.ReadLine() == "1")
                    _userInteractor.RunFilteringInteraction();
                else
                {
                    _userInteractor.RunLibActions();
                    UpdateRepoFile();
                }
                
                Console.WriteLine("Write 'exit' to exit or just enter any other value");

                if (Console.ReadLine() == "exit")
                    break;
            } 
        }

        private List<Book> GetBooksFromFile(string fileName)
        {
            string json = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<List<Book>>(json);
        }

        private void UpdateRepoFile() => File.WriteAllText("Books.json", JsonConvert.SerializeObject(_libraryProcessor.Books));
    }
}
