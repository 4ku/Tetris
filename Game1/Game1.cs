using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
   

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture;
        Texture2D background;
        Texture2D cell;
        Vector2 posoftable;
        Vector2 PosOb;
        Vector2 PosNext=new Vector2(350,150);
        float rotation = 0f;
        SpriteFont font = null;
         int WIDTH = 540;
        int HEIGHT = 720;
        int WaHcell = 20;
        int CellNumW = 10;
        int CellNumH = 20;
        bool[,] table;
        Random RandomInt = new Random();
        Objects activOb;
        float delta = 450f;
        float deltapres = 60f;
        float deltapresU = 100f;
        float   timeFrame = 0f;
        float timepr = 0f;
        float timeprU = 0f;
        bool GameOver = false;
        int delrows = 0;
        int Type = 0;
        int curType;
        int[] nextOb;
  
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //Texture2D texture = Content.Load(texture); 
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
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 760;
           // graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            posoftable = new Vector2(100, 100);
            table = new bool[CellNumH, CellNumW];
            nextOb = new int[3];
            for(int i = 0; i < 3; i++)
            {
                int t = RandomInt.Next(1, 8);
                while (t == Type)
                {
                    t = RandomInt.Next(1, 8);
                }
                Type = t;
                nextOb[i] = t;
            }
            
            newOb();

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
            texture = Content.Load<Texture2D>("ACBF_PC");
            font = Content.Load<SpriteFont>("font");
            background = Content.Load<Texture2D>("tet");
            cell = Content.Load<Texture2D>("cell");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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

            /* float delta = (float)gameTime.ElapsedGameTime.Milliseconds;
             if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                 Exit();
             if (Keyboard.GetState().IsKeyDown(Keys.Q))
             {
                 rotation += 0.001f * delta;
             }
             MouseState ms = Mouse.GetState();
             if(ms.LeftButton == ButtonState.Pressed)
             {
                 rotation -= 0.001f * delta;
             }*/
            //this.Window.Title = ms.Position.ToString();
            //this.Window.Title = ms.ScrollWheelValue.ToString();
            // TODO: Add your update logic here
           // Mouse.GetState().
            if ( Keyboard.GetState().IsKeyDown(Keys.Escape)||GameOver) Exit();
            float k = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            timepr += k; timeprU += k;
            if (timeprU >= deltapresU)
            {
                timeprU = 0f;
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    bool[,] nar=new bool[activOb.n,activOb.m];
                     Array.Copy(activOb.ar, nar, activOb.n*activOb.m);
                    activOb.ar = new bool[activOb.m, activOb.n];
                  
                    int i0 = 0; int j0 = 0;

                    for (int j = 0; j < activOb.m; j++)
                    {
                        for (int i = activOb.n - 1; i >= 0; i--)
                        {
                            activOb.ar[i0, j0] = nar[i, j];
                            if (j0 + 1 != activOb.n) j0++;
                            else
                            {
                                j0 = 0;i0++;
                            }
                        }
                    }
                    int t = activOb.m; activOb.m = activOb.n; activOb.n = t;

                    
                        bool noright = true;
                        for (int i = activOb.n - 1; i >= 0; i--)
                        {
                            for (int j = 0; j < activOb.m; j++)
                            {
                                if (activOb.ar[i, j] == true && ((int)PosOb.Y + j >= CellNumW))
                                {
                                    noright = false; goto Oll;
                                }
                            }
                        }
                      Oll: if (!noright) PosOb.Y -= 1;


                    bool noleft = true;
                    for (int i = activOb.n - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < activOb.m; j++)
                        {
                            if (activOb.ar[i, j] == true && ((int)PosOb.Y + j < 0) )
                            {
                                noleft = false; goto Oll1;
                            }
                        }
                    }
                Oll1: if (!noleft) PosOb.Y += 1;

                }
            }
            if (timepr >= deltapres)
            {
                timepr = 0f;
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    bool noright = true;
                    for (int i = activOb.n - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < activOb.m; j++)
                        {
                            if (activOb.ar[i, j] == true && (((int)PosOb.Y + j + 1 >= CellNumW) || (table[(int)PosOb.X + i, (int)PosOb.Y + j + 1] == true)))
                            {
                                noright = false; goto Oll;
                            }
                        }
                    }
                Oll: if (noright) PosOb.Y += 1;
                }
                
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    bool noleft = true;
                    for (int i = activOb.n - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < activOb.m; j++)
                        {
                            if (activOb.ar[i, j] == true && (((int)PosOb.Y + j - 1 < 0) || (table[(int)PosOb.X + i, (int)PosOb.Y + j - 1] == true)))
                            {
                                noleft = false; goto Oll1;
                            }
                        }
                    }
                Oll1: if (noleft) PosOb.Y -= 1;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    Down();
                }
            }
            // System.Threading.Thread.Sleep(200);

            timeFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeFrame > delta )
            {
                Down();
            }

            if(!GameOver)Window.Title = delrows.ToString();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); 
            spriteBatch.Begin();
            for (int i=0; i < CellNumH; i++)
            {
                for (int j = 0; j < CellNumW; j++) {
                    //    spriteBatch.Draw(cell, new Vector2(i*WaHcell+100, j*WaHcell+100), new Rectangle(0, 0, 10, 10), Color.Black, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(cell, new Vector2(j * WaHcell, i * WaHcell) + posoftable, new Rectangle(0, 0, WaHcell, WaHcell), Color.White);
                    if (table[i, j]) spriteBatch.Draw(cell, new Vector2(j * WaHcell, i * WaHcell) + posoftable, new Rectangle(0, 0, WaHcell, WaHcell), Color.Black);
                }
            }

            //    spriteBatch.Draw(background, new Rectangle(0, 0, WIDTH, HEIGHT), new Rectangle(0, 0, background.Width, background.Height), Color.White);
            //  spriteBatch.Draw(texture,new  Vector2(300,300),new Rectangle(0,0,texture.Width,texture.Height), Color.White,rotation,new Vector2(texture.Width, texture.Height),1f,SpriteEffects.None, 0f);

            // TODO: Add your drawing code here

            for (int i = 0; i < activOb.n; i++)
            {
                for (int j = 0; j < activOb.m; j++)
                {
                    if(activOb.ar[i,j])
                    spriteBatch.Draw(cell, new Vector2((j + (int)PosOb.Y) * WaHcell, (i + (int)PosOb.X) * WaHcell) + posoftable, new Rectangle(0, 0, WaHcell, WaHcell), Color.Black);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                Objects next = Objects.getObj(nextOb[i]);
                for (int k = 0; k < next.n; k++)
                {
                    for (int j = 0; j < next.m; j++)
                    {
                        if (next.ar[k, j])
                            spriteBatch.Draw(cell, new Vector2((j) * WaHcell, (k) * WaHcell) + PosNext, new Rectangle(0, 0, WaHcell, WaHcell), Color.Black);
                    }
                }
                PosNext += new Vector2(0, 50);

            }
            PosNext= new Vector2(350, 150);
            spriteBatch.DrawString(font, "Hello World!", Vector2.Zero, Color.Yellow);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void newOb()
        {
            activOb = Objects.getObj(nextOb[0]);
            curType = nextOb[0];
            int t = RandomInt.Next(1, 8);
            while (t == Type)
            {
                t= RandomInt.Next(1, 8);
            }
            Type = t;
            nextOb[0] = nextOb[1];nextOb[1] = nextOb[2];nextOb[2] = t;
            
            PosOb = new Vector2(0, 3);
            for(int i = 0; i < activOb.n;i++)
            {
                for(int j = 0; j < activOb.m;j++)
                {
                    //if(activOb.ar[i,j]) table[(int)PosOb.X + i,(int) PosOb.Y+j]= true;
                    if (activOb.ar[i,j] == true && table[(int)PosOb.X + i, (int)PosOb.Y + j] == true)
                    {
                        GameOver = true;
                        goto iop;
                    }
                }
            }
        iop: int k;
        }




        public bool noDown()
        {
            bool noOb = false;
            timeFrame = 0f;

            for (int i = activOb.n - 1; i >= 0; i--)
            {
                for (int j = 0; j < activOb.m; j++)
                {
                    if (activOb.ar[i, j] == true && (((int)PosOb.X + i + 1 >= CellNumH) || (table[(int)PosOb.X + i + 1, (int)PosOb.Y + j] == true)))
                    {
                        noOb = true; goto Olol;
                    }
                }
            }
            Olol: 
            return noOb;
        }
        public void Down()
        {
           

        
            if (!noDown()) PosOb.X += 1;
            else
            {
                for (int i = 0; i < activOb.n; i++)
                {
                    for (int j = 0; j < activOb.m; j++)
                    {
                        if ((int)PosOb.X + i < CellNumH && activOb.ar[i, j]) table[(int)PosOb.X + i, (int)PosOb.Y + j] = true;
                    }
                }
                for(int i=(int)PosOb.X;i< Math.Min((int)PosOb.X+activOb.n,CellNumH); i++)
                {
                    if (check(i))
                    {
                        removerow(i); delrows++;
                    }
                }
                newOb();
            }
        }

        public bool check(int row)
        {
            bool f = true;

                for (int j = 0; j <CellNumW; j++)
                {
                    if (table[row, j] == false)
                    {
                    f = false;
                        break;
                    }   
            }
                    return f;
        }
        public void removerow(int row)
        {
            for(int i= row; i > 0; i--)
            {
                for (int j = 0; j < CellNumW; j++)
                {
                    table[i, j] = table[i - 1, j];
                }
            }
            for (int j = 0; j < CellNumW; j++) table[0, j] = false;

        }




    }
}
