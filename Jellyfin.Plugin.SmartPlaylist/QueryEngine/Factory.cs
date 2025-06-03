using System;
using System.Linq;
using Jellyfin.Data.Entities;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;

namespace Jellyfin.Plugin.SmartPlaylist.QueryEngine
{
    internal class OperandFactory
    {
        // Returns a specific operand povided a baseitem, user, and library manager object.
        public static Operand GetMediaType(ILibraryManager libraryManager, BaseItem baseItem, User user)
        {
            var operand = new Operand(baseItem.Name);
            var people = libraryManager.GetPeople(baseItem);
            if (people.Count != 0)
            {
                // Maps to MediaBrowser.Model.Entities.PersonType
                operand.Actors = [.. people.Where(x => x.Type.Equals("Actor")).Select(x => x.Name)];
                operand.Composers = [.. people.Where(x => x.Type.Equals("Composer")).Select(x => x.Name)];
                operand.Directors = [.. people.Where(x => x.Type.Equals("Director")).Select(x => x.Name)];
                operand.GuestStars = [.. people.Where(x => x.Type.Equals("GuestStar")).Select(x => x.Name)];
                operand.Producers = [.. people.Where(x => x.Type.Equals("Producer")).Select(x => x.Name)];
                operand.Writers = [.. people.Where(x => x.Type.Equals("Writer")).Select(x => x.Name)];
            }

            operand.Genres = [.. baseItem.Genres];
            operand.IsPlayed = baseItem.IsPlayed(user);
            operand.Studios = [.. baseItem.Studios];
            operand.CommunityRating = baseItem.CommunityRating.GetValueOrDefault();
            operand.CriticRating = baseItem.CriticRating.GetValueOrDefault();
            operand.MediaType = baseItem.MediaType.ToString();
            operand.Album = baseItem.Album;

            if (baseItem.PremiereDate.HasValue)
            {
                operand.PremiereDate = new DateTimeOffset(baseItem.PremiereDate.Value).ToUnixTimeSeconds();
            }

            operand.DateCreated = new DateTimeOffset(baseItem.DateCreated).ToUnixTimeSeconds();
            operand.DateLastRefreshed = new DateTimeOffset(baseItem.DateLastRefreshed).ToUnixTimeSeconds();
            operand.DateLastSaved = new DateTimeOffset(baseItem.DateLastSaved).ToUnixTimeSeconds();
            operand.DateModified = new DateTimeOffset(baseItem.DateModified).ToUnixTimeSeconds();

            operand.FolderPath = baseItem.ContainingFolderPath;
            return operand;
        }
    }
}