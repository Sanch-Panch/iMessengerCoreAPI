using iMessengerCoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace iMessengerCoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class iMessengerCoreAPIController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get([FromQuery] List<Guid> clients)
        {
            RGDialogsClients rGDialogsClients = new RGDialogsClients();
            List<RGDialogsClients> information = rGDialogsClients.Init();
            var makeAllZeroGuID = new Guid();
            Guid result = new Guid();

            var dictionary = new Dictionary<Guid, List<Guid>>();

            for (int i = 0; i < information.Count; i++)
            {
                var key = information[i].IDRGDialog;
                if (!dictionary.ContainsKey(key)) dictionary[key] = new List<Guid>();
                dictionary[key].Add(information[i].IDClient);
            }

            foreach (var dialog in dictionary)
            {
                if (clients.SequenceEqual(dialog.Value))
                {
                    result = dialog.Key;
                    Console.WriteLine(dialog.Key);
                    return Ok(dialog.Key);
                }
            }
            if (result.CompareTo(makeAllZeroGuID) == 0)
            {
                Console.WriteLine(makeAllZeroGuID);
                return Ok(makeAllZeroGuID);
            }
            return Created("", "Done");
        }
    }
}
