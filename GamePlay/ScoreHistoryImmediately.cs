namespace AlhambraScoringAndroid.GamePlay
{
    public class ScoreHistoryImmediately : ScoreHistory
    {
        public int PlayerNumber { get; private set; }
        public int Score { get; private set; }

        public ScoreHistoryImmediately(Game game, int playerNumber, int score) : base(game)
        {
            PlayerNumber = playerNumber;
            Score = score;
        }

        public override void Revert()
        {
            Game.PlayerRevertScore(PlayerNumber, Score);
        }

        protected override string DisplayName()
        {
            return $"Natychmiastowe punkty dla {Game.GetPlayer(PlayerNumber).Name}: {Score}";
        }
    }
}