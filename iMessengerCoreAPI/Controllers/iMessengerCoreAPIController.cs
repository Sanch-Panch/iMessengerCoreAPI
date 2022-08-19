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
            var dialogs_temp = new Dictionary<Guid, int>();
            var dialogs_result = new Dictionary<Guid, List<Guid>>();
            List<Guid> temp = new List<Guid>();
            var makeAllZeroGuID = new Guid();
            List<Guid> client;
            Guid result = new Guid();

            foreach (RGDialogsClients count in information)
            {
                temp.Add(count.IDClient);
                if (dialogs_temp.ContainsKey(count.IDRGDialog)) dialogs_temp[count.IDRGDialog]++;
                else dialogs_temp.Add(count.IDRGDialog, 1);
            }

            int idt = 0;
            int mng = 0;
            foreach (var person in dialogs_temp)
            {
                List<Guid> temp_result = new List<Guid>();
                mng = mng + person.Value;
                for (int i = idt; i < mng; i++)
                {
                    temp_result.Add(temp[i]);
                    idt++;
                }
                dialogs_result.Add(person.Key, temp_result);
            }

            foreach (var person_result in dialogs_result)
            {
                if (dialogs_result.TryGetValue(person_result.Key, out client))
                {
                    if (clients.SequenceEqual(client))
                    {
                        result = person_result.Key;
                        Console.WriteLine(person_result.Key);
                        return Ok(person_result.Key);
                    }
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
