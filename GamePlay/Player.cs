using AlhambraScoringAndroid.Attributes;
using AndroidBase.Tools;
using System;
using System.Linq;
using System.Reflection;
using static Android.Renderscripts.Sampler;
using static Android.Widget.ImageView;

namespace AlhambraScoringAndroid.GamePlay
{
    public class Player
    {
        private readonly Game Game;

        public const string DirkName = "Dirk";

        public string Name { get; private set; }
        public bool Dirk { get; private set; }

        public int Score => (ScoreDetails1 + ScoreDetails2 + ScoreDetails3 + ScoreMeantime).Sum;
        public ScoreDetails ScoreDetails1 { get; private set; }
        public ScoreDetails ScoreDetails2 { get; private set; }
        public ScoreDetails ScoreDetails3 { get; private set; }
        public ScoreDetails ScoreMeantime { get; private set; }
        private ScoreDetails CurrentScoreDetails => GetScoreDetails(Game.ScoreRound);

        protected Player(Game game, string name, bool dirk)
        {
            Game = game;

            Name = name;
            Dirk = dirk;

            ScoreDetails1 = new ScoreDetails();
            ScoreDetails2 = new ScoreDetails();
            ScoreDetails3 = new ScoreDetails();
            ScoreMeantime = new ScoreDetails();
        }

        public Player(Game game, string name) : this(game, name, false)
        {
        }

        public static Player CreateDirk(Game game)
        {
            return new Player(game, DirkName, true);
        }

        public ScoreDetails GetScoreDetails(ScoringRound round)
        {
            switch (round)
            {
                case ScoringRound.First:
                    return ScoreDetails1;
                case ScoringRound.Second:
                    return ScoreDetails2;
                case ScoringRound.ThirdBeforeLeftover:
                    return ScoreDetails3;
                case ScoringRound.Third:
                    return ScoreDetails3;
                case ScoringRound.Finish:
                    return ScoreDetails1 + ScoreDetails2 + ScoreDetails3 + ScoreMeantime;
                default:
                    throw new ArgumentException();
            }
        }

        public void AddScore(int score, ScoreType scoreType)
        {
            PropertyInfo field = typeof(ScoreDetails).GetProperties().Single(p => p.GetFieldAttribute<ScoreDetailsPointsAttribute>()?.ScoreType == scoreType);

            ScoreDetails scoreDetails = (scoreType == ScoreType.Immediately || scoreType == ScoreType.Starting) ? ScoreMeantime : CurrentScoreDetails;
            field.SetValue(scoreDetails, ((int)field.GetValue(scoreDetails)) + score);
        }
        //do Invaders, Medina, BuildingsWithoutServantTile przekazywane dodatnie
        //do Immediately przekazywane ujemne
        public void RemoveScore(int score, bool allowNegative, ScoreType scoreType)
        {
            //in case you may not have less than 0 points, but there is already
            if (!(!allowNegative && Score < 0))
            {
                int removedScore = score;
                if (Score < Math.Abs(removedScore) && !allowNegative)
                    removedScore = Score * Math.Sign(removedScore);

                AddScore(removedScore, scoreType);
            }
        }

        public void RevertAddScore(int score, ScoreType scoreType)
        {
            AddScore(-score, scoreType);
        }

        public void RestoreScore((ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime) initialScoring)
        {
            ScoreDetails1 = initialScoring.scoreDetails1;
            ScoreDetails2 = initialScoring.scoreDetails2;
            ScoreDetails3 = initialScoring.scoreDetails3;
            ScoreMeantime = initialScoring.scoreMeantime;
        }
    }
}