using System;
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
        private const int REACT_MAX = 3;
        private int NeedIncreaseTimer;

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
            NeedIncreaseTimer++;
            if (NeedIncreaseTimer % 720 == 0)
            {
                controlledActor.increaseFastNeeds();
                Console.WriteLine(controlledActor.ToString());
            }
            else
                if (NeedIncreaseTimer % 1440 == 0)
                    controlledActor.increaseSlowNeeds();


         


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
                    
                    if (next <= 1) controlledActor.State = new ActorState(ActorState.AState.Seek);
                    break;
                case ActorState.AState.Play:
                    interactWithObject(level, ActorState.AState.Play);
                    if (next <= 1) controlledActor.State = new ActorState(ActorState.AState.Seek);
                    break;
                case ActorState.AState.Rampage:
                    controlledActor.State = new ActorState(ActorState.AState.Seek);
                    break;
                case ActorState.AState.Sleep:
                     interactWithObject(level, ActorState.AState.Play);
                    if (next <= 1) controlledActor.State = new ActorState(ActorState.AState.Seek);
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
                    if (controlledActor.Path == null) controlledActor.State = new ActorState(ActorState.AState.Wander);
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
                    float xChange = level.Player.PixelPosition.X - level.Player.PrevPos.X;
                    float yChange = level.Player.PixelPosition.Y - level.Player.PrevPos.Y;
                    Console.WriteLine("posChange: " + xChange + ", " + yChange);
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
            Dictionary<Point, double> squarePreference = new Dictionary<Point, double>();
            List<GridSquare> targets = new List<GridSquare>();
            foreach (Actor actor in level.Actors)
            {
                if (squarePreference.ContainsKey(actor.GridspacePosition))
                {
                    squarePreference[actor.GridspacePosition] += Desirability(level.Grid[actor.GridspacePosition.X, actor.GridspacePosition.Y]);
                }
                else
                {
                    squarePreference[actor.GridspacePosition] = Desirability(level.Grid[actor.GridspacePosition.X, actor.GridspacePosition.Y]);
                }
            }
            foreach (WorldObject entity in level.Objects)
            {
                if (squarePreference.ContainsKey(entity.GridspacePosition))
                {
                    squarePreference[entity.GridspacePosition] += Desirability(level.Grid[entity.GridspacePosition.X, entity.GridspacePosition.Y]);
                }
                else
                {
                    squarePreference[entity.GridspacePosition] = Desirability(level.Grid[entity.GridspacePosition.X, entity.GridspacePosition.Y]);
                }
            }
            foreach (Point point in squarePreference.Keys)
            {
                if (squarePreference[point] > maxPreference)
                {
                    targets.Clear();
                    maxPreference = squarePreference[point];
                }
                else if (squarePreference[point] == maxPreference && maxPreference != 0 && squarePreference[point] != 0)
                {
                    targets.Add(level.Grid[point.X, point.Y]);
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
            /*desirability = (target.TotalNurture * controlledActor.NurtureLevel) + 
                           (target.TotalPlay * controlledActor.PlayLevel) + 
                           (target.TotalRampage * controlledActor.Mood) + 
                           (target.TotalSleep * controlledActor.SleepLevel);*/
           // Console.WriteLine("Target Play:" + target.TotalPlay +  "MyPlay:" + controlledActor.PlayLevel + "Desire:" + desirability);
           // Causes infinity...?  desirability /= Utils.EuclideanDistance(new Vector2(controlledActor.Position.X/Canvas.SQUARE_SIZE, controlledActor.Position.Y/Canvas.SQUARE_SIZE), target.Location);
            switch (controlledActor.TargetState)
            {
                case ActorState.AState.Nurture:
                    desirability += target.TotalNurture * controlledActor.NurtureLevel;
                    break;
                case ActorState.AState.Play:
                    desirability += target.TotalPlay * controlledActor.PlayLevel;
                    break;
                case ActorState.AState.Rampage:
                    desirability += target.TotalRampage * controlledActor.RampageLevel;
                    break;
                case ActorState.AState.Sleep:
                    desirability += target.TotalSleep * controlledActor.SleepLevel;
                    break;
            }
            return desirability;
        }

        /// <summary>
        /// Breadth-first search.
        /// </summary>
        /// <param name="targets">Goal states.</param>
        /// <param name="origin">Starting state.</param>
        /// <param name="worldGrid">State space.</param>
        /// <param name="parentArray">For keeping track of the actual path. Keeps track of the progress of the BFS.</param>
        /// <returns></returns>
        private List<GridSquare> FindPath(List<GridSquare> targets, GridSquare origin, GridSquare[,] worldGrid, Point[,] parentArray)
        {
            if (targets.Count == 0) return null;
            List<GridSquare> path = new List<GridSquare>();

            //Dictionary<GridSquare, double> pQueue = new Dictionary<GridSquare, double>();
            PriorityQueue<int, GridSquare> pQueue = new PriorityQueue<int, GridSquare>();
            Dictionary<GridSquare, int> gValues = new Dictionary<GridSquare, int>();

            parentArray[origin.Location.X, origin.Location.Y] = new Point(-1, -1);
            gValues.Add(origin, 0);
            pQueue.Enqueue(GetHValue(origin, targets), origin);

            while (pQueue.Count != 0)
            {
                KeyValuePair<int, GridSquare> kvp = pQueue.Dequeue();
                GridSquare lookingAt = kvp.Value;
                //if we are looking at a target
                if (targets.Contains(lookingAt))
                {
                    List<Point> pointPath = ExtractPathFromTarget(parentArray, lookingAt.Location, origin.Location);
                    foreach (Point point in pointPath)
                    {
                        path.Add(worldGrid[point.X, point.Y]);
                    }
                    return path;
                }
                //if we are not looking at a target
                else
                {
                    //push all adjacent squares to queue with relevant information cached
                    foreach (GridSquare adjacentSquare in lookingAt.Adjacent)
                    {
                        //assign the gvalue of whatever we're looking at
                        Point lookingAtParent = parentArray[lookingAt.Location.X, lookingAt.Location.Y];
                        //if we're looking at the origin, the gvalue is 1
                        if (lookingAtParent.X == -1 && lookingAtParent.Y == -1)
                        {
                            gValues[lookingAt] = 1;
                        }
                        //if we'renot, the gvalue is parent + 1
                        else
                        {
                            gValues[lookingAt] = gValues[worldGrid[lookingAtParent.X, lookingAtParent.Y]] + 1;
                        }
                        //if we haven't already looked at it
                        if (!gValues.ContainsKey(adjacentSquare))
                        {
                            //mark what we're looking at as the parent of everything adjacent to it
                            parentArray[adjacentSquare.Location.X, adjacentSquare.Location.Y] = lookingAt.Location;
                            //if we're not looknig at the origin
                            if (lookingAt.Location != origin.Location)
                            {
                                //lookup parent gvalue
                                GridSquare parent = worldGrid[lookingAt.Location.X, lookingAt.Location.Y];
                                int fValue = gValues[parent] + 1;
                                //evaluate hvalue and enqueue
                                fValue += GetHValue(adjacentSquare, targets);
                                pQueue.Enqueue(fValue, adjacentSquare);
                            }
                            //if we are looking at the origin
                            else
                            {
                                //parent gvalue = 0
                                int fValue = 1;
                                //evaluate hvalue and enqueue
                                fValue += GetHValue(adjacentSquare, targets);
                                pQueue.Enqueue(fValue, adjacentSquare);
                            }
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
            while ((parent.X != -1 && parent.Y != -1) && i < 100)
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

        private int ManhattanDistance(Point p1, Point p2)
        {
            int distance = 0;
            distance += Math.Abs(p2.X - p1.X);
            distance += Math.Abs(p2.Y - p1.Y);
            return distance;
        }

        private int GetHValue(GridSquare square, List<GridSquare> targets)
        {
            int minimumDistance = int.MaxValue;
            foreach (GridSquare target in targets)
            {
                if (ManhattanDistance(square.Location, target.Location) < minimumDistance)
                {
                    minimumDistance = ManhattanDistance(square.Location, target.Location);
                }
            }
            return minimumDistance;
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
               // if (o.Type.CanActivate)
               // {
                    switch (action)
                    {
                        case ActorState.AState.Nurture:
                           // if (o.Type.NurtureLevel > 2)
                          //  {
                                //o.activate();
                                controlledActor.NurtureLevel--;
                                if (controlledActor.NurtureLevel < 0) controlledActor.NurtureLevel = 0;
                                interacted = true;
                           // }
                            break;
                        case ActorState.AState.Play:
                          //  if (o.Type.PlayLevel > 2)
                          //  {
                               // o.activate();
                                controlledActor.PlayLevel--;
                                if (controlledActor.PlayLevel < 0) controlledActor.PlayLevel = 0;
                                interacted = true;
                          //  }
                            break;
                        case ActorState.AState.Sleep:
                           // if (o.Type.SleepLevel > 2)
                           // {
                                controlledActor.SleepLevel--;
                                if (controlledActor.SleepLevel < 0) controlledActor.SleepLevel = 0;
                                interacted = true;
                           // }
                            break;
                    //}
                }
            }
            return interacted;
        }

    }


}
