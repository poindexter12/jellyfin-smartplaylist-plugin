using System;
using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Jellyfin.Plugin.SmartPlaylist
{
    public class Plugin : BasePlugin<BasePluginConfiguration>, IHasWebPages
    {
        public Plugin(
            IApplicationPaths applicationPaths,
            IXmlSerializer xmlSerializer
        ) : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }


        public override Guid Id => Guid.Parse("3311dfd2-fe3b-4367-a3f0-0dcea5ba07cd");

        public override string Name => "SmartPlaylist";

        public override string Description => "SmartPlaylist is a Jellyfin plugin that allows you to create dynamic playlists based on various criteria and conditions.";

        public static Plugin Instance { get; private set; }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return
            [
                new PluginPageInfo
                {
                    //Name = "smartplaylist.html",
                    //EmbeddedResourcePath = string.Format("{0}.Configuration.smartplaylist.html", GetType().Namespace),
                }
            ];
        }
    }
}