using System.IO;
using System.Linq;
using MediaBrowser.Controller;

namespace Jellyfin.Plugin.SmarterPlaylist
{
    public interface ISmarterPlaylistFileSystem
    {
        string BasePath { get; }
        string GetSmarterPlaylistFilePath(string SmarterPlaylistId);
        string[] GetSmarterPlaylistFilePaths(string userId);
        string[] GetAllSmarterPlaylistFilePaths();
        string GetSmarterPlaylistPath(string userId, string playlistId);
    }

    public class SmarterPlaylistFileSystem : ISmarterPlaylistFileSystem
    {
        // Class Constructor for SmarterPlaylistFileSystem
        // Creates directory if it doesn't exist
        public SmarterPlaylistFileSystem(IServerApplicationPaths serverApplicationPaths)
        {
            BasePath = Path.Combine(serverApplicationPaths.DataPath, "SmarterPlaylists");
            if (!Directory.Exists(BasePath)) Directory.CreateDirectory(BasePath);
        }

        public string BasePath { get; }

        public string GetSmarterPlaylistFilePath(string SmarterPlaylistId)
        {
            return Directory.GetFiles(BasePath, $"{SmarterPlaylistId}.json", SearchOption.AllDirectories).First();
        }

        public string[] GetSmarterPlaylistFilePaths(string userId)
        {
            return Directory.GetFiles(BasePath);
        }

        public string[] GetAllSmarterPlaylistFilePaths()
        {
            return Directory.GetFiles(BasePath, "*.json", SearchOption.AllDirectories);
        }

        public string GetSmarterPlaylistPath(string userId, string playlistId)
        {
            return Path.Combine(BasePath, $"{playlistId}.json");
        }
    }
}