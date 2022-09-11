using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Caching;



namespace ChuckNorrisConsoleApp
{
    class Program
    {
        const string _CHUCK_NORRIS_API_BASE = "https://api.chucknorris.io";
        static char _key;
        static int _JokeID = 0;
        private static readonly Dictionary<int, CacheItem> _internalCache = new Dictionary<int, CacheItem>(); 
        static int _CacheJokeID;

         public static async Task Main(string[] args)
        {
            PrintScreen("Initial",null, null);
         
            string[] joke = await GetRandomJokes();
            //Set(_JokeID, joke[0], 3);
            //PrintScreen("NewJoke", _JokeID, joke);


            while (_key != 'e')
            {
                if (_key == 'j' || _key == 'y')
                {
                    _JokeID++;
                    joke = await GetRandomJokes();
                    Set(_JokeID, joke[0], 3);

                    _CacheJokeID = _JokeID;
                    PrintScreen("NewJoke", _JokeID, joke);
                }
                else if (_key == 'n')
                {
                    _CacheJokeID = _CacheJokeID + 1;
                    joke[0] = Get(_CacheJokeID);
                    if (joke[0] != null) 
                    { 
                        PrintScreen("NewJoke", _CacheJokeID, joke); 
                    }
                    else 
                    { 
                        PrintScreen("Error", null, null); 
                    }
                }
                else if (_key == 'p')
                {
                    _CacheJokeID = _CacheJokeID - 1;
                    joke[0] = Get(_CacheJokeID);
                    if (joke[0] != null) 
                    { 
                        PrintScreen("NewJoke", _CacheJokeID, joke); 
                    }
                    else 
                    { 
                        PrintScreen("Error", null, null); 
                    }
                }
                else {
                    _key = 'e';
                }
            }  
        }

         /// <summary>
         /// Set Jokes in Cache values
         /// </summary>
         /// <param name="keyValue"></param>
         /// <param name="joke"></param>
         /// <param name="expiresMinutes"></param>         
        public static void Set(int  keyValue, string joke, int expiresMinutes)
        {
            _internalCache.Add(keyValue, new CacheItem(joke, expiresMinutes));
        }

        /// <summary>
        /// Get Joke from Cache 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public static string Get(int keyValue)
        {
            string result=null;
            if (_internalCache.ContainsKey(keyValue))
            {
                result = _internalCache[keyValue].Key.ToString();
            }
            return result;

        }

        /// <summary>
        /// Print Console based on scenario
        /// </summary>
        /// <param name="states"></param>
        /// <param name="JokeID"></param>
        /// <param name="joke"></param>
        private static void PrintScreen(string states, int? JokeID, string[] joke)
        {
            switch (states)
            {
                case "Initial":
                    //Console.Clear();
                    Console.WriteLine("========================================================");
                    Console.WriteLine("|          <= Chuck Norris Joke Generator  =>           |");
                    Console.WriteLine("|                Ready to hear a great Joke... |");
                    Console.WriteLine("|                    Press y for yes                   |");
                    Console.WriteLine("|                    Press e to exit                   |");
                    Console.WriteLine("========================================================");
                    GetEnteredKey(Console.ReadKey());
                    break;

                case "NewJoke":
                    //Console.Clear();
                    Console.WriteLine("\n=============================================================================================");
                    Console.WriteLine(string.Join("     ", JokeID));
                    Console.WriteLine(string.Join("\n ", joke));                    
                    Console.WriteLine("\n Press P to prev        J to New Joke        E to exit                    Press N to next ");
                    GetEnteredKey(Console.ReadKey());
                    break;

                case "Error":
                    Console.WriteLine("\n----------------------");
                    Console.WriteLine("|   Thats the end..  |");
                    Console.WriteLine("----------------------");
                    Console.WriteLine("\n Press P to prev       J to New Joke         E to exit                    Press N to next ");
                    Console.WriteLine("==============================================================================================");
                    GetEnteredKey(Console.ReadKey());
                    break;

            }    
                 
        }

        /// <summary>
        /// Define values to input keys
        /// </summary>
        /// <param name="consoleKeyInfo"></param>
        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.J:
                    _key = 'j';
                    break;
                case ConsoleKey.P:
                    _key = 'p';
                    break;
                case ConsoleKey.N:
                    _key = 'n';
                    break;
                case ConsoleKey.E:
                    _key = 'e';
                    break;
                case ConsoleKey.Y:
                    _key = 'y';
                    break;

            }
        }
        
        /// <summary>
        /// Get random Joke from helper class
        /// </summary>
        /// <returns></returns>
        private static async Task<String[]> GetRandomJokes()
        {            
            string[] joke = APIHelperClass.GetRandomJokes(_CHUCK_NORRIS_API_BASE);
            return joke;
        }
    }
}
