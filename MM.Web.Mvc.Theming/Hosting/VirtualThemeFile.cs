using System.IO;
using System.Web.Hosting;

namespace MM.Web.Mvc.Theming.Hosting
{
    public class VirtualThemeFile : VirtualFile
    {
        private readonly VirtualFile _virtualFile;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Web.Hosting.VirtualFile" /> class.
        /// </summary>
        /// <param name="virtualFile"> </param>
        /// <param name="originallyRequestedVirtualPath"> </param>
        public VirtualThemeFile(VirtualFile virtualFile, string originallyRequestedVirtualPath)
            : base(originallyRequestedVirtualPath)
        {
            _virtualFile = virtualFile;
        }

        /// <summary>
        ///     When overridden in a derived class, returns a read-only stream to the virtual resource.
        /// </summary>
        /// <returns>
        ///     A read-only stream to the virtual file.
        /// </returns>
        public override Stream Open()
        {
            return _virtualFile.Open();
        }
    }
}