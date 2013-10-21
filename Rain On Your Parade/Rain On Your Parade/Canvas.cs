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
                    canvasGrid[i,j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), new Point(i, j));
                }
            }

            //
            //Reactivate once actor pathing is fixed
            //

            //canvasGrid[6, 0] = new GridSquare(ActorType.Type.Cat, new Point(6, 0));
            //canvasGrid[1, 8] = new GridSquare(ActorType.Type.Kid, new Point(1, 8));
            //canvasGrid[6, 6] = new GridSquare(ActorType.Type.Mom, new Point(6, 6));
            canvasGrid[1, 1] = new GridSquare(ObjectType.Type.SunnySpot, new Point(1, 1));
            canvasGrid[4, 1] = new GridSquare(ObjectType.Type.Laundry, new Point(4, 1));
            canvasGrid[8, 1] = new GridSquare(ObjectType.Type.House, new Point(8, 1));
            canvasGrid[9, 1] = new GridSquare(ObjectType.Type.House, new Point(9, 1));
            canvasGrid[2, 8] = new GridSquare(ObjectType.Type.House, new Point(2, 8));
            canvasGrid[9, 2] = new GridSquare(ObjectType.Type.House, new Point(9, 2));
            canvasGrid[10, 2] = new GridSquare(ObjectType.Type.Garden, new Point(10, 2));
            canvasGrid[2, 3] = new GridSquare(ObjectType.Type.Pool, new Point(2, 3));
            canvasGrid[3, 2] = new GridSquare(ObjectType.Type.House, new Point(3, 2));
            canvasGrid[4, 2] = new GridSquare(ObjectType.Type.House, new Point(4, 2));
            canvasGrid[3, 3] = new GridSquare(ObjectType.Type.House, new Point(3, 3));
            canvasGrid[4, 3] = new GridSquare(ObjectType.Type.House, new Point(4, 3));
            canvasGrid[0, 5] = new GridSquare(ObjectType.Type.Chalking, new Point(0, 5));
            canvasGrid[1, 5] = new GridSquare(ObjectType.Type.Chalking, new Point(1, 5));
            canvasGrid[2, 5] = new GridSquare(ObjectType.Type.Chalking, new Point(2, 5));
            canvasGrid[3, 5] = new GridSquare(ObjectType.Type.Chalking, new Point(3, 5));
            canvasGrid[4, 5] = new GridSquare(ObjectType.Type.Chalking, new Point(4, 5));
            canvasGrid[5, 5] = new GridSquare(ObjectType.Type.Chalking, new Point(5, 5));
            canvasGrid[6, 5] = new GridSquare(ObjectType.Type.Chalking, new Point(6, 5));
            canvasGrid[6, 4] = new GridSquare(ObjectType.Type.Chalking, new Point(6, 4));
            canvasGrid[6, 3] = new GridSquare(ObjectType.Type.Chalking, new Point(6, 3));
            canvasGrid[7, 3] = new GridSquare(ObjectType.Type.Chalking, new Point(7, 3));
            canvasGrid[8, 3] = new GridSquare(ObjectType.Type.Chalking, new Point(8, 3));
            canvasGrid[9, 3] = new GridSquare(ObjectType.Type.Chalking, new Point(9, 3));
            canvasGrid[10, 3] = new GridSquare(ObjectType.Type.Chalking, new Point(10, 3));
            canvasGrid[2, 6] = new GridSquare(ObjectType.Type.House, new Point(2, 6));
            canvasGrid[3, 6] = new GridSquare(ObjectType.Type.House, new Point(3, 6));
            canvasGrid[4, 6] = new GridSquare(ObjectType.Type.House, new Point(4, 6));
            canvasGrid[8, 5] = new GridSquare(ObjectType.Type.House, new Point(8, 5));
            canvasGrid[9, 5] = new GridSquare(ObjectType.Type.House, new Point(9, 5));
            canvasGrid[8, 6] = new GridSquare(ObjectType.Type.House, new Point(8, 6));
            canvasGrid[9, 6] = new GridSquare(ObjectType.Type.House, new Point(9, 6));
            canvasGrid[5, 7] = new GridSquare(ObjectType.Type.SunnySpot, new Point(5, 7));
            canvasGrid[9, 7] = new GridSquare(ObjectType.Type.Garden, new Point(9, 7));

        }

       
    }
      
}
