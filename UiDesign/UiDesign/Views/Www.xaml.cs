using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiDesign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Www : ContentPage
    {
        public Www(string text)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = "<!DOCTYPE html><html><body><h1>My First Heading</h1><p>My first paragraph.</p><script src= \"https://player.twitch.tv/js/embed/v1.js\"></script><div id=\"SamplePlayerDivID\"></div><script type=\"text/javascript\">  var options = {    width: 854,    height: 480,    channel: \"h2p_gucio\",    parent: [\"localhost\"]  };  var player = new Twitch.Player(\"SamplePlayerDivID\", options);  player.setVolume(0.5);</script></body></html>";

            Browser.Source = @"https://www.twitch.tv/" + text;
        }
    }
}