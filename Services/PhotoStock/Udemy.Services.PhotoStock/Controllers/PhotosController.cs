using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy.Services.PhotoStock.Dtos;
using Udemy.Shared.ControllerBases;
using Udemy.Shared.Dtos;

namespace Udemy.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file,CancellationToken cancellationToken)
        {
            if (file!=null&&file.Length>0)
            {
                string path = System.IO.Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Photos",file.FileName);
                using (var str=new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(str, cancellationToken);
                }
                var returnPath = "Photos/" + file.FileName;

                PhotoDto photoDto = new() { Url=returnPath};
                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty",400));
        }

        [HttpDelete]
        public IActionResult Delete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Photos",photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo is not found", 404));

            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
