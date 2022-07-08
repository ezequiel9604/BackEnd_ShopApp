
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
    private readonly IRepositoryCategory _repoCategory;
    private readonly IRepositoryBrand _repoBrand;
    private readonly IRepositoryImage _repoImage;
    private readonly IRepositorySubitem _repoSubitem;
    private readonly IRepositoryComment _repoComment;

    public ServiceItem(
        IMapper mapper, 
        IRepositoryItem repoItem,
        IRepositoryCategory repoCategory,
        IRepositoryBrand repoBrand,
        IRepositoryImage repoImage,
        IRepositorySubitem repoSubitem,
        IRepositoryComment repoComment)
    {
        _mapper = mapper;
        _repoItem = repoItem;
        _repoBrand = repoBrand;
        _repoCategory = repoCategory;
        _repoImage = repoImage;
        _repoSubitem = repoSubitem;
        _repoComment = repoComment;
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

                var cate = await _repoCategory.GetById(itm.CategoryId);
                var bra = await _repoBrand.GetById(itm.BrandId);

                itemdto.Category = cate.Name;
                itemdto.Brand = bra.Name;

                var imgs = await _repoImage.GetByItemId(itm.Id);
                itemdto.Images = _mapper.Map<List<ImageDTO>>(imgs);

                var subs = await _repoSubitem.GetByItemId(itm.Id);
                itemdto.SubItems = _mapper.Map<List<SubItemDTO>>(subs);

                var comms = await _repoComment.GetByItemId(itm.Id);
                itemdto.Comments = _mapper.Map<List<CommentDTO>>(comms);

                itemDtos.Add(itemdto);
            }

            return itemDtos;
        }
        catch (Exception e)
        {
            //System.Console.WriteLine("/******** "+e.StackTrace+" *******/");
            //System.Console.WriteLine("/******** " + e.Message + " *******/");

            return new List<ItemDTO>();
        }

    }

    public async Task<string> Create(ItemDTO itemdto)
    {

        if (string.IsNullOrEmpty(itemdto.Title) || string.IsNullOrEmpty(itemdto.Description) ||
            string.IsNullOrEmpty(itemdto.Brand) || string.IsNullOrEmpty(itemdto.State) ||
            string.IsNullOrEmpty(itemdto.Category) || itemdto.Quality < 0 || itemdto.Quality > 5 ||
            itemdto.Images.IsNullOrEmpty() || itemdto.SubItems.IsNullOrEmpty())
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

            var cate = await _repoCategory.GetByName(itemdto.Category);
            var bra = await _repoBrand.GetByName(itemdto.Brand);

            var item = _mapper.Map<Item>(itemdto);
            item.Id = randomId;
            item.Images = _mapper.Map<List<Image>>(itemdto.Images);
            item.SubItems = _mapper.Map<List<SubItem>>(itemdto.SubItems);
            item.Category = cate;
            item.Brand = bra;

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
                item.Category = await _repoCategory.GetByName(itemdto.Category);

            if (!string.IsNullOrEmpty(itemdto.Brand))
                item.Brand = await _repoBrand.GetByName(itemdto.Brand);

            if (!itemdto.Images.IsNullOrEmpty())
                item.Images = _mapper.Map<List<Image>>(itemdto.Images);

            if (!itemdto.SubItems.IsNullOrEmpty())
                item.SubItems = _mapper.Map<List<SubItem>>(itemdto.SubItems);

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
