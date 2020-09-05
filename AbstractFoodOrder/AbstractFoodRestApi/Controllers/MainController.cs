using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.BusinessLogics;
using AbstractFoodOrderBusinessLogic.Interfaces;
using AbstractFoodOrderBusinessLogic.ViewModels;
using AbstractFoodRestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbstractFoodRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IKitLogic _kit;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, IKitLogic kit, MainLogic main)
        {
            _order = order;
            _kit = kit;
            _main = main;
        }

        [HttpGet]
        public List<KitModel> GetKitList() => _kit.Read(null)?.Select(rec => Convert(rec)).ToList();
        [HttpGet]
        public KitModel GetKit(int KitId) => Convert(_kit.Read(new KitBindingModel { Id = KitId })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
       OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _main.CreateOrder(model);
        private KitModel Convert(KitViewModel model)
        {
            if (model == null) return null;
            return new KitModel
            {
                Id = model.Id,
                KitName = model.KitName,
                Price = model.Price
            };
        }
    }
}
