using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieApp
{
    internal class Menus
    {
        public static void ShowMenus()
        {

            bool showMainMenu = true;

            while (showMainMenu)
            {
                LoginMenu();
                showMainMenu = MainMenu();
            }
        }

        public static string LoginMenu()
        {
            Console.WriteLine("1) Login");
            Console.WriteLine("2) Create account");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    return username;
                case "2":
                    Console.Write("Username? ");
                    string newUsername = Console.ReadLine();
                    return newUsername;
            };

            return "";
        }

        public static async bool MainMenu()
        {
            Console.WriteLine("Choose an option");

            Console.WriteLine("1) Add movie to database");
            Console.WriteLine("2) Add movie to users watchlist");
            Console.WriteLine("3) View users watchlist");
            Console.WriteLine("4) Create user");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Movie title: ");
                    string movieTitle = Console.ReadLine();

                    Console.Write("Release year: ");
                    string releaseYear = Console.ReadLine();

                    string jsonResponse = await MovieList.GetMovieAysnc(Program.client, movieTitle, releaseYear);
                    MovieList.AddMovieToDb(jsonResponse);

                    return false;


            }
        }
    }
}
