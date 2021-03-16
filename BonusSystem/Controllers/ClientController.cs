using System;
using System.Threading.Tasks;
using BonusSystem.Models;
using BonusSystem.Models.Exceptions;
using BonusSystem.Models.Services;
using BonusSystem.Models.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BonusSystem.Controllers
{
    public class ClientController : Controller
    {
        private readonly IEditClient _editClient;
        private readonly IRemoveClient _removeClient;
        private readonly IGetClient _getClient;

        public ClientController([FromServices]IEditClient editClient,
                                [FromServices]IRemoveClient removeClient,
                                [FromServices]IGetClient getClient)
        {
            _editClient = editClient;
            _removeClient = removeClient;
            _getClient = getClient;
        }

        public async Task<IActionResult> View(Guid id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            try
            {
                var client = await _getClient.GetClientIncludeBonusCardAsync(id);
                return View(client);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            try
            {
                var client = await _getClient.GetClientAsync(id);
                return View(client);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Client client)
        {
            if(client is null) return NotFound();

            try
            {
                await _editClient.EditAsync(client);
                return RedirectToAction("View", new { id = client.Id });
            }
            catch(ClientNotFoundException)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> RemoveClient(Guid id)
        {
            if (id == null || id == Guid.Empty) return NotFound();

            try
            {
                await _removeClient.RemoveAsync(id);
                return RedirectToAction("Index", new { controller = "Home" });
            }
            catch(ClientNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
