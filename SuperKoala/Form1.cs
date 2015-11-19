using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
//using System.Diagnostics; //TODO jen pro debug

namespace SuperKoule
{
    enum d { up = 0, right = 1, down = 2, left = 3 } //direction

    public partial class MainProgram : Form {

        Map mapa;
        Timer gameTicker;
        Graphics gCanvas;
        Bitmap frame;
        Hero hero;

        Point cPos = new Point(0, 0); //canvas position

        Point maxPos;

        bool pause;

        //hud numbers
        Bitmap hud_zero = new Bitmap(Properties.Resources.hud_0);
        Bitmap hud_one = new Bitmap(Properties.Resources.hud_1);
        Bitmap hud_two = new Bitmap(Properties.Resources.hud_2);
        Bitmap hud_three = new Bitmap(Properties.Resources.hud_3);
        Bitmap hud_four = new Bitmap(Properties.Resources.hud_4);
        Bitmap hud_five = new Bitmap(Properties.Resources.hud_5);
        Bitmap hud_six = new Bitmap(Properties.Resources.hud_6);
        Bitmap hud_seven = new Bitmap(Properties.Resources.hud_7);
        Bitmap hud_eight = new Bitmap(Properties.Resources.hud_8);
        Bitmap hud_nine = new Bitmap(Properties.Resources.hud_9);

        Bitmap hud_coins = new Bitmap(Properties.Resources.hud_coins);
        Bitmap hud_heart = new Bitmap(Properties.Resources.hud_heartFull);
        Bitmap hud_heart_empty = new Bitmap(Properties.Resources.hud_heartEmpty);

        Bitmap hud_key_blue = new Bitmap(Properties.Resources.hud_keyBlue);
        Bitmap hud_key_red = new Bitmap(Properties.Resources.hud_keyRed);
        Bitmap hud_key_green = new Bitmap(Properties.Resources.hud_keyGreen);
        Bitmap hud_key_yellow = new Bitmap(Properties.Resources.hud_keyYellow);

        Bitmap hud_key_blue_disabled = new Bitmap(Properties.Resources.hud_keyBlue_disabled);
        Bitmap hud_key_red_disabled = new Bitmap(Properties.Resources.hud_keyRed_disabled);     
        Bitmap hud_key_green_disabled = new Bitmap(Properties.Resources.hud_keyGreen_disabled);    
        Bitmap hud_key_yellow_disabled = new Bitmap(Properties.Resources.hud_keyYellow_disabled);

        string[] levels;
    
        public MainProgram()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);

            pbGameCanvas.Width = this.Width;
            pbGameCanvas.Height = this.Height;
            pbGameCanvas.BackColor = Color.FromArgb(208, 244, 247);

            Constants.canvasWidth = this.Width;
            Constants.canvasHeight = this.Height;

            levels = new string[] {global::SuperKoule.Properties.Resources.map111,
                                   global::SuperKoule.Properties.Resources.map11,/**/
                                   global::SuperKoule.Properties.Resources.map2,
                                   global::SuperKoule.Properties.Resources.map3};

            //controls
            var pos = this.PointToScreen(newgame_btn.Location);
            pos = pbGameCanvas.PointToClient(pos);
            newgame_btn.Parent = pbGameCanvas;
            newgame_btn.Location = pos;
            newgame_btn.BackColor = Color.Transparent;

            pos = this.PointToScreen(replay_btn.Location);
            pos = pbGameCanvas.PointToClient(pos);
            replay_btn.Parent = pbGameCanvas;
            replay_btn.Location = pos;
            replay_btn.BackColor = Color.Transparent;

            pos = this.PointToScreen(pause_btn.Location);
            pos = pbGameCanvas.PointToClient(pos);
            pause_btn.Parent = pbGameCanvas;
            pause_btn.Location = pos;
            pause_btn.BackColor = Color.Transparent;

            pos = this.PointToScreen(congrat_btn.Location);
            pos = pbGameCanvas.PointToClient(pos);
            congrat_btn.Parent = pbGameCanvas;
            congrat_btn.Location = pos;
            congrat_btn.BackColor = Color.Transparent;
            congrat_btn.Visible = false;

            pos = this.PointToScreen(superkoala_header.Location);
            pos = pbGameCanvas.PointToClient(pos);
            superkoala_header.Parent = pbGameCanvas;
            superkoala_header.Location = pos;
            superkoala_header.BackColor = Color.Transparent;

