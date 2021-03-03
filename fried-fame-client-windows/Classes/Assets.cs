using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fried_fame_client_windows.Classes
{
    class Assets
    {
        /// <summary>
        /// Asynchronously loads an image.
        /// </summary>
        /// <param name="name">Name of image within assets/images folder</param>
        /// <returns>Image</returns>
        public static async Task<Image> LoadImageAsync(string name)
        {
            return await Task.Run<Image>(delegate ()
            {
                return Assets.LoadImage(name);
            });
        }

        public static Image LoadImage(string name)
        {
            Classes.Logging.Info("Loading Image " + name);
            return Image.FromFile(Path.Combine(
                Environment.CurrentDirectory, "assets\\images", name));
        }

        /// <summary>
        /// Asynchronously loads an image.
        /// </summary>
        /// <param name="name">Name of image within assets/images folder</param>
        /// <returns>Image</returns>
        public static async Task<Icon> LoadIconAsync(string name)
        {
            return await Task.Run<Icon>(delegate ()
            {
                return Assets.LoadIcon(name);
            });
        }

        public static Icon LoadIcon(string name)
        {
            Classes.Logging.Info("Loading Icon " + name);
            return Icon.ExtractAssociatedIcon(Path.Combine(
                Environment.CurrentDirectory, "assets\\images", name));
        }
    }
}
