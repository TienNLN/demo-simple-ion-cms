using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using demo_simple_ion_cms.Constants;
using demo_simple_ion_cms.Entities;
using demo_simple_ion_cms.IRepositories;
using demo_simple_ion_cms.IServices;
using demo_simple_ion_cms.Models.Common;
using demo_simple_ion_cms.Models.Datas.FavouriteFoods;
using demo_simple_ion_cms.Models.Responses.FavouriteFoods;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace demo_simple_ion_cms.Services
{
    public class FavouriteFoodService : IFavouriteFoodService
    {
        private readonly IMapper _mapper;

        private readonly IFavouriteFoodRepository _favouriteFoodRepository;

        public FavouriteFoodService(IMapper mapper,
            IFavouriteFoodRepository favouriteFoodRepository)
        {
            _mapper = mapper;

            _favouriteFoodRepository = favouriteFoodRepository;
        }

        public async Task<GenericResult<FavouriteFoodDTO>> GetById(int id)
        {
            var targetFavouriteFood = await _favouriteFoodRepository.GetById(id);

            if (targetFavouriteFood == null)
            {
                Log.Error(ErrorConstants.ERR_FAVOURITE_FOOD_NOT_FOUND);

                return GenericResult<FavouriteFoodDTO>.Error((int)HttpStatusCode.NotFound,
                    ErrorConstants.ERR_FAVOURITE_FOOD_NOT_FOUND);
            }

            var response = _mapper.Map<FavouriteFoodDTO>(targetFavouriteFood);

            return GenericResult<FavouriteFoodDTO>.Success(response);
        }

        private async Task<bool> IsFavFoodNameExisted(string favFoodName)
        {
            var currentFavouriteFoods = await GetAll();

            var currentFavouriteFoodNames = currentFavouriteFoods.Data
                .AsEnumerable()
                .Select(tempFavFood => tempFavFood.Name).ToList();

            if (currentFavouriteFoodNames.Contains(favFoodName))
                return true;

            return false;
        }

        public async Task<GenericResult<List<FavouriteFoodDTO>>> CreateNew(FavouriteFoodCreateRangeDataModel dataModel)
        {
            Log.Information("Processing create Favourite Food.");
            
            for (var i = 0; i < dataModel.FavouriteFoodNames.Count; i++)
            {
                if (await IsFavFoodNameExisted(dataModel.FavouriteFoodNames[i]))
                {
                    Log.Information($"Removing '{dataModel.FavouriteFoodNames[i]}' from the target list.");
                    
                    dataModel.FavouriteFoodNames.RemoveAt(i);
                    i--;
                }
            }

            List<FavouriteFood> targetFavouriteFoods = new();

            foreach (var tempFavFoodName in dataModel.FavouriteFoodNames)
            {
                var targetFavFood = new FavouriteFood
                {
                    Name = tempFavFoodName, 
                    CreatedTime = DateTime.UtcNow, 
                    CustomerId = dataModel.CustomerId
                };
                
                _favouriteFoodRepository.Create(targetFavFood);
                Log.Information($"Create Favourite Food successfully.");
                targetFavouriteFoods.Add(targetFavFood);
            }

            await _favouriteFoodRepository.SaveAsync();
            
            Log.Information($"Save list of favourite food of customer '{dataModel.CustomerId}' sucessfully.");

            var response = _mapper.Map<List<FavouriteFoodDTO>>(targetFavouriteFoods);

            return GenericResult<List<FavouriteFoodDTO>>.Success(response);
        }

        public async Task<GenericResult<FavouriteFoodDTO>> CreateNew(FavouriteFoodCreateDataModel dataModel)
        {
            Log.Information("Processing create Favourite Food.");
            
            if (await IsFavFoodNameExisted(dataModel.FavouriteFoodName))
            {
                Log.Error(ErrorConstants.ERR_FAVOURITE_FOOD_NAME_EXISTED);
                
                return GenericResult<FavouriteFoodDTO>.Error((int)HttpStatusCode.BadRequest, 
                    ErrorConstants.ERR_FAVOURITE_FOOD_NAME_EXISTED);
            }

            var targetFavFood = new FavouriteFood
            {
                Name = dataModel.FavouriteFoodName,
                CreatedTime = DateTime.UtcNow,
                CustomerId = dataModel.CustomerId
            };

            _favouriteFoodRepository.Create(targetFavFood);
            await _favouriteFoodRepository.SaveAsync();

            var response = _mapper.Map<FavouriteFoodDTO>(targetFavFood);

            Log.Information($"Create Favourite Food '{targetFavFood.Id}' successfully.");
            
            return GenericResult<FavouriteFoodDTO>.Success(response);
        }

        public async Task<GenericResult<List<FavouriteFoodDTO>>> GetAll()
        {
            var targetFavouriteFoods = await _favouriteFoodRepository
                .Get()
                .AsNoTracking()
                .ToListAsync();

            var response = _mapper.Map<List<FavouriteFoodDTO>>(targetFavouriteFoods);

            return GenericResult<List<FavouriteFoodDTO>>.Success(response);
        }
    }
}