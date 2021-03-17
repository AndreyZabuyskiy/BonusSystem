using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BonusSystem.Models;
using BonusSystem.Models.ViewModels;
using BonusSystem.Models.Services;
using BonusSystem.Models.UseCases;

namespace BonusSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICreateClient _createClient;
        private readonly ICreateBonusCard _createBonusCard;
        private readonly IPersist _persist;
        private readonly IGetClients _getClients;
        private readonly ISearchClient _searchClient;

        public HomeController([FromServices]ICreateClient createClient,
                                [FromServices]ICreateBonusCard createBonusCard,
                                [FromServices]IPersist persist,
                                [FromServices]IGetClients getClients,
                                [FromServices]ISearchClient searchClient)
        {
            _createClient = createClient;
            _createBonusCard = createBonusCard;
            _persist = persist;
            _getClients = getClients;
            _searchClient = searchClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var clients = await _getClients.GetClientsIncludeBonusCardAsync();
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
                await _persist.PersistAsync(client);
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
            if(model is null) return RedirectToAction("Search");

            try
            {
                var client = await _searchClient.SearchByPhoneNumberAsync(model.PhoneNumber);
                return RedirectToAction("View", new { controller = "Client", id = client.Id });
            }
            catch
            {
                return RedirectToAction("Search");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SearchByNumberCard(SearchClientView model)
        {
            if(model is null) return RedirectToAction("Search");

            try
            {
                var client = await _searchClient.SearchByNumberCardAsync(model.NumberCard);
                return RedirectToAction("View", new { controller = "Client", id = client.Id });
            }
            catch
            {
                return RedirectToAction("Search");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
