using AutoMapper;
using Dto;
using Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StyleService : IStyleService
    {
        private readonly IStyleRepository _styleRepository;
        private readonly IMapper _mapper;

        public StyleService(IStyleRepository styleRepository, IMapper mapper)
        {
            _styleRepository = styleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DtoStyleIdName>> GetStyles()
        {
            var styles = await _styleRepository.GetStyles();
            var styleDtos = _mapper.Map<List<Style>, List<DtoStyleIdName>>(styles);
            return styleDtos;
        }

        public async Task<DtoStyleIdName> AddNewStyle(DtoStyleAll newStyle)
        {
            var styleEntity = _mapper.Map<Style>(newStyle);

            var savedStyle = await _styleRepository.AddNewStyle(styleEntity);
            return _mapper.Map<DtoStyleIdName>(savedStyle);

        }

        public async Task<DtoStyleIdName> Delete(int id)
        {
            var savedStyle = await _styleRepository.Delete(id);
            return _mapper.Map<DtoStyleIdName>(savedStyle);

        }
    }
}

