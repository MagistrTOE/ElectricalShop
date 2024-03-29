﻿using MediatR;
using MyElectricalShop.Domain.Interfaces;
using Core.Exceptions;

namespace MyElectricalShop.Application.ActionMethods.Companies.Delete
{
    public class DeleteCompanyRequest : IRequest
    {
        public int Id { get; }

        public DeleteCompanyRequest(int id)
        {
            Id = id;
        }
    }

    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyRequest>
    {
        private readonly ICompanyRepository _companyRepository;

        public DeleteCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Unit> Handle(DeleteCompanyRequest request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetById(request.Id);
            if (company == null)
                throw new ArgumentNotFoundException($"Сущность с заданным Id: {request.Id} не найдена.");

            await _companyRepository.Delete(company);

            return Unit.Value;
        }
    }
}
