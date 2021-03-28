using System.ComponentModel;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum ScoringRound
    {
        [Description("Pierwsza")]
        First,
        [Description("Druga")]
        Second,
        [Description("Trzecia przed czymś tam")]
        ThirdBeforeLeftover,
        [Description("Trzecia")]
        Third,
        [Description("Koniec")]
        Finish
    }
}