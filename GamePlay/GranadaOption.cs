using System.ComponentModel;

namespace AlhambraScoringAndroid.GamePlay
{
    public enum  GranadaOption
    {
        [Description("No Granada")]
        Without,
        [Description("Alhambra + Granada")]
        With,
        [Description("Only Granada")]
        Alone,
    }
}