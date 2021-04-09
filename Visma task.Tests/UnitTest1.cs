using NUnit.Framework;
using System.Collections.Generic;
using Visma_task.Classes;
using FluentAssertions;
using System;
using Visma_task.Enums;

namespace Visma_task.Tests
{
    public class LibraryProcessorTests
    {
        [Test]
        public void AddNewBook_Given_ListWithoutNewBook_ListWithOneNewBook()
        {
            //Arrange
            List<Book> initialBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling"
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin"
                }
            };

            List<Book> finalBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling"
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin"
                },
                new Book()
                {
                    Id = 2,
                    Name = "Range",
                    Author = "David Epstein"
                }
            };

            LibraryProcessor libProcessor = new LibraryProcessor(initialBookList);

            //Act
            libProcessor.AddNewBook(new Book()
            {
                Name = "Range",
                Author = "David Epstein"
            });

            //Assert
            libProcessor.Books.Should().BeEquivalentTo(finalBookList);
        }

        [Test]
        public void AddNewBook_Given_ListWithoutNewBooks_ListWithTwoNewBooks()
        {
            //Arrange
            List<Book> initialBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling"
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin"
                }
            };

            List<Book> finalBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling"
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin"
                },
                new Book()
                {
                    Id = 2,
                    Name = "Range",
                    Author = "David Epstein"
                },
                new Book()
                {
                    Id = 3,
                    Name = "New book",
                    Author = "New book's author"
                }
            };

            LibraryProcessor libProcessor = new LibraryProcessor(initialBookList);

            //Act
            libProcessor.AddNewBook(new Book()
            {
                Name = "Range",
                Author = "David Epstein"
            });

            libProcessor.AddNewBook(new Book()
            {
                Name = "New book",
                Author = "New book's author"
            });

            //Assert
            libProcessor.Books.Should().BeEquivalentTo(finalBookList);
        }

        [Test]
        public void TakeBook_Given_BooksList_BookTakenSuccessfully()
        {
            //Arrange

            List<Book> initialBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling"
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin"
                },
                new Book()
                {
                    Id = 2,
                    Name = "Range",
                    Author = "David Epstein"
                },
                new Book()
                {
                    Id = 3,
                    Name = "New book",
                    Author = "New book's author"
                }
            };

            DateTime dateTheBookWillBeTaken = DateTime.Parse("2020-05-06");
            DateTime dateTheBookWillBeReturned = DateTime.Parse("2020-06-06");

            LibraryProcessor libProcessor = new LibraryProcessor(initialBookList);

            //Act
            BookTakingStatus bookTakingStatus = libProcessor.TakeBook(1, "Tom", dateTheBookWillBeTaken, dateTheBookWillBeReturned);

            //Assert
            bookTakingStatus.Should().BeEquivalentTo(BookTakingStatus.SuccessfullyTaken);
        }

        [Test]
        public void TakeBook_Given_TakenBooksList_BookAlradyTaken()
        {
            //Arrange

            List<Book> initialBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling",
                    BookHolder = new Customer()
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin",
                    BookHolder = new Customer()
                },
                new Book()
                {
                    Id = 2,
                    Name = "Range",
                    Author = "David Epstein",
                    BookHolder = new Customer()
                },
                new Book()
                {
                    Id = 3,
                    Name = "New book",
                    Author = "New book's author",
                    BookHolder = new Customer()
                }
            };

            DateTime dateTheBookWillBeTaken = DateTime.Parse("2020-05-06");
            DateTime dateTheBookWillBeReturned = DateTime.Parse("2020-06-06");

            LibraryProcessor libProcessor = new LibraryProcessor(initialBookList);

            //Act
            BookTakingStatus bookTakingStatus = libProcessor.TakeBook(1, "Tom", dateTheBookWillBeTaken, dateTheBookWillBeReturned);

            //Assert
            bookTakingStatus.Should().BeEquivalentTo(BookTakingStatus.BookAlradyTaken);
        }

        [Test]
        public void TakeBook_Given_BooksList_SpecifiedTimeTooLong()
        {
            //Arrange
            List<Book> initialBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling",
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin",
                },
                new Book()
                {
                    Id = 2,
                    Name = "Range",
                    Author = "David Epstein",
                },
                new Book()
                {
                    Id = 3,
                    Name = "New book",
                    Author = "New book's author",
                }
            };

            DateTime dateTheBookWillBeTaken = DateTime.Parse("2020-05-06");
            DateTime dateTheBookWillBeReturned = DateTime.Parse("2020-08-06");

            LibraryProcessor libProcessor = new LibraryProcessor(initialBookList);

            //Act
            BookTakingStatus bookTakingStatus = libProcessor.TakeBook(1, "Tom", dateTheBookWillBeTaken, dateTheBookWillBeReturned);

            //Assert
            bookTakingStatus.Should().BeEquivalentTo(BookTakingStatus.SpecifiedTimeTooLong);
        }

        [Test]
        public void TakeBook_Given_CustomerWithReachedLimit_ReachedBookLimit()
        {
            //Arrange
            List<Book> initialBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling",
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin",
                },
                new Book()
                {
                    Id = 2,
                    Name = "Range",
                    Author = "David Epstein",
                },
                new Book()
                {
                    Id = 3,
                    Name = "New book",
                    Author = "New book's author",
                }
            };

            DateTime dateTheBookWillBeTaken = DateTime.Parse("2020-05-06");
            DateTime dateTheBookWillBeReturned = DateTime.Parse("2020-06-06");

            LibraryProcessor libProcessor = new LibraryProcessor(initialBookList, new List<Customer>()
                {
                    new Customer()
                    {
                        Name = "Jack",
                        TakenBookIdsList = new List<int>(){ 2 }
                    },
                    new Customer()
                    {
                        Name = "Tom",
                        TakenBookIdsList = new List<int>(){ 0, 2, 3 }
                    }
                }
            );

            //Act
            BookTakingStatus bookTakingStatus = libProcessor.TakeBook(1, "Tom", dateTheBookWillBeTaken, dateTheBookWillBeReturned);

            //Assert
            bookTakingStatus.Should().BeEquivalentTo(BookTakingStatus.ReachedBookLimit);
        }

        [Test]
        public void ReturnBook_Given_NotReturnedBook_ReturnedOnTime()
        {
            //Arrange
            List<Book> initialBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling",
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin",
                    BookHolder = new Customer()
                    {
                        Name = "Tom",
                        TakenBookIdsList = new List<int>(){ 1 }
                    },
                    EstimatedReturn = DateTime.Parse("2020-05-06")
                },
            };

            LibraryProcessor libProcessor = new LibraryProcessor(initialBookList);

            //Act
            BookReturningStatus bookTakingStatus = libProcessor.ReturnBook(1, DateTime.Parse("2020-05-05"));

            //Assert
            bookTakingStatus.Should().BeEquivalentTo(BookReturningStatus.ReturnedOnTime);
        }

        [Test]
        public void ReturnBook_Given_NotReturnedBook_ReturnedAfterEstimatedTime()
        {
            //Arrange
            List<Book> initialBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling",
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin",
                    BookHolder = new Customer()
                    {
                        Name = "Tom",
                        TakenBookIdsList = new List<int>(){ 1 }
                    },
                    EstimatedReturn = DateTime.Parse("2020-05-06")
                },
            };

            LibraryProcessor libProcessor = new LibraryProcessor(initialBookList);

            //Act
            BookReturningStatus bookTakingStatus = libProcessor.ReturnBook(1, DateTime.Parse("2020-05-07"));

            //Assert
            bookTakingStatus.Should().BeEquivalentTo(BookReturningStatus.ReturnedAfterEstimatedTime);
        }

        [Test]
        public void DeleteBookById_Given_ExistingBooksList_BooksWithDeletedBook()
        {
            //Arrange
            List<Book> initialBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling"
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin"
                },
                new Book()
                {
                    Id = 2,
                    Name = "Range",
                    Author = "David Epstein"
                },
                new Book()
                {
                    Id = 3,
                    Name = "New book",
                    Author = "New book's author"
                }
            };

            List<Book> finalBookList = new List<Book>()
            {
                new Book()
                {
                    Id = 0,
                    Name = "Harry Potter",
                    Author = "Rowling"
                },
                new Book()
                {
                    Id = 1,
                    Name = "The Lord of the Rings",
                    Author = "Tolkin"
                },
                new Book()
                {
                    Id = 3,
                    Name = "New book",
                    Author = "New book's author"
                }
            };

            LibraryProcessor libProcessor = new LibraryProcessor(initialBookList);

            //Act
            libProcessor.DeleteBookById(2);

            //Assert
            libProcessor.Books.Should().BeEquivalentTo(finalBookList);
        }
    }
}