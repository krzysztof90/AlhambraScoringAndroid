using System;

namespace AlhambraScoringAndroid.GamePlay
{
    public class ResultPlayerHistory
    {
        public string Name { get; set; }
        public ScoreDetails ScoreDetails1 { get; set; }
        public ScoreDetails ScoreDetails2 { get; set; }
        public ScoreDetails ScoreDetails3 { get; set; }
        public ScoreDetails ScoreMeantime { get; set; }

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
    }
}