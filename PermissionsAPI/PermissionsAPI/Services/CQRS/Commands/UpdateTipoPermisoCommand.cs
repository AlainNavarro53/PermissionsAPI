using MediatR;

public class UpdateTipoPermisoCommand : IRequest
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
}
