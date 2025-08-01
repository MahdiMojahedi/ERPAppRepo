using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorFitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterController : ControllerBase
    {
        private readonly IServiceMaster _masterService;
        private readonly IHiracicalReportService _hierarchyService;
        public MasterController(IServiceMaster masterService, IHiracicalReportService hierarchyService)
        {
            _masterService = masterService;
            _hierarchyService = hierarchyService;
        }

        [HttpGet]
        public ActionResult<BaseListItemDto<MasterDto>> GetAll()
        {
            var list = _masterService.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<BaseDto<MasterDto>> GetById(int id)
        {
            var listResult = _masterService.FindByIDs(new List<int> { id });
            if (listResult == null || listResult.Any())
                return NotFound(new BaseDto<MasterDto>(false, new List<string> { "Item not found" }, null));

            return Ok(new BaseDto<MasterDto>(true, new List<string>(), listResult.First()));
        }

        [HttpPost]
        public ActionResult<BaseDto<MasterDto>> Create([FromBody] MasterDto dto)
        {
            var result = _masterService.Add(dto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.MasterId }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<BaseDto<MasterDto>> Update(int id, [FromBody] MasterDto dto)
        {
            if (id != dto.MasterId)
                return BadRequest(new BaseDto<MasterDto>(false, new List<string> { "Id mismatch" }, null));

            var result = _masterService.Update(dto);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseDto<MasterDto>> Delete(int id)
        {
            var result = _masterService.Remove(id);
            if (!result)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("export")]
        public IActionResult ExportToExcel([FromQuery] string title, [FromQuery] string code)
        {
            var stream = _masterService.ExportToExcel(title, code);
            return File(stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Masters.xlsx");
        }

        [HttpGet("report/hierarchy")]
        public async Task<IActionResult> GetHierarchyReport()
        {
            var report = await _hierarchyService.GetHierarchyReportAsync();
            return Ok(report);
        }
    }
}
