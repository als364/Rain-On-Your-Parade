using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Rain_On_Your_Parade
{
    class ActorController : Controller
    {
        Actor controlledActor;

        public ActorController(Actor actor) : base(actor)
        {
            controlledActor = actor;
        }

        public override void Update(GameTime gameTime, WorldState worldState)
        {
            GridSquare target = preferenceSearch(controlledActor.getType(), worldState);
        }

        public GridSquare preferenceSearch(ActorType actorType, WorldState worldState)
        {
            
        }

        public GridSquare breadthFirstSearch(GridSquare target, GridSquare currentSquare)
        {
            Queue<GridSquare> queue = new Queue<GridSquare>();
            HashSet<GridSquare> seen = new HashSet<GridSquare>();
            queue.Enqueue(currentSquare);
            seen.Add(currentSquare);
            while (queue.Count != 0)
            {
                GridSquare lookingAt = queue.Dequeue();
                if (lookingAt.Equals(target))
                {
                    //no, this does not actually work
                    return lookingAt;
                }
                
            }
        }
    }
}
