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

            Delation exist = _list.Delations.FirstOrDefault(d => d.Id == delation.Id);
            if (exist != null)
                return BadRequest("Delation given to create already exist");
            using (StreamWriter file = new StreamWriter("db.json"))
            {
                _list.Delations.Add(delation);
                string jsonList = JsonConvert.SerializeObject(_list);
                file.Write(jsonList);
            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(string id)
        {
            DelationsList _list = await getJson();
            if (_list == null)
                return NotFound("json not found");
            using (StreamWriter file = new StreamWriter("db.json"))
            {
                Delation toDelete = _list.Delations.FirstOrDefault(d => d.Id == id);
                if(toDelete != null)
                     _list.Delations.Remove(toDelete);
                string jsonList = JsonConvert.SerializeObject(_list);
                file.Write(jsonList);
            }
            return Ok();
        }

        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            DelationsList _list = await getJson();
            if (_list == null)
                return NotFound("json not found");
            using (StreamWriter file = new StreamWriter("db.json"))
            {
                List<Delation> toDelete = _list.Delations.ToList();
                if (toDelete != null)
                {
                    foreach (var del in toDelete) _list.Delations.Remove(del);
                }
                string jsonList = JsonConvert.SerializeObject(_list);
                file.Write(jsonList);
            }
            return Ok();
        }

        [HttpDelete("deleteByName/{name}")]
        public async Task<IActionResult> DeleteAll(string name)
        {
            DelationsList _list = await getJson();
            if (_list == null)
                return NotFound("json not found");
            using (StreamWriter file = new StreamWriter("db.json"))
            {
                List<Delation> toDelete = _list.Delations.Where(d => d.name == name).ToList();
                if (toDelete != null)
                {
                    foreach (var del in toDelete) _list.Delations.Remove(del);
                }
                string jsonList = JsonConvert.SerializeObject(_list);
                file.Write(jsonList);
            }
            return Ok();
        }




    }
}
