using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace MM.Web.Mvc.Theming.Hosting
{
    public class VirtualThemePathProvider : VirtualPathProvider
    {
        private readonly string _themeFolder;

        private readonly string _themeName;

        public VirtualThemePathProvider(string themeName, string themeFolder = "Themes")
        {
            if(string.IsNullOrWhiteSpace(themeName))
            {
                throw new ArgumentException("A theme name must be specified when using this ViewPathProvider.",
                    "themeName");
            }

            if(string.IsNullOrWhiteSpace(themeFolder) || themeFolder == "/")
            {
                throw new ArgumentException("A theme folder name must be specified when using this ViewPathProvider.",
                    "themeFolder");
            }

            _themeName = themeName;
            _themeFolder = VirtualPathUtility.RemoveTrailingSlash(themeFolder);
        }

        /// <summary>
        ///     Gets a value that indicates whether a directory exists in the virtual file system.
        /// </summary>
        /// <returns>
        ///     true if the directory exists in the virtual file system; otherwise, false.
        /// </returns>
        /// <param name="virtualDir"> The path to the virtual directory. </param>
        public override bool DirectoryExists(string virtualDir)
        {
            var virtualThemeDir = toVirtualThemePath(virtualDir);

            return base.DirectoryExists(virtualThemeDir) || base.DirectoryExists(virtualDir);
        }

        /// <summary>
        ///     Gets a value that indicates whether a file exists in the virtual file system.
        /// </summary>
        /// <returns>
        ///     true if the file exists in the virtual file system; otherwise, false.
        /// </returns>
        /// <param name="virtualPath"> The path to the virtual file. </param>
        public override bool FileExists(string virtualPath)
        {
            var virtualThemePath = toVirtualThemePath(virtualPath);

            // Check if virtual path exists, if not, check original path
            return base.FileExists(virtualThemePath) || base.FileExists(virtualPath);
        }

        /// <summary>
        ///     Creates a cache dependency based on the specified virtual paths.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Web.Caching.CacheDependency" /> object for the specified virtual resources.
        /// </returns>
        /// <param name="virtualPath"> The path to the primary virtual resource. </param>
        /// <param name="virtualPathDependencies"> An array of paths to other resources required by the primary virtual resource. </param>
        /// <param name="utcStart"> The UTC time at which the virtual resources were read. </param>
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies,
            DateTime utcStart)
        {
            // TODO we should do something better than this
            return null;
        }

        /// <summary>
        ///     Gets a virtual directory from the virtual file system.
        /// </summary>
        /// <returns>
        ///     A descendent of the <see cref="T:System.Web.Hosting.VirtualDirectory" /> class that represents a directory in the
        ///     virtual file system.
        /// </returns>
        /// <param name="virtualDir"> The path to the virtual directory. </param>
        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            var virtualThemePath = toVirtualThemePath(virtualDir);

            if(!base.DirectoryExists(virtualThemePath))
            {
                return base.GetDirectory(virtualDir);
            }

            var directory = base.GetDirectory(virtualThemePath);
            var wrappedDirectory = new VirtualThemeDirectory(directory, virtualDir);
            return wrappedDirectory;
        }

        /// <summary>
        ///     Gets a virtual file from the virtual file system.
        /// </summary>
        /// <returns>
        ///     A descendent of the <see cref="T:System.Web.Hosting.VirtualFile" /> class that represents a file in the virtual
        ///     file system.
        /// </returns>
        /// <param name="virtualPath"> The path to the virtual file. </param>
        public override VirtualFile GetFile(string virtualPath)
        {
            var virtualThemePath = toVirtualThemePath(virtualPath);

            if(virtualPath == virtualThemePath || !base.FileExists(virtualThemePath))
            {
                return base.GetFile(virtualPath);
            }

            var file = base.GetFile(virtualThemePath);
            var wrappedFile = new VirtualThemeFile(file, virtualPath);
            return wrappedFile;
        }

        private string toVirtualThemePath(string originalPath)
        {
            var firstCharacter = originalPath.Substring(0, 1);

            if(firstCharacter != "~" && firstCharacter != "/")
            {
                return originalPath;
            }

            // create the theme directory path
            var themeDirectoryPart = _themeFolder + "/" + _themeName + "/";

            // check if already a theme path
            if(originalPath.Contains(themeDirectoryPart))
            {
                return originalPath;
            }

            var themePath = (firstCharacter == "~")
                ? originalPath.Insert(2, themeDirectoryPart)
                : originalPath.Insert(1, themeDirectoryPart);

            return themePath;
        }
    }
}