﻿using System.Collections.Generic;
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
                    canvasGrid[i,j] = new GridSquare(new List<WorldObject>(), new List<Actor>(), true);
                }
            }

            canvasGrid[6, 0] = new GridSquare(new List<WorldObject>(), new List<Actor>() {new Actor(new ActorType(ActorType.Type.Cat,0,0,0,0,0,0,0), new Vector2(6*80,0), 0, new ActorState(ActorState.AState.Sleep)) }, true);
            canvasGrid[1, 1] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.SunnySpot, true, 3, 1, 0, 0), new Vector2(1 * 80, 1 * 80)) }, new List<Actor>(), true);
            canvasGrid[4, 1] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Laundry, true, 1, 1, 1, 1), new Vector2(4 * 80, 1 * 80)) }, new List<Actor>(), true);
            canvasGrid[8, 1] = new GridSquare(new List<WorldObject>{new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(8 * 80, 1 * 80))}, new List<Actor>(), false);
            canvasGrid[9, 1] = new GridSquare(new List<WorldObject>{new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(9 * 80, 1 * 80))}, new List<Actor>(), false);
            canvasGrid[8, 2] = new GridSquare(new List<WorldObject>{new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(8 * 80, 2 * 80))}, new List<Actor>(), false);
            canvasGrid[9, 2] = new GridSquare(new List<WorldObject>{new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(9 * 80, 2 * 80))}, new List<Actor>(), false);
            canvasGrid[10, 2] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Garden, true, 3, 0, 3, 3), new Vector2(10 * 80, 2 * 80)) }, new List<Actor>(), true);
            canvasGrid[2, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Pool, true, 0, 3, 0, 0), new Vector2(2 * 80, 3 * 80)) }, new List<Actor>(), true);
            canvasGrid[3, 2] = new GridSquare(new List<WorldObject> { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(3 * 80, 2 * 80)) }, new List<Actor>(), false);
            canvasGrid[4, 2] = new GridSquare(new List<WorldObject> { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(4 * 80, 2 * 80)) }, new List<Actor>(), false);
            canvasGrid[3, 3] = new GridSquare(new List<WorldObject> { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(3 * 80, 3 * 80)) }, new List<Actor>(), false);
            canvasGrid[4, 3] = new GridSquare(new List<WorldObject> { new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(4 * 80, 3 * 80)) }, new List<Actor>(), false);
            canvasGrid[0, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(0 * 80, 5 * 80)) }, new List<Actor>(), true);
            canvasGrid[1, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(1 * 80, 5 * 80)) }, new List<Actor>(), true);
            canvasGrid[2, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(2 * 80, 5 * 80)) }, new List<Actor>(), true);
            canvasGrid[3, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(3 * 80, 5 * 80)) }, new List<Actor>(), true);
            canvasGrid[4, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(4 * 80, 5 * 80)) }, new List<Actor>(), true);
            canvasGrid[5, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(5 * 80, 5 * 80)) }, new List<Actor>(), true);
            canvasGrid[6, 5] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(6 * 80, 5 * 80)) }, new List<Actor>(), true);
            canvasGrid[6, 4] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(6 * 80, 4 * 80)) }, new List<Actor>(), true);
            canvasGrid[6, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(6 * 80, 3 * 80)) }, new List<Actor>(), true);
            canvasGrid[7, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(7 * 80, 3 * 80)) }, new List<Actor>(), true);
            canvasGrid[8, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(8 * 80, 3 * 80)) }, new List<Actor>(), true);
            canvasGrid[9, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(9 * 80, 3 * 80)) }, new List<Actor>(), true);
            canvasGrid[10, 3] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Chalking, false, 1, 3, 0, 1), new Vector2(10 * 80, 3 * 80)) }, new List<Actor>(), true);
            canvasGrid[2, 6] = new GridSquare(new List<WorldObject>{new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(2 * 80, 6 * 80))}, new List<Actor>(), false);
            canvasGrid[3, 6] = new GridSquare(new List<WorldObject>{new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(3 * 80, 6 * 80))}, new List<Actor>(), false);
            canvasGrid[4, 6] = new GridSquare(new List<WorldObject>{new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(4 * 80, 6 * 80))}, new List<Actor>(), false);
            canvasGrid[6, 6] = new GridSquare(new List<WorldObject>(), new List<Actor>() { new Actor(new ActorType(ActorType.Type.Mom, 0, 0, 0, 0, 0, 0, 0), new Vector2(6*80, 6*80), 0, new ActorState(ActorState.AState.Nurture)) }, true);
            canvasGrid[8, 5] = new GridSquare(new List<WorldObject> {new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(8 * 80, 5 * 80))}, new List<Actor>(), false);
            canvasGrid[9, 5] = new GridSquare(new List<WorldObject> {new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(9 * 80, 5 * 80))}, new List<Actor>(), false);
            canvasGrid[8, 6] = new GridSquare(new List<WorldObject> {new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(8 * 80, 6 * 80))}, new List<Actor>(), false);
            canvasGrid[9, 6] = new GridSquare(new List<WorldObject> {new WorldObject(new ObjectType(ObjectType.TypeName.House, false, 0, 0, 0, 0), new Vector2(9 * 80, 6 * 80))}, new List<Actor>(), false);
            canvasGrid[5, 7] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.SunnySpot, true, 3, 1, 0, 0), new Vector2(5 * 80, 7 * 80)) }, new List<Actor>(), true);
            canvasGrid[9, 7] = new GridSquare(new List<WorldObject>() { new WorldObject(new ObjectType(ObjectType.TypeName.Garden, true, 3, 0, 3, 3), new Vector2(9 * 80, 7 * 80)) }, new List<Actor>(), true);
            canvasGrid[1, 8] = new GridSquare(new List<WorldObject>(), new List<Actor>() { new Actor(new ActorType(ActorType.Type.Kid, 0, 0, 0, 0, 0, 0, 0), new Vector2(1*80, 8*80), 0, new ActorState(ActorState.AState.Play)) }, true);

        }

       
    }
      
}
