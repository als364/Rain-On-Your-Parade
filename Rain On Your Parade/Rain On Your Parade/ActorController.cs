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
        private int NeedIncreaseTimer;
        private const int MAX_ENJOY_TIME = 300;
        private int enjoyTime;
        private const int MAX_RUN_COOLDOWN = 80;
        private int runCoolDown = MAX_RUN_COOLDOWN;
        private const float RUN_SPEED = 2f;
        private const float WALK_SPEED = .75f;
        private const int SWITCH_TO_WANDER = 25;
        private float velX = 0f;
        private float velY = 0f;
        private const double RAINBOW_RADIUS = 400;


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
            if (controlledActor.State.State != ActorState.AState.Rainbow && controlledActor.State.State != ActorState.AState.RainbowWalk)
            {
                Dictionary<GridSquare, Actor> rainbows = new Dictionary<GridSquare, Actor>();
                foreach (WorldObject r in level.rainbows.Keys)
                {
                    if (Utils.EuclideanDistance(r.PixelPosition, controlledActor.PixelPosition) <= RAINBOW_RADIUS)
                    {
                        rainbows.Add(level.Grid[r.GridspacePosition.X, r.GridspacePosition.Y], null);
                    }
                    
                }
                if (rainbows.Count != 0)
                {
                    controlledActor.TargetState = ActorState.AState.Rainbow;
                    //PreferenceSearch determines the most desired square.
                    //FindPath finds a path to it.
                    controlledActor.Path = FindPath(rainbows, level.Grid[actorSquare.X, actorSquare.Y], level.Grid, new Point[level.Width, level.Height]);
                    //if pathing to rainbow fails, wander
                    if (controlledActor.Path == null) controlledActor.State = new ActorState(ActorState.AState.Wander);
                    else
                    {
                        //Now, walk there.
                        controlledActor.State = new ActorState(ActorState.AState.RainbowWalk);
                    }
                }
            }

            NeedIncreaseTimer++;
            if (controlledActor.rainCooldown > 0) controlledActor.rainCooldown--;
            if (controlledActor.InteractionTimer < 0) controlledActor.InteractionTimer++;

            if (NeedIncreaseTimer % 720 == 0)
            {
                controlledActor.increaseFastNeeds();
            }
            else
            {
                if (NeedIncreaseTimer % 1440 == 0)
                {
                    controlledActor.increaseSlowNeeds();
                }
            }

            Random random = new Random();
            int next = random.Next(1000); //Let the actor choose a new state in a random way
            actorSquare = controlledActor.GridspacePosition;

            bool switchWander = random.Next(100) < SWITCH_TO_WANDER;

            //Determines whether the actor close to the cloud, and if so, changes its state to Run
            if (controlledActor.State.State != ActorState.AState.Run && level.nearEnoughForInteraction(level.Player, controlledActor)) //&& controlledActor.State.State != controlledActor.TargetState)
            {
                controlledActor.State.State = ActorState.AState.Run;
                controlledActor.Path = null;

                if (controlledActor.InteractingActor != null)
                {
                    controlledActor.IncrementMood();
                    controlledActor.InteractionTimer = -120;
                    controlledActor.InteractingActor.State.State = ActorState.AState.Seek;
                    controlledActor.InteractingActor.InteractionTimer = -120;
                    controlledActor.InteractingActor.InteractingActor = null;
                    controlledActor.InteractingActor = null;
                }


                float xChange = level.Player.PixelPosition.X - level.Player.PrevPos.X;
                float yChange = level.Player.PixelPosition.Y - level.Player.PrevPos.Y;

                velX = 0f;
                velY = 0f;

                if (xChange > 0) { velX = RUN_SPEED; controlledActor.FacesLeft = false; }
                else if (xChange == 0) { velX = 0; }
                else { velX = -RUN_SPEED; controlledActor.FacesLeft = true; }

                if (yChange > 0) { velY = RUN_SPEED; }
                else if (yChange == 0) { velY = 0; }
                else { velY = -RUN_SPEED; }

                if (xChange == 0 && yChange == 0)
                {
                    velX = RUN_SPEED;
                }

            }

            //Determines whether the actor is close enough to interact with another actor, and if so, changes its state appropriately
            foreach (Actor a in level.interactableActors(controlledActor))
            {
                if (level.nearEnoughForInteraction(a, controlledActor))
                {
                    if (controlledActor.InteractionTimer == 0 && a.InteractionTimer == 0 && 
                        ((a.Mood >= 3 && controlledActor.Mood >= 3) || (a.Mood > 4 || controlledActor.Mood > 4)))
                    {
                        controlledActor.State.State = ActorState.AState.Fight;
                        controlledActor.InteractingActor = a;
                        a.State.State = ActorState.AState.Fight;
                        a.InteractingActor = controlledActor;
                    }
                    else if (controlledActor.InteractionTimer == 0 &&
                            (DetermineTargetState() == ActorState.AState.Nurture && a.Mood > 3 && controlledActor.Mood < 3) ||
                            (a.TargetState == ActorState.AState.Nurture && controlledActor.Mood > 3 && a.Mood < 3))
                    {
                        controlledActor.State.State = ActorState.AState.Comfort;
                        a.State.State = ActorState.AState.Comfort;
                        controlledActor.InteractingActor = a;
                        a.InteractingActor = controlledActor;
                    }
                    
                }
            }
            switch (controlledActor.State.State)
            {
                #region RainbowState
                case ActorState.AState.Rainbow:
                    if (level.Grid[controlledActor.GridspacePosition.X, controlledActor.GridspacePosition.Y].Objects[0].Type.TypeName == ObjectType.Type.SunnyRainbowSpot 
                        && level.Grid[controlledActor.GridspacePosition.X, controlledActor.GridspacePosition.Y].Objects[0].Activated)
                    {
                        break;
                    }
                    else
                    {
                        controlledActor.InteractingObject = null;
                        controlledActor.State.State = switchWander ? ActorState.AState.Wander : ActorState.AState.Seek;
                        break;
                    }
                #endregion
                #region FightState
                case ActorState.AState.Fight:
                    controlledActor.InteractionTimer++;

                    if (controlledActor.InteractionTimer >= 360)
                    {
                        controlledActor.InteractionTimer = -300;
                        if (controlledActor.Mood < 5)
                        controlledActor.Mood = controlledActor.Mood + 1;
                        controlledActor.State.State = switchWander ? ActorState.AState.Wander : ActorState.AState.Seek;
                        controlledActor.InteractingActor = null;
                    }

                    break;
                #endregion
                #region ComfortState
                case ActorState.AState.Comfort:
                    controlledActor.InteractionTimer++;

                    if (controlledActor.InteractionTimer >= 360)
                    {
                        controlledActor.InteractionTimer = -300;
                        if (controlledActor.Mood > 0) controlledActor.Mood = controlledActor.Mood - 1;
                        controlledActor.NurtureLevel--;
                        controlledActor.State.State = switchWander ? ActorState.AState.Wander : ActorState.AState.Seek;
                        controlledActor.InteractingActor = null;
                    }

                    break;
                #endregion
                #region NurtureState
                case ActorState.AState.Nurture:
                    if (enjoyTime == 0)
                    {
                        interactWithObject(level, ActorState.AState.Nurture);
                        controlledActor.InteractingObject = null;
                        controlledActor.State.State = ActorState.AState.Wander;
                    }
                    else
                    {
                        enjoyTime--;
                    }
                    break;
                #endregion
                #region PlayState
                case ActorState.AState.Play:
                    if (enjoyTime == 0)
                    {
                        interactWithObject(level, ActorState.AState.Play);
                        controlledActor.InteractingObject = null;
                        controlledActor.State.State = ActorState.AState.Wander;
                    }
                    else
                    {
                        enjoyTime--;
                    }
                    break;
                #endregion
                #region RampageState
                case ActorState.AState.Rampage:
                    controlledActor.InteractingObject = null;
                    controlledActor.State.State = ActorState.AState.Seek;
                    break;
                #endregion
                #region SleepState
                case ActorState.AState.Sleep:
                    if (enjoyTime == 0)
                    {
                        interactWithObject(level, ActorState.AState.Sleep);
                        controlledActor.InteractingObject = null;
                        controlledActor.State.State = ActorState.AState.Wander;
                    }
                    else
                    {
                        enjoyTime--;
                    }
                    break;
                #endregion
                #region SeekState
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
                    if (controlledActor.Path == null) controlledActor.State.State = ActorState.AState.Wander;
                    else
                    {
                        //Now, walk there.
                        if (controlledActor.TargetIsActor)
                        {
                            controlledActor.State.State = ActorState.AState.Hunt;
                        }
                        else
                        {
                            controlledActor.State.State = ActorState.AState.Walk;
                        }
                    }
                    break;
                #endregion
                #region WalkState
                case ActorState.AState.Walk:
                    if (controlledActor.Path.Count < 1)
                    {
                        controlledActor.State.State = switchWander ? ActorState.AState.Wander : ActorState.AState.Seek;
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
                                if (controlledActor.TargetIsActor && nextSquare.Actors.Count == 0)
                                {
                                    controlledActor.TargetIsActor = false;
                                    controlledActor.State.State = switchWander ? ActorState.AState.Wander : ActorState.AState.Seek;
                                }
                                else if (!controlledActor.TargetIsActor && nextSquare.Objects.Count == 0)
                                {
                                    controlledActor.State.State = switchWander ? ActorState.AState.Wander : ActorState.AState.Seek;
                                }
                                else
                                {
                                    if (nextSquare.Objects.Count > 0)
                                    {
                                        controlledActor.InteractingObject = nextSquare.Objects[0];
                                        nextSquare.Objects[0].AddInteractingActor(controlledActor);
                                    }
                                    else
                                    {
                                        Debug.WriteLine("People are interacting with empty squares that don't even have people in them.");
                                    }
                                    controlledActor.State.State = controlledActor.TargetState;
                                }
                                enjoyTime = MAX_ENJOY_TIME;
                            }
                            else
                            {
                                controlledActor.Path.RemoveAt(0);
                                nextSquare = controlledActor.Path[0];
                                nextSquare.calculateLevels();
                            }
                        }
                        //If I'm not within the next square on the path, make sure my velocity is set correctly (necessary for first square) Move uniformly to the next square
                        if (nextSquare.Location.X * Canvas.SQUARE_SIZE - controlledActor.PixelPosition.X <= 0)
                        {
                            Velx = controlledActor.Mood > 4 ? -RUN_SPEED : -WALK_SPEED;
                        }
                        else
                        {
                            Velx = controlledActor.Mood > 4 ? RUN_SPEED : WALK_SPEED;
                        }
                        if (nextSquare.Location.Y * Canvas.SQUARE_SIZE - controlledActor.PixelPosition.Y <= 0)
                        {
                            Vely = controlledActor.Mood > 4 ? -RUN_SPEED : -WALK_SPEED;
                        }
                        else
                        {
                            Vely = controlledActor.Mood > 4 ? RUN_SPEED : WALK_SPEED;
                        }
                        controlledActor.Velocity = new Vector2(Velx, Vely);
                        //controlledActor.Velocity = new Vector2(nextSquare.Location.X * Canvas.SQUARE_SIZE - controlledActor.Position.X, 
                        //                                       nextSquare.Location.Y * Canvas.SQUARE_SIZE - controlledActor.Position.Y)/30
                        //Move the actor
                        controlledActor.PixelPosition = Vector2.Add(controlledActor.PixelPosition, controlledActor.Velocity);
                        //controlledActor.GridspacePosition = new Point((int)(controlledActor.PixelPosition.X / Canvas.SQUARE_SIZE),
                                                                        //(int)(controlledActor.PixelPosition.Y / Canvas.SQUARE_SIZE));
                    }
                    break;
                #endregion
                #region HuntState
                case ActorState.AState.Hunt:
                    if (controlledActor.TargetActor == null)
                    {
                        controlledActor.TargetIsActor = false;
                        controlledActor.State.State = ActorState.AState.Seek;
                        break;
                    }
                    if (controlledActor.Path.Count < 1)
                    {
                        controlledActor.State.State = ActorState.AState.Seek;
                    }
                    //The actual moving along the path.
                    else
                    {
                        if (!controlledActor.Path.Contains(level.Grid[controlledActor.TargetActor.GridspacePosition.X, controlledActor.TargetActor.GridspacePosition.Y]))
                        {
                            Dictionary<GridSquare, Actor> newTarget = new Dictionary<GridSquare,Actor>();
                            newTarget.Add(level.Grid[controlledActor.TargetActor.GridspacePosition.X, controlledActor.TargetActor.GridspacePosition.Y], null);
                            controlledActor.Path = FindPath(newTarget,
                                                            level.Grid[actorSquare.X, actorSquare.Y],
                                                            level.Grid, new Point[level.Width, level.Height]);
                        }
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
                                if (controlledActor.TargetIsActor && nextSquare.Actors.Count == 0)
                                {
                                    controlledActor.TargetIsActor = false;
                                    controlledActor.State.State = ActorState.AState.Seek;
                                }
                                else if (!controlledActor.TargetIsActor && nextSquare.Objects.Count == 0)
                                {
                                    controlledActor.State.State = ActorState.AState.Seek;
                                }
                                else
                                {
                                    controlledActor.State.State = controlledActor.TargetState;
                                }
                                enjoyTime = MAX_ENJOY_TIME;
                            }
                            else
                            {
                                controlledActor.Path.RemoveAt(0);
                                nextSquare = controlledActor.Path[0];
                                nextSquare.calculateLevels();
                            }
                        }
                        //If I'm not within the next square on the path, make sure my velocity is set correctly (necessary for first square) Move uniformly to the next square
                        if (nextSquare.Location.X * Canvas.SQUARE_SIZE - controlledActor.PixelPosition.X <= 0)
                        {
                            Velx = controlledActor.Mood > 4 ? -RUN_SPEED : -WALK_SPEED;
                        }
                        else
                        {
                            Velx = controlledActor.Mood > 4 ? RUN_SPEED : WALK_SPEED;
                        }
                        if (nextSquare.Location.Y * Canvas.SQUARE_SIZE - controlledActor.PixelPosition.Y <= 0)
                        {
                            Vely = controlledActor.Mood > 4 ? -RUN_SPEED : -WALK_SPEED;
                        }
                        else
                        {
                            Vely = controlledActor.Mood > 4 ? RUN_SPEED : WALK_SPEED;
                        }
                        controlledActor.Velocity = new Vector2(Velx, Vely);
                        //controlledActor.Velocity = new Vector2(nextSquare.Location.X * Canvas.SQUARE_SIZE - controlledActor.Position.X, 
                        //                                       nextSquare.Location.Y * Canvas.SQUARE_SIZE - controlledActor.Position.Y)/30
                        //Move the actor
                        controlledActor.PixelPosition = Vector2.Add(controlledActor.PixelPosition, controlledActor.Velocity);
                        //controlledActor.GridspacePosition = new Point((int)(controlledActor.PixelPosition.X / Canvas.SQUARE_SIZE),
                        //(int)(controlledActor.PixelPosition.Y / Canvas.SQUARE_SIZE));
                    }
                    break;
                #endregion
                #region RainbowWalkState
                case ActorState.AState.RainbowWalk:
                    if (controlledActor.Path.Count < 1)
                    {
                        controlledActor.State = new ActorState(ActorState.AState.Seek);
                    }
                    //The actual moving along the path.
                    else
                    {
                        if (!controlledActor.Path.ElementAt(controlledActor.Path.Count - 1).Objects[0].Activated)
                        {
                            controlledActor.State.State = switchWander ? ActorState.AState.Wander : ActorState.AState.Seek;
                            break;
                        }
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
                                if (controlledActor.TargetIsActor && nextSquare.Actors.Count == 0)
                                {
                                    controlledActor.TargetIsActor = false;
                                    controlledActor.State.State = ActorState.AState.Seek;
                                }
                                else if (!controlledActor.TargetIsActor && nextSquare.Objects.Count == 0)
                                {
                                    controlledActor.State.State = ActorState.AState.Seek;
                                }
                                else
                                {
                                    controlledActor.State.State = controlledActor.TargetState;
                                }
                                enjoyTime = MAX_ENJOY_TIME;
                            }
                            else
                            {
                                controlledActor.Path.RemoveAt(0);
                                nextSquare = controlledActor.Path[0];
                                nextSquare.calculateLevels();
                            }
                        }
                        //If I'm not within the next square on the path, make sure my velocity is set correctly (necessary for first square) Move uniformly to the next square
                        if (nextSquare.Location.X * Canvas.SQUARE_SIZE - controlledActor.PixelPosition.X <= 0)
                        {
                            Velx = controlledActor.Mood > 4 ? -RUN_SPEED : -WALK_SPEED;
                        }
                        else
                        {
                            Velx = controlledActor.Mood > 4 ? RUN_SPEED : WALK_SPEED;
                        }
                        if (nextSquare.Location.Y * Canvas.SQUARE_SIZE - controlledActor.PixelPosition.Y <= 0)
                        {
                            Vely = controlledActor.Mood > 4 ? -RUN_SPEED : -WALK_SPEED;
                        }
                        else
                        {
                            Vely = controlledActor.Mood > 4 ? RUN_SPEED : WALK_SPEED;
                        }
                        controlledActor.Velocity = new Vector2(Velx, Vely);
                        //controlledActor.Velocity = new Vector2(nextSquare.Location.X * Canvas.SQUARE_SIZE - controlledActor.Position.X, 
                        //                                       nextSquare.Location.Y * Canvas.SQUARE_SIZE - controlledActor.Position.Y)/30
                        //Move the actor
                        controlledActor.PixelPosition = Vector2.Add(controlledActor.PixelPosition, controlledActor.Velocity);
                        //controlledActor.GridspacePosition = new Point((int)(controlledActor.PixelPosition.X / Canvas.SQUARE_SIZE),
                        //(int)(controlledActor.PixelPosition.Y / Canvas.SQUARE_SIZE));
                    }
                    break;
                #endregion
                #region WanderState
                case ActorState.AState.Wander:
                    Dictionary<GridSquare, Actor> wanderTarget = new Dictionary<GridSquare, Actor>();
                    wanderTarget.Add(level.Grid[random.Next(level.Width), random.Next(level.Height)], null);
                    controlledActor.Path = FindPath(wanderTarget, level.Grid[actorSquare.X, actorSquare.Y],
                                                    level.Grid, new Point[level.Width, level.Height]);
                    if (controlledActor.Path != null)
                    {
                        controlledActor.State.State = ActorState.AState.Walk;
                    }

                    if (next <= 300) controlledActor.State.State = ActorState.AState.Seek;
                    break;
                #endregion
                #region RunState
                //Actor runs from the cloud if it's within a radius of the cloud
                case ActorState.AState.Run:

                    Vector2 newPosition = controlledActor.PixelPosition;
                    Vector2 newVelocity = new Vector2(velX, velY);

                    newPosition = Vector2.Add(controlledActor.PixelPosition, newVelocity);

                    Point newPosGridPos = new Point((int)((newPosition.X + controlledActor.spriteWidth / 2) / Canvas.SQUARE_SIZE),
                        (int)((newPosition.Y + controlledActor.spriteHeight / 2) / Canvas.SQUARE_SIZE));
                    if (!level.Grid[newPosGridPos.X, newPosGridPos.Y].IsPassable)
                    {
                        if (newPosGridPos.X != controlledActor.GridspacePosition.X) {
                            velX = -velX;
                            newVelocity = new Vector2(-newVelocity.X, newVelocity.Y);
                        }
                        if (newPosGridPos.Y != controlledActor.GridspacePosition.Y)
                        {
                            velY = -velY;
                            newVelocity = new Vector2(newVelocity.X, -newVelocity.Y);
                        }
                        newPosition = Vector2.Add(controlledActor.PixelPosition, newVelocity);
                        newVelocity = new Vector2(velX, velY);
                    }

                    if (newPosition.X + controlledActor.spriteWidth > GameEngine.SCREEN_WIDTH)
                    {
                        newPosition = new Vector2(GameEngine.SCREEN_WIDTH - controlledActor.spriteWidth, newPosition.Y);
                        newVelocity = new Vector2(0, newVelocity.Y);
                    }
                    if (newPosition.X < 0)
                    {
                        newPosition = new Vector2(0, newPosition.Y);
                        newVelocity = new Vector2(0, newVelocity.Y);
                    }
                    if (newPosition.Y + controlledActor.spriteHeight > GameEngine.SCREEN_HEIGHT)
                    {
                        newPosition = new Vector2(newPosition.X, GameEngine.SCREEN_HEIGHT - controlledActor.spriteHeight);
                        newVelocity = new Vector2(newVelocity.X, 0);
                    }
                    if (newPosition.Y < 0)
                    {
                        newPosition = new Vector2(newPosition.X, 0);
                        newVelocity = new Vector2(newVelocity.X, 0);
                    }
                        
                    if (runCoolDown < 1)
                    {
                        controlledActor.TargetState = ActorState.AState.Seek;
                        controlledActor.State.State = ActorState.AState.Seek;
                        runCoolDown = MAX_RUN_COOLDOWN;
                    }
                    else
                    {
                        controlledActor.PixelPosition = newPosition;
                        controlledActor.Velocity = newVelocity;
                        runCoolDown--;
                    }

                    break;
                #endregion
                #region DefaultState
                //Again, a specialcase. Just dumps things into Seek, making sure to zero their velocity first.
                default:
                    if (controlledActor.Path.Count == 0)
                    {
                        controlledActor.Velocity = new Vector2();
                        controlledActor.State.State = ActorState.AState.Seek;
                    }
                    else if (controlledActor.Path.Count == 1) //at target!
                    {
                        controlledActor.Velocity = new Vector2();
                        if (!PreferenceSearch(level).Keys.ToList().Contains(controlledActor.Path[0]))
                        {
                            controlledActor.State.State = ActorState.AState.Seek;
                        }
                    }
                    else
                    {
                    }
                    break;
                #endregion
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
        private Dictionary<GridSquare, Actor> PreferenceSearch(Canvas level)
        {
            double maxPreference = 0;
            Dictionary<Actor, double> actorPreference = new Dictionary<Actor, double>();
            Dictionary<Point, double> objectPreference = new Dictionary<Point, double>();
            Dictionary<GridSquare, Actor> targets = new Dictionary<GridSquare, Actor>();
            
            //Desirability of each actor and object
            foreach (Actor actor in level.Actors)
            {
                actorPreference[actor] = Desirability(level.Grid[actor.GridspacePosition.X, actor.GridspacePosition.Y]);
            }
            foreach (WorldObject entity in level.Objects)
            {
                objectPreference[entity.GridspacePosition] = Desirability(level.Grid[entity.GridspacePosition.X, entity.GridspacePosition.Y]);
            }

            actorPreference[controlledActor] = 0;
            objectPreference[controlledActor.GridspacePosition] = 0;

            //Preference of each actor and object
            foreach (Actor actor in actorPreference.Keys)
            {
                if (actorPreference[actor] > maxPreference)
                {
                    targets.Clear();
                    targets.Add(level.Grid[actor.GridspacePosition.X, actor.GridspacePosition.Y], actor);
                    maxPreference = actorPreference[actor];
                }
                else if (actorPreference[actor] == maxPreference && maxPreference != 0 && actorPreference[actor] != 0)
                {
                    if (!targets.Keys.Contains(level.Grid[actor.GridspacePosition.X, actor.GridspacePosition.Y]))
                    {
                        targets.Add(level.Grid[actor.GridspacePosition.X, actor.GridspacePosition.Y], actor);
                    }
                    else
                    {
                        targets[level.Grid[actor.GridspacePosition.X, actor.GridspacePosition.Y]] = actor;
                    }
                }
            }
            foreach (Point entity in objectPreference.Keys)
            {
                if (objectPreference[entity] > maxPreference)
                {
                    targets.Clear();
                    targets.Add(level.Grid[entity.X, entity.Y], null);
                    maxPreference = objectPreference[entity];
                }
                else if (objectPreference[entity] == maxPreference && maxPreference != 0 && objectPreference[entity] != 0)
                {
                    if (!targets.Keys.Contains(level.Grid[entity.X, entity.Y]))
                    {
                        targets.Add(level.Grid[entity.X, entity.Y], null);
                    }
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
        /// A* search.
        /// </summary>
        /// <param name="targets">Goal states.</param>
        /// <param name="origin">Starting state.</param>
        /// <param name="worldGrid">State space.</param>
        /// <param name="parentArray">For keeping track of the actual path. Keeps track of the progress of the BFS.</param>
        /// <returns></returns>
        private List<GridSquare> FindPath(Dictionary<GridSquare, Actor> targets, GridSquare origin, GridSquare[,] worldGrid, Point[,] parentArray)
        {
            if (targets.Count == 0) return null;
            List<GridSquare> path = new List<GridSquare>();

            //Dictionary<GridSquare, double> pQueue = new Dictionary<GridSquare, double>();
            PriorityQueue<int, GridSquare> pQueue = new PriorityQueue<int, GridSquare>();
            Dictionary<GridSquare, int> gValues = new Dictionary<GridSquare, int>();

            parentArray[origin.Location.X, origin.Location.Y] = new Point(-1, -1);
            gValues.Add(origin, 0);
            pQueue.Enqueue(GetHValue(origin, targets.Keys.ToList()), origin);

            while (pQueue.Count != 0)
            {
                KeyValuePair<int, GridSquare> kvp = pQueue.Dequeue();
                GridSquare lookingAt = kvp.Value;
                //if we are looking at a target
                if (targets.Keys.Contains(lookingAt))
                {
                    if (targets[lookingAt] != null)
                    {
                        controlledActor.TargetIsActor = true;
                        controlledActor.TargetActor = targets[lookingAt];
                    }
                    else
                    {
                        controlledActor.TargetIsActor = false;
                        controlledActor.TargetActor = null;
                    }
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
                                fValue += GetHValue(adjacentSquare, targets.Keys.ToList());
                                pQueue.Enqueue(fValue, adjacentSquare);
                            }
                            //if we are looking at the origin
                            else
                            {
                                //parent gvalue = 0
                                int fValue = 1;
                                //evaluate hvalue and enqueue
                                fValue += GetHValue(adjacentSquare, targets.Keys.ToList());
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



        private bool ObjectStillInteractable(Canvas level, ActorState.AState action)
        {




            return false;
        }
        /// <summary>
        /// Activates any objects in that actor's grid square if they can be activated by an actor
        /// in their current state.
        /// </summary>
        /// <returns>A boolean stating whether the interaction successfully took place.</returns>
        private bool interactWithObject(Canvas level, ActorState.AState action)
        {
            bool interacted = false;

            List<WorldObject> interactObjs = level.interactableObjects(controlledActor);

            foreach (WorldObject o in interactObjs)
            {
                switch (action)
                {
                    case ActorState.AState.Nurture:
                        // if (o.Type.NurtureLevel > 2)
                        //  {
                        if(o.Type.CanActivate) o.activate();
                        controlledActor.NurtureLevel--;
                        controlledActor.DecrementMood();
                        if (controlledActor.NurtureLevel < 0) controlledActor.NurtureLevel = 0;
                        interacted = true;
                        // }
                        break;
                    case ActorState.AState.Play:
                        //  if (o.Type.PlayLevel > 2)
                        //  {
                        if(o.Type.CanActivate) o.activate();
                        controlledActor.PlayLevel--;
                        controlledActor.DecrementMood();
                        if (controlledActor.PlayLevel < 0) controlledActor.PlayLevel = 0;
                        interacted = true;
                        //  }
                        break;
                    case ActorState.AState.Sleep:
                        // if (o.Type.SleepLevel > 2)
                        // {
                        controlledActor.SleepLevel--;
                        controlledActor.DecrementMood();
                        if (controlledActor.SleepLevel < 0) controlledActor.SleepLevel = 0;
                        interacted = true;
                        // }
                        break;
                }
                
            }
            return interacted;
        }

    }


}
