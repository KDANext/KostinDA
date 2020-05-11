using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopDatebaseImplement;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith_sWorkshopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientLogic clientLogic;
        private readonly IMessageInfoLogic messageInfoLogic;
        public ClientController(IClientLogic clientLogic, IMessageInfoLogic messageInfoLogic)
        {
            this.clientLogic = clientLogic;
            this.messageInfoLogic = messageInfoLogic;
        }
        [HttpGet]
        public ClientViewModel Login(string login, string password) => clientLogic.Read(new ClientBindingModel
        { 
            Login = login, Password = password }).FirstOrDefault();
        [HttpPost]
        public void Register(ClientBindingModel model) => clientLogic.CreateOrUpdate(model);
        [HttpPost]
        public void UpdateData(ClientBindingModel model) => clientLogic.CreateOrUpdate(model);
        public List<MessageInfoViewModel> GetMessages(MessageInfoBindingModel model) => messageInfoLogic.Read(model);
    }

}
