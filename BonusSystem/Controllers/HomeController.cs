using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BonusSystem.Models;
using BonusSystem.Models.Db;
using BonusSystem.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using BonusSystem.Models.Services;
using BonusSystem.Models.UseCases;

namespace BonusSystem.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext _db;
        private ICreateClient _createClient;
        private ICreateBonusCard _createBonusCard;
        private IPersist _saveClientDb;
        private IGetClients _getClients;

        public HomeController(ApplicationContext db, [FromServices]ICreateClient createClient,
                                        [FromServices]ICreateBonusCard createBonusCard,
                                        [FromServices]IPersist saveClientDb,
                                        [FromServices]IGetClients getClients)
        {
            _db = db;
            _createClient = createClient;
            _createBonusCard = createBonusCard;
            _saveClientDb = saveClientDb;
            _getClients = getClients;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var clients = await _getClients.GetClientsAsync(true);
                return View(clients);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientBonusCardView model)
        {
            if (ModelState.IsValid)
            {
                var client = _createClient.Create(model);
                var card = _createBonusCard.Create(model.Balance);
                client.BonusCard = card;
                await _saveClientDb.PersistAsync(client);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search() 
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> SearchByPhoneNumber(SearchClientView model)
        {
            if(model != null)
            {
                var client = await _db.Clients.FirstOrDefaultAsync(c => c.PhoneNumber == model.PhoneNumber);

                if(client != null)
                    return RedirectToAction("View", new { controller = "Client", id = client.Id });
            }

            return RedirectToAction("Search");
        }

        [HttpPost]
        public async Task<IActionResult> SearchByNumberCard(SearchClientView model)
        {
            if (model != null)
            {
                var card = await _db.BonusCards.Include(c => c.Client)
                                               .FirstOrDefaultAsync(c => c.Number == model.NumberCard);

                if (card != null)
                    return RedirectToAction("View", new { controller = "Client", id = card.Client.Id });
            }

            return RedirectToAction("Search");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
