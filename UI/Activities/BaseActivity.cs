﻿using AlhambraScoringAndroid.GamePlay;
using AndroidBase;
using System;
using System.Collections.Generic;

namespace AlhambraScoringAndroid.UI.Activities
{
    public abstract class BaseActivity : AndroidBaseActivity<MyApplication>
    {
        protected override int ToolbarResource => Resource.Id.toolbar;
        protected override int MainMenuResource => Resource.Menu.menu_main;
        protected override Dictionary<int, Action> MenuActions => new Dictionary<int, Action>()
        {
            [Resource.Id.action_newGame] = () => Application.NewGamePrompt(this),
            [Resource.Id.action_showHistory] = () => Application.ShowHistory(this),
            [Resource.Id.action_settings] = () => Application.ShowSettings(),
            [Resource.Id.action_about] = () => Application.ShowAbout(),
        };

        public Game Game => Application.Game;
    }
}