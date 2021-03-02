using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Http;

namespace Image_box.Models.ImageProcessing
{
    // TODO: Take care of edge cases and implement security considerations and refactor code.
    // TODO: Review Code to increase speed.
    // TODO: remove static and make storage a dependency. Register as service in App.
    public static class ImageProcessor
    {
        public static bool IsSupportedFileExtension(string fileName)
        {


            var ext = Path.GetExtension(fileName)
                .Substring(1);

            var x = MagickNET.SupportedFormats
                .Any(info => info.Format.ToString() == ext);
            


            return !string.IsNullOrEmpty(ext) &&
                   MagickNET.SupportedFormats
                       .Any(format => format.IsReadable && 
                                      format.Format.ToString().Equals(ext, StringComparison.OrdinalIgnoreCase));
        }

        public static async Task<int> CompressAndStore(IFormFile imageFile, IIMageStore storage)
        {
            if (!IsSupportedFileExtension(imageFile.FileName))
            {
                throw new Exception("File extension not supported");

            }
            var compressedImage = await Compress(imageFile);
            await storage.Store(await Compress(imageFile));

            return compressedImage.Length;
        }

        private static async Task<byte[]> Compress(IFormFile imageFile, int maximumFileBytes =
            512000)
        {
            return await Compress(imageFile.OpenReadStream(), maximumFileBytes);
        }

        private static async Task<byte[]> Compress(Stream imageStream,
            int maximumFileBytes = 51200)
        {
            if (!imageStream.CanRead) throw new Exception("Invalid Stream");

            using var image = new MagickImage(imageStream);

            // Set initial Values for image processing.
            // Allow best quality image compression and convert image file to jpeg.
            image.Strip();
            image.Quality = 100;
            // We use JPEG lossy compression, since images are for the web so original file is not needed.
            image.Format = MagickFormat.Jpg;

            var imageSize = imageStream.Length;

            // Image is to large. We reduce the quality of the compression until
            // file size is less than  512kb
            while (imageSize > maximumFileBytes)
            {
                await using (var memoryStream = new MemoryStream())
                {
                    await image.WriteAsync(memoryStream);

                    imageSize = memoryStream.Length;

                    if (imageSize < maximumFileBytes) return memoryStream.ToArray();
                }

                image.Quality--;
            }

            // We can store the image with the highest quality of compression.
            await using (var memoryStream = new MemoryStream())
            {
                await image.WriteAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}