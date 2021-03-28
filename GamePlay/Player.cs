using System;

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
            switch (scoreType)
            {
                case ScoreType.Immediately:
                ScoreMeantime.ImmediatelyPoints += score;
                    break;
                case ScoreType.WallLength:
                    CurrentScoreDetails.WallLength += score;
                    break;
                case ScoreType.PavilionNumber:
                    CurrentScoreDetails.PavilionNumber += score;
                    break;
                case ScoreType.SeraglioNumber:
                    CurrentScoreDetails.SeraglioNumber += score;
                    break;
                case ScoreType.ArcadesNumber:
                    CurrentScoreDetails.ArcadesNumber += score;
                    break;
                case ScoreType.ChambersNumber:
                    CurrentScoreDetails.ChambersNumber += score;
                    break;
                case ScoreType.GardenNumber:
                    CurrentScoreDetails.GardenNumber += score;
                    break;
                case ScoreType.TowerNumber:
                    CurrentScoreDetails.TowerNumber += score;
                    break;
                case ScoreType.BuildingsBonuses:
                    CurrentScoreDetails.BuildingsBonuses += score;
                    break;
                case ScoreType.Orchards:
                    CurrentScoreDetails.Orchards += score;
                    break;
                case ScoreType.Bathhouses:
                    CurrentScoreDetails.Bathhouses += score;
                    break;
                case ScoreType.WishingWells:
                    CurrentScoreDetails.WishingWells += score;
                    break;
                case ScoreType.CompletedProjects:
                    CurrentScoreDetails.CompletedProjects += score;
                    break;
                case ScoreType.Animals:
                    CurrentScoreDetails.Animals += score;
                    break;
                case ScoreType.BlackDices:
                    CurrentScoreDetails.BlackDices += score;
                    break;
                case ScoreType.Handymen:
                    CurrentScoreDetails.Handymen += score;
                    break;
                case ScoreType.Treasures:
                    CurrentScoreDetails.Treasures += score;
                    break;
                case ScoreType.Mission1:
                    CurrentScoreDetails.Mission1 += score;
                    break;
                case ScoreType.Mission2:
                    CurrentScoreDetails.Mission2 += score;
                    break;
                case ScoreType.Mission3:
                    CurrentScoreDetails.Mission3 += score;
                    break;
                case ScoreType.Mission4:
                    CurrentScoreDetails.Mission4 += score;
                    break;
                case ScoreType.Mission5:
                    CurrentScoreDetails.Mission5 += score;
                    break;
                case ScoreType.Mission6:
                    CurrentScoreDetails.Mission6 += score;
                    break;
                case ScoreType.Mission7:
                    CurrentScoreDetails.Mission7 += score;
                    break;
                case ScoreType.Mission8:
                    CurrentScoreDetails.Mission8 += score;
                    break;
                case ScoreType.Mission9:
                    CurrentScoreDetails.Mission9 += score;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public void RemoveScore(int score, ScoreType scoreType)
        {
            int removedScore = score;
            if (Score < removedScore)
                removedScore = Score;

            switch (scoreType)
            {
                case ScoreType.BuildingsWithoutServantTile:
                    CurrentScoreDetails.BuildingsWithoutServantTile += removedScore;
                    break;
                default:
                    throw new ArgumentException();
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