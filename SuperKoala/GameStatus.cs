using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperKoule
{
    static class GameStatus
    {
        public static int level = 0;
        public static bool gameOver = false;
        public static bool winner   = false;
        public static int lives = Constants.HERO_DEFAULT_LIVES;
        public static int Lives
        {
            get
            {
                return lives;
            }
            set
            {
                lives = Math.Max(value, 0);
            }
        }
        public static int score = 0;
        public static int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }
        public static int scoreInThisLevel = 0;
        public static int visibility = 0;

        public static bool redKey;
        public static bool blueKey;
        public static bool yellowKey;
        public static bool greenKey;

        public static void Init()
        {
            lives = Constants.HERO_DEFAULT_LIVES;
            score = 0;
            scoreInThisLevel = 0;
            level = 0;
            gameOver = false;
            winner = false;
            visibility = 0;
            redKey    = false;
            greenKey  = false;
            blueKey   = false;
            yellowKey = false;
        }
        public static void NextLevel()
        {
            level++;
            scoreInThisLevel = score;
            lives = Constants.HERO_DEFAULT_LIVES;
            gameOver = false;
            winner = false;
            visibility = 0;
            redKey = false;
            greenKey = false;
            blueKey = false;
            yellowKey = false;
        }
        public static void Retry()
        {
            score = scoreInThisLevel;
            lives = Constants.HERO_DEFAULT_LIVES;
            gameOver = false;
            winner = false;
            visibility = 0;
            redKey = false;
            greenKey = false;
            blueKey = false;
            yellowKey = false;
        }
    }
}
