using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using AndroidBase.UI;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI.Activities
{
    //TODO instrukcja przygotowania rund. Instrukcja o czym pamiętać w trakcie gry
    [Activity(Label = "@string/setup", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameSetupInstructionActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_game_setup;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            List<SetupInstructions> setupTiles = new List<SetupInstructions>();
            List<SetupInstructions> setupCards = new List<SetupInstructions>();
            List<SetupInstructions> setupOther = new List<SetupInstructions>();

            AddSetupInstruction(setupTiles, SetupInstructions.PutBuildingsOfPowerTiles, Game.HasModule(AlhambraBase.ExpansionModule.DesignerBuildingsOfPower));
            AddSetupInstruction(setupTiles, SetupInstructions.PutCampTiles, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionCamps));
            AddSetupInstruction(setupTiles, SetupInstructions.PutBazaarsTiles, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionBazaars));
            AddSetupInstruction(setupTiles, SetupInstructions.PutMagicalBuildingsTiles, Game.HasModule(AlhambraBase.ExpansionModule.QueenieMagicalBuildings));
            AddSetupInstruction(setupTiles, SetupInstructions.PutMedinaTiles, Game.HasModule(AlhambraBase.ExpansionModule.QueenieMedina));
            AddSetupInstruction(setupTiles, SetupInstructions.PutNewBuildingGroundsTiles, Game.HasModule(AlhambraBase.ExpansionModule.DesignerNewBuildingGrounds));
            AddSetupInstruction(setupTiles, SetupInstructions.PutBathhouseTiles, Game.HasModule(AlhambraBase.ExpansionModule.DesignerBathhouses));
            AddSetupInstruction(setupTiles, SetupInstructions.PutWishingWellTiles, Game.HasModule(AlhambraBase.ExpansionModule.DesignerWishingWell));
            AddSetupInstruction(setupTiles, SetupInstructions.ShuffleBuildingTiles, Game.GranadaOption != GranadaOption.Alone);
            AddSetupInstruction(setupTiles, SetupInstructions.GranadaShuffleBuildingTiles, Game.GranadaOption != GranadaOption.Without);
            AddSetupInstruction(setupTiles, SetupInstructions.DealTrader, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionStreetTrader));

            AddSetupInstruction(setupTiles, SetupInstructions.PlaceBuildings, Game.GranadaOption != GranadaOption.Alone);
            AddSetupInstruction(setupTiles, SetupInstructions.PlaceBuildingsPreviewBoard, Game.HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles));
            AddSetupInstruction(setupTiles, SetupInstructions.GiveBuildingsToDirk, Game.InvolvedDirk);
            AddSetupInstruction(setupTiles, SetupInstructions.GranadaPlaceBuildings, Game.GranadaOption != GranadaOption.Without);

            AddSetupInstruction(setupCards, SetupInstructions.RemoveCardDeck, Game.InvolvedDirk);
            AddSetupInstruction(setupCards, SetupInstructions.PutDiamondCards, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionDiamonds));
            AddSetupInstruction(setupCards, SetupInstructions.ShuffleCards);
            AddSetupInstruction(setupCards, SetupInstructions.DealMoney, !Game.HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles));
            AddSetupInstruction(setupCards, SetupInstructions.ChoseMoney, Game.HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles));
            AddSetupInstruction(setupCards, SetupInstructions.PlaceCards);
            AddSetupInstruction(setupCards, SetupInstructions.DivideCards);
            AddSetupInstruction(setupCards, SetupInstructions.Put2ScoringCards, Game.GranadaOption != GranadaOption.With);
            AddSetupInstruction(setupCards, SetupInstructions.Put1ScoringCardMiddle, Game.GranadaOption == GranadaOption.With);
            AddSetupInstruction(setupCards, SetupInstructions.PutCurrencyExchangeCards, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionCurrencyExchangeCards));
            AddSetupInstruction(setupCards, SetupInstructions.PutCityGatesCards, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionCityGates));
            AddSetupInstruction(setupCards, SetupInstructions.PutCharacters, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionCharacters));
            AddSetupInstruction(setupCards, SetupInstructions.PutCityWalls, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionCityWalls));
            AddSetupInstruction(setupCards, SetupInstructions.PutMasterBuilders, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionMasterBuilders) && Game.PlayersCount != 6);
            AddSetupInstruction(setupCards, SetupInstructions.PutMasterBuilders6, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionMasterBuilders) && Game.PlayersCount == 6);
            AddSetupInstruction(setupCards, SetupInstructions.PutPowerOfSultan, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionPowerOfSultan));
            AddSetupInstruction(setupCards, SetupInstructions.JoinPiles);

            AddSetupInstruction(setupOther, SetupInstructions.ShuffleSquares, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionSquares));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleWatchtowers, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionWatchtowers));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleMajorConstructionProjects, Game.HasModule(AlhambraBase.ExpansionModule.DesignerMajorConstructionProjects));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceGateBoard, Game.HasModule(AlhambraBase.ExpansionModule.DesignerGatesWithoutEnd));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleExtensions, Game.HasModule(AlhambraBase.ExpansionModule.DesignerExtensions));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceRedPalaceComponents, Game.HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles));

            AddSetupInstruction(setupOther, SetupInstructions.DealVizier, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionViziersFavour));
            AddSetupInstruction(setupOther, SetupInstructions.DealBonusCards3, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionBonusCards) && Game.PlayersCount == 3);
            AddSetupInstruction(setupOther, SetupInstructions.DealBonusCards2, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionBonusCards) && (Game.PlayersCount == 4 || Game.PlayersCount == 5));
            AddSetupInstruction(setupOther, SetupInstructions.DealBonusCards1, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionBonusCards) && Game.PlayersCount == 6);
            AddSetupInstruction(setupOther, SetupInstructions.PlaceCityGates, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionCityGates));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceCityWalls, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionCityWalls));
            AddSetupInstruction(setupOther, SetupInstructions.DealThieves4, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionThieves) && Game.PlayersCount == 3);
            AddSetupInstruction(setupOther, SetupInstructions.DealThieves3, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionThieves) && Game.PlayersCount == 4);
            AddSetupInstruction(setupOther, SetupInstructions.DealThieves2, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionThieves) && (Game.PlayersCount == 5 || Game.PlayersCount == 6));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceChange, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionChange));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceTrader, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionStreetTrader));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceTraderTiles, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionStreetTrader));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceTreasureChamber, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionTreasureChamber));
            AddSetupInstruction(setupOther, SetupInstructions.DealMasterBuilders, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionMasterBuilders));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleInvasion, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionInvaders));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleScout, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionInvaders));
            AddSetupInstruction(setupOther, SetupInstructions.PlacePowerOfSultan, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionPowerOfSultan));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleCaravanserai, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionCaravanserai));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceCaravanserai, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionCaravanserai));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceArtOfTheMoors, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionArtOfTheMoors));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceFalcons, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionFalconers));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleBuildingSites, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionBuildingSites));
            AddSetupInstruction(setupOther, SetupInstructions.DealExchangeCertificate, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionExchangeCertificates));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceExchangeCertificate1, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionExchangeCertificates) && Game.PlayersCountWithoutDirk == 2);
            AddSetupInstruction(setupOther, SetupInstructions.PlaceExchangeCertificate2, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionExchangeCertificates) && (Game.PlayersCountWithoutDirk == 3 || Game.PlayersCount == 4));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceExchangeCertificate3, Game.HasModule(AlhambraBase.ExpansionModule.ExpansionExchangeCertificates) && (Game.PlayersCount == 5 || Game.PlayersCount == 6));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceMagicalBuildings, Game.HasModule(AlhambraBase.ExpansionModule.QueenieMagicalBuildings));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceNewBuildingGrounds, Game.HasModule(AlhambraBase.ExpansionModule.DesignerNewBuildingGrounds));
            AddSetupInstruction(setupOther, SetupInstructions.DealMajorConstructionMarker, Game.HasModule(AlhambraBase.ExpansionModule.DesignerMajorConstructionProjects));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleServantTiles, Game.HasModule(AlhambraBase.ExpansionModule.DesignerPalaceStaff));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleOrchardsBoards, Game.HasModule(AlhambraBase.ExpansionModule.DesignerOrchards));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleOrchardsFruits, Game.HasModule(AlhambraBase.ExpansionModule.DesignerOrchards));
            AddSetupInstruction(setupOther, SetupInstructions.RemoveCraftsmen5, Game.HasModule(AlhambraBase.ExpansionModule.DesignerTravellingCraftsmen) && (Game.PlayersCount == 3 || Game.PlayersCount == 4));
            AddSetupInstruction(setupOther, SetupInstructions.RemoveCraftsmen6, Game.HasModule(AlhambraBase.ExpansionModule.DesignerTravellingCraftsmen) && (Game.PlayersCount == 5 || Game.PlayersCount == 6));
            AddSetupInstruction(setupOther, SetupInstructions.DealCraftsmen, Game.HasModule(AlhambraBase.ExpansionModule.DesignerTravellingCraftsmen));
            AddSetupInstruction(setupOther, SetupInstructions.RemoveProjectTiles, Game.HasModule(AlhambraBase.ExpansionModule.DesignerFreshColors) && Game.PlayersCount == 3);
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleProjectTiles, Game.HasModule(AlhambraBase.ExpansionModule.DesignerFreshColors));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceColorTiles, Game.HasModule(AlhambraBase.ExpansionModule.DesignerFreshColors));
            AddSetupInstruction(setupOther, SetupInstructions.DealPalaceDesigners, Game.HasModule(AlhambraBase.ExpansionModule.DesignerPalaceDesigners));
            AddSetupInstruction(setupOther, SetupInstructions.PlacePalaceDesigners, Game.HasModule(AlhambraBase.ExpansionModule.DesignerPalaceDesigners));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleAnimals, Game.HasModule(AlhambraBase.ExpansionModule.DesignerAlhambraZoo));
            AddSetupInstruction(setupOther, SetupInstructions.DealHandymen, Game.HasModule(AlhambraBase.ExpansionModule.DesignerHandymen));
            AddSetupInstruction(setupOther, SetupInstructions.DealPersonalBuildingMarket, Game.HasModule(AlhambraBase.ExpansionModule.FanPersonalBuildingMarket));
            AddSetupInstruction(setupOther, SetupInstructions.DealTreasures, Game.HasModule(AlhambraBase.ExpansionModule.FanTreasures));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceMissions, Game.HasModule(AlhambraBase.ExpansionModule.FanCaliphsGuidelines));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceNewMarket, Game.HasModule(AlhambraBase.ExpansionModule.NewMarket));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceGuardBoard, Game.HasModule(AlhambraBase.ExpansionModule.RedPalaceLandTiles));

            Dictionary<string, List<SetupInstructions>> setup = new Dictionary<string, List<SetupInstructions>>()
            {
                [Resources.GetString(Resource.String.tiles)] = setupTiles,
                [Resources.GetString(Resource.String.cards)] = setupCards,
                [Resources.GetString(Resource.String.other)] = setupOther,
            };

            ExpandableListViewExtension expandableListView = FindViewById<ExpandableListViewExtension>(Resource.Id.expandableListView);
            ExpandListCheckBoxAdapterMultiple<SetupInstructions> adapter = new ExpandListCheckBoxAdapterMultiple<SetupInstructions>(this, setup);
            expandableListView.SetAdapter(adapter);
            expandableListView.Expand();

            Button playButton = FindViewById<Button>(Resource.Id.startButton);
            playButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.GameStart();
            });
        }

        public override void OnBackPressed()
        {
            if (Game.GameInProgress)
            {
                new AlertDialog.Builder(this)
                    .SetTitle(Resources.GetString(Resource.String.game_ending))
                    .SetMessage(Resources.GetString(Resource.String.continue_question))
                    .SetPositiveButton(Resources.GetString(Resource.String.yes), new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) =>
                    {
                        base.OnBackPressed();
                        Game.Reset(false);
                    }))
                    .SetNegativeButton(Resources.GetString(Resource.String.no), new DialogInterfaceOnClickListener(null))
                    .Show();
            }
            else
            {
                base.OnBackPressed();
                Game.Reset(false);
            }
        }

        private void AddSetupInstruction(List<SetupInstructions> setup, SetupInstructions instructions, bool condition = true)
        {
            if (condition)
                setup.Add(instructions);
        }
    }
}
