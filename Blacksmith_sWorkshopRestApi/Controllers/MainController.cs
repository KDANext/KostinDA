using Blacksmith_sWorkshopBusinessLogic.BusinessLogics;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith_sWorkshopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IProductLogic _product;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, IProductLogic product, MainLogic main)
        {
            _order = order;
            _product = product;
            _main = main;
        }
        [HttpGet]
        public List<ProductViewModel> GetProductList() => _product.Read(null);
        [HttpGet]
        public ProductViewModel GetProduct(int productId) => Convert(_product.Read(new ProductBindingModel { Id = productId })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        {
            ClientId = clientId
        });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);
        private ProductViewModel Convert(ProductViewModel model)
        {
            if (model == null) return null;
            return new ProductViewModel
            {
                Id = model.Id,
                ProductName = model.ProductName,
                Price = model.Price
            };
        }
    }
}
