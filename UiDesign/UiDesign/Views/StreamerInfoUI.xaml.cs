using Acr.UserDialogs;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiDesign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StreamerInfoUI : ContentPage
    {
        private string nickname;
        private string previewUrl;
        private TwitchClass twitchClass = new TwitchClass();
        private string url;

        public StreamerInfoUI(string nick)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            nickname = nick;

            Prepare(nick);
        }

        public async void Prepare(string nick)
        {
            using (UserDialogs.Instance.Loading("Ładowanie..."))
            {
                var res = await PreparePage(nick);
            }
        }

        public async Task<int> PreparePage(string nick)
        {
            nickname = nick;

            this.BackgroundImageSource = FileImageSource.FromFile("drawable/twitch_gradient_flip.png");

            var userinfo = await twitchClass.GetUserInfo(nickname);
            var userstreaminfo = await twitchClass.GetUserStreamInfo(userinfo.Matches[0].Id);
            var channelstreaminfo = await twitchClass.GetChannelStreamInfo(userinfo.Matches[0].Id);

            StreamerImage.Source = userinfo.Matches[0].Logo;

            onofflabel.IsVisible = true;
            imgSrc2.IsVisible = true;
            StreamerImage.IsVisible = true;

            if (userstreaminfo.Stream == null)
            {
                url = channelstreaminfo.Url;

                imgSrc2.Source = "ic_launcher2.png";
                onofflabel.Text = "Offline!";
                onofflabel.TextColor = Color.FromHex("#9E9E9E");

                StreamerNameResult.Text = userinfo.Matches[0].DisplayName;
                //StreamerBioResult.Text = details.Bio;

                //StreamerBioResult.Text = "x";

                StreamerStatusResult.Text = "x";

                //StreamerStreamViewersResult.Text = "x";
            }
            else
            {
                StreamerNameResult.Text = userinfo.Matches[0].DisplayName;
                //StreamerBioResult.Text = details.Bio;

                //StreamerBioResult.Text = result2.Stream.Game;

                url = channelstreaminfo.Url;

                previewUrl = userstreaminfo.Stream.Preview.Large;

                PreviewImage.Source = previewUrl;

                StreamerStatusResult.Text = channelstreaminfo.Status;

                //StreamerStreamViewersResult.Text = result2.Stream.Viewers.ToString();

                if (userstreaminfo.Stream.StreamType == "live")
                {
                    imgSrc2.Source = "ic_launcher.png";
                    onofflabel.Text = "Online!";
                    onofflabel.TextColor = Color.FromHex("#00FF14");
                }
                else
                {
                    imgSrc2.Source = "ic_launcher2.png";
                    onofflabel.Text = "Offline!";
                    onofflabel.TextColor = Color.FromHex("#9E9E9E");
                }
            }

            return 1;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri(url));
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Ładownie"))
            {
                await Navigation.PushAsync(new Www(nickname));
                await Task.Delay(1500);
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestPage(nickname));
        }
    }
}