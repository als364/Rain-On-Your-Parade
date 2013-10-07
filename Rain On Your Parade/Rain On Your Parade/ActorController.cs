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
            switch (controlledActor.State.State)
            {
                /*case ActorState.AState.Nurture:
                    break;
                case ActorState.AState.Play:
                    break;
                case ActorState.AState.Rampage:
                    break;
                case ActorState.AState.Sleep:
                    break;*/
                case ActorState.AState.Seek:
                    ActorState.AState newState = DetermineTargetState();
                    controlledActor.TargetState = newState;
                    controlledActor.Path = FindPath(PreferenceSearch(worldState),
                                                        worldState.StateOfWorld[(int)controlledActor.Position.X, (int)controlledActor.Position.Y],
                                                        worldState.StateOfWorld, new Point[worldState.worldWidth, worldState.worldHeight]);
                    controlledActor.State = new ActorState(ActorState.AState.Walk);
                    break;
                case ActorState.AState.Walk:
                    if (controlledActor.Path.Count == 0)
                    {
                        controlledActor.State = new ActorState(ActorState.AState.Seek);
                    }
                    else if (controlledActor.Path.Count == 1)
                    {
                        controlledActor.State = new ActorState(controlledActor.TargetState);
                    }
                    else
                    {
                        GridSquare nextSquare = controlledActor.Path[0];
                        if (nextSquare.Contains(controlledActor.Position))
                        {
                            controlledActor.Path.RemoveAt(0);
                            nextSquare = controlledActor.Path[0];
                            controlledActor.Velocity = new Vector2(nextSquare.Location.X - controlledActor.Position.X, nextSquare.Location.Y - controlledActor.Position.Y);
                        }
                        controlledActor.Position = Vector2.Add(controlledActor.Position, controlledActor.Velocity);
                    }
                    break;
                default:
                    if (controlledActor.Path.Count == 0)
                    {
                        controlledActor.State = new ActorState(ActorState.AState.Seek);
                    }
                    else if (controlledActor.Path.Count == 1) //at target!
                    {
                        controlledActor.Velocity = new Vector2();
                        if (!PreferenceSearch(worldState).Contains(controlledActor.Path[0]))
                        {
                            controlledActor.State = new ActorState(ActorState.AState.Seek);
                        }
                    }
                    else
                    {
                    }
                    break;
            }
        }

        private ActorState.AState DetermineTargetState()
        {
            ActorState.AState currentState = controlledActor.State.State;
            int maxLevel = 0;
            List<ActorState.AState> highestNeeds = new List<ActorState.AState>();
            if (controlledActor.Mood > maxLevel)
            {
                maxLevel = controlledActor.Mood;
                highestNeeds.Clear();
                highestNeeds.Add(ActorState.AState.Rampage);
            }
            else if (controlledActor.Mood == maxLevel)
            {
                highestNeeds.Add(ActorState.AState.Rampage);
            }
            if (controlledActor.NurtureLevel > maxLevel)
            {
                maxLevel = controlledActor.NurtureLevel;
                highestNeeds.Clear();
                highestNeeds.Add(ActorState.AState.Nurture);
            }
            else if (controlledActor.NurtureLevel == maxLevel)
            {
                highestNeeds.Add(ActorState.AState.Nurture);
            }
            if (controlledActor.PlayLevel > maxLevel)
            {
                maxLevel = controlledActor.PlayLevel;
                highestNeeds.Clear();
                highestNeeds.Add(ActorState.AState.Play);
            }
            else if (controlledActor.PlayLevel == maxLevel)
            {
                highestNeeds.Add(ActorState.AState.Play);
            }
            if (controlledActor.SleepLevel > maxLevel)
            {
                maxLevel = controlledActor.SleepLevel;
                highestNeeds.Clear();
                highestNeeds.Add(ActorState.AState.Sleep);
            }
            else if (controlledActor.SleepLevel == maxLevel)
            {
                highestNeeds.Add(ActorState.AState.Sleep);
            }
            Random random = new Random();
            int index = random.Next(highestNeeds.Count);
            return highestNeeds[index];
        }

        private List<GridSquare> PreferenceSearch(WorldState worldState)
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

        private double Desirability(GridSquare target, Vector2 currentLocation)
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
            desirability /= Utils.EuclideanDistance(currentLocation, target.Location);
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
