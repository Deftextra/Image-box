using System.Threading.Tasks;

namespace Image_box.Models.ImageProcessing
{
    
    // Image Storage medium
    public interface IIMageStore
    {
        public Task Store(byte[] imageFile);
    }
}