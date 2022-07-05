
using AutoMapper;
using Domain.Interfaces.Services;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class ServiceItem : IServiceItem
{

    private readonly IMapper _mapper;
    private readonly IRepositoryItem _repoItem;

    public ServiceItem(
        IMapper mapper, 
        IRepositoryItem repoItem)
    {
        _mapper = mapper;
        _repoItem = repoItem;
    }

    public async Task<List<ItemDTO>> GetAll()
    {
        try
        {
            var items = await _repoItem.GetAll();

            if (items is null)
                return new List<ItemDTO>();


            var itemDtos = new List<ItemDTO>();
            foreach (var itm in items)
            {
                var itemdto = _mapper.Map<ItemDTO>(itm);
                itemdto.ImageDTOs = _mapper.Map<List<ImageDTO>>(itm.Images).ToArray();
                itemdto.SubItemDTOs = _mapper.Map<List<SubItemDTO>>(itm.SubItems).ToArray();
                itemdto.CommentDTOs = _mapper.Map<List<CommentDTO>>(itm.Comments).ToArray();

                itemDtos.Add(itemdto);
            }

            return itemDtos;
        }
        catch (Exception e)
        {
            System.Console.WriteLine("/******** "+e.StackTrace+" *******/");

            return new List<ItemDTO>();
        }

    }

    public async Task<string> Create(ItemDTO itemdto)
    {

        if (string.IsNullOrEmpty(itemdto.Title) || string.IsNullOrEmpty(itemdto.Description) ||
            string.IsNullOrEmpty(itemdto.Brand) || string.IsNullOrEmpty(itemdto.State) ||
            string.IsNullOrEmpty(itemdto.Category) || itemdto.Quality < 0 || itemdto.Quality > 5 ||
            itemdto.ImageDTOs.IsNullOrEmpty() || itemdto.SubItemDTOs.IsNullOrEmpty())
        {
            return "No empty allow!";
        }

        try
        {
            // creating an id to new ITEM
            var randomId = "ITM-" + new Random().Next(1000, 9999);
            while (true)
            {
                if (await _repoItem.GetById(randomId) is null)
                    break;

                randomId = "ITM-" + new Random().Next(1000, 9999);
            }

            //var cat = _repositoryCategory.GetByName(itemdto.Category);
            //var bra = _repositoryBrand.GetByName(itemdto.Brand);

            var item = _mapper.Map<Item>(itemdto);
            item.Id = randomId;
            item.Images = _mapper.Map<List<Image>>(itemdto.ImageDTOs);
            item.SubItems = _mapper.Map<List<SubItem>>(itemdto.SubItemDTOs);
            //item.CategoryId = cat.Id;
            //item.BrandId = bra.Id;

            _repoItem.Create(item);

            int affectedRows = await _repoItem.SaveAllChanges();

            if (affectedRows > 0)
                return "Success!";

            return "No action!";

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            return "Database error!";
        }

    }


    public async Task<string> Update(ItemDTO itemdto)
    {
        try
        {
            var item = await _repoItem.GetById(itemdto.Id);

            if (item is null)
                return "No exist!";

            if (!string.IsNullOrEmpty(itemdto.Title))
                item.Title = itemdto.Title;

            if (!string.IsNullOrEmpty(itemdto.Description))
                item.Description = itemdto.Description;

            if (itemdto.Quality >= 1 && itemdto.Quality <= 5)
                item.Quality = itemdto.Quality;

            if (!string.IsNullOrEmpty(itemdto.State))
                item.State = itemdto.State;

            if (!string.IsNullOrEmpty(itemdto.Category))
                //item.CategoryId = _repositoryCategory.GetByName(itemdto.Category).Id;

            if (!string.IsNullOrEmpty(itemdto.Brand))
                //item.BrandId = _repositoryBrand.GetByName(itemdto.Brand).Id;

            if (!itemdto.ImageDTOs.IsNullOrEmpty())
                item.Images = _mapper.Map<List<Image>>(itemdto.ImageDTOs);

            if (!itemdto.SubItemDTOs.IsNullOrEmpty())
                item.SubItems = _mapper.Map<List<SubItem>>(itemdto.SubItemDTOs);

            _repoItem.Update(item);

            int affectedRows = await _repoItem.SaveAllChanges();

            if (affectedRows > 0)
                return "Success!";

            return "No action!";

        }
        catch (Exception)
        {
            return "Database error!";
        }

    }


    public async Task<string> Delete(string id)
    {

        if (id.IsNullOrEmpty())
            return "No empty allow!";

        try
        {
            var item = await _repoItem.GetById(id);

            if (item is null)
                return "No exist!";

            _repoItem.Delete(item);

            int affectedRows = await _repoItem.SaveAllChanges();

            if (affectedRows > 0)
                return "Success!";

            return "No action!";

        }
        catch (Exception)
        {
            return "Database error!";
        }

    }


}
