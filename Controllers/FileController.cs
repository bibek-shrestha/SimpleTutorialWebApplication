using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace SimpleTutorialWebApplication.Controllers;

[ApiController]
[Route("api/files")]
public class FileController: ControllerBase
{
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

    public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    {
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
            ?? throw new ArgumentNullException(nameof(fileExtensionContentTypeProvider));
    }

    [HttpGet]
    public ActionResult GetFile()
    {
        var filePath = "Bibek_Shrestha_Resume_e.pdf";
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }
        if (!_fileExtensionContentTypeProvider.TryGetContentType(filePath, out var contentType))
        {
            contentType = "application/octet-stream";
        }
        var bytes = System.IO.File.ReadAllBytes(filePath);
        return File(bytes, contentType, Path.GetFileName(filePath));
    }
}
