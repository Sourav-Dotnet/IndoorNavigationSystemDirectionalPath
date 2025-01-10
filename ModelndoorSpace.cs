using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapstedAssignment
{
    internal class ModelndoorSpace
    {
        // Room class to represent rooms
        public class Room
        {
            public string Id { get; set; }  // This is unique identifier Id as string
            public string Name { get; set; }

            public Room(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        // Path class to represent paths between two rooms start and destination
        public class Path
        {
            public Room StartRoom { get; set; }
            public Room DesRoom { get; set; }
            public int Length { get; set; }            
            public Path(Room room1, Room room2, int length)
            {
                StartRoom = room1;
                DesRoom = room2;
                Length = length;                
            }
        }

        // This Status shows true means Directinal  
        public class IsDirectional
        {
            public bool Status { get; set; }
        }
    }
}
