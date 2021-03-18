namespace Image_box.Models.SecureAttachmentsDownload.Services
{
    public interface ISecureUrlGenerator
    {
        string GenerateSecureAttachmentUrl(string id, string url);
        bool ValidateUrl(string url, string id);
        bool ValidateToken(string id, string token);
    }
}