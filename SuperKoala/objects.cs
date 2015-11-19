using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;   
using System.Text;
using System.Windows;



namespace SuperKoule
{

    struct Vector
    {
        private double x;
        private double y;
        
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                this.x = value;
            }
        }
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                this.y = value;
            }
        }

        public Vector(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public static Vector operator+(Vector one, Vector two)
        {
            Vector result = new Vector();
            result.X = one.X + two.X;
            result.Y = one.Y + two.Y;
            return result;
        }
        public static Vector operator -(Vector one, Vector two)
        {
            Vector result = new Vector();
            result.X = one.X - two.X;
            result.Y = one.Y - two.Y;
            return result;
        }
        public static Vector operator +(Point one, Vector two)
        {
            Vector result = new Vector();
            result.X = one.X + two.X;
            result.Y = one.Y + two.Y;
            return result;
        }
        public static Vector operator *(double scalar, Vector vect)
        {
            Vector result = new Vector();
            result.X = vect.X * scalar;
            result.Y = vect.Y * scalar;
            return result;
        }

        public Point toPoint()
        {
            int ix = (int)Math.Round(X);
            int iy = (int)Math.Round(Y);
            Point result = new Point(ix, iy);
            return result;
        }

        public Vector Clamp(Vector min, Vector max)
        {
            double x;
            double y;
            x = Math.Min(this.X, max.X);
            x = Math.Max(x, min.X);
            y = Math.Min(this.Y, max.Y);
            y = Math.Max(y, min.Y);

            return new Vector(x,y);
        }
    } 
 


    enum typObjektu { platform, hero, hazard, decoration, empty, action, monsterStopper }
    enum sprite {basic, 
                 signRight, signLeft, exit, fence, brokenFence, cloud, plant, rock, snowHill, bush,  //decorations
                 tileHalf, tileRight, tileLeft, tileDirt, tileCenter, iceTile, iceTileRight, iceTileLeft, iceTileCenter,  //platforms
                 fly, blob, snail, spikes,//monsters
                 coin, coinGold, water, iceEffect, star, heart, monsterStopper, key, keyBlue, keyRed, keyGreen, door, doorTop
    };
    /**
     *  z index: 5 decoration, 10 basic, 15 action, 20 monster, 30 hero,
     * 
     **/
    /// <summary> 
    ///  Abstraktní třída, která reprezentuje všechny objekty ve hře. Jsou z ní odvozeny další třídy 
    /// </summary> 
    abstract class GameObject : IComparable
    {
        public bool visible = true;
        public bool destroy = false;
        public int z = 10;


        protected Vector gravity;
        protected Vector gravityStep;
        protected Double dt = ((double) Constants.INTERVAL) / 1000;
        
        public Rectangle hitArea;


        protected Point position;
        public    Point Position
        {
            get
            {
                return position;
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        protected Point desiredPos;
        public    Point DesiredPos
        {
            get
            {
                return desiredPos;
            }
            set
            {
                desiredPos = value;
                hitArea.X = value.X;
                hitArea.Y = value.Y;     
            }
        }

        protected Vector desiredPosVector;

        protected int x;
        protected int y;
        public int X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
                hitArea.X = value;
                this.x = value;
            }
        }
        public int Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
                hitArea.Y = value;
                this.y = value;
            }
        }

        public int width;
        public int height;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                hitArea.Width = value;
                this.y = value;
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                hitArea.Height= value;
            }
        }

        protected typObjektu type;
        public typObjektu Type() {
            return this.type;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return -1;

            GameObject other = obj as GameObject;
            if (other != null)
                return this.z.CompareTo(other.z);
            else
                throw new ArgumentException("Object is not a GameObject");
        }

        public void Init(int x, int y, int s, int v)
        {
            this.X = x;
            this.Y = y;
            Width = s;
            Height = v;

            this.dt = ((double) Constants.INTERVAL) / 1000;
            this.gravity = new Vector(0.0, Constants.GRAVITACE);
            this.gravityStep = dt * gravity;
        }
        public void initHitArea(int x, int y, int s, int v)
        {
            this.hitArea = new Rectangle(x, y, s, v);
        }


        public Point GetTileCoordinates()
        {
            int px = this.X + this.width / 2; //pozice stredu x
            int py = this.Y + this.height / 2;
            int x = px / Constants.GRID_SIZE_X;
            int y = py / Constants.GRID_SIZE_Y;

            Point tileCoords = new Point(x, y);
            return tileCoords;
        }

        abstract public void Contact(GameObject sender, d direction);
        abstract public void SetSpriteByType(sprite t);
        abstract public Bitmap Draw();
        abstract public void Action();
    }


    class Movable : GameObject
    {
        protected Bitmap jumpImg;
        protected Bitmap idleImg;
        protected Bitmap hurtImg;
        protected List<Bitmap> walkImages;
        protected int walkImgCounter = 0;
        protected int walkImgStep = 10;
        protected bool rotatedToLeft;


        protected Vector velocity = new Vector(0.0, 0.0);
        protected Vector acceleration = new Vector(0.0, 0.0);

        public bool onGround;
        public double friction;

        public void initMovable(int x, int y, int s, int v)
        {
            desiredPos.X = X;
            desiredPos.Y = Y;
            friction = Constants.FRICTION;
            onGround = false;
            z = 20;
        }

        public override void Action()
        {
        }
        public override void Contact(GameObject sender, d direction)
        {
        }
        public override void SetSpriteByType(sprite t)
        {
        }
        public override Bitmap Draw()
        {
            Bitmap img = new Bitmap(Properties.Resources.koala_idle);
            return img;
        }
    }


    class Hero : Movable
    {
        bool mightJump = false;
        bool kDown = false;
        bool runLeft = false;
        bool runRight = false;

        Vector forwardMove = new Vector(Constants.HERO_ACC, 0.0);
        Vector forwardMoveAir = new Vector(Constants.HERO_ACC_AIR, 0.0);
        Vector jumpForce = new Vector(0.0, Constants.JUMP_FORCE);

        Vector minMovement = new Vector(
            -Constants.HERO_TOP_SPEED_X,
             Constants.HERO_TOP_JUMP_SPEED);

        Vector maxMovement = new Vector(
             Constants.HERO_TOP_SPEED_X,
             Constants.HERO_TOP_FALL_SPEED);

        //List<Point> checkpoints = new List<Point>(); 
        //TODO checkpoints , action items s vlajkou

        private int protectedAfterHurt;
        private int hurtAnimationStep;

        public Hero(int x, int y, int s, int v) { 
            this.type = typObjektu.hero;

            idleImg = new Bitmap(Properties.Resources.koala_idle);
            jumpImg = new Bitmap(Properties.Resources.koala_jump);
            hurtImg = new Bitmap(Properties.Resources.koala_hurt);
            walkImages = new List<Bitmap>();
            walkImages.Add(new Bitmap(Properties.Resources.koala_walk01));
            walkImages.Add(new Bitmap(Properties.Resources.koala_walk02));
            walkImgStep = 5;

            protectedAfterHurt = 0;
            hurtAnimationStep = 5;
            /*idleImg = new Bitmap(Properties.Resources.p3_stand);
            jumpImg = new Bitmap(Properties.Resources.p3_jump);
            walkImages = new List<Bitmap>();
            walkImages.Add(new Bitmap(Properties.Resources.p3_walk04));
            walkImages.Add(new Bitmap(Properties.Resources.p3_walk05));
            walkImages.Add(new Bitmap(Properties.Resources.p3_walk06));
            walkImages.Add(new Bitmap(Properties.Resources.p3_walk07));
            walkImgStep = 5;*/

            Init(x, y, s, v);
            initHitArea(x + 2, y, s - 4, v);
            initMovable(x, y, s, v);

            z = 30;
        }
        public override void Action()
        {

            Vector forwardMoveStep;

            if (onGround)
            {
                velocity = new Vector(velocity.X*friction, 0); //friction  /0.0 - 1.0, coeficient
                forwardMoveStep = dt * forwardMove;
            }
            else
            {
                forwardMoveStep = dt * forwardMoveAir;
            }




            velocity = velocity + gravityStep;

            double jumpCutOff = Constants.JUMP_CUTOFF;

            if (mightJump && onGround)
            {
                velocity = velocity + jumpForce;
            }
            else if (!mightJump & velocity.Y < jumpCutOff) //player stops pressing up; jump is negative
            {
                velocity.Y = jumpCutOff;
            }

            if (runRight)
            {
                velocity = velocity + forwardMoveStep;
            }
            if (runLeft)
            {
                velocity = velocity - forwardMoveStep;
            }

            velocity = velocity.Clamp(minMovement,maxMovement);



            Vector stepVelocity = dt * velocity;
            if (stepVelocity.Y<0.6 & stepVelocity.Y>0)
            {
                stepVelocity.Y = 1;
            }
            desiredPosVector = position + stepVelocity;

            DesiredPos = desiredPosVector.toPoint();
            onGround = false;
            friction = Constants.FRICTION; 

        }
        public override void Contact(GameObject sender, d direction)
        {
            switch (sender.Type())
            {
                case typObjektu.platform:
                    if ((direction == d.right & velocity.X > 0) ||  (direction == d.left & velocity.X < 0))
                    {
                        velocity.X = 0;

                    }
                    

                    if (direction == d.up || direction == d.down)
                    {
                        velocity.Y = 0;

                    }
                    break;
                case typObjektu.action:
                    ActionItem action = (ActionItem) sender;
                    switch (action.action)
	                {
                        case a.scoreplus:
                            Sounds.coin.Play();
                            GameStatus.Score = GameStatus.Score + action.scorePoints;
                            break;
                        case a.water:
                            friction = Constants.FRICTION_WATER;
                            break;
                        case a.ice:
                            friction = Constants.FRICTION_ICE;
                            break;
                        case a.heart:
                            break;
                        case a.key:
                            Sounds.key.Play();
                            switch (action.color)
                            {
                                case c.red:
                                    GameStatus.redKey = true;
                                    break;
                                case c.green:
                                    GameStatus.greenKey = true;
                                    break;
                                case c.blue:
                                    GameStatus.blueKey = true;
                                    break;
                                case c.yellow:
                                    GameStatus.yellowKey = true;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case a.door:
                            if (action.opened)
                            {
                                GameStatus.winner = true;
                                GameStatus.gameOver = true;
                            }
                            break;
                        default:
                            break;
	                }
                    break;
                case typObjektu.hazard: //TODO from top
                    if (false)  //monster and from top
	                {
		 
	                } else {
                        if (protectedAfterHurt == 0)
                        {
                            
                            protectedAfterHurt = Constants.HERO_HURT_PROTECTIOIN / Constants.INTERVAL; ;
                            velocity.X = -velocity.X*0.3;
                            velocity.Y = Constants.JUMP_FORCE;
                            onGround = false;
                            GameStatus.Lives--;
                            //if (GameStatus.Lives > 0)
                            {
                                Sounds.playHurtSound();
                            }

                            //restartOnCheckpoint();
                        }

                    }

                    
                    break;
                default:
                    break;
            }
        }
        /*private void restartOnCheckpoint()
        {

        }*/
        public void keyDown(d key)
        {
            switch (key)
            {
                case d.up:
                     mightJump= true;
                    break;
                case d.down:
                    kDown = true;
                    break;
                case d.left:
                    runLeft = true;
                    break;
                case d.right:
                    runRight = true;
                    break;
                default:
                    break;
            }
        }
        public void keyUp(d key)
        {
          switch (key)
            {
                case d.up:
                    mightJump = false;
                    break;
                case d.down:
                    kDown = false;
                    break;
                case d.left:
                    runLeft = false;
                    break;
                case d.right:
                    runRight = false;
                    break;
                default:
                    break;
            }
        }
        public override Bitmap Draw()
        {
            Bitmap img;

            if (velocity.X > 1)
            {
                if (rotatedToLeft)
                {
                    hurtImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    idleImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    jumpImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    foreach (var bmp in walkImages)
                    {
                        bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }

                    rotatedToLeft = false;
                }
            }
            if (velocity.X < -1)
            {
                if (!rotatedToLeft)
                {
                    hurtImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    idleImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    jumpImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    foreach (var bmp in walkImages)
                    {
                        bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                    rotatedToLeft = true;
                }
            }

            if (!onGround)
            {
                img = jumpImg;
            } else
            if (onGround &(runLeft || runRight))
            {
                walkImgCounter++;
                walkImgCounter = walkImgCounter % (walkImgStep * walkImages.Count);
                img = walkImages[walkImgCounter / walkImgStep];

            }
            else
            {
                img = idleImg;
            }

            if (protectedAfterHurt > 0)
            {
                protectedAfterHurt = protectedAfterHurt - 1;
                if ((protectedAfterHurt / hurtAnimationStep) % 2 == 0)
                {
                    img = hurtImg;
                }
            }

            return img;

            /*Graphics g = Graphics.FromImage(img);
            Rectangle rect = new Rectangle( //hitArea test
                position.X - hitArea.X,
                position.Y - hitArea.Y,
                2*(hitArea.Width),
                2 * (hitArea.Height)
                );
            g.DrawRectangle(Pens.Red,rect);*/

        }

    }



    class Platform : GameObject
    {
        Bitmap spriteImg = new Bitmap(Properties.Resources.grass);

        public Platform(int x, int y, int sirka, int vyska)
        {
            this.X = x;
            this.Y = y;
            this.width = sirka;
            this.height = vyska;
            this.type = typObjektu.platform;
            Init(x, y, sirka, vyska);
        }

        public override void Contact(GameObject sender, d direction)
        {
        }
        public override void SetSpriteByType(sprite t)
        {
           switch (t)
            {
                case sprite.basic:
                    break;
                case sprite.tileHalf:
                    spriteImg = new Bitmap(Properties.Resources.grassHalf); 
                    break;
                case sprite.tileRight:
                    spriteImg = new Bitmap(Properties.Resources.grassRight); 
                    break;
                case sprite.tileLeft:
                    spriteImg = new Bitmap(Properties.Resources.grassLeft); 
                    break;
                case sprite.tileCenter:
                    spriteImg = new Bitmap(Properties.Resources.grassMid); 
                    break;
                case sprite.tileDirt:
                    spriteImg = new Bitmap(Properties.Resources.grassCenter);
                    break;
                case sprite.iceTile:
                    spriteImg = new Bitmap(Properties.Resources.snow);
                    break;
                case sprite.iceTileRight:
                    spriteImg = new Bitmap(Properties.Resources.snowCliffRight);
                    break;
                case sprite.iceTileLeft:
                    spriteImg = new Bitmap(Properties.Resources.snowCliffLeft);
                    break;
                case sprite.iceTileCenter:
                    spriteImg = new Bitmap(Properties.Resources.snowMid);
                    break;
                case sprite.water:
                    spriteImg = new Bitmap(Properties.Resources.liquidWaterTop_mid);
                    break;
                default:
                    break;
            }

        }
        public override Bitmap Draw()
        {
            return spriteImg;
        }
        public override void Action()
        {
        }
    }
    
    class EmptySpace : GameObject
    {
        public EmptySpace(int x, int y, int sirka, int vyska)
        {
            this.X = x;
            this.Y = y;
            this.width = 0;
            this.height = 0;
            this.type = typObjektu.empty;
            this.visible = false;
            Init(x, y, 0, 0);
        }

        public override void Contact(GameObject sender, d direction)
        {
        }

        public override void SetSpriteByType(sprite t)
        {
        }
        public override Bitmap Draw()
        {
            return null;
        }
        public override void Action()
        {
        }

    }
    class Decoration : GameObject
    {
        
        Bitmap spriteImg = null;
        public Decoration(int x, int y, int sirka, int vyska)
        {
            z = 5;
            this.X = x;
            this.Y = y;
            this.width = sirka;
            this.height = vyska;
            this.type = typObjektu.decoration;
            this.visible = true;
            Init(x, y, sirka, vyska);
            
        }

        public override void Contact(GameObject sender, d direction)
        {
        }

        public override void SetSpriteByType(sprite t)
        {
            switch (t)
            {
                case sprite.signRight:
                    this.spriteImg = new Bitmap(Properties.Resources.signRight);
                    z = 40; //in front of the hero
                    break;
                case sprite.signLeft:
                    this.spriteImg = new Bitmap(Properties.Resources.signLeft);
                    z = 40; 
                    break;
                case sprite.exit:
                    this.spriteImg = new Bitmap(Properties.Resources.signExit);
                    z = 40;
                    break;
                case sprite.fence:
                    this.spriteImg = new Bitmap(Properties.Resources.fence);

                    break;
                case sprite.brokenFence:
                     this.spriteImg = new Bitmap(Properties.Resources.fenceBroken);
                    break;
                case sprite.snowHill:
                    this.spriteImg = new Bitmap(Properties.Resources.snowhill);
                    break;
                case sprite.bush:
                    this.spriteImg = new Bitmap(Properties.Resources.bush);
                    X = X - 20;
                    Height = 70;
                    Y = Y - (70 - Constants.GRID_SIZE_Y);
                    
                    Width = 80;
                    break;
                case sprite.cloud:
                    spriteImg = new Bitmap(Properties.Resources.cloud1);
                    X = X - 20;
                    Y = Y - 10;
                    Height = 71;
                    Width = 128;
                    break;
                case sprite.plant:
                    spriteImg = new Bitmap(Properties.Resources.plant);
                    Height = 70;
                    Width = 70;
                    Y = Y - (70 - Constants.GRID_SIZE_Y);
                    z = 40;
                    break;
                case sprite.rock:
                    spriteImg = new Bitmap(Properties.Resources.rock);
                    Height = 70;
                    Width = 70;

                    Y = Y - (70 - Constants.GRID_SIZE_Y);
                    break;
                default:
                    spriteImg = new Bitmap(Properties.Resources.plant);
                    Height = 40;
                    Width = 30;
                    Y = Y - (40 - Constants.GRID_SIZE_Y);
                    break;
            }
        }
        public override Bitmap Draw()
        {
            return spriteImg;
        }
        public override void Action()
        {
        }

    }

    enum a { scoreplus, water, ice, heart, stop, key, door};
    enum c {red, green, blue, yellow, silver};
    class ActionItem : GameObject
    {
        Bitmap spriteImg;
        public a action = a.scoreplus;
        public int scorePoints;
        public c color = c.yellow;
        public bool opened;
        public ActionItem(int x, int y, int sirka, int vyska)
        {
            this.X = x;
            this.Y = y;
            this.width = sirka;
            this.height = vyska;
            this.type = typObjektu.action;
            z = 15;
            scorePoints = 1;
            opened = false;
            this.spriteImg = new Bitmap(Properties.Resources.coinSilver);
            Init(x, y, sirka, vyska);
        }

        public override void Contact(GameObject sender, d direction)
        {
            if (sender.Type() == typObjektu.hero)
            {
                if (action == a.scoreplus || action == a.key)
                {
                    destroy = true;
                    visible = false;
                    
                }
            }
        }

        public override void SetSpriteByType(sprite t)
        {
            switch (t)
            {
                case sprite.water:
                    spriteImg = new Bitmap(Properties.Resources.liquidWaterTop_mid);
                    action = a.water;
                    z = 40;
                    break;
                case sprite.iceEffect:
                    spriteImg = new Bitmap(Properties.Resources.ice_effect);
                    visible = false; //implement function for this
                    action = a.ice;
                    break;
                case sprite.monsterStopper:
                    this.spriteImg = new Bitmap(Properties.Resources.ice_effect);
                    action = a.stop;
                    break;
                case sprite.coin:
                    this.spriteImg = new Bitmap(Properties.Resources.coinSilver);
                    action = a.scoreplus;

                    break;
                case sprite.coinGold:
                    this.spriteImg = new Bitmap(Properties.Resources.coinGold);
                    action = a.scoreplus;
                    scorePoints = 2;
                    break;
                case sprite.star:
                    this.spriteImg = new Bitmap(Properties.Resources.star);
                    action = a.scoreplus;
                    scorePoints = 5;
                    break;
                case sprite.key:
                    this.spriteImg = new Bitmap(Properties.Resources.keyYellow);
                    action = a.key;

                    break;
                case sprite.keyBlue:
                    this.spriteImg = new Bitmap(Properties.Resources.keyBlue);
                    color = c.blue;
                    break;
                case sprite.keyRed:
                    this.spriteImg = new Bitmap(Properties.Resources.keyRed);
                    color = c.red;
                    break;
                case sprite.keyGreen:
                    this.spriteImg = new Bitmap(Properties.Resources.keyGreen);
                    color = c.green;
                    break;
                case sprite.door:
                    this.spriteImg = new Bitmap(Properties.Resources.door_closed);
                    action = a.door;

                    break;
                case sprite.doorTop:
                    this.spriteImg = new Bitmap(Properties.Resources.coinSilver);
                    action = a.scoreplus;

                    break;
                default:
                    break;
            }
        }
        public override Bitmap Draw()
        {
            return spriteImg;
        }
        public override void Action()
        {
            if (action == a.door )
            {
                if (GameStatus.redKey &
                    GameStatus.blueKey &
                    GameStatus.greenKey &
                    GameStatus.yellowKey)
                {
                    this.spriteImg = new Bitmap(Properties.Resources.door_opened);
                    opened = true;
                }
            }
        }

    }

    class Monster : Movable
    {

        Vector forwardMove;
        bool runLeft;
        bool applyGravity;

        public Monster(int x, int y, int s, int v) { 
            this.type = typObjektu.hazard;

            Init(x, y, s, v);
            initHitArea(x, y, s, v);
            initMovable(x, y, s, v);
            walkImages = new List<Bitmap>();
            runLeft = true;
            applyGravity = true;
            forwardMove = new Vector(450,0); //3 pixels in one dt

            z = 30;
        }

        public override void SetSpriteByType(sprite t)
        {
            switch (t)
            {
                case sprite.fly:
                    Y = Y + 20;
                    jumpImg = new Bitmap(Properties.Resources.flyFly1);
                    hurtImg = new Bitmap(Properties.Resources.flyDead);

                    walkImages.Add(new Bitmap(Properties.Resources.flyFly1));
                    walkImages.Add(new Bitmap(Properties.Resources.flyFly2));
                    applyGravity = false;
                    walkImgStep = 2;
                    forwardMove.X = 700;
                    break;
                case sprite.blob:
                    hurtImg = new Bitmap(Properties.Resources.slimeDead);
                    jumpImg = new Bitmap(Properties.Resources.slimeJump);

                    walkImages.Add(new Bitmap(Properties.Resources.slimeWalk1));
                    walkImages.Add(new Bitmap(Properties.Resources.slimeWalk2));
                    walkImgStep = 5;
                    break;
                case sprite.snail:
                    jumpImg = new Bitmap(Properties.Resources.snailShell);
                    hurtImg = new Bitmap(Properties.Resources.snailShell);
                    walkImages.Add(new Bitmap(Properties.Resources.snailWalk1));
                    walkImages.Add(new Bitmap(Properties.Resources.snailWalk2));
                    walkImgStep = 10;
                    forwardMove.X = 200;
                    break;
                case sprite.spikes:
                    jumpImg = new Bitmap(Properties.Resources.spikes);
                    idleImg = jumpImg;
                    walkImages.Add(jumpImg);
                    this.Width = Constants.GRID_SIZE_X;
                    forwardMove.X = 0;
                    break;

            }
        }
        public override void Action()
        {

            Vector forwardMoveStep;

            if (applyGravity)
            {
                velocity = velocity + gravityStep;
            }

            Vector stepVelocity = dt * velocity;
            if (stepVelocity.Y<0.6 & stepVelocity.Y>0)
            {
                stepVelocity.Y = 1;
            }
            desiredPosVector = position + stepVelocity;
            if (onGround || !applyGravity)
            {
                velocity = new Vector(velocity.X * friction, 0); //friction  /0.0 - 1.0, coeficient
                forwardMoveStep = dt * forwardMove;
            }
            else
            {
                forwardMoveStep = new Vector(0, 0);
            } 
             
            if (runLeft)
            {
                velocity = velocity - forwardMoveStep;
            }
            else
            {
                velocity = velocity + forwardMoveStep;
            }


            DesiredPos = desiredPosVector.toPoint();
            onGround = false;
            friction = Constants.FRICTION; 

        }

        public override void Contact(GameObject sender, d direction)
        {
            switch (sender.Type())
            {
                case typObjektu.platform:
                    if ((direction == d.right) || (direction == d.left))
                    {
                        runLeft = !runLeft;
                        velocity.X = 0;
                    }


                    if (direction == d.up || direction == d.down)
                    {
                        velocity.Y = 0;

                    }
                    break;
                case typObjektu.action:
                    ActionItem action = (ActionItem)sender;
                    if (action.action == a.stop)
                    {
                        runLeft = !runLeft;
                        X = velocity.X > 0 ? sender.X - Width : sender.X + sender.Width;
                        velocity.X = 0;

                    }

                    break;
            }
        }
        public override Bitmap Draw()
        {


            Bitmap img;

            if (velocity.X < -1)
            {
                if (rotatedToLeft)
                {
                    hurtImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    jumpImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    foreach (var bmp in walkImages)
                    {
                        bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }

                    rotatedToLeft = false;
                }
            }
            if (velocity.X > 1)
            {
                if (!rotatedToLeft)
                {
                    hurtImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    jumpImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    foreach (var bmp in walkImages)
                    {
                        bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                    rotatedToLeft = true;
                }
            }

            if (!onGround & applyGravity)
            {
                img = jumpImg;
            }
            else
            {
                walkImgCounter++;
                walkImgCounter = walkImgCounter % (walkImgStep * walkImages.Count);
                img = walkImages[walkImgCounter / walkImgStep];

            }

            return img;
        }

    }

    static class objects
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainProgram());


        }
    }
}
