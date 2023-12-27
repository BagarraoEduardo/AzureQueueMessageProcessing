using Microsoft.AspNetCore.Http;
using ReaderAPI.Integration.ParserAPI;

namespace ReaderAPI.Business;

public interface IFileHandler
{
    Task<List<FileInfo>> GetAllFiles();
}
