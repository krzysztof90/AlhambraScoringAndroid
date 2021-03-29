using AlhambraScoringAndroid.GamePlay;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace AlhambraScoringAndroid.UI.Activities
{
    [Activity(Label = "Historia", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class ResultsHistoryActivity : BaseActivity
    {
        protected override int ContentView => Resource.Layout.activity_history;

        private LinearLayout container;

        public ResultsHistoryActivity()
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            container = FindViewById<LinearLayout>(Resource.Id.container);

            foreach (ResultHistory resultHistory in Application.Results)
            {
                HistoryPanel historyPanel = new HistoryPanel(this, resultHistory.StartDateTime, (DateTime)resultHistory.EndDateTime);
                container.AddView(historyPanel);
                container.RequestLayout();
            }
        }

        public void ShowResult(HistoryPanel historyPanel)
        {
            Application.ShowResult(historyPanel.StartDateTime);
        }

        public void RemoveResult(HistoryPanel historyPanel)
        {
            new AlertDialog.Builder(this)
                .SetTitle("Usuwanie wyniku")
                .SetMessage("Czy kontynuować?")
                .SetPositiveButton("Yes", new DialogInterfaceOnClickListener((IDialogInterface dialog, int which) =>
                {
                    container.RemoveView(historyPanel);
                    container.RequestLayout();
                    Application.RemoveResult(historyPanel.StartDateTime);
                    Application.SaveResults();
                }))
                .SetNegativeButton("No", new DialogInterfaceOnClickListener(null))
                .Show();
        }
    }
}