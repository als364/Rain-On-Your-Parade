using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Rain_On_Your_Parade
{
    abstract class Controller
    {
        protected Model controlledModel;
        public Model ControlledModel { get { return controlledModel; } }

        public Controller(Model model)
        {
            controlledModel = model;
        }

        abstract public void Update(GameTime gameTime, WorldState worldState);
    }
}
