using Acr.UserDialogs;
using dotMorten.Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading.Tasks;
using TwitchLib.Api;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiDesign.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        private static TwitchAPI api;
        private TwitchClass twitchClass = new TwitchClass();
        private string lastquery;

        public StartPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            this.BackgroundImageSource = FileImageSource.FromFile("drawable/twitch_gradient_flip.png");

            
        }

        

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StreamerInfoUI("h2p_gucio"));
        }

        private async void MaterialButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestPage("h2p_gucio"));
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing,
            // otherwise assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (sender.Text.Length > 3)
                {
                    using (UserDialogs.Instance.Loading("Wyszukiwanie..."))
                    {
                        var result = Task.Run(() => twitchClass.GetPossibleStreamerName(sender.Text)).Result;
                        sender.ItemsSource = result;
                    }
                }

                //Set the ItemsSource to be your filtered dataset
            }
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            Navigation.PushAsync(new StreamerInfoUI(sender.Text));
            sender.Text = null;
            sender.ItemsSource = null;
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.
            }
            else
            {
                // User hit Enter from the search box. Use args.QueryText to determine what to do.
            }
        }
    }
}