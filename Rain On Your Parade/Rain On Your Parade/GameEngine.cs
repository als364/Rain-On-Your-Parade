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
        public enum WinCondition { Objects, Actors, Mood };

        public enum GameState { MainMenu, PauseMenu, Game };

        public const int SCREEN_WIDTH = 960;
        public const int SCREEN_HEIGHT = 720;

        public const int MAX_RAINBOW_TIME = 400;
        
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
        Texture2D waterDrop;
        Texture2D background;
        Texture2D menu_background;

        SpriteFont font;

        Logger log;

        GameState state;
        MainMenu mainMenu;
        LevelComplete levelEnd;
        LevelStart levelStart;
        PauseScreen pauseScreen;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        bool levelNotStarted;
        int levelStartDelay;
        bool levelPaused;
        bool levelHasEnded;


        //Boolean menuOn = true;

        //Add levels to Levels folder and then add name to this array
        /*public static string[] levels = { "sample.xml",
                                          "Level1.xml",
                                          "Soak the Cat.xml",
                                          "Kill the Flowers.xml",
                                          "Kill the Flowers v2.xml",
                                          "Make Kids Cry.xml",
                                          "Showdown.xml",
                                          "Errand Boys.xml"};
        */
        private int stage = 1;

        //public static int STAGE_NUM = levels.GetLength(0) ;
        public static int STAGE_NUM = 9;

        public GameEngine()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
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
            levelEnd = new LevelComplete();
            levelStart = new LevelStart();
            pauseScreen = new PauseScreen();

            levelNotStarted = true;
            levelStartDelay = 10;
            levelHasEnded = false;
            levelPaused = false;

            //if (menuOn) mainMenu.Initialize();
            switch (state)
            {
                case GameState.Game:
                    //This is where the level building goes. I don't care about an XML parsing framework yet
                    level = new Canvas(stage);
                    //////////////////////////////////////////////
                    //level = LevelParser.parse(levels[stage]);
                    ///////////////////////////////////////////

                    // Debug.WriteLine("Y: " + level.Grid[6, 6].Actors[0].Position.Y);
                    //int quota = 100;
                    ////worldState = new WorldState(quota,level.Grid);
                    //level.MoodObjective = quota;

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

                    levelEnd.Initialize();

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
            waterDrop = Content.Load<Texture2D>("water_drop");
            background = Content.Load<Texture2D>("background");
            menu_background = Content.Load<Texture2D>("menu_bg");
            font = Content.Load<SpriteFont>("DefaultFont");
            mainMenu.LoadContent(this.Content);
            levelEnd.LoadContent(this.Content);
            levelStart.LoadContent(this.Content);
            pauseScreen.LoadContent(this.Content);

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

                    #region GameOver
                    if (levelHasEnded)
                    {
                        //Record Results
                        int optionSelected = levelEnd.Update();

                        if (optionSelected == 0) //Continue
                        {
                            if (stage++ >= STAGE_NUM)
                            {
                                stage = 1;
                            }
                            levelHasEnded = false;
                            levelNotStarted = true;
                            Initialize();
                            return;
                        }
                        if (optionSelected == 2) //Main Menu
                        {
                            levelHasEnded = false;
                            state = GameState.MainMenu;
                            Initialize();
                            return;
                        }
                        if (optionSelected == 1) //Replay
                        {
                            levelHasEnded = false;
                            levelNotStarted = true;
                            Initialize();
                            return;
                        }
                        return;
                    } 
                    #endregion GameOver

                    #region GameStart
                    if (levelNotStarted)
                    {
                        int readyToStart = levelStart.Update();
                        if (readyToStart == 0) //Don't Start Yet
                        {
                            return;
                        }
                        if (readyToStart == 2) //Main Menu
                        {
                            levelNotStarted = false;
                            state = GameState.MainMenu;
                            Initialize();
                            return;
                        }
                        else //Start Game
                        {
                            levelNotStarted = false;
                        }
                    }
                    #endregion GameStart

                    #region GamePaused
                    if (levelPaused) {
                        int pauseOption = pauseScreen.Update();
                        if (pauseOption == 0) //Continue
                        {
                            levelPaused = false;
                        }
                        if (pauseOption == 1) //Main Menu
                        {
                            state = GameState.MainMenu;
                            Initialize();
                        }
                        //Still Paused
                        return;
                    }
                    #endregion GamePaused

                    

                    //if (framesTillLog == 0)
                    //{
                    //    log.Log(level, gameTime);
                    //    framesTillLog = LOG_FRAMES;
                    //}
                    //else
                    //{
                    //    framesTillLog--;
                    //}

                    level.updateMood();

                    #region WinConditions
                    switch (level.win)
                    {
                        case WinCondition.Mood:
                            level.percentWon = ((float)level.angerActors.Count) / ((float)level.goalAngerActors.Count);
                            if (level.percentWon >= 0.99)
                            {
                                levelHasEnded = true;
                            }
                            break;
                        case WinCondition.Actors:
                            level.percentWon = ((float)level.angerActors.Count) / ((float)level.goalAngerActors.Count);
                            if (level.percentWon >= 0.99)
                            {
                                levelHasEnded = true;
                            }
                            break;
                        case WinCondition.Objects:
                            level.percentWon = ((float)level.angerObjects.Count) / (float)level.goalAngerObjects.Count;
                            if (level.percentWon >= 0.99)
                            {
                                levelHasEnded = true;
                            }
                            break;
                    }
                    #endregion WinConditions

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

                            if (model.sleepSprite != null)
                            {
                                model.sleepSprite.Update();
                            }

                            if (model.rainSprite != null)
                            {
                                model.rainSprite.Update();
                            }

                            if (model.abSprite != null)
                            {
                                model.abSprite.Update();
                            }

                            /*int MoodSum = 0;

                            if (model is Actor)
                            {
                                Actor actor = (Actor)model;
                                MoodSum += actor.Mood;
                            }
                            level.Mood = moodSum;*/
                        }
                    }

                    if (levelStartDelay > 0)
                    {
                        levelStartDelay--;
                    }
                    else
                    {
                        foreach (Controller controller in controllers)
                        {
                            controller.Update(gameTime, level);
                        }
                        level.upateGridSquares();
                        
                    }

                    break;

                case GameState.MainMenu:
                    int selected = mainMenu.Update();
                    if (selected > 0)
                    {
                        state = GameState.Game;
                        levelNotStarted = true;
                        stage = selected;
                        Initialize();
                        return;
                    }
                    break;
            }

            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Back))
                Exit();
            if (keyboardState.IsKeyDown(Keys.Delete))
            {
                state = GameState.MainMenu;
            }
            if (keyboardState.IsKeyDown(Keys.R))
            {
                Initialize();
                return;
            }
            if (keyboardState.IsKeyDown(Keys.Escape) && oldKeyboardState.IsKeyUp(Keys.Escape))
            {
                levelPaused = true;
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

                    //Game Background
                    spriteBatch.Begin();
                    spriteBatch.Draw(background, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), new Color(level.moodTint(),level.moodTint(),level.moodTint()));
                    spriteBatch.End();

                    //Draw all models
                    foreach (View view in views)
                    {
                        view.Draw(spriteBatch); //calls the AnimatedSprite draw function which includes begin/end
                    }

                    //Draw the Water Meter
                    spriteBatch.Begin();
                    spriteBatch.Draw(batterybar, new Rectangle(0, 0, SCREEN_WIDTH, 40), Color.Black);
                    spriteBatch.Draw(batterybar, new Rectangle(45, 5, Player.MAX_RAIN * 40 + 10, 30), Color.LightSteelBlue);
                    spriteBatch.Draw(batterybar, new Rectangle(50, 10, Player.MAX_RAIN * 40, 20), Color.Azure);
                    spriteBatch.Draw(waterDrop, new Rectangle(7,7, 30, 30), Color.White);
                    for (int i = 1; i <= level.Player.Rain; i++)
                    {
                        spriteBatch.Draw(batterybar,
                            new Rectangle(i * 40 + 10, 10, 40, 20), Color.Blue);
                    }                    

                    //spriteBatch.DrawString(font, "Water", new Vector2(20, 5), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
              
                    //Draw the mood Meter
                    spriteBatch.Draw(batterybar, new Rectangle(0, 40, SCREEN_WIDTH, 40), Color.Black);
                    //spriteBatch.Draw(batterybar, new Rectangle(45, 45, Player.MAX_RAIN * 40 + 10, 30), Color.LightSteelBlue);
                    //spriteBatch.Draw(batterybar, new Rectangle(50, 50, Player.MAX_RAIN * 40, 20), Color.Azure);
                    spriteBatch.Draw(batterybar, new Rectangle(45, 45, level.goalAngerObjects.Count*22 + level.goalAngerActors.Count*52 + 10, 30), Color.LightSteelBlue);
                    spriteBatch.Draw(batterybar, new Rectangle(50, 50, level.goalAngerObjects.Count*22 + level.goalAngerActors.Count*52, 20), Color.Azure);
                    //int percentInt = (int)(level.percentWon * Player.MAX_RAIN * 40);
                    //spriteBatch.Draw(batterybar, new Rectangle(50, 50, percentInt, 20), Color.Firebrick);
                    spriteBatch.DrawString(font, "Goal", new Vector2(4, 50), Color.White, 0, new Vector2(0, 0), 0.7f, SpriteEffects.None, 0);

                    //Draw the incrementation
                    for (int i = 1; i <= Player.MAX_RAIN; i++)
                    {
                        spriteBatch.Draw(batterybar,
                            new Rectangle(i * 40 + 10, 10, 1, 20), Color.DarkBlue);
                        //spriteBatch.Draw(batterybar,
                         //   new Rectangle(i * 40 + 10, 50, 1, 20), Color.DarkRed);
                    }

                    //TODO: Display Icon of objects/actors that need to be ruined
                    for (int i = 0; i < level.goalAngerActors.Count; i++)
                    {
                        Color iconBG = (level.goalAngerActors[i].Type.TypeName == ActorType.Type.Cat) ? Color.Orange :
                            (level.goalAngerActors[i].Type.TypeName == ActorType.Type.Kid) ? Color.Blue : Color.Green;
                        Color iconColor = (level.goalAngerActors[i].Mood > 4) ? Color.DarkRed : iconBG;
                        string iconTexture = (level.goalAngerActors[i].Type.TypeName == ActorType.Type.Cat) ? "cat" :
                            (level.goalAngerActors[i].Type.TypeName == ActorType.Type.Kid) ? "kid" : "mom";
                        spriteBatch.Draw(batterybar, new Rectangle(50 + i * 52, 50, 50, 20), iconColor);
                        spriteBatch.DrawString(font, iconTexture, new Vector2(50 + i * 52 + 25, 50), Color.White, 0, new Vector2(font.MeasureString(iconTexture).X / 2, 0), 0.6f, SpriteEffects.None, 0);
                    }

                    for (int i = 0; i < level.goalAngerObjects.Count; i++)
                    {
                        int y_dist = 50;
                        int x_dist = i;

                        Color iconColor = (!level.goalAngerObjects[i].Activated) ? Color.DarkRed :
                            (level.goalAngerObjects[i].Type.TypeName == ObjectType.Type.Laundry) ? Color.Fuchsia : Color.Goldenrod;
                        string iconTexture = (level.goalAngerObjects[i].Type.TypeName == ObjectType.Type.Laundry) ? "Ln" : "Fl";
                        spriteBatch.Draw(batterybar, new Rectangle(50 + x_dist * 22, y_dist, 20, 20), iconColor);
                        spriteBatch.DrawString(font, iconTexture, new Vector2(50 + x_dist * 22 + 10, y_dist), Color.White, 0, new Vector2(font.MeasureString(iconTexture).X / 2, 0), 0.6f, SpriteEffects.None, 0);
                    }


                    //Remind Player of their objective
                    //spriteBatch.DrawString(font, level.objectiveMessage, new Vector2(SCREEN_WIDTH/2, 5), Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);

                    //string info = "[ESC] for Pause/Controls\n[R] to Restart";
                    //spriteBatch.DrawString(font, info, new Vector2(SCREEN_WIDTH/2, 2), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);
                    //spriteBatch.DrawString(font, "Time Remaining: " + ((int)timer).ToString(), new Vector2(SCREEN_WIDTH - 200, 2), Color.White, 0, new Vector2(0, 0), 0.9f, SpriteEffects.None, 0);


                

                    string info = "[ESC] for Pause/Controls | [R] to Restart";
                    spriteBatch.DrawString(font, info, new Vector2(SCREEN_WIDTH - 5, 22), Color.White, 0, new Vector2(font.MeasureString(info).X, 0), 0.6f, SpriteEffects.None, 0);
                    //spriteBatch.DrawString(font, "[R] to Restart", new Vector2(SCREEN_WIDTH - 5, 22), Color.White, 0, new Vector2(font.MeasureString("[R] to Restart").X, 0), 0.8f, SpriteEffects.None, 0);


                    //spriteBatch.DrawString(font, info, new Vector2(SCREEN_WIDTH/2, 5), Color.White, 0, new Vector2(0, 0), 0.8f, SpriteEffects.None, 0);
                    string hintTxt = (level.hint != null) ? level.hint : "";
                    spriteBatch.DrawString(font, hintTxt, new Vector2(SCREEN_WIDTH / 2, 2), Color.White, 0, new Vector2(0, 0), 0.6f, SpriteEffects.None, 0);

                    spriteBatch.End();

                    //Pause Screen
                    if (levelPaused) {
                        pauseScreen.Draw(spriteBatch, level.title, level.objectiveMessage, level.initialRain);
                    }

                    //Game Over Screen
                    if (levelHasEnded)
                    {
                        levelEnd.Draw(spriteBatch);
                    }
                    
                    //Game Start Screen
                    if (levelNotStarted)
                    {
                        levelStart.Draw(spriteBatch, level.title, level.objectiveMessage,level.initialRain);
                        //levelEnd.Draw(spriteBatch);
                    }

                    break;
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    spriteBatch.Draw(menu_background, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), Color.White);
                    spriteBatch.End();
                    mainMenu.Draw(spriteBatch);
                    break;
            }
            base.Draw(gameTime);

        }
    }
}
