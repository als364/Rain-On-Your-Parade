using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;

namespace Rain_On_Your_Parade
{
    public class GridSquare
    {

        private List<WorldObject> objects;       // list of objects on GridSquare
        private List<Actor> actors;              // list of actors on GridSquare
        public  List<GridSquare> adjacent;       // list of adjacent GridSquares
	    private bool isPassable;                 // can actors enter/see past GridSquare
	    private int totalSleep;                  // total sleep attr on GridSquare
	    private int totalPlay;                   // total play attr on GridSquare
	    private int totalNurture;                // total nurture attr on GridSquare
	    private int totalRampage;                // total rampage attr on GridSquare
        private Point location;

        #region Getters and Setters

        public List<WorldObject> Objects
        {
            get
            {
                return objects;
            }

            set
            {
                objects = value;
            }
        }

        public List<Actor> Actors
        {
            get
            {
                return actors;
            }

            set
            {
                actors = value;
            }
        }

        public List<GridSquare> Adjacent
        {
            get
            {
                return adjacent;
            }
            set
            {
                adjacent = value;
            }
        }

        public bool IsPassable
        {
            get
            {
                return isPassable;
            }

            set
            {
                isPassable = value;
            }
        }

        public int TotalSleep
        {
            get
            {
                foreach (WorldObject o in objects)
                {
                    totalSleep += o.Type.SleepLevel;
                }
                foreach (Actor a in actors)
                {
                    totalSleep += a.Type.GridSleepEffect;
                }
                return totalSleep;
            }

            set
            {
                totalSleep = value;
            }
        }

        public int TotalPlay
        {
            get
            {
                return totalPlay;
            }

            set
            {
                totalPlay = value;
            }
        }

        public int TotalNurture
        {
            get
            {
                return totalNurture;
            }

            set
            {
                totalNurture = value;
            }
        }

        public int TotalRampage
        {
            get
            {
                return totalRampage;
            }

            set
            {
                totalRampage = value;
            }
        }

        public Point Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }


        #endregion


        /// <summary>GridSquare Constructor</summary>
        /// <param name="o">ArrayList of objects on GridSquare</param>
        /// <param name="a">ArrayList of actors on GridSquare</param>
        /// <param name="is_passable">Ability of actors to enter/see past GridSquare</param>
        /// <devdoc>
        /// Create a GridSquare containing all objects and actors
        /// from the objectlist and actorlist, it's ability to be passed
        /// by actors, and the total amount of sleep, play, nurture, and rampage
        /// attributes present on the GridSquare.
        /// </devdoc>
        /// 

        public GridSquare(ObjectType.Type newObj, Point location)
        {
            WorldObject o = new WorldObject(newObj, location);
            objects = new List<WorldObject> () {o};
            actors = new List<Actor>();
            isPassable = o.Type.Passable;
            this.location = location;

            foreach (WorldObject obj in objects)
            {
                totalSleep += obj.Type.SleepLevel;
                totalPlay += obj.Type.PlayLevel;
                totalNurture += obj.Type.NurtureLevel;
                totalRampage += obj.Type.RampageLevel;
            }
            adjacent = new List<GridSquare>();
        }

        public GridSquare(ActorType.Type newAct, Point location)
        {
            Actor a = new Actor(newAct, location);
            objects = new List<WorldObject>();
            actors = new List<Actor>() { a };
            isPassable = true;
            this.location = location;

            foreach (Actor act in actors)
            {
                totalSleep += act.SleepLevel;
                totalPlay += act.PlayLevel;
                totalNurture += act.NurtureLevel;
                totalRampage += act.RampageLevel;
            }

            adjacent = new List<GridSquare>();
        }

        public GridSquare(List<WorldObject> o, List<Actor> a, Point location)
        {
            objects = o;
            actors = a;
            this.location = location;

            this.calculateLevels();

            adjacent = new List<GridSquare>();
        }

        public void calculateLevels(){
            totalSleep = 0;
            totalPlay = 0;
            totalNurture = 0;
            totalRampage = 0;

            foreach (WorldObject obj in objects)
            {
                totalSleep += obj.Type.SleepLevel;
                totalPlay += obj.Type.PlayLevel;
                totalNurture += obj.Type.NurtureLevel;
                totalRampage += obj.Type.RampageLevel;
                isPassable = isPassable || obj.Type.Passable;
            }

            foreach (Actor act in actors)
            {
                totalSleep += act.SleepLevel;
                totalPlay += act.PlayLevel;
                totalNurture += act.NurtureLevel;
                totalRampage += act.RampageLevel;
            }

        }

        public bool Contains(Vector2 point)
        {
            bool xContained = (point.X >= location.X * Canvas.SQUARE_SIZE - 2 && point.X <= location.X * Canvas.SQUARE_SIZE + 2);
            bool yContained = (point.Y >= location.Y * Canvas.SQUARE_SIZE - 2 && point.Y <= location.Y * Canvas.SQUARE_SIZE + 2);
           // Console.WriteLine("Point.X: " + point.X);
           // Console.WriteLine("Point.Y: " + point.Y);
           // Console.WriteLine("Location.X: " + location.X * Canvas.SQUARE_SIZE);
           // Console.WriteLine("Location.Y: " + location.Y * Canvas.SQUARE_SIZE);

            return xContained && yContained;
        }

        public override string ToString()
        {
            string foo = location.ToString() + ":\nPassable: " + isPassable + "\nTotal Sleep: " + totalSleep + "\nTotal Play: " + totalPlay +
                "\nTotal Nurture: " + totalNurture + "\nTotal Rampage: " + totalRampage + "\n Adjacent: \n";
            foreach (GridSquare g in adjacent)
            {
                foo += "\t" + g.Location.ToString() + "\n";
            }
            foo += "Objects:\n";
            foreach (WorldObject o in objects)
            {
                foo += o.ToString() + "\n";
            }
            foo += "Actors:\n";
            foreach (Actor a in actors)
            {
                foo += a.ToString() + "\n";
            }
            return foo;
        }

        /// <summary>Public stuff about the method</summary>
        /// <param name="foo">It's an integer apparently</param>
        /// <devdoc>This method doesn't do anything yet.</devdoc>
        public void doSomethingGridSquare(int foo) 
        {
            //do stuff here
        }
    }
}
