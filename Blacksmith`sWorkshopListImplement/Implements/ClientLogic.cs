using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using Blacksmith_sWorkshopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blacksmith_sWorkshopListImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        private readonly DataListSingleton source;
        public ClientLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ClientBindingModel model)
        {
            Client tempClient = model.Id.HasValue ? null : new Client
            {
                Id = 1
            };
            foreach (var Client in source.Clients)
            {
                if (Client.Login == model.Login && Client.Id != model.Id)
                {
                    throw new Exception("Уже есть клиент с таким логином");
                }
                if (!model.Id.HasValue && Client.Id >= tempClient.Id)
                {
                    tempClient.Id = Client.Id + 1;
                }
                else if (model.Id.HasValue && Client.Id == model.Id)
                {
                    tempClient = Client;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempClient == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempClient);
            }
            else
            {
                source.Clients.Add(CreateModel(model, tempClient));
            }

        }
        public void Delete(ClientBindingModel model)
        {
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == model.Id.Value)
                {
                    source.Clients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            List<ClientViewModel> result = new List<ClientViewModel>();
            foreach (var Client in source.Clients)
            {
                if (model != null)
                {
                    if (Client.Id == model.Id)
                    {
                        result.Add(CreateViewModel(Client));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(Client));
            }
            return result;
        }
        private Client CreateModel(ClientBindingModel model, Client Client)
        {
            Client.ClientFIO = model.ClientFIO;
            Client.Login = model.Login;
            Client.Password = model.Password;    
            return Client;
        }
        private ClientViewModel CreateViewModel(Client Client)
        {
            return new ClientViewModel
            {
                Id = Client.Id,
                ClientFIO = Client.ClientFIO
            };
        }
    }
}
