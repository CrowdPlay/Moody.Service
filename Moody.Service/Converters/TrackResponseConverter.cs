using Moody.Models.Data;
using Moody.Models.Requests;

namespace Moody.Service.Converters
{
    public class TrackResponseConverter : IConverter<Track, RequestTrack>
    {
        public RequestTrack Convert(Track track)
        {
            return new RequestTrack
            {
                id = track.TrackId
            };
        }
    }
}