namespace AlhambraScoringAndroid.GamePlay
{
    public class ScoreDetails
    {
        public int ImmediatelyPoints { get; set; }
        public int WallLength { get; set; }
        public int PavilionNumber { get; set; }
        public int SeraglioNumber { get; set; }
        public int ArcadesNumber { get; set; }
        public int ChambersNumber { get; set; }
        public int GardenNumber { get; set; }
        public int TowerNumber { get; set; }
        public int BuildingsBonuses { get; set; }
        public int BuildingsWithoutServantTile { get; set; }
        public int Orchards { get; set; }
        public int Bathhouses { get; set; }
        public int WishingWells { get; set; }
        public int CompletedProjects { get; set; }
        public int Animals { get; set; }
        public int BlackDices { get; set; }
        public int Handymen { get; set; }
        public int Treasures { get; set; }
        public int Mission1 { get; set; }
        public int Mission2 { get; set; }
        public int Mission3 { get; set; }
        public int Mission4 { get; set; }
        public int Mission5 { get; set; }
        public int Mission6 { get; set; }
        public int Mission7 { get; set; }
        public int Mission8 { get; set; }
        public int Mission9 { get; set; }

        public int Sum => ImmediatelyPoints + WallLength + PavilionNumber + SeraglioNumber + ArcadesNumber + ChambersNumber + GardenNumber + TowerNumber - BuildingsWithoutServantTile + Orchards + Bathhouses + WishingWells + CompletedProjects + Animals + BlackDices + Handymen + Treasures + Mission1 + Mission2 + Mission3 + Mission4 + Mission5 + Mission6 + Mission7 + Mission8 + Mission9;

        public ScoreDetails Copy()
        {
            return this + new ScoreDetails();
        }

        public static ScoreDetails operator +(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2)
        {
            ScoreDetails scoreDetails = new ScoreDetails();
            scoreDetails.ImmediatelyPoints = scoreDetails1.ImmediatelyPoints + scoreDetails2.ImmediatelyPoints;
            scoreDetails.WallLength = scoreDetails1.WallLength + scoreDetails2.WallLength;
            scoreDetails.PavilionNumber = scoreDetails1.PavilionNumber + scoreDetails2.PavilionNumber;
            scoreDetails.SeraglioNumber = scoreDetails1.SeraglioNumber + scoreDetails2.SeraglioNumber;
            scoreDetails.ArcadesNumber = scoreDetails1.ArcadesNumber + scoreDetails2.ArcadesNumber;
            scoreDetails.ChambersNumber = scoreDetails1.ChambersNumber + scoreDetails2.ChambersNumber;
            scoreDetails.GardenNumber = scoreDetails1.GardenNumber + scoreDetails2.GardenNumber;
            scoreDetails.TowerNumber = scoreDetails1.TowerNumber + scoreDetails2.TowerNumber;
            scoreDetails.BuildingsBonuses = scoreDetails1.BuildingsBonuses + scoreDetails2.BuildingsBonuses;
            scoreDetails.BuildingsWithoutServantTile = scoreDetails1.BuildingsWithoutServantTile + scoreDetails2.BuildingsWithoutServantTile;
            scoreDetails.Orchards = scoreDetails1.Orchards + scoreDetails2.Orchards;
            scoreDetails.Bathhouses = scoreDetails1.Bathhouses + scoreDetails2.Bathhouses;
            scoreDetails.WishingWells = scoreDetails1.WishingWells + scoreDetails2.WishingWells;
            scoreDetails.CompletedProjects = scoreDetails1.CompletedProjects + scoreDetails2.CompletedProjects;
            scoreDetails.Animals = scoreDetails1.Animals + scoreDetails2.Animals;
            scoreDetails.BlackDices = scoreDetails1.BlackDices + scoreDetails2.BlackDices;
            scoreDetails.Handymen = scoreDetails1.Handymen + scoreDetails2.Handymen;
            scoreDetails.Treasures = scoreDetails1.Treasures + scoreDetails2.Treasures;
            scoreDetails.Mission1 = scoreDetails1.Mission1 + scoreDetails2.Mission1;
            scoreDetails.Mission2 = scoreDetails1.Mission2 + scoreDetails2.Mission2;
            scoreDetails.Mission3 = scoreDetails1.Mission3 + scoreDetails2.Mission3;
            scoreDetails.Mission4 = scoreDetails1.Mission4 + scoreDetails2.Mission4;
            scoreDetails.Mission5 = scoreDetails1.Mission5 + scoreDetails2.Mission5;
            scoreDetails.Mission6 = scoreDetails1.Mission6 + scoreDetails2.Mission6;
            scoreDetails.Mission7 = scoreDetails1.Mission7 + scoreDetails2.Mission7;
            scoreDetails.Mission8 = scoreDetails1.Mission8 + scoreDetails2.Mission8;
            scoreDetails.Mission9 = scoreDetails1.Mission9 + scoreDetails2.Mission9;
            return scoreDetails;
        }
    }
}