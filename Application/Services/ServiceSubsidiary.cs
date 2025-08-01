using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using ClosedXML.Excel;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application.Services
{
    public class ServiceSubsidiary : IServiceSubsidiary
    {
        private readonly ISubsidiaryRepository _repository;
        private readonly IMapper _mapper;

        public ServiceSubsidiary(ISubsidiaryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public SubsidiaryDto GetById(int id)
        {
            var entity = _repository.GetById(id);
            return _mapper.Map<SubsidiaryDto>(entity);
        }

        public IEnumerable<SubsidiaryDto> GetAll()
        {
            var entities = _repository.GetAll()
                .Where(x => !x.IsDeleted)
                .ToList();

            return _mapper.Map<List<SubsidiaryDto>>(entities);
        }

        public IEnumerable<SubsidiaryDto> FindByIDs(IEnumerable<int> ids)
        {
            var entities = _repository.FindByIDs(ids.ToList())
                .Where(x => !x.IsDeleted)
                .ToList();

            return _mapper.Map<List<SubsidiaryDto>>(entities);
        }

        public BaseDto<SubsidiaryDto> Add(SubsidiaryDto dto)
        {
            var entity = _mapper.Map<Subsidiary>(dto);
            entity.CreatedAt = DateTime.Now;
            entity.IsDeleted = false;

            _repository.Add(entity);
            _repository.Save();

            var createdDto = _mapper.Map<SubsidiaryDto>(entity);
            return new BaseDto<SubsidiaryDto>(true, new List<string>(), createdDto);
        }

        public BaseDto<SubsidiaryDto> Update(SubsidiaryDto dto)
        {
            var entity = _repository.GetById(dto.SubsidiaryId);

            if (entity == null || entity.IsDeleted)
                return new BaseDto<SubsidiaryDto>(false, new List<string> { "Subsidiary not found." }, null);

            // Only update editable fields
            entity.Title = dto.Title;
            entity.Code = dto.Code;
            entity.DebitAmount = dto.DebitAmount;
            entity.CreditAmount = dto.CreditAmount;
            entity.IsLastLevel = dto.IsLastLevel;
            entity.UpdatedAt = DateTime.Now;

            _repository.Update(entity);
            _repository.Save();

            var updatedDto = _mapper.Map<SubsidiaryDto>(entity);
            return new BaseDto<SubsidiaryDto>(true, new List<string>(), updatedDto);
        }

        public bool Remove(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null || entity.IsDeleted)
                return false;

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.Now;

            _repository.Update(entity);
            _repository.Save();
            return true;
        }

        public PaginatedListDto<SubsidiaryDto> GetPaged(SubsidiaryFilterDto filter)
        {
            var query = _repository.GetAll()
                .Where(x => !x.IsDeleted)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(x => x.Title.ToLower().Contains(filter.Title.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Code))
                query = query.Where(x => x.Code.ToLower().Contains(filter.Code.ToLower()));

            var totalCount = query.Count();

            var items = query
                .OrderBy(x => x.Title)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return new PaginatedListDto<SubsidiaryDto>
            {
                TotalCount = totalCount,
                Items = _mapper.Map<List<SubsidiaryDto>>(items)
            };
        }

        public MemoryStream ExportToExcel(string titleFilter = null, string codeFilter = null)
        {
            var query = _repository.GetAll()
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(titleFilter))
                query = query.Where(x => x.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(codeFilter))
                query = query.Where(x => x.Code.Contains(codeFilter, StringComparison.OrdinalIgnoreCase));

            var data = query
                .Select(x => new
                {
                    x.Title,
                    x.Code,
                    Debit = x.DebitAmount,
                    Credit = x.CreditAmount,
                    Created = x.CreatedAt
                })
                .ToList();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Subsidiaries");

            // Header
            worksheet.Cell(1, 1).Value = "Title";
            worksheet.Cell(1, 2).Value = "Code";
            worksheet.Cell(1, 3).Value = "Debit";
            worksheet.Cell(1, 4).Value = "Credit";
            worksheet.Cell(1, 5).Value = "Created Date";

            // Body
            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = data[i].Title;
                worksheet.Cell(i + 2, 2).Value = data[i].Code;
                worksheet.Cell(i + 2, 3).Value = data[i].Debit;
                worksheet.Cell(i + 2, 4).Value = data[i].Credit;
                worksheet.Cell(i + 2, 5).Value = data[i].Created;
            }

            // Formatting
            worksheet.Columns().AdjustToContents();
            worksheet.Column(3).Style.NumberFormat.Format = "#,##0.00"; // Debit
            worksheet.Column(4).Style.NumberFormat.Format = "#,##0.00"; // Credit
            worksheet.Column(5).Style.DateFormat.Format = "yyyy-MM-dd HH:mm";

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }

        
    }
}
