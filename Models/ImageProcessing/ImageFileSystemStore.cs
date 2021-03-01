using System;
using System.IO;
using System.Threading.Tasks;

namespace Image_box.Models.ImageProcessing
{
    
    public class ImageFileSystemStore : IIMageStore
    {
        private readonly int _maxFileSize;

        private readonly string _storageDirectoryPath;

        public ImageFileSystemStore(string storageDirectoryPath, int maxFileSize)
        {
            _storageDirectoryPath = storageDirectoryPath;

            _maxFileSize = maxFileSize;
        }

        public async Task Store(byte[] image)
        {
            if (image.Length > _maxFileSize) throw new Exception("Image file size is to large");

            var filePath = Path.Combine(_storageDirectoryPath, Path.GetRandomFileName() + ".jpeg");
            
            await File.WriteAllBytesAsync(filePath, image);
        }
    }
}