﻿using System;
using System.Threading.Tasks;
using BonusSystem.Models;
using BonusSystem.Models.Db;
using BonusSystem.Models.Exceptions;
using BonusSystem.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonusSystem.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationContext _db;
        private IEditClient _editClient;
        private IRemoveClient _removeClient;

        public ClientController(ApplicationContext db,
                                [FromServices]IEditClient editClient,
                                [FromServices]IRemoveClient removeClient)
        {
            _db = db;
            _editClient = editClient;
            _removeClient = removeClient;
        }

        public async Task<IActionResult> View(Guid id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var client = await _db.Clients.Include(c => c.BonusCard)
                                      .FirstOrDefaultAsync(c => c.Id == id);

            if (client is null) return NotFound();

            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var client = await _db.Clients.FirstOrDefaultAsync(c => c.Id == id);

            if (client is null) return NotFound();

            return View(client);
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
