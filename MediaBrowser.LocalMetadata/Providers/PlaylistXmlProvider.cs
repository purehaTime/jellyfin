using System.Threading;
using MediaBrowser.Controller.Playlists;
using MediaBrowser.Controller.Providers;
using MediaBrowser.LocalMetadata.Parsers;
using MediaBrowser.LocalMetadata.Savers;
using MediaBrowser.Model.IO;
using Microsoft.Extensions.Logging;

namespace MediaBrowser.LocalMetadata.Providers
{
    /// <summary>
    /// Playlist xml provider.
    /// </summary>
    public class PlaylistXmlProvider : BaseXmlProvider<Playlist>
    {
        private readonly ILogger<PlaylistXmlParser> _logger;
        private readonly IProviderManager _providerManager;
        private readonly IDirectoryService _directoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistXmlProvider"/> class.
        /// </summary>
        /// <param name="fileSystem">Instance of the <see cref="IFileSystem"/> interface.</param>
        /// <param name="logger">Instance of the <see cref="ILogger{PlaylistXmlParser}"/> interface.</param>
        /// <param name="providerManager">Instance of the <see cref="IProviderManager"/> interface.</param>
        /// <param name="directoryService">The directory service.</param>
        public PlaylistXmlProvider(
            IFileSystem fileSystem,
            ILogger<PlaylistXmlParser> logger,
            IProviderManager providerManager,
            IDirectoryService directoryService)
            : base(fileSystem)
        {
            _logger = logger;
            _providerManager = providerManager;
            _directoryService = directoryService;
        }

        /// <inheritdoc />
        protected override void Fetch(MetadataResult<Playlist> result, string path, CancellationToken cancellationToken)
        {
            new PlaylistXmlParser(_logger, _providerManager).Fetch(result, path, cancellationToken);
        }

        /// <inheritdoc />
        protected override FileSystemMetadata? GetXmlFile(ItemInfo info)
        {
            return _directoryService.GetFile(PlaylistXmlSaver.GetSavePath(info.Path));
        }
    }
}
