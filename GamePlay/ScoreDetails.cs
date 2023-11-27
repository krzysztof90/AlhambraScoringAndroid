using AlhambraScoringAndroid.Attributes;
using AndroidBase.Tools;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace AlhambraScoringAndroid.GamePlay
{
    public class ScoreDetails
    {
        [ScoreDetailsPointsAttribute(ScoreType.Starting)]
        [XmlAttribute]
        public int StartingPoints { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Immediately)]
        [XmlAttribute]
        public int ImmediatelyPoints { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.WallLength)]
        [XmlAttribute]
        public int WallLength { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.PavilionNumber)]
        [XmlAttribute]
        public int Pavilion { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.SeraglioNumber)]
        [XmlAttribute]
        public int Seraglio { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.ArcadesNumber)]
        [XmlAttribute]
        public int Arcades { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.ChambersNumber)]
        [XmlAttribute]
        public int Chambers { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.GardenNumber)]
        [XmlAttribute]
        public int Garden { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.TowerNumber)]
        [XmlAttribute]
        public int Tower { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.BuildingsBonuses)]
        [XmlAttribute]
        public int BuildingsBonuses { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.TheCityWatch)]
        [XmlAttribute]
        public int TheCityWatch { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Camps)]
        [XmlAttribute]
        public int Camps { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.StreetTraders)]
        [XmlAttribute]
        public int StreetTraders { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.TreasureChamber)]
        [XmlAttribute]
        public int TreasureChamber { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Invaders)]
        [XmlAttribute]
        public int Invaders { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Bazaars)]
        [XmlAttribute]
        public int Bazaars { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.ArtOfTheMoors)]
        [XmlAttribute]
        public int ArtOfTheMoors { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Falconers)]
        [XmlAttribute]
        public int Falconers { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Watchtowers)]
        [XmlAttribute]
        public int Watchtowers { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Medina)]
        [XmlAttribute]
        public int Medina { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.BuildingsWithoutServantTile)]
        [XmlAttribute]
        public int BuildingsWithoutServantTile { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Orchards)]
        [XmlAttribute]
        public int Orchards { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Bathhouses)]
        [XmlAttribute]
        public int Bathhouses { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.WishingWells)]
        [XmlAttribute]
        public int WishingWells { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.CompletedProjects)]
        [XmlAttribute]
        public int CompletedProjects { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Animals)]
        [XmlAttribute]
        public int Animals { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.BlackDices)]
        [XmlAttribute]
        public int BlackDices { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Handymen)]
        [XmlAttribute]
        public int Handymen { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Treasures)]
        [XmlAttribute]
        public int Treasures { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Mission1)]
        [XmlAttribute]
        public int Mission1 { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Mission2)]
        [XmlAttribute]
        public int Mission2 { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Mission3)]
        [XmlAttribute]
        public int Mission3 { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Mission4)]
        [XmlAttribute]
        public int Mission4 { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Mission5)]
        [XmlAttribute]
        public int Mission5 { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Mission6)]
        [XmlAttribute]
        public int Mission6 { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Mission7)]
        [XmlAttribute]
        public int Mission7 { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Mission8)]
        [XmlAttribute]
        public int Mission8 { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Mission9)]
        [XmlAttribute]
        public int Mission9 { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Guards)]
        [XmlAttribute]
        public int Guards { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.MoatLength)]
        [XmlAttribute]
        public int MoatLength { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Arena)]
        [XmlAttribute]
        public int Arena { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.BathHouse)]
        [XmlAttribute]
        public int BathHouse { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Library)]
        [XmlAttribute]
        public int Library { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Hostel)]
        [XmlAttribute]
        public int Hostel { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Hospital)]
        [XmlAttribute]
        public int Hospital { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Market)]
        [XmlAttribute]
        public int Market { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.Park)]
        [XmlAttribute]
        public int Park { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.School)]
        [XmlAttribute]
        public int School { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.ResidentialArea)]
        [XmlAttribute]
        public int ResidentialArea { get; set; }
        [ScoreDetailsPointsAttribute(ScoreType.WallMoatCombination)]
        [XmlAttribute]
        public int WallMoatCombination { get; set; }

        public int Sum => StartingPoints + ImmediatelyPoints + WallLength + Pavilion + Seraglio + Arcades + Chambers + Garden + Tower + TheCityWatch + Camps + StreetTraders + TreasureChamber - Invaders + Bazaars + ArtOfTheMoors + Falconers + Watchtowers - Medina - BuildingsWithoutServantTile + Orchards + Bathhouses + WishingWells + CompletedProjects + Animals + BlackDices + Handymen + Treasures + Mission1 + Mission2 + Mission3 + Mission4 + Mission5 + Mission6 + Mission7 + Mission8 + Mission9 + Guards + MoatLength + Arena + BathHouse + Library + Hostel + Hospital + Market + Park + School + ResidentialArea + WallMoatCombination;

        public ScoreDetails Copy()
        {
            return this + new ScoreDetails();
        }

        public static ScoreDetails operator +(ScoreDetails scoreDetails1, ScoreDetails scoreDetails2)
        {
            ScoreDetails scoreDetails = new ScoreDetails();

            foreach (PropertyInfo field in typeof(ScoreDetails).GetProperties().Where(p => p.GetFieldAttribute<ScoreDetailsPointsAttribute>() != null))
                field.SetValue(scoreDetails, ((int)field.GetValue(scoreDetails1)) + ((int)field.GetValue(scoreDetails2)));

            return scoreDetails;
        }
    }
}