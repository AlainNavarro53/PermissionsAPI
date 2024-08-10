using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Models;

public class CreateTipoPermisoHandler : IRequestHandler<CreateTipoPermisoCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTipoPermisoHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateTipoPermisoCommand request, CancellationToken cancellationToken)
    {
        var tipoPermiso = _mapper.Map<TipoPermiso>(request);
        await _unitOfWork.TipoPermisoRepository.AddAsync(tipoPermiso);
        await _unitOfWork.CompleteAsync();

        return tipoPermiso.Id;
    }
}
