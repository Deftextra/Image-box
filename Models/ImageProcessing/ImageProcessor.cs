using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Http;

namespace Image_box.Models.ImageProcessing
{
    // TODO: Refactor and move to separate files.

    // Image Storage medium
    public interface IIMageStore
    {
        public Task Store(byte[] imageFile);
    }

    // Store Image file in File system.
    public class ImageFileSystemStore : IIMageStore
    {
        private readonly string _storageDirectoryPath;

        public ImageFileSystemStore(string storageDirectoryPath)
        {
            _storageDirectoryPath = storageDirectoryPath;
        }

        public async Task Store(byte[] image)
        {
            var filePath = Path.Combine(_storageDirectoryPath, Path.GetRandomFileName() + ".jpeg");

            await File.WriteAllBytesAsync(filePath, image);
        }
    }


    // TODO: Take care of edge cases and implement security considerations and refactor code.
    // TODO: Review Code to increase speed.
    public static class ImageProcessor
    {
        public static async Task<int> CompressAndStore(IFormFile imageFile, IIMageStore storage)
        {
            var compressedImage = await Compress(imageFile);
            
            await storage.Store(await Compress(imageFile));

            return compressedImage.Length;
        }


        private static async Task<byte[]> Compress(IFormFile imageFile, int maximumFileBytes =
            512000)
        {
            return await Compress(imageFile.OpenReadStream(), maximumFileBytes);
        }

        private static async Task<byte[]> Compress(Stream imageStream, int maximumFileBytes = 51200)
        {
            if (!imageStream.CanRead) throw new Exception("Invalid Stream");

            using var image = new MagickImage(imageStream);
            var optimiser = new ImageOptimizer();

            // Set initial Values for image processing.
            // Allow best quality image compression and convert image file to jpeg.
            image.Strip();
            image.Quality = 100;
            // We use JPEG lossy compression, since images are for the web so original file is not
            // needed.
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