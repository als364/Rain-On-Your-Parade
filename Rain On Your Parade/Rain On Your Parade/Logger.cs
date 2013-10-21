using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;

namespace Rain_On_Your_Parade
{
    class Logger
    {
        DateTime now;

        public Logger()
        {
            now = DateTime.Now;
            File.AppendAllText("log.txt", now.ToString() + "\nScreen Width:" + GameEngine.SCREEN_WIDTH + "\nScreen Height: " + GameEngine.SCREEN_HEIGHT + "\n");
        }

        public void Log(WorldState state, GameTime gameTime)
        {
            File.AppendAllText("log.txt", gameTime.TotalGameTime + "\n" + state.ToString() + "\n");
            //foreach (GridSquare g in state.StateOfWorld)
            //{
            //    File.AppendAllText("log.txt", g.ToString() + "\n");
            //}
        }
    }
}
