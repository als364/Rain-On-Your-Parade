﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Diagnostics;
#endregion

namespace Rain_On_Your_Parade
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameEngine : Game
    {
        public enum WinCondition { Objects, Actors, Malice };

        public enum GameState { MainMenu, PauseMenu, Game };

        public const int SCREEN_WIDTH = 800;
        public const int SCREEN_HEIGHT = 800;

        public const int MAX_RAINBOW_TIME = 200;

        private int stage = 1;

        public static int STAGE_NUM= 10;
        public const int LOG_FRAMES = 120;
        private int framesTillLog = 0;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Model> models;
        List<View> views;
        List<Controller> controllers;

        //WorldState worldState;
        Canvas level;

        Texture2D batterybar;
        Texture2D battery;
        Texture2D background;
        Texture2D menu_background;
        SpriteFont font;

        Logger log;

        GameState state;
        MainMenu mainMenu;
        //Boolean menuOn = true;

        public GameEngine()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            //GraphicsDevice.BlendState = BlendState.AlphaBlend;
            //content = new ContentManager(Services);
            Content.RootDirectory = "Content";
            models = new List<Model>();
            views = new List<View>();
            controllers = new List<Controller>();
            state = GameState.MainMenu;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here
            resetGame();
        }

        public void resetGame()
        {
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            models.Clear();
            views.Clear();
            controllers.Clear();

            mainMenu = new MainMenu();
            //if (menuOn) mainMenu.Initialize();
            switch (state)
            {
                case GameState.Game:
                    //This is where the level building goes. I don't care about an XML parsing framework yet
                    level = new Canvas(stage);

                    // Debug.WriteLine("Y: " + level.Grid[6, 6].Actors[0].Position.Y);
                    int quota = 100;
                    //worldState = new WorldState(quota,level.Grid);
                    level.MaliceObjective = quota;

                    //Debug.WriteLine("Y: " + worldState.getActors().ToArray()[1].Position.Y);
                    foreach (WorldObject o in level.Objects)
                    {
                        View objects = new View(o);
                        models.Add(o);
                        views.Add(objects);
                    }
                    foreach (Actor a in level.Actors)
                    {
                        View actors = new View(a);
                        models.Add(a);
                        views.Add(actors);
                        Controller actorController = new ActorController(a);
                        controllers.Add(actorController);
                    }

                    View player = new View(level.Player);
                    views.Add(player);
                    models.Add(level.Player);
                    controllers.Add(new PlayerController(level.Player));

                    //models.Add(slider);
                    //views.Add(sliderView);
                    //controllers.Add(sliderController);
                    break;
                case GameState.MainMenu:
                    mainMenu.Initialize();
                    break;
                case GameState.PauseMenu:
                    break;
            }
                log = new Logger();

                base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (Model model in models)
            {
                if (model != null)
                {
                    model.LoadContent(this.Content);
                }
            }

            batterybar = Content.Load<Texture2D>("SliderBackground");
            battery = Content.Load<Texture2D>("grass");
            background = Content.Load<Texture2D>("background");
            menu_background = Content.Load<Texture2D>("menu_bg");
            font = Content.Load<SpriteFont>("DefaultFont");
            mainMenu.LoadContent(this.Content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (!menuOn)
            switch (state)
            {
                case GameState.Game:
                    if (framesTillLog == 0)
                    {
                        log.Log(level, gameTime);
                        framesTillLog = LOG_FRAMES;
                    }
                    else
                    {
                        framesTillLog--;
                    }

                    level.updateMalice();

                    switch (level.win)
                    {
                        case WinCondition.Malice:
                            level.percentWon = ((float)level.Malice) / ((float)level.MaliceObjective);
                            if (level.percentWon >= .99)
                            {
                                if (stage++ > STAGE_NUM)
                                {
                                    stage = 1;
                                }
                                Initialize();
                                return;
                            }
                            break;
                        case WinCondition.Actors:
                            level.percentWon = ((float)level.maliceActors.Count) / (float)level.Actors.Count;
                            if (level.percentWon >= .99)
                            {
                                if (stage++ > STAGE_NUM)
                                {
                                    stage = 1;
                                }
                                Initialize();
                                return;
                            }
                            break;
                        case WinCondition.Objects:
                            int activableObjCount = 0;
                            foreach (WorldObject o in level.Objects)
                            {
                                if (o.Type.CanActivate)
                                {
                                    activableObjCount++;
                                }
                            }
                            level.percentWon = ((float)level.maliceObjects.Count) / (float)activableObjCount;
                            if (level.percentWon >= .99)
                            {
                                if (stage++ > STAGE_NUM)
                                {
                                    stage = 1;
                                }
                                Initialize();
                                return;
                            }
                            break;
                    }

                    // TODO: Add your update logic here

                    List<WorldObject> toRemove = new List<WorldObject>();
                    List<WorldObject> rainbowKeys = new List<WorldObject>();
                    foreach (WorldObject r in level.rainbows.Keys)
                    {
                        rainbowKeys.Add(r);
                    }
                    foreach (WorldObject r in rainbowKeys)
                    {
                        level.rainbows[r] = (int)level.rainbows[r] - 1;
                        if (((int)level.rainbows[r]) == 0)
                        {
                            toRemove.Add(r);
                        }
                    }
                    foreach (WorldObject r in toRemove)
                    {
                        r.deactivate();
                        level.rainbows.Remove(r);
                    }


                    foreach (Model model in models)
                    {
                        if (model != null)
                        {   //TODO: Encapsulate this in individual classes
                            model.activatedSprite.Update();

                            if (model.deactivatedSprite != null)
                            {
                                model.deactivatedSprite.Update();
                            }

                            /*int maliceSum = 0;

                            if (model is Actor)
                            {
                                Actor actor = (Actor)model;
                                maliceSum += actor.Mood;
                            }
                            level.Malice = maliceSum;*/
                        }
                    }

                    foreach (Controller controller in controllers)
                    {
                        controller.Update(gameTime, level);
                    }
                    level.upateGridSquares();
                    break;
                case GameState.MainMenu:
                    int selected = mainMenu.Update();
                    if (selected > 0)
                    {
                        state = GameState.Game;
                        stage = selected;
                        Initialize();
                        return;
                    }
                    break;
                case GameState.PauseMenu:
                    break;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                state = GameState.MainMenu;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                Initialize();
                return;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LimeGreen);

            //if (menuOn) mainMenu.Draw(spriteBatch);
            //else
            //{
            switch (state)
            {
                case GameState.Game:
                    // TODO: Add your drawing code here
                    //Console.Write(level.Malice.ToString()+"\n"+level.MaliceObjective.ToString()+"\n");
                    spriteBatch.Begin();
                    spriteBatch.Draw(background, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), new Color(level.maliceTint(),level.maliceTint(),level.maliceTint()));
                    spriteBatch.End();

                    foreach (View view in views)
                    {
                        view.Draw(spriteBatch); //calls the AnimatedSprite draw function which includes begin/end
                    }

                    spriteBatch.Begin();
                    spriteBatch.Draw(batterybar, new Rectangle(0, 0, SCREEN_WIDTH, 40), Color.Black);
                    for (int i = 0; i < level.Player.Rain; i++)
                    {
                        spriteBatch.Draw(batterybar,
                            new Rectangle(i * 10 + 5, 5, 10, 30), Color.Blue);
                    }
                    spriteBatch.DrawString(font, "Water", new Vector2(20, 5), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);

<<<<<<< HEAD
                    spriteBatch.DrawString(font, level.objectiveMessage, new Vector2(20, 20), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);


                    /*
=======
>>>>>>> 3da7431c3285db042e7ace52aa03f8106e92e7d3
                    spriteBatch.Draw(batterybar, new Rectangle(0, 40, SCREEN_WIDTH, 40), Color.Black);
                    for (int i = 0; i < level.Malice; i++)
                    {
                        spriteBatch.Draw(batterybar,
                            new Rectangle(i * 10 + 5, 45, 10, 30), Color.Firebrick);
                    }
                    spriteBatch.DrawString(font, "Malice", new Vector2(20, 45), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                    
                    spriteBatch.End();
                    break;
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    spriteBatch.Draw(menu_background, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), Color.White);
                    spriteBatch.End();
                    mainMenu.Draw(spriteBatch);
                    break;
                case GameState.PauseMenu:
                    //TODO: implement pause menu
                    break;
            }
            base.Draw(gameTime);

        }
    }
}
