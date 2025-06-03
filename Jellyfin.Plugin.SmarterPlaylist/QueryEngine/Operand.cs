using System.Collections.Generic;

namespace Jellyfin.Plugin.SmarterPlaylist.QueryEngine
{
    public class Operand(string name)
    {
        public List<string> Actors { get; set; } = [];
        public List<string> Composers { get; set; } = [];
        public float CommunityRating { get; set; } = 0;
        public float CriticRating { get; set; } = 0;
        public List<string> Directors { get; set; } = [];
        public List<string> Genres { get; set; } = [];
        public List<string> GuestStars { get; set; } = [];
        public bool IsPlayed { get; set; }
        public string Name { get; set; } = name;
        public string FolderPath { get; set; } = string.Empty;
        public double PremiereDate { get; set; } = 0;
        public List<string> Producers { get; set; } = [];
        public List<string> Studios { get; set; } = [];
        public List<string> Writers { get; set; } = [];
        public string MediaType { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public double DateCreated { get; set; } = 0;
        public double DateLastRefreshed { get; set; } = 0;
        public double DateLastSaved { get; set; } = 0;
        public double DateModified { get; set; } = 0;
    }
}