using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI.Activities
{
        //TODO instrukcja przygotowania gry + rund w zależności od wybranych modułów + dirk
    [Activity(Label = "Setup", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class GameSetupInstructionActivity : BaseActivity
    {
        private ExpandableListViewExtension expandableListView;

        protected override int ContentView => Resource.Layout.activity_game_setup;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            List<SetupInstructions> setupTiles = new List<SetupInstructions>();
            List<SetupInstructions> setupCards = new List<SetupInstructions>();
            //TODO pozostałe

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
            AddSetupInstruction(setupTiles, SetupInstructions.PlaceBuildings, Game.GranadaOption != GranadaOption.Alone);
            AddSetupInstruction(setupTiles, SetupInstructions.GranadaPlaceBuildings, Game.GranadaOption != GranadaOption.Without);
            AddSetupInstruction(setupTiles, SetupInstructions.ShuffleSquares, Game.HasModule(ExpansionModule.ExpansionSquares));
            AddSetupInstruction(setupTiles, SetupInstructions.ShuffleWatchtowers, Game.HasModule(ExpansionModule.ExpansionWatchtowers));
            AddSetupInstruction(setupTiles, SetupInstructions.ShuffleMajorConstructionProjects, Game.HasModule(ExpansionModule.DesignerMajorConstructionProjects));
            AddSetupInstruction(setupTiles, SetupInstructions.ShuffleGateBoard, Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd));
            AddSetupInstruction(setupTiles, SetupInstructions.ShuffleExtensions, Game.HasModule(ExpansionModule.DesignerExtensions));

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
            AddSetupInstruction(setupCards, SetupInstructions.BonusCardsDeal3, Game.HasModule(ExpansionModule.ExpansionBonusCards) && Game.PlayersCount == 3);
            AddSetupInstruction(setupCards, SetupInstructions.BonusCardsDeal2, Game.HasModule(ExpansionModule.ExpansionBonusCards) && (Game.PlayersCount == 4 || Game.PlayersCount == 5));
            AddSetupInstruction(setupCards, SetupInstructions.BonusCardsDeal1, Game.HasModule(ExpansionModule.ExpansionBonusCards) && Game.PlayersCount == 6);
            AddSetupInstruction(setupCards, SetupInstructions.ThievesDeal4, Game.HasModule(ExpansionModule.ExpansionThieves) && Game.PlayersCount == 3);
            AddSetupInstruction(setupCards, SetupInstructions.ThievesDeal3, Game.HasModule(ExpansionModule.ExpansionThieves) && Game.PlayersCount == 4);
            AddSetupInstruction(setupCards, SetupInstructions.ThievesDeal2, Game.HasModule(ExpansionModule.ExpansionThieves) && (Game.PlayersCount == 5 || Game.PlayersCount == 6));
            AddSetupInstruction(setupCards, SetupInstructions.MasterBuildersDeal, Game.HasModule(ExpansionModule.ExpansionMasterBuilders));
            AddSetupInstruction(setupCards, SetupInstructions.ShuffleInvasion, Game.HasModule(ExpansionModule.ExpansionInvaders));
            AddSetupInstruction(setupCards, SetupInstructions.ShuffleScout, Game.HasModule(ExpansionModule.ExpansionInvaders));
            AddSetupInstruction(setupCards, SetupInstructions.ShuffleCaravanserai, Game.HasModule(ExpansionModule.ExpansionCaravanserai));

            Dictionary<string, List<SetupInstructions>> setup = new Dictionary<string, List<SetupInstructions>>()
            {
                ["Tiles"] = setupTiles,
                ["Cards"] = setupCards,
            };

            expandableListView = FindViewById<ExpandableListViewExtension>(Resource.Id.expandableListView);
            ExpandListCheckBoxAdapter<SetupInstructions> adapter = new ExpandListCheckBoxAdapter<SetupInstructions>(this, setup, true);
            expandableListView.SetAdapter(adapter);
            expandableListView.Expand();

            Button playButton = FindViewById<Button>(Resource.Id.playButton);
            playButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Application.GameStart();
            });
        }

        private void AddSetupInstruction(List<SetupInstructions> setup, SetupInstructions instructions, bool condition = true)
        {
            if (condition)
                setup.Add(instructions);
        }
    }
}
