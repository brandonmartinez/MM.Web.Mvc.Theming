using System.Collections;
using System.Web.Hosting;

namespace MM.Web.Mvc.Theming.Hosting
{
    public class VirtualThemeDirectory : VirtualDirectory
    {
        private readonly VirtualDirectory _virtualDirectory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Web.Hosting.VirtualDirectory" /> class.
        /// </summary>
        /// <param name="virtualDirectory"> </param>
        /// <param name="originallyRequestedVirtualPath"> </param>
        public VirtualThemeDirectory(VirtualDirectory virtualDirectory, string originallyRequestedVirtualPath)
            : base(originallyRequestedVirtualPath)
        {
            // TODO: we should probably take in *both* the theme and default dir, then merge the files somehow
            _virtualDirectory = virtualDirectory;
        }

        /// <summary>
        ///     Gets a list of all the subdirectories contained in this directory.
        /// </summary>
        /// <returns>
        ///     An object implementing the <see cref="T:System.Collections.IEnumerable" /> interface containing
        ///     <see cref="T:System.Web.Hosting.VirtualDirectory" /> objects.
        /// </returns>
        public override IEnumerable Directories
        {
            get
            {
                return _virtualDirectory.Directories;
            }
        }

        /// <summary>
        ///     Gets a list of all files contained in this directory.
        /// </summary>
        /// <returns>
        ///     An object implementing the <see cref="T:System.Collections.IEnumerable" /> interface containing
        ///     <see cref="T:System.Web.Hosting.VirtualFile" /> objects.
        /// </returns>
        public override IEnumerable Files
        {
            get
            {
                return _virtualDirectory.Files;
            }
        }

        /// <summary>
        ///     Gets a list of the files and subdirectories contained in this virtual directory.
        /// </summary>
        /// <returns>
        ///     An object implementing the <see cref="T:System.Collections.IEnumerable" /> interface containing
        ///     <see cref="T:System.Web.Hosting.VirtualFile" /> and <see cref="T:System.Web.Hosting.VirtualDirectory" /> objects.
        /// </returns>
        public override IEnumerable Children
        {
            get
            {
                return _virtualDirectory.Children;
            }
        }
    }
}