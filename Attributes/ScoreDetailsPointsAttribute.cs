using AlhambraScoringAndroid.GamePlay;
using System;

namespace AlhambraScoringAndroid.Attributes
{
    public class ScoreDetailsPointsAttribute : Attribute
    {
        public ScoreType ScoreType { get; set; }

        public ScoreDetailsPointsAttribute(ScoreType scoreType)
        {
            ScoreType = scoreType;
        }
    }
}