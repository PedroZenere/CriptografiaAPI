using CriptografiaAPI.Application.Operations.ViewModel;
using CriptografiaAPI.Criptografar;
using CriptografiaAPI.Domain.Operations;
using CriptografiaAPI.Infra.Context;
using CriptografiaAPI.Infra.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CriptografiaAPI.Application.Operations.Command
{
    public class CreateOperationCommand : IRequest<OperationViewModel>
    {
        public int? Id { get; set; }
        public string UserDocument { get; set; }
        public string CreditCard { get; set; }
        public int Value { get; set; }
    }

    public sealed class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, OperationViewModel>
    {
        private readonly DbSet<Operation> _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICriptografiaService _service;

        public CreateOperationCommandHandler(IApplicationContext context, IUnitOfWork unitOfWork, ICriptografiaService service)
        {
            _dbSet = context.Set<Operation>();
            _unitOfWork = unitOfWork;
            _service = service;
        }

        public async Task<OperationViewModel> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            var operation = new Operation(
                _service.EncryptString(request.UserDocument),
                _service.EncryptString(request.CreditCard),
                request.Value
            );
           
            var result = !request.Id.HasValue ?
                await _dbSet.AddAsync(operation, cancellationToken) :
                _dbSet.Update(operation);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new OperationViewModel(result.Entity.Id, result.Entity.UserDocument, result.Entity.CreditCard, result.Entity.Value);
        }
    }
}
