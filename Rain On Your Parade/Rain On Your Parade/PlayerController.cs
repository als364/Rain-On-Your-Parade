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

        public PlayerController(Player player)
            : base(player)
        {
            this.player = player;
        }

        public override void Update(GameTime gameTime, WorldState worldState)
        {
            KeyboardState ks = Keyboard.GetState();

            if (coolDown == 0) //
            {
                if (ks.IsKeyDown(Keys.R))
                {
                    if (Absorb())
                    {
                        player.Velocity = new Vector2(0, 0);
                        coolDown = COOL_DOWN;
                    }
                }
                if (ks.IsKeyDown(Keys.Space))
                {
                    if (Rain())
                    {
                        player.Velocity = new Vector2(0, 0);
                        coolDown = COOL_DOWN;
                    }
                }

                //if (player.Velocity.Length() < MAX_SPEED)
                //{
                    if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
                    {
                       player.Velocity = Vector2.Subtract(player.Velocity,new Vector2(0, ACCELERATION));
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
                            player.Velocity = Vector2.Subtract(player.Velocity,new Vector2(DECELERATION,0));
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
                            player.Velocity = Vector2.Add(player.Velocity, new Vector2(DECELERATION,0));
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
                            player.Velocity = Vector2.Subtract(player.Velocity,new Vector2(0, DECELERATION));
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
            }

            player.Position = Vector2.Add(player.Position, player.Velocity);

            if (player.Position.X + player.spriteWidth > GameEngine.SCREEN_WIDTH)
            {
                player.Position = new Vector2(GameEngine.SCREEN_WIDTH - player.spriteWidth, player.Position.Y);
                player.Velocity = new Vector2(0, player.Velocity.Y);
            }
            if (player.Position.X < 0)
            {
                player.Position = new Vector2(0, player.Position.Y);
                player.Velocity = new Vector2(0, player.Velocity.Y);
            }
            if (player.Position.Y + player.spriteHeight > GameEngine.SCREEN_HEIGHT)
            {
                player.Position = new Vector2(player.Position.X, GameEngine.SCREEN_HEIGHT - player.spriteHeight);
                player.Velocity = new Vector2(player.Velocity.X, 0);
            }
            if (player.Position.Y < 0)
            {
                player.Position = new Vector2(player.Position.X, 0);
                player.Velocity = new Vector2(player.Velocity.X, 0);
            }
        }

        private bool Rain()
        {
            if (player.Rain > 0)
            {
                player.Rain--;
                //do other stuff, affect world, draw rain etc.
                return true;
            }
            return false;
        }

        private bool Absorb()
        {
            return false;
        }
    }
}
