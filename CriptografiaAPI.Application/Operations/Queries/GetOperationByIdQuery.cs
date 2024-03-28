using CriptografiaAPI.Application.Operations.ViewModel;
using CriptografiaAPI.Criptografar;
using CriptografiaAPI.Domain.Operations;
using CriptografiaAPI.Infra.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CriptografiaAPI.Application.Operations.Queries
{
    public class GetOperationByIdQuery : IRequest<OperationViewModel>
    {
        public int Id { get; set; }
    }

    public sealed class GetOperationByIdQueryHandler : IRequestHandler<GetOperationByIdQuery, OperationViewModel>
    {
        private readonly DbSet<Operation> _dbSet;
        private readonly ICriptografiaService _service;

        public GetOperationByIdQueryHandler(IApplicationContext context, ICriptografiaService service)
        {
            _dbSet = context.Set<Operation>();
            _service = service;
        }
        public async Task<OperationViewModel> Handle(GetOperationByIdQuery request, CancellationToken cancellationToken)
        {
            var result =  await _dbSet.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return new OperationViewModel(result.Id, _service.DecryptString(result.UserDocument), _service.DecryptString(result.CreditCard), result.Value);
        }
    }
}
