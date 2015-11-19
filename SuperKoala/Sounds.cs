using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SuperKoule
{
    static class Sounds
    {
        public static System.Media.SoundPlayer coin = new System.Media.SoundPlayer(Properties.Resources.coin_sound);
        public static System.Media.SoundPlayer key = new System.Media.SoundPlayer(Properties.Resources.key_sound);
        public static System.Media.SoundPlayer hurt = new System.Media.SoundPlayer(Properties.Resources.hurt_sound);
        public static System.Media.SoundPlayer dead = new System.Media.SoundPlayer(Properties.Resources.dead_sound);
        public static System.Media.SoundPlayer applaus = new System.Media.SoundPlayer(Properties.Resources.applaus_sound);

        public static void Init()
        {
            coin.LoadAsync();
            key.LoadAsync();
            hurt.LoadAsync();
            dead.LoadAsync();
            applaus.LoadAsync();
        }

        public static void playHurtSound()
        {
            coin.Stop();
            key.Stop();
            hurt.Play();
        }
    }
}
