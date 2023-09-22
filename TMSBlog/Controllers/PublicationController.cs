using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TMSBlog.ApiResult;
using TMSBlog.Models;

namespace TMSBlog.Controllers
{
    public class PublicationController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _filePath = "C:\\repos\\TMSBlog\\TMSBlog\\Storage\\Publication.json";
        public PublicationController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ApiResult<PageDto<Publication>> GetAll()
        {
            string jsonText = string.Empty;
            if (System.IO.File.Exists(_filePath))
                jsonText = System.IO.File.ReadAllText(_filePath);

            var result = JsonConvert.DeserializeObject<List<Publication>>(jsonText);

            var apiResult = new PageDto<Publication>()
            {
                Items = result.ToArray(),
                TotalCount = result.Count()
            };

            return apiResult.ToApiResult();
        }

        [HttpGet]
        public ApiResult<Publication> Get(long id)
        {
            string jsonText = string.Empty;
            if (System.IO.File.Exists(_filePath))
                jsonText = System.IO.File.ReadAllText(_filePath);

            var result = JsonConvert.DeserializeObject<List<Publication>>(jsonText);
            var publication = result.Where(it => it.Id == id).FirstOrDefault();
            if(publication == null)
            {
                return ApiResult<Publication>.CreateFailed(
                           Errors.BadRequest.Code,
                           $"Publication # {id} not found");
            }

            var apiResult = new Publication()
            {
                Id = publication.Id,
                Name = publication.Name,
                Description = publication.Description,
                Content = publication.Content,
            };

            return apiResult.ToApiResult();
        }

        [HttpPost]
        public ApiResult<Publication> Post([FromBody]Publication publication)
        {
            string jsonText = string.Empty;
            if (System.IO.File.Exists(_filePath))
                jsonText = System.IO.File.ReadAllText(_filePath);

            var result = JsonConvert.DeserializeObject<List<Publication>>(jsonText);
            if(result == null)
            {
                result = new List<Publication>();
            }
            result.Add(publication);

            var publicationJson = JsonConvert.SerializeObject(result, Formatting.Indented);
            System.IO.File.WriteAllText(_filePath, publicationJson);

            return publication.ToApiResult();
        }
    }
}
