using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AlhambraScoringAndroid.GamePlay
{
    public class ResultHistory
    {
        [XmlAttribute(AttributeName = "startTime")]
        public DateTime StartDateTime { get; set; }
        [XmlAttribute(AttributeName = "endTime")]
        public DateTime? EndDateTime { get; set; }
        public List<ExpansionModule> Modules { get; set; }
        [XmlAttribute(AttributeName = "granadaOption")]
        public GranadaOption GranadaOption { get; set; }
        public List<CaliphsGuidelinesMission> CaliphsGuidelines { get; set; }
        public List<NewScoreCard> NewScoreCards { get; set; }
        [XmlAttribute(AttributeName = "scoreRound")]
        public ScoringRound ScoreRound { get; set; }
        public List<ResultPlayerHistory> Players { get; set; }
    }
}