using MediatR;
using MyElectricalShop.Domain.Interfaces;

namespace MyElectricalShop.Application.ActionMethods.Products.Delete
{
    public class DeleteProductRequest : IRequest
    {
        public Guid Id { get; }

        public DeleteProductRequest(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteProductHandler : IRequestHandler<DeleteProductRequest>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id);
            await _productRepository.Delete(product);

            return Unit.Value;
        }
    }
}
