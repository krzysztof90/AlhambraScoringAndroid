using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.GamePlay
{
    public class ResultHistory
    {
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public List<ExpansionModule> Modules { get; set; }
        public GranadaOption GranadaOption { get; set; }
        public List<CaliphsGuidelinesMission> CaliphsGuidelines { get; set; }
        public List<NewScoreCard> NewScoreCards { get; set; }
        public ScoringRound ScoreRound { get; set; }
        public List<ResultPlayerHistory> Players { get; set; }
    }
}