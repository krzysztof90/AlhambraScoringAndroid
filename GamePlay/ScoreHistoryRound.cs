using AlhambraScoringAndroid.Tools;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.GamePlay
{
    public class ScoreHistoryRound: ScoreHistory
    {
        public ScoringRound ScoreRound { get; private set; }
        public List<(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime)> InitialScoring { get; private set; }

        public ScoreHistoryRound(Game game, ScoringRound scoreRound, List<(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2, ScoreDetails scoreDetails3, ScoreDetails scoreMeantime)> initialScoring) : base(game)
        {
            ScoreRound = scoreRound;
            InitialScoring = initialScoring;
        }

        public override void Revert()
        {
            Game.RoundRevertScore(ScoreRound, InitialScoring);
        }

        protected override string DisplayName()
        {
            return $"Punktowanie rundy {ScoreRound.GetEnumDescription()}";
        }
    }
}