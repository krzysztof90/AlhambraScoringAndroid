using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.GamePlay
{
    public class ResultHistory
    {
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public List<ExpansionModule> Modules { get; set; }
        public ScoringRound ScoreRound { get; set; }
        public List<ResultPlayerHistory> Players { get; set; }
    }
}