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
                    if (ks.IsKeyDown(Keys.W))
                    {
                       player.Velocity = Vector2.Subtract(player.Velocity,new Vector2(0, ACCELERATION));
                    }
                    if (ks.IsKeyDown(Keys.D))
                    {
                        player.Velocity = Vector2.Add(player.Velocity, new Vector2(ACCELERATION, 0));
                    }
                    if (ks.IsKeyDown(Keys.S))
                    {
                        player.Velocity = Vector2.Add(player.Velocity, new Vector2(0, ACCELERATION));
                    }
                    if (ks.IsKeyDown(Keys.A))
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
                        player.Velocity = Vector2.Subtract(player.Velocity,new Vector2(ACCELERATION,0));
                    }
                    else
                    {
                        player.Velocity = Vector2.Add(player.Velocity, new Vector2(ACCELERATION,0));
                    }
                }

                if (Math.Abs(player.Velocity.Y) > 0 && ks.IsKeyUp(Keys.W) && ks.IsKeyUp(Keys.S))
                {
                    if (player.Velocity.Y > 0)
                    {
                        player.Velocity = Vector2.Subtract(player.Velocity,new Vector2(0, ACCELERATION));
                    }
                    else
                    {
                        player.Velocity = Vector2.Add(player.Velocity, new Vector2(0, ACCELERATION));
                    }
                }
            }
            else
            {
                coolDown--;
            }

            player.Position = Vector2.Add(player.Position, player.Velocity);
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
    }
}
