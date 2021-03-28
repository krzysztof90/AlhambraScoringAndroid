using System;
using System.Globalization;

namespace AlhambraScoringAndroid.GamePlay
{
    public abstract class ScoreHistory
    {
        public DateTime Time { get; private set; }
        protected Game Game { get; private set; }

        public abstract void Revert();
        protected abstract string DisplayName();

        public ScoreHistory(Game game)
        {
            Game = game;
            Time = DateTime.Now;
        }

        public string FullDisplayName()
        {
            return $"{Time.ToString("HH:mm:ss", CultureInfo.CreateSpecificCulture("es-ES"))} {DisplayName()}";
        }
    }
}