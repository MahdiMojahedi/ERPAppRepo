using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorFitAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubsidiaryController : ControllerBase
    {
        private readonly IServiceSubsidiary _SubsidiaryService;

        public SubsidiaryController(IServiceSubsidiary SubsidiaryService)
        {
            _SubsidiaryService = SubsidiaryService;
        }

        [HttpGet]
        public ActionResult<BaseListItemDto<SubsidiaryDto>> GetAll()
        {
            var list = _SubsidiaryService.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<BaseDto<SubsidiaryDto>> GetById(int id)
        {
            var listResult = _SubsidiaryService.FindByIDs(new List<int> { id });
            if (listResult == null || listResult.Any())
                return NotFound(new BaseDto<SubsidiaryDto>(false, new List<string> { "Item not found" }, null));

            return Ok(new BaseDto<SubsidiaryDto>(true, new List<string>(), listResult.First()));
        }

        [HttpPost]
        public ActionResult<BaseDto<SubsidiaryDto>> Create([FromBody] SubsidiaryDto dto)
        {
            var result = _SubsidiaryService.Add(dto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data.SubsidiaryId }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<BaseDto<SubsidiaryDto>> Update(int id, [FromBody] SubsidiaryDto dto)
        {
            if (id != dto.SubsidiaryId)
                return BadRequest(new BaseDto<SubsidiaryDto>(false, new List<string> { "Id mismatch" }, null));

            var result = _SubsidiaryService.Update(dto);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseDto<SubsidiaryDto>> Delete(int id)
        {
            var result = _SubsidiaryService.Remove(id);
            if (!result)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("export")]
        public IActionResult ExportToExcel([FromQuery] string title, [FromQuery] string code)
        {
            var stream = _SubsidiaryService.ExportToExcel(title, code);
            return File(stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Subsidiarys.xlsx");
        }
    }


}
