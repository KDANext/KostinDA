using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopDatebaseImplement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blacksmith_sWorkshopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientLogic clientLogic;
        private readonly IMessageInfoLogic messageInfoLogic;
        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 10;

        public ClientController(IClientLogic clientLogic, IMessageInfoLogic messageInfoLogic)
        {
            this.clientLogic = clientLogic;
            this.messageInfoLogic = messageInfoLogic;
        }
        [HttpGet] public ClientViewModel Login(string login, string password) => clientLogic.Read(new ClientBindingModel { Login = login, Password = password })?[0];

        [HttpGet] public List<MessageInfoViewModel> GetMessages(int clientId) => messageInfoLogic.Read(new MessageInfoBindingModel { ClientId = clientId });

        [HttpPost] public void Register(ClientBindingModel model) { CheckData(model); clientLogic.CreateOrUpdate(model); }

        [HttpPost] public void UpdateData(ClientBindingModel model) { CheckData(model); clientLogic.CreateOrUpdate(model); }

        private void CheckData(ClientBindingModel model)
        {
            if (!Regex.IsMatch(model.Login, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")) { throw new Exception("В качестве логина почта указана должна быть"); }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length < _passwordMinLength || !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длиной от {_passwordMinLength} до {_passwordMaxLength} должен быть и из цифр, букв и небуквенных символов должен состоять");
            }
        }
    }
}
