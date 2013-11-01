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
        string log;
        string personalLog;

        public Logger()
        {
            now = DateTime.Now;
            log = now.Month + "-" + now.Day + "-" + now.Year + " " + now.Hour + "-" + now.Minute + " log.txt";
            personalLog = Environment.UserName + " " + now.Month + "-" + now.Day + "-" + now.Year + " " + now.Hour + "-" + now.Minute + " log.txt";
         
            File.AppendAllText(log, now.ToString() + "\nScreen Width:" + GameEngine.SCREEN_WIDTH + "\nScreen Height: " + GameEngine.SCREEN_HEIGHT + "\n");
        }

        public Logger(string dir)
        {
            if (!dir.EndsWith("\\"))
            {
                dir += "\\";
            }
            now = DateTime.Now;
            log = dir + now.Month + "-" + now.Day + "-" + now.Year + " " + now.Hour + "-" + now.Minute + " log.txt";
            personalLog = dir + Environment.UserName + " " + now.Month + "-" + now.Day + "-" + now.Year + " " + now.Hour + "-" + now.Minute + " log.txt";

            File.AppendAllText(log, now.ToString() + "\nScreen Width:" + GameEngine.SCREEN_WIDTH + "\nScreen Height: " + GameEngine.SCREEN_HEIGHT + "\n");
        }

        public void Log(Canvas level, GameTime gameTime)
        {
            File.AppendAllText(log, gameTime.TotalGameTime + "\n" + level.ToString() + "\n");
            //foreach (GridSquare g in state.StateOfWorld)
            //{
            //    File.AppendAllText("log.txt", g.ToString() + "\n");
            //}
        }

        public void Log(Object o)
        {
            File.AppendAllText(personalLog, o.ToString());
        }

    }
}
