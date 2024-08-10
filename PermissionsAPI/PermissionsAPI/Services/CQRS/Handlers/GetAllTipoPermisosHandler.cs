using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.DTOs;

public class GetAllTipoPermisosHandler : IRequestHandler<GetAllTipoPermisosQuery, IEnumerable<TipoPermisoDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllTipoPermisosHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TipoPermisoDto>> Handle(GetAllTipoPermisosQuery request, CancellationToken cancellationToken)
    {
        var tipoPermisos = await _unitOfWork.TipoPermisoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TipoPermisoDto>>(tipoPermisos);
    }
}