            pos = this.PointToScreen(next_level_btn.Location);
            pos = pbGameCanvas.PointToClient(pos);
            next_level_btn.Parent = pbGameCanvas;
            next_level_btn.Location = pos;
            next_level_btn.BackColor = Color.Transparent;
            next_level_btn.Visible = false;

            pos = this.PointToScreen(retry_btn.Location);
            pos = pbGameCanvas.PointToClient(pos);
            retry_btn.Parent = pbGameCanvas;
            retry_btn.Location = pos;
            retry_btn.BackColor = Color.Transparent;
            retry_btn.Visible = false;

            pause_btn.Location = new Point(pbGameCanvas.Width - 20 - pause_btn.Width, 10);
            replay_btn.Location = new Point(pbGameCanvas.Width - 20 - pause_btn.Width - 15 - replay_btn.Width, 11);
            congrat_btn.Location = new Point(pbGameCanvas.Width / 2 - congrat_btn.Width / 2, pbGameCanvas.Height / 5);
            newgame_btn.Location = new Point(pbGameCanvas.Width / 2 - newgame_btn.Width / 2, pbGameCanvas.Height / 2 - newgame_btn.Height / 2);
            superkoala_header.Location = new Point(pbGameCanvas.Width / 2 - superkoala_header.Width / 2, pbGameCanvas.Height / 5);
            next_level_btn.Location = new Point(pbGameCanvas.Width / 2 - next_level_btn.Width / 2, pbGameCanvas.Height / 2 - next_level_btn.Height / 2);
            retry_btn.Location = new Point(pbGameCanvas.Width / 2 - retry_btn.Width / 2, pbGameCanvas.Height / 2 - retry_btn.Height / 2);
        }

        private void Pause()
        {
            pause = true;
            gameTicker.Stop();

        }

        private void Play()
        {
            pause = false;
            gameTicker.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            { 
                hero.keyDown(d.up); };
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) {
                hero.keyDown(d.left); };
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) {
                hero.keyDown(d.down); };
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                hero.keyDown(d.right);

            };

        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) { hero.keyUp(d.up); };
            if (e.KeyCode == Keys.A) { hero.keyUp(d.left); };
            if (e.KeyCode == Keys.S) { hero.keyUp(d.down); };
            if (e.KeyCode == Keys.D) { hero.keyUp(d.right); };


        }
        private void hideControls()
        {
            newgame_btn.Visible = false;
            congrat_btn.Visible = false;
            superkoala_header.Visible = false;
            next_level_btn.Visible = false;
            retry_btn.Visible = false;
        }
        private void Newgame(object sender, EventArgs e)
        {
            GameStatus.Init();

            hideControls();

            mapInit();
           
        }

        private void Nextlevel(object sender, EventArgs e)
        {
            GameStatus.NextLevel();

            hideControls();

            mapInit();
        }
        private void Retry(object sender, EventArgs e)
        {
            GameStatus.Retry();

            hideControls();

            mapInit();
        }

        private void mapInit()
        {
            MapLoader allmaps = new MapLoader(levels[GameStatus.level]);

            frame = new Bitmap(pbGameCanvas.Width, pbGameCanvas.Height);
            gCanvas = Graphics.FromImage(frame);
            pbGameCanvas.Image = frame;

            hero = new Hero(100, 0, 35, 50);
            mapa = new Map();

            mapa.AddObject(hero);

            while (allmaps.ObjectsLeft())
            {
                mapa.AddObject(allmaps.GetNextObject());
            }

            maxPos.Y = Constants.MAP_PIXEL_HEIGHT;
            if (gameTicker != null)
            {
                gameTicker.Dispose();

            }

            gameTicker = new Timer();

            gameTicker.Interval = Constants.INTERVAL;
            gameTicker.Tick += new EventHandler(herniCasTick);
            gameTicker.Start();       
        }
        private void herniCasTick(object sender, EventArgs e)
        {
            if (hero.Y > maxPos.Y)
            {
                GameStatus.gameOver = true;
            }

            if (!GameStatus.gameOver)
            {
                /*Stopwatch sw = new Stopwatch(); //debug

                sw.Start();*/

                mapa.MakeActions();
                mapa.FindCollisions();
                DrawAll();

                if (GameStatus.Lives == 0)
                {
                    GameStatus.gameOver = true;
                }

                /*sw.Stop(); //debug

                Console.WriteLine("Elapsed={0}", sw.ElapsedMilliseconds);*/
            }
            else
            {
                if (GameStatus.winner)
                {
                    makeWinner();
                }
                else {
                    makeGameOver();
                }
                
            }

        }

        private void disposeUnused()
        {
            if (gameTicker != null)
            {
                gameTicker.Dispose();
            }
            if (gCanvas != null)
            {
                gCanvas.Dispose();
            }
            
            
        }

        private void makeWinner()
        {
            GameStatus.gameOver = true;
            congrat_btn.Visible = true;
            if (GameStatus.level == levels.Length - 1)
            {
                absoluteWinner();
                superkoala_header.Visible = true;
                congrat_btn.Visible = false;
                newgame_btn.Visible = true;
                
            }
            else
            {
                next_level_btn.Visible = true;
            }
            gameTicker.Stop();
            Bitmap bmp = (Bitmap)pbGameCanvas.Image;
            bmp = saturate(bmp);


            using(Graphics g = Graphics.FromImage(bmp))
            {
            
              g.FillRectangle(

                  new SolidBrush(Color.FromArgb(0,139,164)),
                  new Rectangle(pbGameCanvas.Width/2 - 200,0,400,pbGameCanvas.Height));

              int score = GameStatus.Score;
              int order = 0;
              g.DrawImage(
                  hud_coins,
                  Constants.canvasWidth / 2 + 35, pbGameCanvas.Height / 5 + congrat_btn.Height + 50
                  );
              do
              {

                  g.DrawImage(
                      getNumberImg(score % 10),
                      Constants.canvasWidth / 2 - 50 - 35 * order, pbGameCanvas.Height / 5 + congrat_btn.Height + 55
                      );
                  score = score / 10;
                  order++;
              } while (score != 0);

            }

            pbGameCanvas.Image = bmp;
            Sounds.applaus.Play();
            //disposeUnused();
        }

        private void absoluteWinner()
        {
            //TODO winn all levels
        }
        private void makeGameOver()
        {
            GameStatus.gameOver = true;
            retry_btn.Visible = true;
            gameTicker.Stop();
            Bitmap bmp = (Bitmap)pbGameCanvas.Image;
            bmp = grayish(bmp);
            pbGameCanvas.Image = bmp;
            Sounds.dead.Play();

            //disposeUnused();
        }

        public static Bitmap saturate(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix; EDITED, only grayish
            float c = 1.3f;
            float t = (1.0f - c) / 2.0f;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] 
              {
                 new float[] {c, 0, 0, 0, 0},
                 new float[] {0, c, 0, 0, 0},
                 new float[] {0, 0, c, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {t, t, t, 0, 1}
              });


            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
        public static Bitmap grayish(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix; EDITED, only grayish
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] 
              {
         new float[] {.40f*1.5f, .15f, .15f, 0, 0},
         new float[] {.30f, .50f*1.3f, .30f, 0, 0},
         new float[] {.05f, .05f, .20f*1.7f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
              });


            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
        private void DrawGrid(Graphics g)
        {
            for (int iy = 0; iy < Constants.NbrOfTilesY; iy++)
            {
                g.DrawLine(
                    Pens.Black,
                    0, iy * Constants.GRID_SIZE_Y,
                    Constants.canvasWidth, iy * Constants.GRID_SIZE_Y);

            }
            for (int ix = 0; ix < Constants.canvasWidth / Constants.GRID_SIZE_X; ix++)
            {
                g.DrawLine(
                    Pens.Black,
                    ix * Constants.GRID_SIZE_X, 0,
                    ix * Constants.GRID_SIZE_X, Constants.canvasHeight);
            }
        }

        private Bitmap getNumberImg(int number)
        {
            switch (number)
            {   
                case 1:
                    return hud_one;
                case 2:
                    return hud_two;
                case 3:
                    return hud_three;
                case 4:
                    return hud_four;
                case 5:
                    return hud_five;
                case 6:
                    return hud_six;
                case 7:
                    return hud_seven;
                case 8:
                    return hud_eight;
                case 9:
                    return hud_nine;
                default:
                    return hud_zero;
            }
        }

        private Bitmap getLiveHeart(int number)
        {
            return GameStatus.lives >= number ? hud_heart : hud_heart_empty;
        }



        private void drawHUD(Graphics g)
        {
            //score
            int score = GameStatus.Score;
            int order = 0;
            g.DrawImage(
                hud_coins,
                Constants.canvasWidth - 205, 10
                );
            do
            {

                g.DrawImage(
                    getNumberImg(score % 10),
                    Constants.canvasWidth - 240 - 35 * order, 15
                    );
                score = score / 10;
                order++;
            } while (score != 0);


            //lives
            for (int i = 0; i < Constants.HERO_DEFAULT_LIVES; i++)
            {
                g.DrawImage(
                getLiveHeart(i + 1),
                20 + 60 * i, 10
                );
            }

            //keys
            int space = 70;
                g.DrawImage( //yellow
                    GameStatus.yellowKey ? hud_key_yellow : hud_key_yellow_disabled,
                    pbGameCanvas.Width/2 - space*2, 13
                    );
                g.DrawImage( //red
                    GameStatus.redKey ? hud_key_red : hud_key_red_disabled,
                    pbGameCanvas.Width / 2 - space, 13
                    );
                g.DrawImage( //blue
                    GameStatus.blueKey ? hud_key_blue : hud_key_blue_disabled,
                    pbGameCanvas.Width / 2, 13
                    );
                g.DrawImage( //green
                    GameStatus.greenKey ? hud_key_green : hud_key_green_disabled,
                    pbGameCanvas.Width / 2 +space, 13
                    );
        }
        public void DrawAll()
        {
            gCanvas.Clear(Color.FromArgb(208, 244,247));

            //DrawGrid(gCanvas);

            cPos.X = hero.X - Constants.canvasWidth / 2;
            cPos.Y = hero.Y - Constants.canvasHeight / 2;

            GameObject objToDisplay = mapa.getNextObj();
            while (objToDisplay != null)
            {

                if (objToDisplay.X - cPos.X + objToDisplay.Width > GameStatus.visibility &
                    objToDisplay.X - cPos.X < Constants.canvasWidth - GameStatus.visibility &
                    objToDisplay.Y - cPos.Y + objToDisplay.Height > GameStatus.visibility &
                    objToDisplay.Y - cPos.Y < Constants.canvasHeight - GameStatus.visibility/2)
                {
                    gCanvas.DrawImage(
                        objToDisplay.Draw(),
                        new Rectangle(
                            objToDisplay.X - cPos.X,
                            objToDisplay.Y - cPos.Y,
                            objToDisplay.Width,
                            objToDisplay.Height)); //TODO pretecce kdyz postavicka moc dlouho pada
                }



                objToDisplay = mapa.getNextObj();
            }

            drawHUD(gCanvas);

            pbGameCanvas.Image = frame;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void resize_game(object sender, EventArgs e)
        {
            pbGameCanvas.Width = this.Width;
            pbGameCanvas.Height = this.Height;

            Constants.canvasWidth = this.Width;
            Constants.canvasHeight = this.Height;
            maxPos.X = pbGameCanvas.Width;
            maxPos.Y = pbGameCanvas.Height;

            pause_btn.Location = new Point(pbGameCanvas.Width - 20 - pause_btn.Width, 10);
            replay_btn.Location = new Point(pbGameCanvas.Width - 20 - pause_btn.Width - 15 - replay_btn.Width, 11);
            congrat_btn.Location = new Point(pbGameCanvas.Width / 2 - congrat_btn.Width / 2, pbGameCanvas.Height / 5);
            newgame_btn.Location = new Point(pbGameCanvas.Width / 2 - newgame_btn.Width / 2, pbGameCanvas.Height / 2 - newgame_btn.Height / 2);
            superkoala_header.Location = new Point(pbGameCanvas.Width / 2 - superkoala_header.Width / 2, pbGameCanvas.Height / 5);
            next_level_btn.Location = new Point(pbGameCanvas.Width / 2 - next_level_btn.Width / 2, pbGameCanvas.Height / 2 - next_level_btn.Height / 2);
            retry_btn.Location = new Point(pbGameCanvas.Width / 2 - retry_btn.Width / 2, pbGameCanvas.Height / 2 - retry_btn.Height / 2);

            frame = new Bitmap(pbGameCanvas.Width, pbGameCanvas.Height);
            gCanvas = Graphics.FromImage(frame);
            if (pause || GameStatus.gameOver)
            {
                DrawAll();
            }
            
            pbGameCanvas.Image = frame;
        }

        private void pause_btn_Click(object sender, EventArgs e)
        {
            if (pause)
            {
                Play();
                pause_btn.BackgroundImage = Properties.Resources.hud_pause;
            }
            else
            {
                Pause();
                pause_btn.BackgroundImage = Properties.Resources.hud_play;
            }
        }

        private void Nextlevel()
        {

        }

    }


}
