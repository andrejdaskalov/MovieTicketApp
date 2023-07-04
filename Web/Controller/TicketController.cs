using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller
{
    [Route("Ticket")]
    public class TicketController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return View(_ticketService.GetAllTicketAsList());
        }
        
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost("Create")]
        public IActionResult Create(Domain.MovieTicket ticket)
        {
            _ticketService.CreateNewTicket(ticket);
            return RedirectToAction("Index");
        }
        
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetSpecificTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }
        
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(int id, Domain.MovieTicket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _ticketService.UpdateExistingTicket(ticket);
                return RedirectToAction("Index");
            }

            return View(ticket);
        }
        
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetSpecificTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }
        
        [HttpPost("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _ticketService.DeleteTicket(id);
            return RedirectToAction("Index");
        }
    }
}