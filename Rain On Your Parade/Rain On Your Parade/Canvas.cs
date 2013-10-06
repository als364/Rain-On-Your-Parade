using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            canvasGrid[1, 1] = new GridSquare(new List<WorldObject>() {new WorldObject(new ObjectType(SunnySpot, false, 3,1,0,0))}, new List<Actor>(), true);
            canvasGrid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
            canvasGrid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
            canvasGrid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
            canvasGrid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
            canvasGrid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
            canvasGrid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
            canvasGrid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
            canvasGrid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
            canvasGrid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
            canvasGrid[i, j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);

        }


    }
}
