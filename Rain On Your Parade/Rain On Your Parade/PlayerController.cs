using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rain_On_Your_Parade
{
    class PlayerController : Controller
    {
        public const float ACCELERATION = 0.3f;
        public const float DECELERATION = 0.2f;
        public const float MAX_SPEED = 5f;
        public const int COOL_DOWN = 15; //amount of time spent raining
        private int coolDown;
        private Player player;
        public bool isRaining = false;


        public PlayerController(Player player)
            : base(player)
        {
            this.player = player;
        }

        public Point shadowPointXY()
        {
            return new Point((int)((player.PixelPosition.X + 40) / Canvas.SQUARE_SIZE), 
                (int)((player.PixelPosition.Y + 120) / Canvas.SQUARE_SIZE));
        }

        public override void Update(GameTime gameTime, Canvas level)
        {
            player.isRaining = isRaining;
            level.Player.PrevPos = new Vector2(player.PixelPosition.X, player.PixelPosition.Y);
            KeyboardState ks = Keyboard.GetState();

            if (coolDown == 0)
            {
                if (ks.IsKeyDown(Keys.LeftShift) || ks.IsKeyDown(Keys.RightShift))
                {
                    if (Absorb(level))
                    {
                        player.Velocity = new Vector2(0, 0);
                        coolDown = COOL_DOWN;
                    }
                }
                if (ks.IsKeyDown(Keys.Space))
                {
                    if (Rain(level))
                    {
                        player.Velocity = new Vector2(0, 0);
                        coolDown = COOL_DOWN;
                    }
                }


                //if (player.Velocity.Length() < MAX_SPEED)
                //{
                if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
                {
                    player.Velocity = Vector2.Subtract(player.Velocity, new Vector2(0, ACCELERATION));
                }
                if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
                {
                    player.Velocity = Vector2.Add(player.Velocity, new Vector2(ACCELERATION, 0));
                }
                if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
                {
                    player.Velocity = Vector2.Add(player.Velocity, new Vector2(0, ACCELERATION));
                }
                if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
                {
                    player.Velocity = Vector2.Subtract(player.Velocity, new Vector2(ACCELERATION, 0));
                }

                if (player.Velocity.Length() > MAX_SPEED)
                {
                    player.Velocity = Vector2.Multiply(Vector2.Normalize(player.Velocity), MAX_SPEED);
                }
                //}

                if (Math.Abs(player.Velocity.X) > 0 && ks.IsKeyUp(Keys.A) && ks.IsKeyUp(Keys.D))
                {
                    if (player.Velocity.X > 0)
                    {
                        if (player.Velocity.X - DECELERATION < 0)
                        {
                            player.Velocity = new Vector2(0, player.Velocity.Y);
                        }
                        else
                        {
                            player.Velocity = Vector2.Subtract(player.Velocity, new Vector2(DECELERATION, 0));
                        }

                    }
                    else
                    {
                        if (player.Velocity.X + DECELERATION > 0)
                        {
                            player.Velocity = new Vector2(0, player.Velocity.Y);
                        }
                        else
                        {
                            player.Velocity = Vector2.Add(player.Velocity, new Vector2(DECELERATION, 0));
                        }

                    }
                }

                if (Math.Abs(player.Velocity.Y) > 0 && ks.IsKeyUp(Keys.W) && ks.IsKeyUp(Keys.S))
                {
                    if (player.Velocity.Y > 0)
                    {
                        if (player.Velocity.Y - DECELERATION < 0)
                        {
                            player.Velocity = new Vector2(player.Velocity.X, 0);
                        }
                        else
                        {
                            player.Velocity = Vector2.Subtract(player.Velocity, new Vector2(0, DECELERATION));
                        }

                    }
                    else
                    {
                        if (player.Velocity.Y + DECELERATION > 0)
                        {
                            player.Velocity = new Vector2(player.Velocity.X, 0);
                        }
                        else
                        {
                            player.Velocity = Vector2.Add(player.Velocity, new Vector2(0, DECELERATION));
                        }

                    }
                }
            }
            else
            {
                coolDown--;
                if (coolDown < 1)
                {
                    isRaining = false;
                }
            }

            player.PixelPosition = Vector2.Add(player.PixelPosition, player.Velocity);

            if (player.PixelPosition.X + player.spriteWidth > GameEngine.SCREEN_WIDTH)
            {
                player.PixelPosition = new Vector2(GameEngine.SCREEN_WIDTH - player.spriteWidth, player.PixelPosition.Y);
                player.Velocity = new Vector2(0, player.Velocity.Y);
            }
            if (player.PixelPosition.X < 0)
            {
                player.PixelPosition = new Vector2(0, player.PixelPosition.Y);
                player.Velocity = new Vector2(0, player.Velocity.Y);
            }
            if (player.PixelPosition.Y + player.spriteHeight > GameEngine.SCREEN_HEIGHT)
            {
                player.PixelPosition = new Vector2(player.PixelPosition.X, GameEngine.SCREEN_HEIGHT - player.spriteHeight);
                player.Velocity = new Vector2(player.Velocity.X, 0);
            }
            if (player.PixelPosition.Y < 0)
            {
                player.PixelPosition = new Vector2(player.PixelPosition.X, 0);
                player.Velocity = new Vector2(player.Velocity.X, 0);
            }

            player.GridspacePosition = new Point((int)(player.PixelPosition.X / Canvas.SQUARE_SIZE),
                                                    (int)(player.PixelPosition.Y / Canvas.SQUARE_SIZE));
        }

        private bool Rain(Canvas level)
        {
            if (player.Rain > 0)
            {
                isRaining = true;
                player.Rain--;
                player.colorAlpha = (1f - ((float)player.Rain / (float)Player.MAX_RAIN));

                foreach (Actor a in level.Actors)
                {
                    Vector2 shadowPos = new Vector2((player.PixelPosition.X + 40), (player.PixelPosition.Y + 120));
                    Vector2 actorPos = a.PixelPosition;

                    //Console.Write(shadowPos.X.ToString() + ", " + shadowPos.Y.ToString() + "\n");

                    float dist = Vector2.Distance(shadowPos, actorPos);

                    //Console.Write(dist.ToString() + "\n");

                    if (Math.Abs(dist) < 100)
                    {
                       
                       if (a.State.State == a.TargetState)
                        {
                           a.IncrementMood();
                           a.IncrementMood();
                           a.State.State = ActorState.AState.Seek;
                           Console.WriteLine("RAINED ON TARGET ITEM");
                        }
                        else
                        {
                            a.IncrementMood();
                        }
                    }

                   
                }

                Point shadowPoint = shadowPointXY();

                List<WorldObject> objects = level.Grid[shadowPoint.X, shadowPoint.Y].Objects;
                foreach (WorldObject o in objects)
                {
                    if (o.ActorsInteracted.Count > 0)
                    {
                        foreach (Actor a in o.ActorsInteracted)
                        {
                            a.IncrementMood();
                        }
                        o.ActorsInteracted.Clear();
                    }

                    int workAround = o.WaterLevel;

                    //Console.Write(o.ToString() + "object rained upon\n");

                    if (o.Type.TypeName == ObjectType.Type.SunnyRainbowSpot && !level.rainbows.ContainsKey(o))
                    {
                        level.rainbows.Add(o, GameEngine.MAX_RAINBOW_TIME);
                    }
                    if (o.Type.IsWetObject)
                    {
                        o.activate();                                        
                    }
                    else
                    {
                        o.deactivate();
                    }

                    if (o.Type.HoldsWater)
                    {
                        o.WaterLevel = workAround + 1;
                    } 
                }

                return true;
            }
            return false;
        }

        private bool Absorb(Canvas level)
        {
            if (player.Rain < Player.MAX_RAIN)
            {
                Point shadowPoint = shadowPointXY();
                
                List<WorldObject> objects = level.Grid[shadowPoint.X, shadowPoint.Y].Objects;
                foreach (WorldObject o in objects)
                {
                    //Console.Write(o.ToString() + "object ABSORBED upon\n");

                    if (o.WaterLevel > 0)
                    {
                        player.Rain++;
                        player.colorAlpha = (1f - ((float)player.Rain / (float)Player.MAX_RAIN));

                        if (--o.WaterLevel == 0)
                        {
                            if (o.Type.IsWetObject)
                            {
                                o.deactivate();
                            }
                            else
                            {
                                o.activate();
                            }
                        }

                        foreach (Actor a in level.Actors)
                        {
                            if (shadowPoint == a.GridspacePosition)
                            {

                                if (a.State.State == a.TargetState)
                                {
                                    a.IncrementMood();
                                    a.IncrementMood();
                                    a.State.State = ActorState.AState.Seek;
                                    Console.WriteLine("RAINED ON TARGET ITEM");
                        
                                }
                                else
                                {
                                    a.IncrementMood();
                                }
                            }
                        }
                    }
                    else { return false; }
                }
                return true;
            }
            else { return false; }
        }
    }
}
