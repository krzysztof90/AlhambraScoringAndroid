using System.Collections.Generic;

namespace AlhambraScoringAndroid.GamePlay
{
    public class RoundScoring
    {
        public List<PlayerScoreData> PlayersScoreData { get;  set; }

        public int GuardsPoints { get; set; }

        public RoundScoring()
        {
            PlayersScoreData = new List<PlayerScoreData>();
        }
    }
}