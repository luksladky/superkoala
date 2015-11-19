using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperKoule
{
    public static class Constants
    {
        public static int MAP_PIXEL_HEIGHT = 600;
        public const int MAP_PIXEL_WIDTH = 3000;

        public const int INTERVAL = 30;

        public const double GRAVITACE = 1000;
        public const double HERO_ACC = 2000;
        public const double HERO_ACC_AIR = 400;
        public const double JUMP_FORCE = -510;
        public const double JUMP_CUTOFF = -250;

        
        public const double HERO_TOP_SPEED_X = 300.0;
        public const double HERO_TOP_FALL_SPEED = 400.0;
        public const double HERO_TOP_JUMP_SPEED = -450.0;

        public const int HERO_DEFAULT_LIVES = 5;
        public const int HERO_HURT_PROTECTIOIN = 1500;

        public const double FRICTION = 0.7;
        public const double FRICTION_WATER = 0.48;
        public const double FRICTION_ICE = 0.98;
        public const int MAX_OBJECTS_NUMBER = 100;

        public const int GRID_SIZE_X = 40;
        public const int GRID_SIZE_Y = 40;
        /*static Constants()
        {
            public const double DT =  INTERVAL/1000;
        }*/
        public static int canvasWidth = 1000;
        public static int canvasHeight = 700;

        public static int nbrOfTilesX = 1;
        public static int NbrOfTilesX
        {
            get
            {
                return nbrOfTilesX;
            }
            set
            {
                nbrOfTilesX = value;
            }
        }
        public static int nbrOfTilesY = 1;
        public static int NbrOfTilesY
        {
            get
            {
                return nbrOfTilesY;
            }
            set
            {
                nbrOfTilesY = value;
                MAP_PIXEL_HEIGHT = value*GRID_SIZE_Y + 100;
            }
        }


    }
}
