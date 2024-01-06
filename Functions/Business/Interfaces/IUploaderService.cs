namespace Functions.Business.Interfaces;

public interface IUploaderService
{
    Task<(bool Success, string ErrorMessage)> UploadFiles();
}
