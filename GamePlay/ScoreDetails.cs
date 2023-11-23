using System.Xml.Serialization;

namespace AlhambraScoringAndroid.GamePlay
{
    public class ScoreDetails
    {
        [XmlAttribute]
        public int ImmediatelyPoints { get; set; }
        [XmlAttribute]
        public int WallLength { get; set; }
        [XmlAttribute]
        public int Pavilion { get; set; }
        [XmlAttribute]
        public int Seraglio { get; set; }
        [XmlAttribute]
        public int Arcades { get; set; }
        [XmlAttribute]
        public int Chambers { get; set; }
        [XmlAttribute]
        public int Garden { get; set; }
        [XmlAttribute]
        public int Tower { get; set; }
        [XmlAttribute]
        public int BuildingsBonuses { get; set; }
        [XmlAttribute]
        public int TheCityWatch { get; set; }
        [XmlAttribute]
        public int Camps { get; set; }
        [XmlAttribute]
        public int StreetTraders { get; set; }
        [XmlAttribute]
        public int TreasureChamber { get; set; }
        [XmlAttribute]
        public int Invaders { get; set; }
        [XmlAttribute]
        public int Bazaars { get; set; }
        [XmlAttribute]
        public int ArtOfTheMoors { get; set; }
        [XmlAttribute]
        public int Falconers { get; set; }
        [XmlAttribute]
        public int Watchtowers { get; set; }
        [XmlAttribute]
        public int Medina { get; set; }
        [XmlAttribute]
        public int BuildingsWithoutServantTile { get; set; }
        [XmlAttribute]
        public int Orchards { get; set; }
        [XmlAttribute]
        public int Bathhouses { get; set; }
        [XmlAttribute]
        public int WishingWells { get; set; }
        [XmlAttribute]
        public int CompletedProjects { get; set; }
        [XmlAttribute]
        public int Animals { get; set; }
        [XmlAttribute]
        public int BlackDices { get; set; }
        [XmlAttribute]
        public int Handymen { get; set; }
        [XmlAttribute]
        public int Treasures { get; set; }
        [XmlAttribute]
        public int Mission1 { get; set; }
        [XmlAttribute]
        public int Mission2 { get; set; }
        [XmlAttribute]
        public int Mission3 { get; set; }
        [XmlAttribute]
        public int Mission4 { get; set; }
        [XmlAttribute]
        public int Mission5 { get; set; }
        [XmlAttribute]
        public int Mission6 { get; set; }
        [XmlAttribute]
        public int Mission7 { get; set; }
        [XmlAttribute]
        public int Mission8 { get; set; }
        [XmlAttribute]
        public int Mission9 { get; set; }
        [XmlAttribute]
        public int MoatLength { get; set; }
        [XmlAttribute]
        public int Arena { get; set; }
        [XmlAttribute]
        public int BathHouse { get; set; }
        [XmlAttribute]
        public int Library { get; set; }
        [XmlAttribute]
        public int Hostel { get; set; }
        [XmlAttribute]
        public int Hospital { get; set; }
        [XmlAttribute]
        public int Market { get; set; }
        [XmlAttribute]
        public int Park { get; set; }
        [XmlAttribute]
        public int School { get; set; }
        [XmlAttribute]
        public int ResidentialArea { get; set; }
        [XmlAttribute]
        public int WallMoatCombination { get; set; }

        public int Sum => ImmediatelyPoints + WallLength + Pavilion + Seraglio + Arcades + Chambers + Garden + Tower + TheCityWatch + Camps + StreetTraders + TreasureChamber - Invaders + Bazaars + ArtOfTheMoors + Falconers + Watchtowers - Medina - BuildingsWithoutServantTile + Orchards + Bathhouses + WishingWells + CompletedProjects + Animals + BlackDices + Handymen + Treasures + Mission1 + Mission2 + Mission3 + Mission4 + Mission5 + Mission6 + Mission7 + Mission8 + Mission9 + MoatLength + Arena + BathHouse + Library + Hostel + Hospital + Market + Park + School + ResidentialArea + WallMoatCombination;

        public ScoreDetails Copy()
        {
            return this + new ScoreDetails();
        }

        public static ScoreDetails operator +(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2)
        {
            ScoreDetails scoreDetails = new ScoreDetails();
            scoreDetails.ImmediatelyPoints = scoreDetails1.ImmediatelyPoints + scoreDetails2.ImmediatelyPoints;
            scoreDetails.WallLength = scoreDetails1.WallLength + scoreDetails2.WallLength;
            scoreDetails.Pavilion = scoreDetails1.Pavilion + scoreDetails2.Pavilion;
            scoreDetails.Seraglio = scoreDetails1.Seraglio + scoreDetails2.Seraglio;
            scoreDetails.Arcades = scoreDetails1.Arcades + scoreDetails2.Arcades;
            scoreDetails.Chambers = scoreDetails1.Chambers + scoreDetails2.Chambers;
            scoreDetails.Garden = scoreDetails1.Garden + scoreDetails2.Garden;
            scoreDetails.Tower = scoreDetails1.Tower + scoreDetails2.Tower;
            scoreDetails.BuildingsBonuses = scoreDetails1.BuildingsBonuses + scoreDetails2.BuildingsBonuses;
            scoreDetails.TheCityWatch = scoreDetails1.TheCityWatch + scoreDetails2.TheCityWatch;
            scoreDetails.Camps = scoreDetails1.Camps + scoreDetails2.Camps;
            scoreDetails.StreetTraders = scoreDetails1.StreetTraders + scoreDetails2.StreetTraders;
            scoreDetails.TreasureChamber = scoreDetails1.TreasureChamber + scoreDetails2.TreasureChamber;
            scoreDetails.Invaders = scoreDetails1.Invaders + scoreDetails2.Invaders;
            scoreDetails.Bazaars = scoreDetails1.Bazaars + scoreDetails2.Bazaars;
            scoreDetails.ArtOfTheMoors = scoreDetails1.ArtOfTheMoors + scoreDetails2.ArtOfTheMoors;
            scoreDetails.Falconers = scoreDetails1.Falconers + scoreDetails2.Falconers;
            scoreDetails.Watchtowers = scoreDetails1.Watchtowers + scoreDetails2.Watchtowers;
            scoreDetails.Medina = scoreDetails1.Medina + scoreDetails2.Medina;
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
            scoreDetails.MoatLength = scoreDetails1.MoatLength + scoreDetails2.MoatLength;
            scoreDetails.Arena = scoreDetails1.Arena + scoreDetails2.Arena;
            scoreDetails.BathHouse = scoreDetails1.BathHouse + scoreDetails2.BathHouse;
            scoreDetails.Library = scoreDetails1.Library + scoreDetails2.Library;
            scoreDetails.Hostel = scoreDetails1.Hostel + scoreDetails2.Hostel;
            scoreDetails.Hospital = scoreDetails1.Hospital + scoreDetails2.Hospital;
            scoreDetails.Market = scoreDetails1.Market + scoreDetails2.Market;
            scoreDetails.Park = scoreDetails1.Park + scoreDetails2.Park;
            scoreDetails.School = scoreDetails1.School + scoreDetails2.School;
            scoreDetails.ResidentialArea = scoreDetails1.ResidentialArea + scoreDetails2.ResidentialArea;
            scoreDetails.WallMoatCombination = scoreDetails1.WallMoatCombination + scoreDetails2.WallMoatCombination;
            return scoreDetails;
        }
    }
}