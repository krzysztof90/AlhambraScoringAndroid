using AlhambraScoringAndroid.GamePlay;
using AlhambraScoringAndroid.Options;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidBase.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.UI
{
    public class PlaceholderPlayerScoreFragment : AndroidX.Fragment.App.Fragment
    {
        private readonly PlayersScoreSectionsPagerAdapter Adapter;

        public int PlayerNumber { get; private set; }
        public Game Game { get; private set; }

        protected bool IsDirk { get; private set; }
        protected bool IsFinalRound { get; private set; }
        protected bool IsThirdBeforeLeftoverRound { get; private set; }
        protected PlayerScoreData CorrectingRoundScoring { get; private set; }
        protected PlayerScoreData PreviousRoundScoring { get; private set; }

        protected View Root { get; private set; }

        public Dictionary<ResultType, IControlViewBase> Controls { get; private set; }

        public PlaceholderPlayerScoreFragment(int index, Game game, List<PlayerScoreData> correctingRoundScoring, PlayersScoreSectionsPagerAdapter adapter)
        {
            Adapter = adapter;

            PlayerNumber = index;
            Game = game;
            Controls = new Dictionary<ResultType, IControlViewBase>();

            IsDirk = Game.GetPlayer(PlayerNumber).Dirk;
            IsFinalRound = Game.ScoreRound == ScoringRound.Third;
            IsThirdBeforeLeftoverRound = Game.ScoreRound == ScoringRound.ThirdBeforeLeftover;
            CorrectingRoundScoring = correctingRoundScoring?[PlayerNumber - 1];
            PreviousRoundScoring = Game.PreviousRoundScoring?[PlayerNumber - 1];
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (Root == null)
            {
                Create(inflater, container);
            }

            return Root;
        }

        public void Create(LayoutInflater inflater, ViewGroup container)
        {
            Root = inflater.Inflate(Resource.Layout.fragment_game_score, container, false);

            CreateControls();
            InitializeControls();
            SetControlsProperties();
            if (CorrectingRoundScoring != null)
            {
                ApplyCorrectingRoundScoring();
            }
            else if (PreviousRoundScoring != null)
            {
                ApplyPreviousRoundScoring();
            }

            Button submitButton = Root.FindViewById<Button>(Resource.Id.submitButton);
            submitButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                Adapter.Submit();
            });
        }

        private void InitializeControls()
        {
            foreach (IControlViewBase control in Controls.Select(c => c.Value))
                control.Initialize();
        }

        public void RestoreValues()
        {
            foreach (IControlViewBase control in Controls.Select(c => c.Value))
                control.RestoreValue();
        }

        private ControlViewBase<T> GetControl<T>(ResultType resultType) where T : struct
        {
            return Controls.ContainsKey(resultType) ? Controls[resultType] as ControlViewBase<T> : null;
        }

        public object GetControlValueObject(ResultType resultType)
        {
            return Controls.ContainsKey(resultType) ? Controls[resultType].GetValueObject() : default;
        }

        public T GetControlValue<T>(ResultType resultType) where T : struct
        {
            return GetControl<T>(resultType)?.Value ?? default;
        }

        private void SetControlValue<T>(ResultType resultType, T value) where T : struct
        {
            ControlViewBase<T> control = GetControl<T>(resultType);
            if (control != null)
                control.Value = value;
        }

        private void SetControlNumberRange(ResultType resultType, int min, int max, SettingsType settingsType)
        {
            ControlNumberView control = GetControl<int>(resultType) as ControlNumberView;
            if (control != null)
                control.SetNumberRange<SettingsType>(min, max, settingsType);
        }

        private void SetControlEnabled(ResultType resultType, bool enabled)
        {
            ControlNumberView control = GetControl<int>(resultType) as ControlNumberView;
            if (control != null)
                control.Enabled = enabled;
        }

        private void SetControlOnValueChange(ResultType resultType, Action onValueChange)
        {
            ControlNumberView control = GetControl<int>(resultType) as ControlNumberView;
            if (control != null)
                control.OnValueChange = onValueChange;
        }

        private void ApplyControlProperties<T>(ControlViewBase<T> control, int labelResourceId, int? colorResourceId = null) where T : struct
        {
            control.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            control.SetLabel(Root.Context.Resources.GetString(labelResourceId));
            if (colorResourceId != null)
                control.SetColor(Root.Context.Resources.GetColorStateList((int)colorResourceId));

            LinearLayout container = Root.FindViewById<LinearLayout>(Resource.Id.container);
            container.AddView(control);
        }

        private ControlNumberView CreateControlNumberView(int labelResourceId, int? colorResourceId = null)
        {
            ControlNumberView control = new ControlNumberView(Root.Context);
            ApplyControlProperties<int>(control, labelResourceId, colorResourceId);

            return control;
        }

        private ControlCheckBoxView CreateControlCheckBoxView(int labelResourceId, int? colorResourceId = null)
        {
            ControlCheckBoxView control = new ControlCheckBoxView(Root.Context);
            ApplyControlProperties<bool>(control, labelResourceId, colorResourceId);

            return control;
        }

        protected void CreateControls()
        {
            if (Game.GranadaOption != GranadaOption.Alone
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.WallLength, CreateControlNumberView(Resource.String.wallsCount));
            if (Game.GranadaOption != GranadaOption.Alone
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.PavilionNumber, CreateControlNumberView(Resource.String.pavilionCount, Resource.Color.colorPavilion));
            if (Game.GranadaOption != GranadaOption.Alone
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.SeraglioNumber, CreateControlNumberView(Resource.String.seraglioCount, Resource.Color.colorSeraglio));
            if (Game.GranadaOption != GranadaOption.Alone
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ArcadesNumber, CreateControlNumberView(Resource.String.arcadesCount, Resource.Color.colorArcades));
            if (Game.GranadaOption != GranadaOption.Alone
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ChambersNumber, CreateControlNumberView(Resource.String.chambersCount, Resource.Color.colorChambers));
            if (Game.GranadaOption != GranadaOption.Alone
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.GardenNumber, CreateControlNumberView(Resource.String.gardenCount, Resource.Color.colorGarden));
            if (Game.GranadaOption != GranadaOption.Alone
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.TowerNumber, CreateControlNumberView(Resource.String.towerCount, Resource.Color.colorTower));
            if (Game.HasModule(ExpansionModule.ExpansionBonusCards)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.BonusCardsPavilionNumber, CreateControlNumberView(Resource.String.bonusCardsPavilionCount, Resource.Color.colorPavilion));
            if (Game.HasModule(ExpansionModule.ExpansionBonusCards)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.BonusCardsSeraglioNumber, CreateControlNumberView(Resource.String.bonusCardsSeraglioCount, Resource.Color.colorSeraglio));
            if (Game.HasModule(ExpansionModule.ExpansionBonusCards)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.BonusCardsArcadesNumber, CreateControlNumberView(Resource.String.bonusCardsArcadesCount, Resource.Color.colorArcades));
            if (Game.HasModule(ExpansionModule.ExpansionBonusCards)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.BonusCardsChambersNumber, CreateControlNumberView(Resource.String.bonusCardsChambersCount, Resource.Color.colorChambers));
            if (Game.HasModule(ExpansionModule.ExpansionBonusCards)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.BonusCardsGardenNumber, CreateControlNumberView(Resource.String.bonusCardsGardenCount, Resource.Color.colorGarden));
            if (Game.HasModule(ExpansionModule.ExpansionBonusCards)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.BonusCardsTowerNumber, CreateControlNumberView(Resource.String.bonusCardsTowerCount, Resource.Color.colorTower));
            if (Game.HasModule(ExpansionModule.ExpansionSquares)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.SquaresPavilionNumber, CreateControlNumberView(Resource.String.squaresPavilionCount, Resource.Color.colorPavilion));
            if (Game.HasModule(ExpansionModule.ExpansionSquares)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.SquaresSeraglioNumber, CreateControlNumberView(Resource.String.squaresSeraglioCount, Resource.Color.colorSeraglio));
            if (Game.HasModule(ExpansionModule.ExpansionSquares)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.SquaresArcadesNumber, CreateControlNumberView(Resource.String.squaresArcadesCount, Resource.Color.colorArcades));
            if (Game.HasModule(ExpansionModule.ExpansionSquares)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.SquaresChambersNumber, CreateControlNumberView(Resource.String.squaresChambersCount, Resource.Color.colorChambers));
            if (Game.HasModule(ExpansionModule.ExpansionSquares)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.SquaresGardenNumber, CreateControlNumberView(Resource.String.squaresGardenCount, Resource.Color.colorGarden));
            if (Game.HasModule(ExpansionModule.ExpansionSquares)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.SquaresTowerNumber, CreateControlNumberView(Resource.String.squaresTowerCount, Resource.Color.colorTower));
            if (Game.HasModule(ExpansionModule.ExpansionCharacters)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.OwnedCharacterTheWiseMan, CreateControlCheckBoxView(Resource.String.ownedCharacterTheWiseMan));
            if (Game.HasModule(ExpansionModule.ExpansionCharacters)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.OwnedCharacterTheCityWatch, CreateControlCheckBoxView(Resource.String.ownedCharacterTheCityWatch));
            if (Game.HasModule(ExpansionModule.ExpansionCamps)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CampsPoints, CreateControlNumberView(Resource.String.campsPoints));
            if (Game.HasModule(ExpansionModule.ExpansionStreetTrader)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.StreetTradersPavilionNumber, CreateControlNumberView(Resource.String.streetTradersPavilionCount, Resource.Color.colorPavilion));
            if (Game.HasModule(ExpansionModule.ExpansionStreetTrader)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.StreetTradersSeraglioNumber, CreateControlNumberView(Resource.String.streetTradersSeraglioCount, Resource.Color.colorSeraglio));
            if (Game.HasModule(ExpansionModule.ExpansionStreetTrader)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.StreetTradersArcadesNumber, CreateControlNumberView(Resource.String.streetTradersArcadesCount, Resource.Color.colorArcades));
            if (Game.HasModule(ExpansionModule.ExpansionStreetTrader)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.StreetTradersChambersNumber, CreateControlNumberView(Resource.String.streetTradersChambersCount, Resource.Color.colorChambers));
            if (Game.HasModule(ExpansionModule.ExpansionStreetTrader)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.StreetTradersGardenNumber, CreateControlNumberView(Resource.String.streetTradersGardenCount, Resource.Color.colorGarden));
            if (Game.HasModule(ExpansionModule.ExpansionStreetTrader)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.StreetTradersTowerNumber, CreateControlNumberView(Resource.String.streetTradersTowerCount, Resource.Color.colorTower));
            if (Game.HasModule(ExpansionModule.ExpansionTreasureChamber)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.TreasuresValue, CreateControlNumberView(Resource.String.treasuresCount));
            if (Game.HasModule(ExpansionModule.ExpansionInvaders)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.UnprotectedSidesNumber, CreateControlNumberView(Resource.String.unprotectedSidesCount));
            if (Game.HasModule(ExpansionModule.ExpansionInvaders)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.UnprotectedSidesNeighbouringNumber, CreateControlNumberView(Resource.String.unprotectedSidesNeighbouringCount));
            if (Game.HasModule(ExpansionModule.ExpansionBazaars)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.BazaarsPoints, CreateControlNumberView(Resource.String.bazaarsPoints));
            if (Game.HasModule(ExpansionModule.ExpansionArtOfTheMoors)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ArtOfTheMoorsPoints, CreateControlNumberView(Resource.String.artOfTheMoorsPoints));
            if (Game.HasModule(ExpansionModule.ExpansionFalconers)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.FalconsBlackNumber, CreateControlNumberView(Resource.String.falconsBlackNumber));
            if (Game.HasModule(ExpansionModule.ExpansionFalconers)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.FalconsBrownNumber, CreateControlNumberView(Resource.String.falconsBrownNumber));
            if (Game.HasModule(ExpansionModule.ExpansionFalconers)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.FalconsWhiteNumber, CreateControlNumberView(Resource.String.falconsWhiteNumber));
            if (Game.HasModule(ExpansionModule.ExpansionWatchtowers)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.WatchtowersNumber, CreateControlNumberView(Resource.String.watchtowersNumber));
            if (Game.HasModule(ExpansionModule.QueenieMedina)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.MedinasNumber, CreateControlNumberView(Resource.String.medinasNumber));
            if (Game.HasModule(ExpansionModule.DesignerPalaceStaff)
                && !IsFinalRound
                && !IsDirk)
                Controls.Add(ResultType.BuildingsWithoutServantTile, CreateControlNumberView(Resource.String.buildingsWithoutServantTile));
            if (Game.HasModule(ExpansionModule.DesignerOrchards)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedGroupOfFruitBoard1, CreateControlCheckBoxView(Resource.String.completedGroupOfFruitBoard1));
            if (Game.HasModule(ExpansionModule.DesignerOrchards)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedGroupOfFruitBoard2, CreateControlCheckBoxView(Resource.String.completedGroupOfFruitBoard2));
            if (Game.HasModule(ExpansionModule.DesignerOrchards)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedGroupOfFruitBoard3, CreateControlCheckBoxView(Resource.String.completedGroupOfFruitBoard3));
            if (Game.HasModule(ExpansionModule.DesignerOrchards)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedGroupOfFruitBoard4, CreateControlCheckBoxView(Resource.String.completedGroupOfFruitBoard4));
            if (Game.HasModule(ExpansionModule.DesignerOrchards)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedGroupOfFruitBoard5, CreateControlCheckBoxView(Resource.String.completedGroupOfFruitBoard5));
            if (Game.HasModule(ExpansionModule.DesignerOrchards)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedGroupOfFruitBoard6, CreateControlCheckBoxView(Resource.String.completedGroupOfFruitBoard6));
            if (Game.HasModule(ExpansionModule.DesignerOrchards)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.FaceDownFruitsNumber, CreateControlNumberView(Resource.String.faceDownFruitsCount));
            if (Game.HasModule(ExpansionModule.DesignerBathhouses)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.BathhousesPoints, CreateControlNumberView(Resource.String.bathhousesPoints));
            if (Game.HasModule(ExpansionModule.DesignerWishingWell)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.WishingWellsPoints, CreateControlNumberView(Resource.String.wishingWellsPoints));
            if (Game.HasModule(ExpansionModule.DesignerFreshColors)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedProjectPavilion, CreateControlCheckBoxView(Resource.String.completedProjectPavilion, Resource.Color.colorPavilion));
            if (Game.HasModule(ExpansionModule.DesignerFreshColors)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedProjectSeraglio, CreateControlCheckBoxView(Resource.String.completedProjectSeraglio, Resource.Color.colorSeraglio));
            if (Game.HasModule(ExpansionModule.DesignerFreshColors)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedProjectArcades, CreateControlCheckBoxView(Resource.String.completedProjectArcades, Resource.Color.colorArcades));
            if (Game.HasModule(ExpansionModule.DesignerFreshColors)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedProjectChambers, CreateControlCheckBoxView(Resource.String.completedProjectChambers, Resource.Color.colorChambers));
            if (Game.HasModule(ExpansionModule.DesignerFreshColors)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedProjectGarden, CreateControlCheckBoxView(Resource.String.completedProjectGarden, Resource.Color.colorGarden));
            if (Game.HasModule(ExpansionModule.DesignerFreshColors)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.CompletedProjectTower, CreateControlCheckBoxView(Resource.String.completedProjectTower, Resource.Color.colorTower));
            if (Game.HasModule(ExpansionModule.DesignerAlhambraZoo)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.AnimalsPoints, CreateControlNumberView(Resource.String.animalsPoints));
            if (Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.OwnedSemiBuildingPavilion, CreateControlCheckBoxView(Resource.String.ownedSemiBuildingPavilion, Resource.Color.colorPavilion));
            if (Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.OwnedSemiBuildingSeraglio, CreateControlCheckBoxView(Resource.String.ownedSemiBuildingSeraglio, Resource.Color.colorSeraglio));
            if (Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.OwnedSemiBuildingArcades, CreateControlCheckBoxView(Resource.String.ownedSemiBuildingArcades, Resource.Color.colorArcades));
            if (Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.OwnedSemiBuildingChambers, CreateControlCheckBoxView(Resource.String.ownedSemiBuildingChambers, Resource.Color.colorChambers));
            if (Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.OwnedSemiBuildingGarden, CreateControlCheckBoxView(Resource.String.ownedSemiBuildingGarden, Resource.Color.colorGarden));
            if (Game.HasModule(ExpansionModule.DesignerGatesWithoutEnd)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.OwnedSemiBuildingTower, CreateControlCheckBoxView(Resource.String.ownedSemiBuildingTower, Resource.Color.colorTower));
            if (Game.HasModule(ExpansionModule.DesignerBuildingsOfPower)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.BlackDiceTotalPips, CreateControlNumberView(Resource.String.blackDiceTotalPips));
            if (Game.HasModule(ExpansionModule.DesignerExtensions)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ExtensionsPavilionCount, CreateControlNumberView(Resource.String.extensionsPavilionCount, Resource.Color.colorPavilion));
            if (Game.HasModule(ExpansionModule.DesignerExtensions)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ExtensionsSeraglioCount, CreateControlNumberView(Resource.String.extensionsSeraglioCount, Resource.Color.colorSeraglio));
            if (Game.HasModule(ExpansionModule.DesignerExtensions)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ExtensionsArcadesCount, CreateControlNumberView(Resource.String.extensionsArcadesCount, Resource.Color.colorArcades));
            if (Game.HasModule(ExpansionModule.DesignerExtensions)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ExtensionsChambersCount, CreateControlNumberView(Resource.String.extensionsChambersCount, Resource.Color.colorChambers));
            if (Game.HasModule(ExpansionModule.DesignerExtensions)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ExtensionsGardenCount, CreateControlNumberView(Resource.String.extensionsGardenCount, Resource.Color.colorGarden));
            if (Game.HasModule(ExpansionModule.DesignerExtensions)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ExtensionsTowerCount, CreateControlNumberView(Resource.String.extensionsTowerCount, Resource.Color.colorTower));
            if (Game.HasModule(ExpansionModule.DesignerHandymen)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.HandymenTilesHighestNumber, CreateControlNumberView(Resource.String.handymenTilesHighestNumber));
            if (Game.HasModule(ExpansionModule.FanTreasures)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.TreasuresPoints, CreateControlNumberView(Resource.String.treasuresValue));
            if (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission1)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.Mission1RowsCount, CreateControlNumberView(Resource.String.mission1RowsCount));
            if (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission2)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.Mission2ColumnsCount, CreateControlNumberView(Resource.String.mission2ColumnsCount));
            if (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission3)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.Mission3Adjacent2BuildingsCount, CreateControlNumberView(Resource.String.mission3Adjacent2BuildingsCount));
            if (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission5)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.Mission5LongestDiagonalLine, CreateControlNumberView(Resource.String.mission5LongestDiagonalLine));
            if (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission6)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.Mission6DoubleWallCount, CreateControlNumberView(Resource.String.mission6DoubleWallCount));
            if (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission8)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.Mission8PathBuildingsNumber, CreateControlNumberView(Resource.String.mission8PathBuildingsNumber));
            if (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission9)
                && IsFinalRound
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.Mission9Grids22Count, CreateControlNumberView(Resource.String.mission9Grids22Count));
            if ((Game.HasModule(ExpansionModule.DesignerBuildingsOfPower) || (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4) && IsFinalRound))
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.SecondLongestWall, CreateControlNumberView(Resource.String.secondLongestWall));
            if (Game.HasModule(ExpansionModule.Granada)
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.MoatLength, CreateControlNumberView(Resource.String.moatLength));
            if (Game.HasModule(ExpansionModule.Granada)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ArenaCount, CreateControlNumberView(Resource.String.arenaCount, Resource.Color.colorArena));
            if (Game.HasModule(ExpansionModule.Granada)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.BathHouseCount, CreateControlNumberView(Resource.String.bathHouseCount, Resource.Color.colorBathHouse));
            if (Game.HasModule(ExpansionModule.Granada)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.LibraryCount, CreateControlNumberView(Resource.String.libraryCount, Resource.Color.colorLibrary));
            if (Game.HasModule(ExpansionModule.Granada)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.HostelCount, CreateControlNumberView(Resource.String.hostelCount, Resource.Color.colorHostel));
            if (Game.HasModule(ExpansionModule.Granada)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.HospitalCount, CreateControlNumberView(Resource.String.hospitalCount, Resource.Color.colorHospital));
            if (Game.HasModule(ExpansionModule.Granada)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.MarketCount, CreateControlNumberView(Resource.String.marketCount, Resource.Color.colorMarket));
            if (Game.HasModule(ExpansionModule.Granada)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ParkCount, CreateControlNumberView(Resource.String.parkCount, Resource.Color.colorPark));
            if (Game.HasModule(ExpansionModule.Granada)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.SchoolCount, CreateControlNumberView(Resource.String.schoolCount, Resource.Color.colorSchool));
            if (Game.HasModule(ExpansionModule.Granada)
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.ResidentialAreaCount, CreateControlNumberView(Resource.String.residentialAreaCount, Resource.Color.colorResidentialArea));
            if (Game.GranadaOption == GranadaOption.With
                && !IsDirk
                && !IsThirdBeforeLeftoverRound)
                Controls.Add(ResultType.WallMoatCombination, CreateControlNumberView(Resource.String.wallMoatCombination));
        }

        protected void SetControlsProperties()
        {
            SetControlOnValueChange(ResultType.BlackDiceTotalPips, () => { EnabledSecondLongestWall(); });

            //TODO wspólne walidacje z Game do metody
            SetControlNumberRange(ResultType.WallLength, 0, Game.WallsMaxLength, SettingsType.ValidateWallLength);
            SetControlNumberRange(ResultType.PavilionNumber, 0, Game.BuildingsMaxCount[BuildingType.Pavilion], SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.SeraglioNumber, 0, Game.BuildingsMaxCount[BuildingType.Seraglio], SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.ArcadesNumber, 0, Game.BuildingsMaxCount[BuildingType.Arcades], SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.ChambersNumber, 0, Game.BuildingsMaxCount[BuildingType.Chambers], SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.GardenNumber, 0, Game.BuildingsMaxCount[BuildingType.Garden], SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.TowerNumber, 0, Game.BuildingsMaxCount[BuildingType.Tower], SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.BonusCardsPavilionNumber, 0, Game.BonusCardsMaxCount[BuildingType.Pavilion], SettingsType.ValidateBonusCards);
            SetControlNumberRange(ResultType.BonusCardsSeraglioNumber, 0, Game.BonusCardsMaxCount[BuildingType.Seraglio], SettingsType.ValidateBonusCards);
            SetControlNumberRange(ResultType.BonusCardsArcadesNumber, 0, Game.BonusCardsMaxCount[BuildingType.Arcades], SettingsType.ValidateBonusCards);
            SetControlNumberRange(ResultType.BonusCardsChambersNumber, 0, Game.BonusCardsMaxCount[BuildingType.Chambers], SettingsType.ValidateBonusCards);
            SetControlNumberRange(ResultType.BonusCardsGardenNumber, 0, Game.BonusCardsMaxCount[BuildingType.Garden], SettingsType.ValidateBonusCards);
            SetControlNumberRange(ResultType.BonusCardsTowerNumber, 0, Game.BonusCardsMaxCount[BuildingType.Tower], SettingsType.ValidateBonusCards);
            SetControlNumberRange(ResultType.SquaresPavilionNumber, 0, 9, SettingsType.ValidateSquares);
            SetControlNumberRange(ResultType.SquaresSeraglioNumber, 0, 9, SettingsType.ValidateSquares);
            SetControlNumberRange(ResultType.SquaresArcadesNumber, 0, 9, SettingsType.ValidateSquares);
            SetControlNumberRange(ResultType.SquaresChambersNumber, 0, 9, SettingsType.ValidateSquares);
            SetControlNumberRange(ResultType.SquaresGardenNumber, 0, 9, SettingsType.ValidateSquares);
            SetControlNumberRange(ResultType.SquaresTowerNumber, 0, 9, SettingsType.ValidateSquares);
            SetControlNumberRange(ResultType.CampsPoints, 0, Game.AllTilesCount, SettingsType.ValidateCamps);
            SetControlNumberRange(ResultType.StreetTradersPavilionNumber, 0, 7, SettingsType.ValidateCitizens);
            SetControlNumberRange(ResultType.StreetTradersSeraglioNumber, 0, 7, SettingsType.ValidateCitizens);
            SetControlNumberRange(ResultType.StreetTradersArcadesNumber, 0, 7, SettingsType.ValidateCitizens);
            SetControlNumberRange(ResultType.StreetTradersChambersNumber, 0, 7, SettingsType.ValidateCitizens);
            SetControlNumberRange(ResultType.StreetTradersGardenNumber, 0, 7, SettingsType.ValidateCitizens);
            SetControlNumberRange(ResultType.StreetTradersTowerNumber, 0, 7, SettingsType.ValidateCitizens);
            SetControlNumberRange(ResultType.TreasuresValue, 0, 42, SettingsType.ValidateTreasures);
            SetControlNumberRange(ResultType.UnprotectedSidesNumber, 0, Game.AllTilesCount, SettingsType.ValidateUnprotectedSides);
            SetControlNumberRange(ResultType.UnprotectedSidesNeighbouringNumber, 0, Game.AllTilesCount, SettingsType.ValidateUnprotectedSides);
            SetControlNumberRange(ResultType.BazaarsPoints, 0, 192, SettingsType.ValidateBazaarsPoints);
            SetControlNumberRange(ResultType.ArtOfTheMoorsPoints, 0, 147, SettingsType.ValidateCultureCounters);
            SetControlNumberRange(ResultType.FalconsBlackNumber, 0, 5, SettingsType.ValidateFalcons);
            SetControlNumberRange(ResultType.FalconsBrownNumber, 0, 5, SettingsType.ValidateFalcons);
            SetControlNumberRange(ResultType.FalconsWhiteNumber, 0, 5, SettingsType.ValidateFalcons);
            SetControlNumberRange(ResultType.WatchtowersNumber, 0, 18, SettingsType.ValidateWatchtower);
            SetControlNumberRange(ResultType.MedinasNumber, 0, 9, SettingsType.ValidateMedin);
            SetControlNumberRange(ResultType.BuildingsWithoutServantTile, 0, Game.AllBuildingsCount, SettingsType.ValidateServants);
            SetControlNumberRange(ResultType.FaceDownFruitsNumber, 0, 35, SettingsType.ValidateSingleFruits);
            SetControlNumberRange(ResultType.BathhousesPoints, 0, (Game.AllTilesCount - 1) * 4 * 6, SettingsType.ValidateBathhouses);
            SetControlNumberRange(ResultType.WishingWellsPoints, 0, 24, SettingsType.ValidateWishingWells);
            SetControlNumberRange(ResultType.AnimalsPoints, 0, 24, SettingsType.ValidateAnimals);
            SetControlNumberRange(ResultType.BlackDiceTotalPips, 0, 18, SettingsType.ValidateBlackDicePips);
            SetControlNumberRange(ResultType.ExtensionsPavilionCount, 0, 2, SettingsType.ValidateExtensions);
            SetControlNumberRange(ResultType.ExtensionsSeraglioCount, 0, 2, SettingsType.ValidateExtensions);
            SetControlNumberRange(ResultType.ExtensionsArcadesCount, 0, 2, SettingsType.ValidateExtensions);
            SetControlNumberRange(ResultType.ExtensionsChambersCount, 0, 2, SettingsType.ValidateExtensions);
            SetControlNumberRange(ResultType.ExtensionsGardenCount, 0, 2, SettingsType.ValidateExtensions);
            SetControlNumberRange(ResultType.ExtensionsTowerCount, 0, 2, SettingsType.ValidateExtensions);
            SetControlNumberRange(ResultType.HandymenTilesHighestNumber, 0, 48, SettingsType.ValidateHandymen);
            SetControlNumberRange(ResultType.TreasuresPoints, 0, 15, SettingsType.ValidateTreasuresPoints);
            SetControlNumberRange(ResultType.Mission1RowsCount, 0, Game.AllTilesCount / 3, SettingsType.ValidateMissions);
            SetControlNumberRange(ResultType.Mission2ColumnsCount, 0, Game.AllTilesCount / 3, SettingsType.ValidateMissions);
            SetControlNumberRange(ResultType.Mission3Adjacent2BuildingsCount, 0, 70, SettingsType.ValidateMissions);
            SetControlNumberRange(ResultType.Mission5LongestDiagonalLine, 0, (Game.AllTilesCount + 1) / 2, SettingsType.ValidateMissions);
            SetControlNumberRange(ResultType.Mission6DoubleWallCount, 0, Game.GetBuildingsAvailableAdjacent(Game.AllWallTilesCount), SettingsType.ValidateMissions);
            SetControlNumberRange(ResultType.Mission8PathBuildingsNumber, 0, (Game.AllTilesCount + 1) / 2, SettingsType.ValidateMissions);
            SetControlNumberRange(ResultType.Mission9Grids22Count, 0, Game.GetBuildingsAvailable2x2Grids(Game.AllTilesCount), SettingsType.ValidateMissions);
            SetControlNumberRange(ResultType.SecondLongestWall, 0, Game.WallsMaxLength / 2 - 2, SettingsType.ValidateSecondLongestWall);
            SetControlNumberRange(ResultType.MoatLength, 0, Game.MoatMaxLength, SettingsType.ValidateMoatLength);
            SetControlNumberRange(ResultType.ArenaCount, 0, 6, SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.BathHouseCount, 0, 6, SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.LibraryCount, 0, 6, SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.HostelCount, 0, 6, SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.HospitalCount, 0, 6, SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.MarketCount, 0, 6, SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.ParkCount, 0, 6, SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.SchoolCount, 0, 6, SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.ResidentialAreaCount, 0, 6, SettingsType.ValidateBuildingsNumber);
            SetControlNumberRange(ResultType.WallMoatCombination, 0, Math.Min(Game.WallsMaxLength, Game.MoatMaxLength), SettingsType.ValidateMoatwall);

            EnabledSecondLongestWall();
        }

        protected void ApplyCorrectingRoundScoring()
        {
            SetControlValue(ResultType.WallLength, CorrectingRoundScoring.WallLength);
            SetControlValue(ResultType.PavilionNumber, CorrectingRoundScoring.BuildingsCount[BuildingType.Pavilion]);
            SetControlValue(ResultType.SeraglioNumber, CorrectingRoundScoring.BuildingsCount[BuildingType.Seraglio]);
            SetControlValue(ResultType.ArcadesNumber, CorrectingRoundScoring.BuildingsCount[BuildingType.Arcades]);
            SetControlValue(ResultType.ChambersNumber, CorrectingRoundScoring.BuildingsCount[BuildingType.Chambers]);
            SetControlValue(ResultType.GardenNumber, CorrectingRoundScoring.BuildingsCount[BuildingType.Garden]);
            SetControlValue(ResultType.TowerNumber, CorrectingRoundScoring.BuildingsCount[BuildingType.Tower]);
            SetControlValue(ResultType.BonusCardsPavilionNumber, CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Pavilion]);
            SetControlValue(ResultType.BonusCardsSeraglioNumber, CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Seraglio]);
            SetControlValue(ResultType.BonusCardsArcadesNumber, CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Arcades]);
            SetControlValue(ResultType.BonusCardsChambersNumber, CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Chambers]);
            SetControlValue(ResultType.BonusCardsGardenNumber, CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Garden]);
            SetControlValue(ResultType.BonusCardsTowerNumber, CorrectingRoundScoring.BonusCardsBuildingsCount[BuildingType.Tower]);
            SetControlValue(ResultType.SquaresPavilionNumber, CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Pavilion]);
            SetControlValue(ResultType.SquaresSeraglioNumber, CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Seraglio]);
            SetControlValue(ResultType.SquaresArcadesNumber, CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Arcades]);
            SetControlValue(ResultType.SquaresChambersNumber, CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Chambers]);
            SetControlValue(ResultType.SquaresGardenNumber, CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Garden]);
            SetControlValue(ResultType.SquaresTowerNumber, CorrectingRoundScoring.SquaresBuildingsCount[BuildingType.Tower]);
            SetControlValue(ResultType.OwnedCharacterTheWiseMan, CorrectingRoundScoring.OwnedCharacterTheWiseMan);
            SetControlValue(ResultType.OwnedCharacterTheCityWatch, CorrectingRoundScoring.OwnedCharacterTheCityWatch);
            SetControlValue(ResultType.CampsPoints, CorrectingRoundScoring.CampsPoints);
            SetControlValue(ResultType.StreetTradersPavilionNumber, CorrectingRoundScoring.StreetTradersNumber[BuildingType.Pavilion]);
            SetControlValue(ResultType.StreetTradersSeraglioNumber, CorrectingRoundScoring.StreetTradersNumber[BuildingType.Seraglio]);
            SetControlValue(ResultType.StreetTradersArcadesNumber, CorrectingRoundScoring.StreetTradersNumber[BuildingType.Arcades]);
            SetControlValue(ResultType.StreetTradersChambersNumber, CorrectingRoundScoring.StreetTradersNumber[BuildingType.Chambers]);
            SetControlValue(ResultType.StreetTradersGardenNumber, CorrectingRoundScoring.StreetTradersNumber[BuildingType.Garden]);
            SetControlValue(ResultType.StreetTradersTowerNumber, CorrectingRoundScoring.StreetTradersNumber[BuildingType.Tower]);
            SetControlValue(ResultType.TreasuresValue, CorrectingRoundScoring.TreasuresCount);
            SetControlValue(ResultType.UnprotectedSidesNumber, CorrectingRoundScoring.UnprotectedSidesCount);
            SetControlValue(ResultType.UnprotectedSidesNeighbouringNumber, CorrectingRoundScoring.UnprotectedSidesNeighbouringCount);
            SetControlValue(ResultType.BazaarsPoints, CorrectingRoundScoring.BazaarsTotalPoints);
            SetControlValue(ResultType.ArtOfTheMoorsPoints, CorrectingRoundScoring.ArtOfTheMoorsPoints);
            SetControlValue(ResultType.FalconsBlackNumber, CorrectingRoundScoring.FalconsBlackNumber);
            SetControlValue(ResultType.FalconsBrownNumber, CorrectingRoundScoring.FalconsBrownNumber);
            SetControlValue(ResultType.FalconsWhiteNumber, CorrectingRoundScoring.FalconsWhiteNumber);
            SetControlValue(ResultType.WatchtowersNumber, CorrectingRoundScoring.WatchtowersNumber);
            SetControlValue(ResultType.MedinasNumber, CorrectingRoundScoring.MedinasNumber);
            SetControlValue(ResultType.BuildingsWithoutServantTile, CorrectingRoundScoring.BuildingsWithoutServantTile);
            SetControlValue(ResultType.CompletedGroupOfFruitBoard1, CorrectingRoundScoring.CompletedGroupOfFruitBoard1);
            SetControlValue(ResultType.CompletedGroupOfFruitBoard2, CorrectingRoundScoring.CompletedGroupOfFruitBoard2);
            SetControlValue(ResultType.CompletedGroupOfFruitBoard3, CorrectingRoundScoring.CompletedGroupOfFruitBoard3);
            SetControlValue(ResultType.CompletedGroupOfFruitBoard4, CorrectingRoundScoring.CompletedGroupOfFruitBoard4);
            SetControlValue(ResultType.CompletedGroupOfFruitBoard5, CorrectingRoundScoring.CompletedGroupOfFruitBoard5);
            SetControlValue(ResultType.CompletedGroupOfFruitBoard6, CorrectingRoundScoring.CompletedGroupOfFruitBoard6);
            SetControlValue(ResultType.FaceDownFruitsNumber, CorrectingRoundScoring.FaceDownFruitsCount);
            SetControlValue(ResultType.BathhousesPoints, CorrectingRoundScoring.BathhousesPoints);
            SetControlValue(ResultType.WishingWellsPoints, CorrectingRoundScoring.WishingWellsPoints);
            SetControlValue(ResultType.CompletedProjectPavilion, CorrectingRoundScoring.CompletedProjects[BuildingType.Pavilion]);
            SetControlValue(ResultType.CompletedProjectSeraglio, CorrectingRoundScoring.CompletedProjects[BuildingType.Seraglio]);
            SetControlValue(ResultType.CompletedProjectArcades, CorrectingRoundScoring.CompletedProjects[BuildingType.Arcades]);
            SetControlValue(ResultType.CompletedProjectChambers, CorrectingRoundScoring.CompletedProjects[BuildingType.Chambers]);
            SetControlValue(ResultType.CompletedProjectGarden, CorrectingRoundScoring.CompletedProjects[BuildingType.Garden]);
            SetControlValue(ResultType.CompletedProjectTower, CorrectingRoundScoring.CompletedProjects[BuildingType.Tower]);
            SetControlValue(ResultType.AnimalsPoints, CorrectingRoundScoring.AnimalsPoints);
            SetControlValue(ResultType.OwnedSemiBuildingPavilion, CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Pavilion]);
            SetControlValue(ResultType.OwnedSemiBuildingSeraglio, CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Seraglio]);
            SetControlValue(ResultType.OwnedSemiBuildingArcades, CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Arcades]);
            SetControlValue(ResultType.OwnedSemiBuildingChambers, CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Chambers]);
            SetControlValue(ResultType.OwnedSemiBuildingGarden, CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Garden]);
            SetControlValue(ResultType.OwnedSemiBuildingTower, CorrectingRoundScoring.OwnedSemiBuildings[BuildingType.Tower]);
            SetControlValue(ResultType.BlackDiceTotalPips, CorrectingRoundScoring.BlackDiceTotalPips);
            SetControlValue(ResultType.ExtensionsPavilionCount, CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Pavilion]);
            SetControlValue(ResultType.ExtensionsSeraglioCount, CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Seraglio]);
            SetControlValue(ResultType.ExtensionsArcadesCount, CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Arcades]);
            SetControlValue(ResultType.ExtensionsChambersCount, CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Chambers]);
            SetControlValue(ResultType.ExtensionsGardenCount, CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Garden]);
            SetControlValue(ResultType.ExtensionsTowerCount, CorrectingRoundScoring.ExtensionsBuildingsCount[BuildingType.Tower]);
            SetControlValue(ResultType.HandymenTilesHighestNumber, CorrectingRoundScoring.HandymenTilesHighestNumber);
            SetControlValue(ResultType.TreasuresPoints, CorrectingRoundScoring.TreasuresPoints);
            SetControlValue(ResultType.Mission1RowsCount, CorrectingRoundScoring.Mission1Count);
            SetControlValue(ResultType.Mission2ColumnsCount, CorrectingRoundScoring.Mission2Count);
            SetControlValue(ResultType.Mission3Adjacent2BuildingsCount, CorrectingRoundScoring.Mission3Count);
            SetControlValue(ResultType.Mission5LongestDiagonalLine, CorrectingRoundScoring.Mission5Count);
            SetControlValue(ResultType.Mission6DoubleWallCount, CorrectingRoundScoring.Mission6Count);
            SetControlValue(ResultType.Mission8PathBuildingsNumber, CorrectingRoundScoring.Mission8Count);
            SetControlValue(ResultType.Mission9Grids22Count, CorrectingRoundScoring.Mission9Count);
            SetControlValue(ResultType.SecondLongestWall, CorrectingRoundScoring.SecondLongestWallLength);
            SetControlValue(ResultType.MoatLength, CorrectingRoundScoring.MoatLength);
            SetControlValue(ResultType.ArenaCount, CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Arena]);
            SetControlValue(ResultType.BathHouseCount, CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.BathHouse]);
            SetControlValue(ResultType.LibraryCount, CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Library]);
            SetControlValue(ResultType.HostelCount, CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Hostel]);
            SetControlValue(ResultType.HospitalCount, CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Hospital]);
            SetControlValue(ResultType.MarketCount, CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Market]);
            SetControlValue(ResultType.ParkCount, CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.Park]);
            SetControlValue(ResultType.SchoolCount, CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.School]);
            SetControlValue(ResultType.ResidentialAreaCount, CorrectingRoundScoring.GranadaBuildingsCount[GranadaBuildingType.ResidentialArea]);
            SetControlValue(ResultType.WallMoatCombination, CorrectingRoundScoring.WallMoatCombinationLength);
        }

        protected void ApplyPreviousRoundScoring()
        {
            SetControlValue(ResultType.OwnedCharacterTheWiseMan, PreviousRoundScoring.OwnedCharacterTheWiseMan);
            SetControlValue(ResultType.OwnedCharacterTheCityWatch, PreviousRoundScoring.OwnedCharacterTheCityWatch);
            SetControlValue(ResultType.CompletedProjectPavilion, PreviousRoundScoring.CompletedProjects[BuildingType.Pavilion]);
            SetControlValue(ResultType.CompletedProjectSeraglio, PreviousRoundScoring.CompletedProjects[BuildingType.Seraglio]);
            SetControlValue(ResultType.CompletedProjectArcades, PreviousRoundScoring.CompletedProjects[BuildingType.Arcades]);
            SetControlValue(ResultType.CompletedProjectChambers, PreviousRoundScoring.CompletedProjects[BuildingType.Chambers]);
            SetControlValue(ResultType.CompletedProjectGarden, PreviousRoundScoring.CompletedProjects[BuildingType.Garden]);
            SetControlValue(ResultType.CompletedProjectTower, PreviousRoundScoring.CompletedProjects[BuildingType.Tower]);
            SetControlValue(ResultType.OwnedSemiBuildingPavilion, PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Pavilion]);
            SetControlValue(ResultType.OwnedSemiBuildingSeraglio, PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Seraglio]);
            SetControlValue(ResultType.OwnedSemiBuildingArcades, PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Arcades]);
            SetControlValue(ResultType.OwnedSemiBuildingChambers, PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Chambers]);
            SetControlValue(ResultType.OwnedSemiBuildingGarden, PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Garden]);
            SetControlValue(ResultType.OwnedSemiBuildingTower, PreviousRoundScoring.OwnedSemiBuildings[BuildingType.Tower]);
        }

        private void EnabledSecondLongestWall()
        {
            SetControlEnabled(ResultType.SecondLongestWall, GetControlValue<int>(ResultType.BlackDiceTotalPips) != 0 || (Game.HasModule(ExpansionModule.FanCaliphsGuidelines) && Game.HasCaliphsGuideline(CaliphsGuidelinesMission.Mission4) && IsFinalRound));
        }
    }
}