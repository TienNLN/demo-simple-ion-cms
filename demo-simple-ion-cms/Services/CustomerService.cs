using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using demo_simple_ion_cms.Entities;
using demo_simple_ion_cms.IRepositories;
using demo_simple_ion_cms.IServices;
using demo_simple_ion_cms.Models.Common;
using demo_simple_ion_cms.Models.Datas;
using demo_simple_ion_cms.Models.Datas.Customers;
using demo_simple_ion_cms.Models.Datas.FavouriteFoods;
using demo_simple_ion_cms.Models.Payloads.Customers;
using demo_simple_ion_cms.Models.Responses;
using demo_simple_ion_cms.Models.Responses.Customers;
using demo_simple_ion_cms.Models.Responses.FavouriteFoods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace demo_simple_ion_cms.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        
        private readonly ICustomerRepository _customerRepository;

        private readonly IFavouriteFoodService _favouriteFoodService;

        public CustomerService(IMapper mapper, 
            ICustomerRepository customerRepository, 
            IFavouriteFoodService favouriteFoodService)
        {
            _mapper = mapper;

            _customerRepository = customerRepository;

            _favouriteFoodService = favouriteFoodService;
        }
        
        public async Task<GenericResult<CustomerDTO>> CreateCustomer(CustomerCreatePayload payload)
        {
            Log.Information("Start create Customer process.");
            
            var targetCustomer = _mapper.Map<Customer>(payload);
            
            targetCustomer.CreatedTime = DateTime.UtcNow;

            _customerRepository.Create(targetCustomer); // Add record to DbContext
            await _customerRepository.SaveAsync(); // Save DbContext to Database
            
            Log.Information("Customer is created successfully.");
            
            // Favourite Food process
            var favouriteFoodDataModel = new FavouriteFoodCreateRangeDataModel
            {
                CustomerId = targetCustomer.Id, 
                FavouriteFoodNames = payload.FavouriteFoodNames
            };
            
            var targetFavouriteFoods = await _favouriteFoodService.CreateNew(favouriteFoodDataModel);

            var response = _mapper.Map<CustomerDTO>(targetCustomer);
            response.FavouriteFoods = _mapper.Map<List<FavouriteFoodDTO>>(targetFavouriteFoods.Data);

            return GenericResult<CustomerDTO>.Success(response);
        }

        public async Task<GenericResult<List<CustomerDTO>>> GetAll()
        {
            var targetCustomers = await _customerRepository
                .Get()
                .AsNoTracking()
                .ToListAsync();

            var response = _mapper.Map<List<CustomerDTO>>(targetCustomers);

            return GenericResult<List<CustomerDTO>>.Success(response);
        }

        public async Task<GenericResult<CustomerDTO>> GetById(int id)
        {
            var targetCustomer = await _customerRepository
                .Get()
                .FirstOrDefaultAsync(tempCustomer => tempCustomer.Id == id);

            if (targetCustomer == null)
            {
                Log.Error("Customer is not found.");
                
                return GenericResult<CustomerDTO>.Error((int)HttpStatusCode.NotFound, 
                    "Customer is not found.");
            }
            
            var response = _mapper.Map<CustomerDTO>(targetCustomer);

            return GenericResult<CustomerDTO>.Success(response);
        }

        public async Task<GenericResult<CustomerDTO>> Update(CustomerUpdateDataModel dataModel)
        {
            var targetCustomer = await _customerRepository.GetById(dataModel.Id);
            
            if (targetCustomer == null)
            {
                Log.Error("Customer is not found.");
                
                return GenericResult<CustomerDTO>.Error((int)HttpStatusCode.NotFound, 
                    "Customer is not found.");
            }

            _mapper.Map(dataModel, targetCustomer);

            _customerRepository.Update(targetCustomer); // Update record in DbContext
            await _customerRepository.SaveAsync(); // Save DbContext to Database

            var response = _mapper.Map<CustomerDTO>(targetCustomer);

            return GenericResult<CustomerDTO>.Success(response);
        }
    }
}