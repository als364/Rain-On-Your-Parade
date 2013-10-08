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
        public const float ACCELERATION = 0.1f;
        public const float MAX_SPEED = 5f;
        public const int COOL_DOWN = 15; //amount of time spent raining
        private int coolDown = 0;
        private Player player;

        public PlayerController(Player player)
            : base(player)
        {
            this.player = player;
        }

        public override void Update(GameTime gameTime, WorldState worldState)
        {
            KeyboardState ks = Keyboard.GetState();
           /* if (coolDown == 0) //
            {
                if (player.Velocity.Length() < MAX_SPEED)
                {
                    if (ks.IsKeyDown(Keys.W))
                    {
                        if (Math.Sqrt(Math.Pow(player.Velocity.Y, 2) + Math.Pow(player.Velocity.X, 2)) <= MAX_SPEED)
                        {
                            player.Velocity = new Vector2(player.Velocity.X, player.Velocity.Y + ACCELERATION);
                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {
                coolDown--;
            }*/
        }
    }
}
