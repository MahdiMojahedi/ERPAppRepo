using Application.Dtos;
using Application.Services;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceMaster
    {

        MasterDto GetById(int id);
        IEnumerable<MasterDto> GetAll();
        IEnumerable<MasterDto> FindByIDs(IEnumerable<int> ids);
        BaseDto<MasterDto> Add(MasterDto dto);
        BaseDto<MasterDto> Update(MasterDto dto);
        bool Remove(int id);
        MemoryStream ExportToExcel(string titleFilter = null, string codeFilter = null);
    }
    public class MasterDto
    {
        public int MasterId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(20)]
        public string Code { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedByPersonId { get; set; }
    }


}
