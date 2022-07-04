
using AutoMapper;
using Domain.Interfaces.Services;
using API.DTOs;
using Domain.Interfaces.Repositories;

namespace API.Services;

public class ServiceOrder : IServiceOrder
{

    private readonly IMapper _mapper;
    private readonly IRepositoryOrder _repoOrder;

    public ServiceOrder(IMapper mapper, IRepositoryOrder repoOrder)
    {
        _mapper = mapper;
        _repoOrder = repoOrder;
    }

    public async Task<List<OrderDTO>> GetAll()
    {

        try
        {
            var orders = await _repoOrder.GetAll();

            if (orders is null)
                return new List<OrderDTO>();

            var orderDtos = new List<OrderDTO>();

            foreach (var order in orders)
            {
                var odto = _mapper.Map<OrderDTO>(order);
                odto.PurchaseDtos = _mapper.Map<List<PurchaseDTO>>(order.Purchases).ToArray();

                orderDtos.Add(odto);
            }

            return orderDtos;

        }
        catch (Exception)
        {
            return new List<OrderDTO>();
        }

    }

    public async Task<List<OrderDTO>> GetByClientId(string clientId)
    {
        try
        {
            var orders = await _repoOrder.GetAll();

            if (orders is null)
                return new List<OrderDTO>();

            var orderDtos = new List<OrderDTO>();

            foreach (var order in orders)
            {
                if(order.ClientId == clientId)
                {
                    var odto = _mapper.Map<OrderDTO>(order);
                    odto.PurchaseDtos = _mapper.Map<List<PurchaseDTO>>(order.Purchases).ToArray();

                    orderDtos.Add(odto);
                }
            }

            return orderDtos;

        }
        catch (Exception)
        {
            return new List<OrderDTO>();
        }
    }

    Task<string> IService<OrderDTO>.Create(OrderDTO obj)
    {
        throw new NotImplementedException();
    }

    Task<string> IService<OrderDTO>.Delete(string obj)
    {
        throw new NotImplementedException();
    }

    Task<string> IService<OrderDTO>.Update(OrderDTO obj)
    {
        throw new NotImplementedException();
    }
}

