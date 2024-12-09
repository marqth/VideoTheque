using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Hosts;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("hosts")]
    public class HostsController : ControllerBase
    {
        private readonly IHostsBusiness _hostsBusiness;

        public HostsController(IHostsBusiness hostsBusiness)
        {
            _hostsBusiness = hostsBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<List<HostViewModel>>> GetHosts()
        {
            var hosts = await _hostsBusiness.GetHosts();
            return Ok(hosts.Select(HostViewModel.ToModel).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<HostViewModel> GetHost(int id)
        {
            var host = _hostsBusiness.GetHost(id);
            return Ok(HostViewModel.ToModel(host));
        }

        [HttpPost]
        public async Task<ActionResult> InsertHost([FromBody] HostViewModel hostViewModel)
        {
            var hostDto = hostViewModel.ToDto();
            await _hostsBusiness.InsertHost(hostDto);
            return CreatedAtAction(nameof(GetHost), new { id = hostDto.Id }, hostDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHost(int id, [FromBody] HostViewModel hostViewModel)
        {
            var hostDto = hostViewModel.ToDto();
            await _hostsBusiness.UpdateHost(id, hostDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHost(int id)
        {
            await _hostsBusiness.DeleteHost(id);
            return NoContent();
        }
    }
}