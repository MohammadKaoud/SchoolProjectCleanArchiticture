using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolProjectCleanArchiticture.Services.Abstract;

public  class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
    }

    public async Task<string> UploadImage(string location, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return "NotImage";

        if (string.IsNullOrEmpty(location))
            throw new ArgumentNullException(nameof(location), "Location cannot be null or empty.");

        var webRootPath = _webHostEnvironment.WebRootPath;
        if (string.IsNullOrEmpty(webRootPath))
            throw new ArgumentNullException(nameof(_webHostEnvironment.WebRootPath), "WebRootPath is null or empty.");

        var path = Path.Combine(webRootPath, location);
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(path, fileName);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        using (var fileToCreate = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(fileToCreate);
            await fileToCreate.FlushAsync();
        }

        return $"{location}/{fileName}";
    }
}
