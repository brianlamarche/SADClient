using System;
using System.IO;
using System.Threading;
using TargetServerCommunicator;

namespace TestTargetServer
{
    class Program
    {       
        static void TestJson()
        {
            Console.WriteLine("Reading test data...");
            string data = File.ReadAllText("jsonTargets.txt");
            Console.WriteLine(data);
        }

        static void Main(string[] args)
        {
            TestJson();

            string teamName = "sqrtdos";
            var gameServer  = new TargetServerInterface(teamName);
            var data        = gameServer.RetrieveGameList();

            Console.WriteLine("Available Games:");
            foreach(var game in data)
            {
                Console.WriteLine(game);
            }

            Console.WriteLine("Start Game [Enter Name]?");
            var gameName = Console.ReadLine();
            gameServer.StartGame(gameName);
             
            Console.WriteLine("Any key to stop:");

            Console.WriteLine("Printing target data...");
            var targets = gameServer.RetrieveTargetList(gameName);
            foreach(var target in targets)
            {
                Console.WriteLine(target);
            }

            Console.WriteLine("Stopping Game");
            gameServer.StopRunningGame();
            
            
            Console.WriteLine("Any key to exit...");
            Console.ReadLine();
        }
    }
}
