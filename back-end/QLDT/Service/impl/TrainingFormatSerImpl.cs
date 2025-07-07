using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Models;
using QLDT.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Service.impl
{
    public class TrainingFormatSerImpl : TrainingFormatSer
    {
        private readonly TrainingFormatRepo _repository;
        private readonly IMapper _mapper;

        public TrainingFormatSerImpl(TrainingFormatRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainingFormatRes>> GetAllAsync()
        {        
            var entities = await _repository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<TrainingFormatRes>>(entities);

            return result;
        }

        public async Task<TrainingFormatRes> CreateAsync(TrainingFormatReq request)
        {
     
            var entity = _mapper.Map<TrainingFormat>(request);

            entity.CreatedDate = request.CreatedDate ?? DateTime.Now;

            //Test
            entity.CreatedBy = "admin";

            //Test
            entity.CreatedDate = request.CreatedDate ?? DateTime.Now;
            entity.CreatedBy = "admin";

            //Test
            entity.ModifiedDate = entity.CreatedDate;
            entity.ModifiedBy = entity.CreatedBy;

            var createdEntity = await _repository.CreateAsync(entity);

            var result = _mapper.Map<TrainingFormatRes>(createdEntity);

            return result;
        }

        public async Task<TrainingFormatRes?> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return null; 
            }
            return _mapper.Map<TrainingFormatRes>(entity);
        }

        public async Task<TrainingFormatRes?> UpdateAsync(long id, TrainingFormatReq request)
        {
            
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                return null; //
            }

            
            existing.Name = request.Name;
            existing.Note = request.Note;
            existing.CreatedDate = request.CreatedDate;

            //Test
            existing.ModifiedDate = DateTime.Now;
            existing.ModifiedBy = "admin";

            // Lưu lại
            var updatedEntity = await _repository.UpdateAsync(existing);

            // Trả về DTO
            return _mapper.Map<TrainingFormatRes>(updatedEntity);
        }
    }


}
