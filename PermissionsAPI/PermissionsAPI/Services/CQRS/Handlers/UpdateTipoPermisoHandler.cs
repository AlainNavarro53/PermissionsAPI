using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Models;

public class UpdateTipoPermisoHandler : IRequestHandler<UpdateTipoPermisoCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTipoPermisoHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTipoPermisoCommand request, CancellationToken cancellationToken)
    {
        var tipoPermiso = await _unitOfWork.TipoPermisoRepository.GetByIdAsync(request.Id);

        if (tipoPermiso == null)
        {
            throw new KeyNotFoundException("Tipo de permiso no encontrado.");
        }

        _mapper.Map(request, tipoPermiso);
        _unitOfWork.TipoPermisoRepository.Update(tipoPermiso);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}
