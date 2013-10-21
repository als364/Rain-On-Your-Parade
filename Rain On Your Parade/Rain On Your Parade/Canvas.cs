using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;

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
                    canvasGrid[i,j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true, new Point(i, j));
                }
            }

            canvasGrid[6, 0] = new GridSquare(new List<WorldObject>(), new List<Actor>() {/*new Actor(new ActorType(ActorType.Type.Cat,0,0,0,0,0,0,0), new Vector2(6*SQUARE_SIZE,0), 0, new ActorState(ActorState.AState.Sleep))*/ }, true, new Point(6, 0));
            canvasGrid[1, 1] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.SunnySpot, true, 0, 3, 0, 0), new Vector2(1 * SQUARE_SIZE, 1 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(1, 1));
            canvasGrid[4, 1] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Laundry, true, 0, 0, 3, 0), new Vector2(4 * SQUARE_SIZE, 1 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(4, 1));
            canvasGrid[8, 1] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(8 * SQUARE_SIZE, 1 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(8, 1));
            canvasGrid[9, 1] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(8 * SQUARE_SIZE, 1 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(9, 1));
            canvasGrid[2, 8] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(9 * SQUARE_SIZE, 1 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(2, 8));
            canvasGrid[9, 2] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(2 * SQUARE_SIZE, 8 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(9, 2));
            canvasGrid[10, 2] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Garden, true, 3, 0, 3, 3), new Vector2(10 * SQUARE_SIZE, 2 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(10, 2));
            canvasGrid[2, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Pool, true, 3, 0, 0, 0), new Vector2(2 * SQUARE_SIZE, 3 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(2, 3));
            canvasGrid[3, 2] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(3 * SQUARE_SIZE, 2 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(3, 2));
            canvasGrid[4, 2] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(4 * SQUARE_SIZE, 2 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(4, 2));
            canvasGrid[3, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(3 * SQUARE_SIZE, 3 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(3, 3));
            canvasGrid[4, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(4 * SQUARE_SIZE, 3 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(4, 3));
            canvasGrid[0, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(0 * SQUARE_SIZE, 5 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(0, 5));
            canvasGrid[1, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(1 * SQUARE_SIZE, 5 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(1, 5));
            canvasGrid[2, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(2 * SQUARE_SIZE, 5 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(2, 5));
            canvasGrid[3, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(3 * SQUARE_SIZE, 5 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(3, 5));
            canvasGrid[4, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(4 * SQUARE_SIZE, 5 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(4, 5));
            canvasGrid[5, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(5 * SQUARE_SIZE, 5 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(5, 5));
            canvasGrid[6, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(6 * SQUARE_SIZE, 5 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(6, 5));
            canvasGrid[6, 4] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(6 * SQUARE_SIZE, 4 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(6, 4));
            canvasGrid[6, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(6 * SQUARE_SIZE, 3 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(6, 3));
            canvasGrid[7, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(7 * SQUARE_SIZE, 3 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(7, 3));
            canvasGrid[8, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(8 * SQUARE_SIZE, 3 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(8, 3));
            canvasGrid[9, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(9 * SQUARE_SIZE, 3 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(9, 3));
            canvasGrid[10, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(10 * SQUARE_SIZE, 3 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(10, 3));
            canvasGrid[2, 6] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(2 * SQUARE_SIZE, 6 * SQUARE_SIZE))} ,new List<Actor>(), false, new Point(2, 6));
            canvasGrid[3, 6] = new GridSquare(new List<WorldObject>(){ new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(3 * SQUARE_SIZE, 6 * SQUARE_SIZE))}, new List<Actor>(), false, new Point(3, 6));
            canvasGrid[4, 6] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(4 * SQUARE_SIZE, 6 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(4, 6));
            canvasGrid[6, 6] = new GridSquare(new List<WorldObject>(){ new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(6 * SQUARE_SIZE, 6 * SQUARE_SIZE))}, new List<Actor>() { /*new Actor(new ActorType(ActorType.Type.Mom, 0, 0, 0, 0, 0, 0, 0), new Vector2(6*SQUARE_SIZE, 6*SQUARE_SIZE), 0, new ActorState(ActorState.AState.Nurture))*/ }, true, new Point(6, 6));
            canvasGrid[8, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(8 * SQUARE_SIZE, 5 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(8, 5));
            canvasGrid[9, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(9 * SQUARE_SIZE, 5 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(9, 5));
            canvasGrid[8, 6] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(8 * SQUARE_SIZE, 6 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(8, 6));
            canvasGrid[9, 6] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 1, 3, 0, 1), new Vector2(9 * SQUARE_SIZE, 6 * SQUARE_SIZE)) }, new List<Actor>(), false, new Point(9, 6));
            canvasGrid[5, 6] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.SunnySpot, true, 0, 3, 0, 0), new Vector2(5 * SQUARE_SIZE, 6 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(5, 6));
            canvasGrid[9, 7] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Garden, true, 3, 0, 3, 3), new Vector2(9 * SQUARE_SIZE, 7 * SQUARE_SIZE)) }, new List<Actor>(), true, new Point(9, 7));
            canvasGrid[1, 8] = new GridSquare(new List<WorldObject>(), new List<Actor>() { new Actor(new ActorType(ActorType.Type.Kid), new Vector2(1 * SQUARE_SIZE, 8 * SQUARE_SIZE), 0, new ActorState(ActorState.AState.Play)) }, true, new Point(1, 8));

        }

       
    }
      
}
