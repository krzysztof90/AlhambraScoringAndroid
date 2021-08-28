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
        private ExpandableListViewExtension expandableListView;

        protected override int ContentView => Resource.Layout.activity_game_setup;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            List<SetupInstructions> setupTiles = new List<SetupInstructions>();
            List<SetupInstructions> setupCards = new List<SetupInstructions>();
            List<SetupInstructions> setupOther = new List<SetupInstructions>();

            AddSetupInstruction(setupTiles, SetupInstructions.PutBuildingsOfPowerTiles, Game.HasModule(ExpansionModule.DesignerBuildingsOfPower));
            AddSetupInstruction(setupTiles, SetupInstructions.PutCampTiles, Game.HasModule(ExpansionModule.ExpansionCamps));
            AddSetupInstruction(setupTiles, SetupInstructions.PutBazaarsTiles, Game.HasModule(ExpansionModule.ExpansionBazaars));
            AddSetupInstruction(setupTiles, SetupInstructions.PutMagicalBuildingsTiles, Game.HasModule(ExpansionModule.QueenieMagicalBuildings));
            AddSetupInstruction(setupTiles, SetupInstructions.PutMedinaTiles, Game.HasModule(ExpansionModule.QueenieMedina));
            AddSetupInstruction(setupTiles, SetupInstructions.PutNewBuildingGroundsTiles, Game.HasModule(ExpansionModule.DesignerNewBuildingGrounds));
            AddSetupInstruction(setupTiles, SetupInstructions.PutBathhouseTiles, Game.HasModule(ExpansionModule.DesignerBathhouses));
            AddSetupInstruction(setupTiles, SetupInstructions.PutWishingWellTiles, Game.HasModule(ExpansionModule.DesignerWishingWell));
            AddSetupInstruction(setupTiles, SetupInstructions.ShuffleBuildingTiles, Game.GranadaOption != GranadaOption.Alone);
            AddSetupInstruction(setupTiles, SetupInstructions.GranadaShuffleBuildingTiles, Game.GranadaOption != GranadaOption.Without);
            AddSetupInstruction(setupTiles, SetupInstructions.DealTrader, Game.HasModule(ExpansionModule.ExpansionStreetTrader));
            AddSetupInstruction(setupTiles, SetupInstructions.PlaceBuildings, Game.GranadaOption != GranadaOption.Alone);
            AddSetupInstruction(setupTiles, SetupInstructions.GiveBuildingsToDirk, Game.InvolvedDirk);
            AddSetupInstruction(setupTiles, SetupInstructions.GranadaPlaceBuildings, Game.GranadaOption != GranadaOption.Without);

            AddSetupInstruction(setupCards, SetupInstructions.RemoveCardDeck, Game.InvolvedDirk);
            AddSetupInstruction(setupCards, SetupInstructions.PutDiamondCards, Game.HasModule(ExpansionModule.ExpansionDiamonds));
            AddSetupInstruction(setupCards, SetupInstructions.ShuffleCards);
            AddSetupInstruction(setupCards, SetupInstructions.DealMoney);
            AddSetupInstruction(setupCards, SetupInstructions.PlaceCards);
            AddSetupInstruction(setupCards, SetupInstructions.DivideCards);
            AddSetupInstruction(setupCards, SetupInstructions.Put2ScoringCards, Game.GranadaOption != GranadaOption.With);
            AddSetupInstruction(setupCards, SetupInstructions.Put1ScoringCardMiddle, Game.GranadaOption == GranadaOption.With);
            AddSetupInstruction(setupCards, SetupInstructions.PutCurrencyExchangeCards, Game.HasModule(ExpansionModule.ExpansionCurrencyExchangeCards));
            AddSetupInstruction(setupCards, SetupInstructions.PutCityGatesCards, Game.HasModule(ExpansionModule.ExpansionCityGates));
            AddSetupInstruction(setupCards, SetupInstructions.PutCharacters, Game.HasModule(ExpansionModule.ExpansionCharacters));
            AddSetupInstruction(setupCards, SetupInstructions.PutCityWalls, Game.HasModule(ExpansionModule.ExpansionCityWalls));
            AddSetupInstruction(setupCards, SetupInstructions.PutMasterBuilders, Game.HasModule(ExpansionModule.ExpansionMasterBuilders) && Game.PlayersCount != 6);
            AddSetupInstruction(setupCards, SetupInstructions.PutMasterBuilders6, Game.HasModule(ExpansionModule.ExpansionMasterBuilders) && Game.PlayersCount == 6);
            AddSetupInstruction(setupCards, SetupInstructions.PutPowerOfSultan, Game.HasModule(ExpansionModule.ExpansionPowerOfSultan));
            AddSetupInstruction(setupCards, SetupInstructions.JoinPiles);

            AddSetupInstruction(setupOther, SetupInstructions.ShuffleSquares, Game.HasModule(ExpansionModule.ExpansionSquares));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleWatchtowers, Game.HasModule(ExpansionModule.ExpansionWatchtowers));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleMajorConstructionProjects, Game.HasModule(ExpansionModule.DesignerMajorConstructionProjects));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleGateBoard, Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleExtensions, Game.HasModule(ExpansionModule.DesignerExtensions));

            AddSetupInstruction(setupOther, SetupInstructions.DealVizier, Game.HasModule(ExpansionModule.ExpansionViziersFavour));
            AddSetupInstruction(setupOther, SetupInstructions.DealBonusCards3, Game.HasModule(ExpansionModule.ExpansionBonusCards) && Game.PlayersCount == 3);
            AddSetupInstruction(setupOther, SetupInstructions.DealBonusCards2, Game.HasModule(ExpansionModule.ExpansionBonusCards) && (Game.PlayersCount == 4 || Game.PlayersCount == 5));
            AddSetupInstruction(setupOther, SetupInstructions.DealBonusCards1, Game.HasModule(ExpansionModule.ExpansionBonusCards) && Game.PlayersCount == 6);
            AddSetupInstruction(setupOther, SetupInstructions.PlaceCityGates, Game.HasModule(ExpansionModule.ExpansionCityGates));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceCityWalls, Game.HasModule(ExpansionModule.ExpansionCityWalls));
            AddSetupInstruction(setupOther, SetupInstructions.DealThieves4, Game.HasModule(ExpansionModule.ExpansionThieves) && Game.PlayersCount == 3);
            AddSetupInstruction(setupOther, SetupInstructions.DealThieves3, Game.HasModule(ExpansionModule.ExpansionThieves) && Game.PlayersCount == 4);
            AddSetupInstruction(setupOther, SetupInstructions.DealThieves2, Game.HasModule(ExpansionModule.ExpansionThieves) && (Game.PlayersCount == 5 || Game.PlayersCount == 6));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceChange, Game.HasModule(ExpansionModule.ExpansionChange));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceTrader, Game.HasModule(ExpansionModule.ExpansionStreetTrader));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceTraderTiles, Game.HasModule(ExpansionModule.ExpansionStreetTrader));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceTreasureChamber, Game.HasModule(ExpansionModule.ExpansionTreasureChamber));
            AddSetupInstruction(setupOther, SetupInstructions.DealMasterBuilders, Game.HasModule(ExpansionModule.ExpansionMasterBuilders));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleInvasion, Game.HasModule(ExpansionModule.ExpansionInvaders));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleScout, Game.HasModule(ExpansionModule.ExpansionInvaders));
            AddSetupInstruction(setupOther, SetupInstructions.PlacePowerOfSultan, Game.HasModule(ExpansionModule.ExpansionPowerOfSultan));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleCaravanserai, Game.HasModule(ExpansionModule.ExpansionCaravanserai));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceCaravanserai, Game.HasModule(ExpansionModule.ExpansionCaravanserai));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceArtOfTheMoors, Game.HasModule(ExpansionModule.ExpansionArtOfTheMoors));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceFalcons, Game.HasModule(ExpansionModule.ExpansionFalconers));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleBuildingSites, Game.HasModule(ExpansionModule.ExpansionBuildingSites));
            AddSetupInstruction(setupOther, SetupInstructions.DealExchangeCertificate, Game.HasModule(ExpansionModule.ExpansionExchangeCertificates));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceExchangeCertificate1, Game.HasModule(ExpansionModule.ExpansionExchangeCertificates) && Game.PlayersCountWithoutDirk == 2);
            AddSetupInstruction(setupOther, SetupInstructions.PlaceExchangeCertificate2, Game.HasModule(ExpansionModule.ExpansionExchangeCertificates) && (Game.PlayersCountWithoutDirk == 3 || Game.PlayersCount == 4));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceExchangeCertificate3, Game.HasModule(ExpansionModule.ExpansionExchangeCertificates) && (Game.PlayersCount == 5 || Game.PlayersCount == 6));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceMagicalBuildings, Game.HasModule(ExpansionModule.QueenieMagicalBuildings));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceNewBuildingGrounds, Game.HasModule(ExpansionModule.DesignerNewBuildingGrounds));
            AddSetupInstruction(setupOther, SetupInstructions.DealMajorConstructionMarker, Game.HasModule(ExpansionModule.DesignerMajorConstructionProjects));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleServantTiles, Game.HasModule(ExpansionModule.DesignerPalaceStaff));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleOrchardsBoards, Game.HasModule(ExpansionModule.DesignerOrchards));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleOrchardsFruits, Game.HasModule(ExpansionModule.DesignerOrchards));
            AddSetupInstruction(setupOther, SetupInstructions.RemoveCraftsmen5, Game.HasModule(ExpansionModule.DesignerTravellingCraftsmen) && (Game.PlayersCount == 3 || Game.PlayersCount == 4));
            AddSetupInstruction(setupOther, SetupInstructions.RemoveCraftsmen6, Game.HasModule(ExpansionModule.DesignerTravellingCraftsmen) && (Game.PlayersCount == 5 || Game.PlayersCount == 6));
            AddSetupInstruction(setupOther, SetupInstructions.DealCraftsmen, Game.HasModule(ExpansionModule.DesignerTravellingCraftsmen));
            AddSetupInstruction(setupOther, SetupInstructions.RemoveProjectTiles, Game.HasModule(ExpansionModule.DesignerFreshColors) && Game.PlayersCount == 3);
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleProjectTiles, Game.HasModule(ExpansionModule.DesignerFreshColors));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceColorTiles, Game.HasModule(ExpansionModule.DesignerFreshColors));
            AddSetupInstruction(setupOther, SetupInstructions.DealPalaceDesigners, Game.HasModule(ExpansionModule.DesignerPalaceDesigners));
            AddSetupInstruction(setupOther, SetupInstructions.PlacePalaceDesigners, Game.HasModule(ExpansionModule.DesignerPalaceDesigners));
            AddSetupInstruction(setupOther, SetupInstructions.ShuffleAnimals, Game.HasModule(ExpansionModule.DesignerAlhambraZoo));
            AddSetupInstruction(setupOther, SetupInstructions.DealHandymen, Game.HasModule(ExpansionModule.DesignerHandymen));
            AddSetupInstruction(setupOther, SetupInstructions.DealPersonalBuildingMarket, Game.HasModule(ExpansionModule.FanPersonalBuildingMarket));
            AddSetupInstruction(setupOther, SetupInstructions.DealTreasures, Game.HasModule(ExpansionModule.FanTreasures));
            AddSetupInstruction(setupOther, SetupInstructions.PlaceMissions, Game.HasModule(ExpansionModule.FanCaliphsGuidelines));

            Dictionary<string, List<SetupInstructions>> setup = new Dictionary<string, List<SetupInstructions>>()
            {
                [Resources.GetString(Resource.String.tiles)] = setupTiles,
                [Resources.GetString(Resource.String.cards)] = setupCards,
                [Resources.GetString(Resource.String.other)] = setupOther,
            };

            expandableListView = FindViewById<ExpandableListViewExtension>(Resource.Id.expandableListView);
            ExpandListCheckBoxAdapter<SetupInstructions> adapter = new ExpandListCheckBoxAdapter<SetupInstructions>(this, setup, true);
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
                        Game.Reset(true);
                    }))
                    .SetNegativeButton(Resources.GetString(Resource.String.no), new DialogInterfaceOnClickListener(null))
                    .Show();
            }
            else
            {
                base.OnBackPressed();
                Game.Reset(true);
            }
        }

        private void AddSetupInstruction(List<SetupInstructions> setup, SetupInstructions instructions, bool condition = true)
        {
            if (condition)
                setup.Add(instructions);
        }
    }
}
