using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using ClosedXML.Excel;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceMaster : IServiceMaster
    {
        private readonly IMasterRepository _masterRepository;
        private readonly IMapper _mapper;

        public ServiceMaster(IMasterRepository masterRepository, IMapper mapper)
        {
            _masterRepository = masterRepository;
            _mapper = mapper;
        }

        public MasterDto GetById(int id)
        {
            var entity = _masterRepository.GetById(id);
            return _mapper.Map<MasterDto>(entity);
        }

        public IEnumerable<MasterDto> GetAll()
        {
            var entities = _masterRepository.GetAll().ToList();
            return _mapper.Map<List<MasterDto>>(entities);
        }

        public IEnumerable<MasterDto> FindByIDs(IEnumerable<int> ids)
        {
            var entities = _masterRepository.FindByIDs(ids.ToList());
            return _mapper.Map<List<MasterDto>>(entities);
        }

        public BaseDto<MasterDto> Add(MasterDto dto)
        {
            var entity = _mapper.Map<Master>(dto);
            _masterRepository.Add(entity);
            _masterRepository.Save();

            var createdDto = _mapper.Map<MasterDto>(entity);
            return new BaseDto<MasterDto>(true, new List<string>(), createdDto);
        }

        public BaseDto<MasterDto> Update(MasterDto dto)
        {
            var entity = _mapper.Map<Master>(dto);
            _masterRepository.Update(entity);
            _masterRepository.Save();

            var updatedDto = _mapper.Map<MasterDto>(entity);
            return new BaseDto<MasterDto>(true, new List<string>(), updatedDto);
        }

        public bool Remove(int id)
        {
            var entity = _masterRepository.GetById(id);
            if (entity == null)
                return false;

            _masterRepository.Remove(entity);
            _masterRepository.Save();
            return true;
        }

        public PaginatedListDto<MasterDto> GetPaged(MasterFilterDto filter)
        {
            var query = _masterRepository.GetAll().AsNoTracking();

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

            return new PaginatedListDto<MasterDto>
            {
                TotalCount = totalCount,
                Items = _mapper.Map<List<MasterDto>>(items)
            };
        }


        public MemoryStream ExportToExcel(string titleFilter = null, string codeFilter = null)
        {
            var query = _masterRepository.GetAll().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(titleFilter))
                query = query.Where(m => m.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(codeFilter))
                query = query.Where(m => m.Code.Contains(codeFilter, StringComparison.OrdinalIgnoreCase));

            var data = query
                .Select(m => new
                {
                    m.Title,
                    m.Code,
                    m.CreatedAt
                })
                .ToList();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Masters");

            // Header
            worksheet.Cell(1, 1).Value = "Title";
            worksheet.Cell(1, 2).Value = "Code";
            worksheet.Cell(1, 5).Value = "CreatedAt";

            // Body
            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = data[i].Title;
                worksheet.Cell(i + 2, 2).Value = data[i].Code;
                worksheet.Cell(i + 2, 5).Value = data[i].CreatedAt;
            }

            // Formatting
            worksheet.Columns().AdjustToContents();
            worksheet.Column(5).Style.DateFormat.Format = "yyyy-MM-dd HH:mm";

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }

    }

}
