using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.VisualBasic;

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
            List<GridSquare> targets = PreferenceSearch(controlledActor.Type, worldState);
            List<GridSquare> path = FindPath(targets, worldState.StateOfWorld[controlledActor.Position.X, controlledActor.Position.Y], worldState.StateOfWorld, new Point[worldState.worldWidth,worldState.worldHeight]);
        }

        private List<GridSquare> PreferenceSearch(ActorType actorType, WorldState worldState)
        {
            double maxPreference = 0;
            List<GridSquare> targets = new List<GridSquare>();
            foreach (GridSquare square in worldState.StateOfWorld)
            {
                double desirability = Desirability(square, controlledActor.Position);
                if (desirability > maxPreference)
                {
                    targets.Clear();
                    targets.Add(square);
                }
                else if (desirability == maxPreference)
                {
                    targets.Add(square);
                }
            }
            return targets;
        }

        private double Desirability(GridSquare target, Point currentLocation)
        {
            double desirability = 0;
            if (!target.IsPassable)
            {
                return 0;
            }
            desirability = (target.TotalNurture * controlledActor.NurtureLevel) + 
                           (target.TotalPlay * controlledActor.PlayLevel) + 
                           (target.TotalRampage * controlledActor.Mood) + 
                           (target.TotalSleep * controlledActor.SleepLevel);
           // desirability /= Utils.EuclideanDistance(currentLocation, target.Location);
            return desirability;
        }

        private List<GridSquare> FindPath(List<GridSquare> targets, GridSquare currentSquare, GridSquare[,] worldGrid, Point[,] parentArray)
        {
            Queue<GridSquare> queue = new Queue<GridSquare>();
            HashSet<GridSquare> seen = new HashSet<GridSquare>();
            List<GridSquare> path = new List<GridSquare>();
            queue.Enqueue(currentSquare);
            seen.Add(currentSquare);
            while (queue.Count != 0)
            {
                GridSquare lookingAt = queue.Dequeue();
                if (targets.Contains(lookingAt))
                {
                    //no, this does not actually work yet
                    List<Point> pointPath = ExtractPathFromTarget(parentArray, lookingAt.Location, currentSquare.Location);
                    foreach(Point point in pointPath)
                    {
                        path.Add(worldGrid[point.X, point.Y]);
                    }
                    return path;
                }
                else
                {
                    foreach (GridSquare gridSquare in lookingAt.Adjacent)
                    {
                        if (!seen.Contains(gridSquare))
                        {
                            seen.Add(gridSquare);
                            queue.Enqueue(gridSquare);
                            parentArray[gridSquare.Location.X, gridSquare.Location.Y] = lookingAt.Location;
                        }
                    }
                }
            }
            return path;
        }

        private List<Point> ExtractPathFromTarget(Point[,] parentArray, Point target, Point origin)
        {
            List<Point> path = new List<Point>();
            Point parent = parentArray[target.X, target.Y];
            path.Add(target);
            while (parent != origin)
            {
                path.Add(parent);
                parent = parentArray[parent.X, parent.Y];
            }
            path.Add(origin);
            path.Reverse();
            return path;
        }
    }
}
