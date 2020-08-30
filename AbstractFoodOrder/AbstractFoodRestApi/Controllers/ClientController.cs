using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.Interfaces;
using AbstractFoodOrderBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbstractFoodRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientLogic _logic;
        private readonly IMessageInfoLogic _messageLogic;

        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 10;

        public ClientController(IClientLogic logic, IMessageInfoLogic messageLogic)
        {
            this._logic = logic;
            this._messageLogic = messageLogic;
        }

        [HttpGet]
        public ClientViewModel Login(string login, string password) => _logic.Read(new ClientBindingModel { Email = login, Password = password })?[0];

        [HttpGet]
        public List<MessageInfoViewModel> GetMessages(int clientId) => _messageLogic.Read(new MessageInfoBindingModel { ClientId = clientId });

        [HttpPost]
        public void Register(ClientBindingModel model)
        {
            CheckData(model);
            _logic.CreateOrUpdate(model);
        }

        [HttpPost]
        public void UpdateData(ClientBindingModel model)
        {
            CheckData(model);
            _logic.CreateOrUpdate(model);
        }

        private void CheckData(ClientBindingModel model)
        {
            if (!Regex.IsMatch(model.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }

            if (model.Password.Length > _passwordMaxLength
                || model.Password.Length < _passwordMinLength
                || !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль должен быть длиной от {_passwordMinLength} до { _passwordMaxLength } и должен состоять из цифр, букв и небуквенных символов");
            }
        }
    }
}
