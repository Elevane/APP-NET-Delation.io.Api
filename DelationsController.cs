using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Delation.io.Api
{
    [ApiController]
    [Route("api/delations")]
    public class DelationsController : Controller
    {
        internal class DelationsList
        {
            public List<Delation> Delations { get; set; }
        }
        


        [HttpGet]
        public async Task<IActionResult> Get()
        {
                return Ok( await getJson());  
        }
        private async Task<DelationsList> getJson()
        {
            DelationsList _list = new DelationsList();
            using (StreamReader r = new StreamReader("db.json"))
            {
                string json = r.ReadToEnd();
                _list = JsonConvert.DeserializeObject<DelationsList>(json);
                return _list;
            }
               
        }
        [HttpPost]
        public async Task<IActionResult> Post(Delation delation)
        {
            DelationsList _list = await getJson();
            if (_list == null)
                return NotFound("json not found");
            using (StreamWriter file = new StreamWriter("db.json"))
            {
                _list.Delations.Add(delation);
                string jsonList = JsonConvert.SerializeObject(_list);
                file.Write(jsonList);
            }
            return Ok();
        }
        
       

       
    }
}
