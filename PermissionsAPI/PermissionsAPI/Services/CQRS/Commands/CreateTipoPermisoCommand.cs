using MediatR;

public class CreateTipoPermisoCommand : IRequest<int>
{
    public string Descripcion { get; set; }
}