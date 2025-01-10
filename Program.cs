using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapstedAssignment
{
    internal class Program
    {      
        // Main method to interact with the user
        static void Main(string[] args)
        {
            // It's added for Directional Purpose
            Console.WriteLine("\n--- Indoor Navigation System ---");
            Console.WriteLine("Are you want to work with Direcional edges?");
            Console.WriteLine("Please enter Y for Yes and N for No.");
            Console.Write("Enter an option: ");
            var dirChoice = Console.ReadLine();

            var navigationSystem = new IndoorNavigationSystem();

            // Set direction status            
            bool isDirectional = false;
            if (dirChoice.ToUpper() == "Y")
            {
                isDirectional = true;
                navigationSystem.SetDirection(isDirectional);
            }                       

            // Predifine set of rooms and paths for single floor layout
            navigationSystem.AddRoom("R1", "ROOM A");
            navigationSystem.AddRoom("R2", "ROOM B");
            navigationSystem.AddRoom("R3", "ROOM C");
            navigationSystem.AddRoom("R4", "ROOM D");
            navigationSystem.AddRoom("R5", "ROOM E");

            navigationSystem.AddPath("R1", "R2", 20);
            navigationSystem.AddPath("R1", "R3", 35);
            navigationSystem.AddPath("R1", "R4", 30);
            navigationSystem.AddPath("R1", "R5", 60);

            navigationSystem.AddPath("R2", "R1", 5);  // It's added for Directional Purpose
            navigationSystem.AddPath("R2", "R3", 10);
            navigationSystem.AddPath("R2", "R4", 15);

            navigationSystem.AddPath("R3", "R4", 20);
            navigationSystem.AddPath("R3", "R5", 40);

            navigationSystem.AddPath("R4", "R5", 15);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n--- Indoor Navigation System ---");
                Console.WriteLine("1. View All Available Rooms");
                Console.WriteLine("2. Find Shortest Path between two Rooms by entering Start Room and Destination Room IDs");
                Console.WriteLine("3. Add Room");
                Console.WriteLine("4. Remove Room");
                Console.WriteLine("5. Add Path");
                Console.WriteLine("6. Remove Path");               
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");
                                               
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": 
                        navigationSystem.DisplayRooms();
                        break;
                    case "2":
                        Console.Write("Enter Start Room ID: ");
                        string startRoomId = Console.ReadLine();
                        Console.Write("Enter Destination Room ID: ");
                        string endRoomId = Console.ReadLine();                       
                        navigationSystem.FindShortestPath(startRoomId, endRoomId);
                        break;
                    case "3":
                        Console.Write("Enter Room ID: ");
                        string roomId = Console.ReadLine();
                        Console.Write("Enter Room Name: ");
                        string roomName = Console.ReadLine();
                        navigationSystem.AddRoom(roomId, roomName);
                        break;
                    case "4":
                        Console.Write("Enter Room ID to remove: ");
                        roomId = Console.ReadLine();
                        navigationSystem.RemoveRoom(roomId);
                        break;
                    case "5":
                        Console.Write("Enter Start Room ID: ");
                        string room1Id = Console.ReadLine();
                        Console.Write("Enter Destination Room ID: ");
                        string room2Id = Console.ReadLine();
                        Console.Write("Enter Path Length: ");
                        int length = int.Parse(Console.ReadLine());
                        navigationSystem.AddPath(room1Id, room2Id, length);
                        break;
                    case "6":
                        Console.Write("Enter Start Room ID: ");
                        room1Id = Console.ReadLine();
                        Console.Write("Enter Destination ID: ");
                        room2Id = Console.ReadLine();
                        navigationSystem.RemovePath(room1Id, room2Id);
                        break;                   
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }    
}
