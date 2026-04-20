using System;
using System.Xml.Serialization;

namespace AlhambraScoringAndroid.GamePlay
{
    public class ResultPlayerHistory
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        public ScoreDetails ScoreDetails1 { get; set; }
        public ScoreDetails ScoreDetails2 { get; set; }
        public ScoreDetails ScoreDetails3 { get; set; }
        public ScoreDetails ScoreMeantime { get; set; }

        public ScoreDetails GetScoreDetails(ScoringRound round)
        {
            return round switch
            {
                ScoringRound.First => ScoreDetails1,
                ScoringRound.Second => ScoreDetails2,
                ScoringRound.ThirdBeforeLeftover => ScoreDetails3,
                ScoringRound.Third => ScoreDetails3,
                ScoringRound.Finish => ScoreDetails1 + ScoreDetails2 + ScoreDetails3 + ScoreMeantime,
                _ => throw new ArgumentException(),
            };
        }
    }
}