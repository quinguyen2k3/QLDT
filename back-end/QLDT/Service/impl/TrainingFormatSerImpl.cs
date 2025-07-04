using AutoMapper;
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
    }


}
