using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Rain_On_Your_Parade
{
    class Canvas
    {
        public const int SQUARE_SIZE = 80;
        private int squaresWide = GameEngine.SCREEN_WIDTH / SQUARE_SIZE;
        private int squaresTall = GameEngine.SCREEN_HEIGHT / SQUARE_SIZE;

        ///TODO: make this more extendable

        public GridSquare[,] canvasGrid;

        public Canvas()
        {
            canvasGrid = new GridSquare[squaresWide, squaresTall];
            for (int i = 0; i < squaresWide; i++)
            {
                for (int j = 0; j < squaresTall; j++)
                {
                    canvasGrid[i,j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
                }
            }

            canvasGrid[0, 6] = new GridSquare(new List<WorldObject>(), new List<Actor>() {new Actor(new ActorType(ActorType.Type.Cat, 0,0,0,0,0,0,0), new Point(0,6), 0, new ActorState(ActorState.AState.Sleep)) }, true);
            canvasGrid[1, 1] = new GridSquare(new List<WorldObject>() {new WorldObject(new ObjectType(ObjectType.TypeName.SunnySpot, true, 3,1,0,0))}, new List<Actor>(), true);
            canvasGrid[4, 1] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Laundry, true, 1, 1, 1, 1)) }, new List<Actor>(), true);
            canvasGrid[1, 8] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[9, 1] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[2, 8] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[9, 2] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[10, 2] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Garden, true, 3, 0, 3, 3)) }, new List<Actor>(), true);
            canvasGrid[2, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Pool, true, 0, 3, 0, 0)) }, new List<Actor>(), true);
            canvasGrid[3, 2] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[4, 2] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[3, 3] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[4, 3] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[0, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[1, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[2, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[3, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[4, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[5, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[6, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[6, 4] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[6, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[7, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[8, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[9, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[10, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1)) }, new List<Actor>(), true);
            canvasGrid[2, 6] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[3, 6] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[4, 6] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[6, 6] = new GridSquare(new List<WorldObject>(), new List<Actor>() { new Actor(new ActorType(ActorType.Type.Mom, 0, 0, 0, 0, 0, 0, 0), new Point(0, 6), 0, new ActorState(ActorState.AState.Nurture)) }, true);
            canvasGrid[8, 5] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[9, 5] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[8, 6] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[9, 6] = new GridSquare(new List<WorldObject>(), new List<Actor>(), false);
            canvasGrid[5, 7] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.SunnySpot, true, 3, 1, 0, 0)) }, new List<Actor>(), true);
            canvasGrid[9, 7] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Garden, true, 3, 0, 3, 3)) }, new List<Actor>(), true);
            canvasGrid[1, 8] = new GridSquare(new List<WorldObject>(), new List<Actor>() { new Actor(new ActorType(ActorType.Type.Kid, 0, 0, 0, 0, 0, 0, 0), new Point(0, 6), 0, new ActorState(ActorState.AState.Play)) }, true);


        }


    }
}
