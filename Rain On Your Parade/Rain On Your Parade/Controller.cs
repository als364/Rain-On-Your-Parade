using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rain_On_Your_Parade
{
    class Controller
    {
        protected Model controlledModel;
        public Model ControlledModel { get { return controlledModel; } }

        public Controller(Model model)
        {
            controlledModel = model;
        }
    }
}
