using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api;

namespace UiDesign
{
    internal class TwitchClass
    {
        private static TwitchAPI api;

        public TwitchClass()
        {
            api = new TwitchAPI();
            api.Settings.ClientId = "gp762nuuoqcoxypju8c569th9wz7q5";
            api.Settings.AccessToken = "7cy27v7v8llv2d12u5v4h2m6612ppc";
        }

        public async Task<TwitchLib.Api.V5.Models.Users.Users> GetUserInfo(string name)
        {
            var y = await api.V5.Users.GetUserByNameAsync(name);

            return y;
        }

        public async Task<TwitchLib.Api.V5.Models.Streams.StreamByUser> GetUserStreamInfo(string channelId)
        {
            var x = await api.V5.Streams.GetStreamByUserAsync(channelId);

            return x;
        }

        public async Task<TwitchLib.Api.V5.Models.Channels.Channel> GetChannelStreamInfo(string channelId)
        {
            var x = await api.V5.Channels.GetChannelByIDAsync(channelId);

            return x;
        }

        public async Task<List<string>> GetPossibleStreamerName(string letters)
        {
            List<string> streamers = new List<string>();

            var x = await api.V5.Search.SearchChannelsAsync(letters, 5);

            foreach (var q in x.Channels)
            {
                streamers.Add(q.DisplayName);
            }

            return streamers;
        }

        public async Task<TwitchLib.Api.V5.Models.Clips.TopClipsResponse> GetClips(string nickname)
        {
            var e = await api.V5.Clips.GetTopClipsAsync(nickname, null, null, 30, TwitchLib.Api.V5.Models.Clips.Period.Week);

            return e;
        }
    }
}