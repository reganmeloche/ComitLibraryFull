using System;
using System.Collections.Generic;
using ComitLibrary.Storage;
using ComitLibrary.Models;

// NOT CURRENTLY USED
namespace ComitLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            // // Dependency Injection and init
            // var bookStorage = new BookStorageList();
            // var patronStorage = new PatronStorageList();
            // var loanStorage = new LoanStorageList();
            // var theLibrary = new LibrarySystem(bookStorage, patronStorage, loanStorage);

            // Console.WriteLine("Welcome to the ComIT Library!");

            // while (true) {
            //     Console.WriteLine("\nPlease select an option:\n" +
            //         "- c: checkout a book\n" + 
            //         "- r: return a book\n" +
            //         "- s: search for a book\n" +
            //         "- a: list all books\n" +
            //         "- p: list all patrons\n" +
            //         "- q: quit\n"
            //     );
            //     string userInput = Console.ReadLine();

            //     // Checkout a book
            //     if (userInput == "c") {
            //         try {
            //             Console.WriteLine("Please enter patron Id: ");
            //             string patronIdInput = Console.ReadLine();
            //             long patronId = Convert.ToInt64(patronIdInput);

            //             Console.WriteLine("Please enter the book Id: ");
            //             string bookIdInput = Console.ReadLine();
            //             long bookId = Convert.ToInt64(bookIdInput);

            //             Loan loan = theLibrary.CheckoutBook(patronId, bookId);
            //             if (loan != null) {
            //                 Console.WriteLine("Book has been checked out!");
            //             } else {
            //                 Console.WriteLine("Something went wrong. Could not check out book");
            //             }
            //         } catch (Exception e) {
            //             Console.WriteLine($"Error: {e.Message}");
            //         }
            //     }

            //     // Return a book
            //     if (userInput == "r") {
            //         try {
            //             Console.WriteLine("Please enter patron Id: ");
            //             string patronIdInput = Console.ReadLine();
            //             long patronId = Convert.ToInt64(patronIdInput);

            //             Console.WriteLine("Please enter the book Id: ");
            //             string bookIdInput = Console.ReadLine();
            //             long bookId = Convert.ToInt64(bookIdInput);

            //             theLibrary.ReturnBook(patronId, bookId);
            //             Console.WriteLine("Book has been checked in!");
            //         } catch (Exception e) {
            //             Console.WriteLine($"Error: {e.Message}");
            //         }
            //     }

            //     // Search for a book
            //     if (userInput == "s") {
            //         try {
            //             Console.WriteLine("What is the title you want to search for?");
            //             string titleToSearch = Console.ReadLine();
            //             List<Book> results = theLibrary.SearchForBook(titleToSearch);
                        
            //             if (results.Count == 0) {
            //                 Console.WriteLine("No matching books were found");
            //             } else {
            //                 foreach (var book in results) {
            //                     Console.WriteLine(book.ToString());
            //                 } 
            //             }
            //         } catch (Exception e) {
            //             Console.WriteLine($"Error: {e.Message}");
            //         }
                    
            //     }

            //     // List all books
            //     if (userInput == "a") {
            //         try {
            //             List<Book> results = theLibrary.GetAllBooks();
            //             foreach (var book in results) {
            //                 Console.WriteLine(book.ToString());
            //             } 
            //         } catch (Exception e) {
            //             Console.WriteLine($"Error: {e.Message}");
            //         }
            //     }

            //     // List all patrons
            //     if (userInput == "p") {
            //         try {
            //             List<Patron> results = theLibrary.GetAllPatrons();
            //             foreach (var patron in results) {
            //                 Console.WriteLine(patron.ToString());
            //             } 
            //         } catch (Exception e) {
            //             Console.WriteLine($"Error: {e.Message}");
            //         }
            //     }

            //     // Quit
            //     if (userInput == "q") {
            //         break;
            //     }
            // }
        }
    }
}
