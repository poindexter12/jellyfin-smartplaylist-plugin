using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jellyfin.Plugin.SmarterPlaylist
{
    public interface ISmarterPlaylistStore
    {
        Task<SmarterPlaylistDto> GetSmarterPlaylistAsync(Guid SmarterPlaylistId);
        Task<SmarterPlaylistDto[]> LoadPlaylistsAsync(Guid userId);
        Task<SmarterPlaylistDto[]> GetAllSmarterPlaylistsAsync();
        Task SaveAsync(SmarterPlaylistDto SmarterPlaylist);
        void Delete(Guid userId, string SmarterPlaylistId);
    }

    public class SmarterPlaylistStore(ISmarterPlaylistFileSystem fileSystem) : ISmarterPlaylistStore
    {
        private readonly ISmarterPlaylistFileSystem _fileSystem = fileSystem;

        public async Task<SmarterPlaylistDto> GetSmarterPlaylistAsync(Guid SmarterPlaylistId)
        {
            var fileName = _fileSystem.GetSmarterPlaylistFilePath(SmarterPlaylistId.ToString());

            return await LoadPlaylistAsync(fileName).ConfigureAwait(false);
        }

        public async Task<SmarterPlaylistDto[]> LoadPlaylistsAsync(Guid userId)
        {
            var deserializeTasks = _fileSystem.GetSmarterPlaylistFilePaths(userId.ToString()).Select(LoadPlaylistAsync).ToArray();

            await Task.WhenAll(deserializeTasks).ConfigureAwait(false);

            return deserializeTasks.Select(x => x.Result).ToArray();
        }

        public async Task<SmarterPlaylistDto[]> GetAllSmarterPlaylistsAsync()
        {
            var deserializeTasks = _fileSystem.GetAllSmarterPlaylistFilePaths().Select(LoadPlaylistAsync).ToArray();

            await Task.WhenAll(deserializeTasks).ConfigureAwait(false);

            return [.. deserializeTasks.Select(x => x.Result)];
        }

        public async Task SaveAsync(SmarterPlaylistDto SmarterPlaylist)
        {
            var filePath = _fileSystem.GetSmarterPlaylistPath(SmarterPlaylist.Id, SmarterPlaylist.FileName);
            await using var writer = File.Create(filePath);
            await JsonSerializer.SerializeAsync(writer, SmarterPlaylist).ConfigureAwait(false);
        }

        public void Delete(Guid userId, string SmarterPlaylistId)
        {
            var filePath = _fileSystem.GetSmarterPlaylistPath(userId.ToString(), SmarterPlaylistId);
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        private async Task<SmarterPlaylistDto> LoadPlaylistAsync(string filePath)
        {
            await using var reader = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<SmarterPlaylistDto>(reader).ConfigureAwait(false);
        }
    }
}