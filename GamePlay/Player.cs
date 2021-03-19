using System;

namespace AlhambraScoringAndroid.GamePlay
{
    public class Player
    {
        public const string DirkName = "Dirk";

        public string Name { get; private set; }
        public bool Dirk { get; private set; }

        public int Score { get; private set; }

        protected Player(string name, bool dirk)
        {
            Name = name;
            Dirk = dirk;
            Score = 0;
        }

        public Player(string name) : this(name, false)
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