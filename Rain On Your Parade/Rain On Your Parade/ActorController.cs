﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace Rain_On_Your_Parade
{
    class ActorController : Controller
    {
        Actor controlledActor;
        private Point actorSquare = new Point(0, 0);
        private bool nearCloud = false;
        private int reactDelay = REACT_MAX;
        private const int REACT_MAX = 5;

        public ActorController(Actor actor) : base(actor)
        {
            controlledActor = actor;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="worldState"></param>
        public override void Update(GameTime gameTime, Canvas level)
        {
            Random random = new Random();
            int next = random.Next(1000); //Let the actor choose a new state in a random way
            actorSquare = controlledActor.GridspacePosition;

            //Determines whether the actor is in the same square as the cloud, and implements a delayed reaction
            if (level.Player.GridspacePosition.X == actorSquare.X
                && level.Player.GridspacePosition.Y == actorSquare.Y)
            {
                if (reactDelay > 0)
                {
                    nearCloud = false;
                    reactDelay--;
                }
                else
                {
                    nearCloud = true;
                    reactDelay = REACT_MAX;
                }
            }
            if (nearCloud) controlledActor.State = new ActorState(ActorState.AState.Run);

            //Console.WriteLine("State: " + controlledActor.State.State);
            switch (controlledActor.State.State)
            {
                //TODO: Implement these four states
                case ActorState.AState.Nurture:
                    interactWithObject(level, ActorState.AState.Nurture);
                    if (next <= 30) controlledActor.State = new ActorState(ActorState.AState.Seek);
                    break;
                case ActorState.AState.Play:
                    interactWithObject(level, ActorState.AState.Play);
                    if (next <= 30) controlledActor.State = new ActorState(ActorState.AState.Seek);
                    break;
                case ActorState.AState.Rampage:
                    controlledActor.State = new ActorState(ActorState.AState.Wander);
                    break;
                case ActorState.AState.Sleep:
                    if (next <= 30) controlledActor.State = new ActorState(ActorState.AState.Seek);
                   // controlledActor.State.State = ActorState.AState.Seek;
                    break;
                case ActorState.AState.Seek:
                    //Actor figures out what state it wants to be 
                    ActorState.AState newState = DetermineTargetState();
                    controlledActor.TargetState = newState;
                    //PreferenceSearch determines the most desired square.
                    //FindPath finds a path to it.
                    controlledActor.Path = FindPath(PreferenceSearch(level),
                                                    level.Grid[actorSquare.X, actorSquare.Y],
                                                    level.Grid, new Point[level.Width, level.Height]);
                    //if none of the squares were desirable, Rampage
                    if (controlledActor.Path == null) controlledActor.State = new ActorState(ActorState.AState.Rampage);
                    else
                    {
                        //Now, walk there.
                        controlledActor.State = new ActorState(ActorState.AState.Walk);
                    }
                    break;

                case ActorState.AState.Walk:
                    //Shouldn't ever happen, really. This is a specialcase. Rather than throwing an error, just find something else to do.
                    if (controlledActor.Path.Count == 0)
                    {
                        controlledActor.State = new ActorState(ActorState.AState.Seek);
                    }
                    //The actual moving along the path.
                    else
                    {
                        //Console.WriteLine(controlledActor.Type.TypeName + " Position: " + controlledActor.GridspacePosition);
                        GridSquare nextSquare = controlledActor.Path[0];
                        float Velx;
                        float Vely;

                        //If I'm within the next square on the path, remove it from the path and set my velocity towards the next one
                        if (nextSquare.Contains(controlledActor.GridspacePosition))
                        {
                            //At target square. Move to target state.  
                            if (controlledActor.Path.Count == 1)
                            {
                                //controlledActor.Path.Clear();
                                controlledActor.State = new ActorState(controlledActor.TargetState);
                                //controlledActor.Path[0].Actors.Remove(controlledActor);
                            }
                            else
                            {
                                //  Console.WriteLine("REMOVING");
                                level.Grid[actorSquare.X, actorSquare.Y].Actors.Remove(controlledActor);
                                controlledActor.Path.RemoveAt(0);
                                controlledActor.Path[0].Actors.Add(controlledActor);
                                nextSquare = controlledActor.Path[0];
                                nextSquare.calculateLevels();
                            }

                            //controlledActor.Velocity = new Vector2(nextSquare.Location.X * Canvas.SQUARE_SIZE - controlledActor.Position.X, nextSquare.Location.Y * Canvas.SQUARE_SIZE - controlledActor.Position.Y)/30;
                            //Console.WriteLine("Velocity: " + controlledActor.Velocity);
                        }
                        //If I'm not within the next square on the path, make sure my velocity is set correctly (necessary for first square) Move uniformly to the next square
                        if (nextSquare.Location.X * Canvas.SQUARE_SIZE - controlledActor.PixelPosition.X <= 0)
                        {
                            Velx = -1f;
                        }
                        else
                        {
                            Velx = 1f;
                        }
                        if (nextSquare.Location.Y * Canvas.SQUARE_SIZE - controlledActor.PixelPosition.Y <= 0)
                        {
                            Vely = -1f;
                        }
                        else
                        {
                            Vely = 1f;
                        }
                        controlledActor.Velocity = new Vector2(Velx, Vely);
                        //controlledActor.Velocity = new Vector2(nextSquare.Location.X * Canvas.SQUARE_SIZE - controlledActor.Position.X, 
                        //                                       nextSquare.Location.Y * Canvas.SQUARE_SIZE - controlledActor.Position.Y)/30
                        //Move the actor
                        controlledActor.PixelPosition = Vector2.Add(controlledActor.PixelPosition, controlledActor.Velocity);
                        controlledActor.GridspacePosition = new Point((int)(controlledActor.PixelPosition.X / Canvas.SQUARE_SIZE), 
                                                                      (int)(controlledActor.PixelPosition.Y / Canvas.SQUARE_SIZE));
                    }
                    break;
                case ActorState.AState.Wander: 
                    List<GridSquare> wanderTarget = new List<GridSquare>();
                    wanderTarget.Add(level.Grid[random.Next(level.Width),random.Next(level.Height)]);
                    controlledActor.Path = FindPath(wanderTarget, level.Grid[actorSquare.X, actorSquare.Y],
                                                    level.Grid, new Point[level.Width, level.Height]);
                    if (controlledActor.Path != null)
                    {
                        controlledActor.State = new ActorState(ActorState.AState.Walk);
                    }
                    break;
                //Actor runs from the cloud if it's in the same square, with a delay
                case ActorState.AState.Run:
                    //Console.WriteLine("State: " + controlledActor.State.State);
                    //Actor figures out what state it wants to be 
                    newState = DetermineTargetState();
                    controlledActor.TargetState = newState;

                    //Determines which direction to run based on the player's movement direction
                    List<GridSquare> target = new List<GridSquare>();
                    float xChange = level.Player.GridspacePosition.X - level.Player.prevPos.X;
                    float yChange = level.Player.GridspacePosition.Y - level.Player.prevPos.Y;
                    //Console.WriteLine("PosChange: " + xChange + ", " + yChange);
                    if (Math.Abs(xChange) > Math.Abs(yChange))
                    {
                        if (xChange > 0)
                        {
                            target.Add(level.Grid[(int)MathHelper.Clamp(actorSquare.X + 3, 0, level.Width-1), actorSquare.Y]);
                        }
                        else
                        {
                            target.Add(level.Grid[(int)MathHelper.Clamp(actorSquare.X - 3, 0, level.Width-1), actorSquare.Y]);
                        }
                    }
                    else
                    {
                        if (yChange > 0)
                        {
                            target.Add(level.Grid[actorSquare.X, (int)MathHelper.Clamp(actorSquare.Y + 3, 0, level.Height-1)]);
                        }
                        else
                        {
                            target.Add(level.Grid[actorSquare.X, (int)MathHelper.Clamp(actorSquare.Y - 3, 0, level.Height-1)]);
                        }
                    }

                    //FindPath finds a path to it.
                    controlledActor.Path = FindPath(target, level.Grid[actorSquare.X, actorSquare.Y],
                                                    level.Grid, new Point[level.Width, level.Height]);

                    //if none of the squares were desirable, Rampage
                    if (controlledActor.Path == null) controlledActor.State = new ActorState(ActorState.AState.Rampage);
                    else
                    {
                        //Now, walk there.
                        controlledActor.State = new ActorState(ActorState.AState.Walk);
                    }
                    break;
                //Again, a specialcase. Just dumps things into Seek, making sure to zero their velocity first.
                default:
                    if (controlledActor.Path.Count == 0)
                    {
                        controlledActor.Velocity = new Vector2();
                        controlledActor.State = new ActorState(ActorState.AState.Seek);
                    }
                    else if (controlledActor.Path.Count == 1) //at target!
                    {
                        controlledActor.Velocity = new Vector2();
                        if (!PreferenceSearch(level).Contains(controlledActor.Path[0]))
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

        /// <summary>
        /// Determines what state the controlled actor should be in based on the attribute levels of that actor.
        /// </summary>
        /// <returns>The new target state of the controlled actor.</returns>
        private ActorState.AState DetermineTargetState()
        {
            //Keeps track of whatever the max attribute level we've seen so far is. This will eventually turn into something more stochastic.
            int maxLevel = 0;
            List<ActorState.AState> highestNeeds = new List<ActorState.AState>();

            //For each of the attributes, keep track of whether they exceed the max attribute seen so far.
            //If so, clear the highestNeeds list and add that one.
            //If it's just equal, append that need.
            //If it's strictly less, ignore it.
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

            //Pick some random highest need.
            Random random = new Random();
            int index = random.Next(highestNeeds.Count);
            return highestNeeds[index];
        }

        /// <summary>
        /// Figure out what grid square(s) are best suited for the controlled actor
        /// </summary>
        /// <param name="worldState">The state of the game world</param>
        /// <returns>A list of equally suitable grid squares</returns>
        private List<GridSquare> PreferenceSearch(Canvas level)
        {
            double maxPreference = 0;
            Dictionary<Point, int> squarePreference = new Dictionary<Point, int>();
            List<GridSquare> targets = new List<GridSquare>();
            foreach (GridSquare square in level.Grid)
            {
                //How desirable /is/ the square
                double desirability = Desirability(square);
                //Console.WriteLine("GridSquare: " + square.Location);
                //Console.WriteLine("Desirability: " + desirability);
                //Console.WriteLine("MaxPreference: " + maxPreference);
                //If it's more desirable than anything else we've seen, clear the targets list and add that square
                if (desirability > maxPreference)
                {
                    targets.Clear();
                    //targets.Add(square);
                   // Console.WriteLine("SquareAdded: " + square);
                    maxPreference = desirability;
                }
                //If it's equally desirable, add it to the list
                else if (desirability == maxPreference && maxPreference != 0 && desirability != 0) //dont add non-desirable squares (maxPreference == 0)
                {
                    targets.Add(square);
                   // Console.WriteLine("SquareAdded: " + square);
                }
            }
            return targets;
        }

        /// <summary>
        /// A heuristic for determining how desirable a square is to the controlled actor.
        /// </summary>
        /// <param name="target">The square whose desirability we're measuring.</param>
        /// <returns>The desirability value of the target square.</returns>
        private double Desirability(GridSquare target)
        {
            double desirability = 0;
            if (!target.IsPassable)
            {
                return 0;
            }
            //This heuristic is broken. We should only look for squares suitable for the target action.
            desirability = (target.TotalNurture * controlledActor.NurtureLevel) + 
                           (target.TotalPlay * controlledActor.PlayLevel) + 
                           (target.TotalRampage * controlledActor.Mood) + 
                           (target.TotalSleep * controlledActor.SleepLevel);
           // Console.WriteLine("Target Play:" + target.TotalPlay +  "MyPlay:" + controlledActor.PlayLevel + "Desire:" + desirability);
           // Causes infinity...?  desirability /= Utils.EuclideanDistance(new Vector2(controlledActor.Position.X/Canvas.SQUARE_SIZE, controlledActor.Position.Y/Canvas.SQUARE_SIZE), target.Location);
            return desirability;
        }

        /// <summary>
        /// Breadth-first search.
        /// </summary>
        /// <param name="targets">Goal states.</param>
        /// <param name="currentSquare">Starting state.</param>
        /// <param name="worldGrid">State space.</param>
        /// <param name="parentArray">For keeping track of the actual path. Keeps track of the progress of the BFS.</param>
        /// <returns></returns>
        private List<GridSquare> FindPath(List<GridSquare> targets, GridSquare currentSquare, GridSquare[,] worldGrid, Point[,] parentArray)
        {
            if (targets.Count == 0) return null;

            foreach (GridSquare p in targets)
            {
               // Debug.WriteLine(p);
            }

            //Debug.WriteLine("Find Path");
            Queue<GridSquare> queue = new Queue<GridSquare>();
            HashSet<GridSquare> seen = new HashSet<GridSquare>();
            List<GridSquare> path = new List<GridSquare>();
            queue.Enqueue(currentSquare);
            seen.Add(currentSquare);
            //As long as we haven't run out of squares...
            while (queue.Count != 0)
            {
                GridSquare lookingAt = queue.Dequeue();
                //When we reach a goal state, go through the parent array to find the actual path the actor should take.
                if (targets.Contains(lookingAt))
                {
                    List<Point> pointPath = ExtractPathFromTarget(parentArray, lookingAt.Location, currentSquare.Location);
                   // Debug.WriteLine(pointPath);
                    foreach(Point point in pointPath)
                    {
                        path.Add(worldGrid[point.X, point.Y]);
                    }
                    return path;
                }
                //Otherwise keep looking...
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

        /// <summary>
        /// Follows pointers back up through the parent array from the specified target to the specified origin. Returns a path from the origin to the target.
        /// </summary>
        /// <param name="parentArray">An array indicating the progress of a breadth-first search.</param>
        /// <param name="target">The goal state.</param>
        /// <param name="origin">The starting state.</param>
        /// <returns>A path from the starting state to the goal state.</returns>
        private List<Point> ExtractPathFromTarget(Point[,] parentArray, Point target, Point origin)
        {
            List<Point> path = new List<Point>();
            Point parent = parentArray[target.X, target.Y];
            path.Add(target);
            int i = 0;
            while (parent != origin && i < 100)
            {
                i++;
                path.Add(parent);
                parent = parentArray[parent.X, parent.Y];
            }
            path.Add(origin);
            path.Reverse();

            /*foreach (Point a in path)
            {
                Debug.WriteLine("Path--------: " + a);
            }*/
            path.RemoveAt(0);
            return path;
        }

        /// <summary>
        /// Activates any objects in that actor's grid square if they can be activated by an actor
        /// in their current state.
        /// </summary>
        /// <returns>A boolean stating whether the interaction successfully took place.</returns>
        private bool interactWithObject(Canvas level, ActorState.AState action)
        {
            bool interacted = false;

            foreach (WorldObject o in level.Grid[actorSquare.X, actorSquare.Y].Objects)
            {
                if (o.Type.CanActivate)
                {
                    switch (action)
                    {
                        case ActorState.AState.Nurture:
                            if (o.Type.NurtureLevel > 2)
                            {
                                o.activate();
                                interacted = true;
                            }
                            break;
                        case ActorState.AState.Play:
                            if (o.Type.PlayLevel > 2)
                            {
                                o.activate();
                                interacted = true;
                            }
                            break;
                    }
                }
            }
            return interacted;
        }
    }


}
