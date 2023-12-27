using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ReaderAPI.Integration.ParserAPI;
using FileOptions = ReaderAPI.Utility.FileOptions;

namespace ReaderAPI.Business;

public class FileHandler : IFileHandler
{
    private readonly FileOptions _options;

    public FileHandler(IOptions<FileOptions> options)
    {
        _options = options.Value;
    }

    public async Task<List<FileInfo>> GetAllFiles()
    {
        var directoryInfo = new DirectoryInfo(_options.Path);

        return directoryInfo.GetFiles().ToList();
    }
}
