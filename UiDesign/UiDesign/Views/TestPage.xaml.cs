using Acr.UserDialogs;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiDesign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public IList<aChatter> aChatters { get; private set; }
        public IList<string> pickerItem { get; private set; }

        private TwitchClass twitchClass = new TwitchClass();

        public class aChatter
        {
            public string title { get; set; }
            public string ImageUrl { get; set; }
            public string Views { get; set; }
            public string ClipUrl { get; set; }
            public string downloadUrl { get; set; }
            public bool Selected { get; set; }

            public override string ToString()
            {
                return title;
            }
        }

        public TestPage(string nick)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            this.BackgroundImageSource = FileImageSource.FromFile("drawable/twitch_gradient_flip.png");

            Prepare(nick);
        }

        public async void Prepare(string nick)
        {
            using (UserDialogs.Instance.Loading("Loading..."))
            {
                var result = await ShowClipsAsync(nick);
            }

            pickerItem = new List<string>();
            pickerItem.Add("24H - TOP10");
            pickerItem.Add("24H - TOP50");

            BindingContext = this;

            CheckPermsAsync();
        }

        private async void CheckPermsAsync()
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync<CalendarPermission>();
            if (status == PermissionStatus.Disabled || status == PermissionStatus.Denied || status == PermissionStatus.Unknown || status == PermissionStatus.Restricted)
            {
                PermissionStatus status2 = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
            }
        }

        public async Task<int> ShowClipsAsync(string nick)
        {
            aChatters = new List<aChatter>();

            var e = await twitchClass.GetClips(nick);

            foreach (var clip in e.Clips)
            {
                string temptitle = Regex.Replace(clip.Title, "[^A-Za-z0-9 ]", "");
                string goodtitle = temptitle.Insert(0, nick + "-");

                var goodUrl = clip.Thumbnails.Tiny.Replace("-preview-86x45.jpg", ".mp4");
                aChatters.Add(new aChatter { title = clip.Title, ImageUrl = clip.Thumbnails.Small, Views = "Wyświetleń: " + clip.Views.ToString(), ClipUrl = clip.Url, downloadUrl = goodUrl });
            }

            return 1;
        }

        private void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            aChatter tappedItem = e.Item as aChatter;

            WebClient webClient = new WebClient();
            webClient.DownloadFile(tappedItem.downloadUrl, "/sdcard/download/" + tappedItem.title + ".mp4");
        }

        private async void MaterialButton_Clicked_All(object sender, EventArgs e)
        {
            Acr.UserDialogs.UserDialogs.Instance.Toast("Rozpoczęto pobieranie wszystkich klipów...", TimeSpan.FromSeconds(4.0));
            await ClipDownloaderAsync(true);
        }

        private async void MaterialButton_Clicked_Selected(object sender, EventArgs e)
        {
            Acr.UserDialogs.UserDialogs.Instance.Toast("Rozpoczęto pobieranie wybranych klipów...", TimeSpan.FromSeconds(4.0));
            await ClipDownloaderAsync(false);
        }

        private async Task<int> ClipDownloaderAsync(bool all)
        {
            foreach (var item in aChatters)
            {
                item.Selected = true;

                if (item.Selected == true)
                {
                    WebClient webClient = new WebClient();
                    await webClient.DownloadFileTaskAsync(item.downloadUrl, "/sdcard/download/" + item.title + ".mp4");
                }
            }

            return 1;
        }
    }
}