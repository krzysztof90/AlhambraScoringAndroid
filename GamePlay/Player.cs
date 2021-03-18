using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid.GamePlay
{
    public class Player
    {
        public  const String DirkName = "Dirk";

        public String Name;
        public bool Dirk;

        public int Score;

        protected Player(String name, bool dirk)
        {
            Name = name;
            Dirk = dirk;
            Score = 0;
        }

        public Player(String name): this(name, false)
        {
        }

        public static Player CreateDirk()
        {
            return new Player(DirkName, true);
        }

        public void AddScore(int score)
        {
            Score += score;
        }
        public void RemoveScore(int score)
        {
            Score -= score;
            if (Score < 0)
                Score = 0;
        }
    }

}