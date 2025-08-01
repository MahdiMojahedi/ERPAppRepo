using Application.Dtos;
using Application.Services;
using Entities;
using IDP.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceSubsidiary
    {
        SubsidiaryDto GetById(int id);
        IEnumerable<SubsidiaryDto> GetAll();
        IEnumerable<SubsidiaryDto> FindByIDs(IEnumerable<int> ids);
        BaseDto<SubsidiaryDto> Add(SubsidiaryDto dto);
        BaseDto<SubsidiaryDto> Update(SubsidiaryDto dto);
        bool Remove(int id);
        MemoryStream ExportToExcel(string titleFilter = null, string codeFilter = null);

    }
    public class SubsidiaryDto
    {
        public int SubsidiaryId { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }

        public bool IsLastLevel { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int MasterId { get; set; }

        public int CreatedByPersonId { get; set; }

        public int? ParentSubsidiaryId { get; set; }
    }
}
