using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MapstedAssignment.ModelndoorSpace;
using static MapstedAssignment.Program;

namespace MapstedAssignment
{
    // Main Navigation System
    public class IndoorNavigationSystem
    {
        private Dictionary<string, Room> rooms = new Dictionary<string, Room>();
        private List<Path> paths = new List<Path>();
        private IsDirectional isDirectional = new IsDirectional();

        // Set Directional Status
        public void SetDirection(bool direction)
        {
            isDirectional.Status = direction;
        }

        // Add a room
        public void AddRoom(string id, string name)
        {
            if (!rooms.ContainsKey(id))
            {
                rooms[id] = new Room(id, name);
                Console.WriteLine($"Room {name} added.");
            }
            else
            {
                Console.WriteLine("Room already exists.");
            }
        }

        // Remove a room
        public void RemoveRoom(string id)
        {
            if (rooms.ContainsKey(id))
            {
                rooms.Remove(id);
                paths.RemoveAll(p => p.StartRoom.Id == id || p.DesRoom.Id == id);
                Console.WriteLine($"Room {id} removed.");
            }
            else
            {
                Console.WriteLine("Room not found.");
            }
        }

        // Add a path between two rooms
        public void AddPath(string room1Id, string room2Id, int length)
        {
            if (rooms.ContainsKey(room1Id) && rooms.ContainsKey(room2Id))
            {
                var room1 = rooms[room1Id];
                var room2 = rooms[room2Id];
                paths.Add(new Path(room1, room2, length));
                if(!isDirectional.Status)  // condition for Add Path with direction status
                paths.Add(new Path(room2, room1, length)); // Bidirectional
                Console.WriteLine($"Path added between {room1.Name} and {room2.Name} with length {length}m.");
            }
            else
            {
                Console.WriteLine("One or both rooms not found.");
            }
        }

        // Remove a path between two rooms
        public void RemovePath(string room1Id, string room2Id)
        {
            var path = paths.FirstOrDefault(p => (p.StartRoom.Id == room1Id && p.DesRoom.Id == room2Id) ||
                                                 (p.StartRoom.Id == room2Id && p.DesRoom.Id == room1Id));
            if (path != null)
            {
                paths.Remove(path);
                Console.WriteLine($"Path between {room1Id} and {room2Id} removed.");
            }
            else
            {
                Console.WriteLine("Path not found.");
            }
        }

        // Display all available rooms
        public void DisplayRooms()
        {
            if (rooms.Count > 0)
            {
                Console.WriteLine("Rooms:");
                foreach (var room in rooms.Values)
                {
                    Console.WriteLine($"ID: {room.Id}, Name: {room.Name}");
                }
            }
            else
            {
                Console.WriteLine("No rooms available.");
            }
        }

        // Find the shortest path between two rooms using Dijkstra's algorithm
        public void FindShortestPath(string startRoomId, string endRoomId)
        {
            if (!rooms.ContainsKey(startRoomId) || !rooms.ContainsKey(endRoomId))
            {
                Console.WriteLine("One or both rooms not found.");
                return;
            }

            var startRoom = rooms[startRoomId];
            var endRoom = rooms[endRoomId];

            var distances = new Dictionary<string, int>();
            var previous = new Dictionary<string, string>();
            var unvisited = new HashSet<string>(rooms.Keys);

            foreach (var room in rooms)
            {
                distances[room.Key] = int.MaxValue;
                previous[room.Key] = null;
            }
            distances[startRoomId] = 0;

            while (unvisited.Count > 0)
            {
                // Get the room with the shortest distance
                var currentRoomId = unvisited.OrderBy(r => distances[r]).First();
                unvisited.Remove(currentRoomId);

                if (currentRoomId == endRoomId)
                    break;

                // Explore neighbors
                var neighbors = isDirectional.Status == true ? (paths.Where(p => p.StartRoom.Id == currentRoomId)) 
                    : (paths.Where(p => p.StartRoom.Id == currentRoomId || p.DesRoom.Id == currentRoomId)); // This condition is being used for Directional Purpose
                foreach (var path in neighbors)
                {
                    var neighborRoomId = path.StartRoom.Id == currentRoomId ? path.DesRoom.Id : path.StartRoom.Id;
                    if (!unvisited.Contains(neighborRoomId)) continue;

                    var newDist = distances[currentRoomId] + path.Length;
                    if (newDist < distances[neighborRoomId])
                    {
                        distances[neighborRoomId] = newDist;
                        previous[neighborRoomId] = currentRoomId;
                    }
                }
            }

            // Reconstruct the shortest path
            var pathList = new List<string>();
            var current = endRoomId;
            while (current != null)
            {
                pathList.Insert(0, current);
                current = previous[current];
            }

            // Display the result
            if (distances[endRoomId] == int.MaxValue)
            {
                Console.WriteLine("No path found.");
            }
            else
            {
                Console.WriteLine($"Shortest path from {startRoom.Name} to {endRoom.Name}:");
                Console.WriteLine(string.Join(" -> ", pathList));
                Console.WriteLine($"Total distance: {distances[endRoomId]}m.");
            }
        }
    }
}
