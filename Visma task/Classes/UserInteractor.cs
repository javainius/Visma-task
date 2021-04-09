using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Visma_task.Enums;
using Visma_task.Filterings;
using Visma_task.Interfaces;

namespace Visma_task.Classes
{
    public class UserInteractor : IUserInteractor
    {
        private readonly Dictionary<string, IUserFilterCommand> _filterings;
        private ILibraryProcessor _libraryProcessor;

        public UserInteractor(ILibraryProcessor libraryProcessor)
        {
            _libraryProcessor = libraryProcessor;

            _filterings = new Dictionary<string, IUserFilterCommand>();

            _filterings.Add("availability", new FilterAvailableBooks(_libraryProcessor.Books));
            _filterings.Add("author", new FilterByAuthor(_libraryProcessor.Books));
            _filterings.Add("category", new FilterByCategory(_libraryProcessor.Books));
            _filterings.Add("isbn", new FilterByISBN(_libraryProcessor.Books));
            _filterings.Add("language", new FilterByLanguage(_libraryProcessor.Books));
            _filterings.Add("name", new FilterByName(_libraryProcessor.Books));
        }

        public void RunFilteringInteraction()
        {
            Console.WriteLine("type filter option: ");
            Console.WriteLine("options: availability, author, category, isbn, language, name");
            string[] actions = new string[] { "availability", "author", "category", "isbn", "language", "name" };

            string filterOption = Console.ReadLine();

            if (actions.Contains(filterOption.ToLower()))
            {
                Console.WriteLine("type in your filter phrase: ");
                string filterPhrase = Console.ReadLine();

                var filteredBooks = _filterings[filterOption].GetFilteredBooks(filterPhrase);

                foreach (var filteredBook in filteredBooks)
                {
                    Console.WriteLine($"book Id: {filteredBook.Id}");
                    Console.WriteLine($"book name: {filteredBook.Name}");
                    Console.WriteLine($"book author: {filteredBook.Author}");
                    Console.WriteLine($"book langauge: {filteredBook.Language}");
                    Console.WriteLine($"book ISBN: {filteredBook.ISBN}");
                    Console.WriteLine($"book name: {filteredBook.PublicationDate}\n");
                }
            }
            else
            {
                Console.WriteLine($"here is all the books because the command '{filterOption}' is unknown \n");

                foreach (var book in _libraryProcessor.Books)
                {
                    Console.WriteLine($"book Id: {book.Id}");
                    Console.WriteLine($"book name: {book.Name}");
                    Console.WriteLine($"book author: {book.Author}");
                    Console.WriteLine($"book langauge: {book.Language}");
                    Console.WriteLine($"book ISBN: {book.ISBN}");
                    Console.WriteLine($"book name: {book.PublicationDate}\n");
                }
            }

        }

        public void RunLibActions()
        {
            Console.WriteLine($"Please choose by typing the letter wether you want to take book, return it, delete it or add new book");
            Console.WriteLine("T --- for taking the book");
            Console.WriteLine("R --- for returning the book");
            Console.WriteLine("A --- for adding a new book");
            Console.WriteLine("D --- for deleting the book");

            string actionOption = Console.ReadLine().ToLower();

            switch (actionOption)
            {
                case "t":
                    TakeBook();
                    break;
                case "r":
                    ReturnBook();
                    break;
                case "a":
                    AddNewBook();
                    break;
                case "d":
                    DeleteBook();
                    break;
                default:
                    Console.WriteLine($"Sorry but command {actionOption} is not recognized");
                    break;
            }


        }
        private void TakeBook()
        {
            Console.WriteLine("Take book to home");
            Console.WriteLine("Write your name");
            string customerName = Console.ReadLine();
            Console.WriteLine("Write book's Id you want to take");
            int bookId = int.Parse(Console.ReadLine());
            Console.WriteLine("Write when you want to take a book: YYYY-MM-DD");
            var takingTime = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Write when you want to return a book: YYYY-MM-DD");
            var returningTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine(_libraryProcessor.TakeBook(bookId, customerName, takingTime, returningTime).ToDescriptionString());
        }

        private void ReturnBook()
        {
            Console.WriteLine("Type an Id of book you want to be returned");
            int bookId = int.Parse(Console.ReadLine());
            Console.WriteLine("When the book has to be returned");
            var returningTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine(_libraryProcessor.ReturnBook(bookId, returningTime).ToDescriptionString());
        }

        private void AddNewBook()
        {
            Console.WriteLine("Add new book: ");

            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Author: ");
            string author = Console.ReadLine();
            Console.WriteLine("Category: ");
            string category = Console.ReadLine();
            Console.WriteLine("Language: ");
            string language = Console.ReadLine();
            Console.WriteLine("Publication date: YYYY-MM-DD");
            string publicationDate = Console.ReadLine();
            Console.WriteLine("ISBN: ");
            string ISBN = Console.ReadLine();

            Book newBook = new Book()
            {
                Name = name,
                Author = author,
                Category = category,
                Language = language,
                PublicationDate = DateTime.Parse(publicationDate),
                ISBN = ISBN
            };

            _libraryProcessor.AddNewBook(newBook);
        }

        private void DeleteBook()
        {
            Console.WriteLine("write an id of a book you want to delete: ");
            int bookId = int.Parse(Console.ReadLine());

            Console.WriteLine(_libraryProcessor.DeleteBookById(bookId));
        }
    }
}

