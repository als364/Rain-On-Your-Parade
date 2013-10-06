using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Rain_On_Your_Parade
{
    class ActorController : Controller
    {
        Actor controlledActor;

        public ActorController(Actor actor) : base(actor)
        {
            controlledActor = actor;
        }

        public override void Update(GameTime gameTime, WorldState worldState)
        {
            GridSquare target = preferenceSearch(controlledActor.getType(), worldState);
        }

        public GridSquare preferenceSearch(ActorType actorType, WorldState worldState)
        {
            
        }

        public List<GridSquare> breadthFirstSearch(GridSquare target, GridSquare currentSquare, GridSquare[][] worldGrid, Point[][] parentArray)
        {
            Queue<GridSquare> queue = new Queue<GridSquare>();
            HashSet<GridSquare> seen = new HashSet<GridSquare>();
            List<GridSquare> path = new List<GridSquare>();
            queue.Enqueue(currentSquare);
            seen.Add(currentSquare);
            while (queue.Count != 0)
            {
                GridSquare lookingAt = queue.Dequeue();
                if (lookingAt.Equals(target))
                {
                    //no, this does not actually work yet
                    List<Point> pointPath = findPath(parentArray, lookingAt.Location, currentSquare.Location);
                    foreach(Point point in pointPath)
                    {
                        path.Add(worldGrid[point.X][point.Y]);
                    }
                }
                else
                {
                    foreach (GridSquare gridSquare in lookingAt.Adjacent)
                    {
                        if (!seen.Contains(gridSquare))
                        {
                            seen.Add(gridSquare);
                            queue.Enqueue(gridSquare);
                            parentArray[gridSquare.Location.X][gridSquare.Location.Y] = lookingAt.Location;
                        }
                    }
                }
            }
        }

        private List<Point> findPath(Point[][] parentArray, Point lookingAt, Point origin)
        {
            List<Point> path = new List<Point>();
            Point parent = parentArray[lookingAt.X][lookingAt.Y];
            path.Add(lookingAt);
            while (parent != origin)
            {
                path.Add(parent);
                parent = parentArray[parent.X][parent.Y];
            }
            path.Add(origin);
            path.Reverse();
            return path;
        }
    }
}
