using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller.Api
{
    [Route("api/tickets")]
    [ApiController]
    [Authorize]
    public class TicketApiController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IMovieService _movieService;

        public TicketApiController(ITicketService ticketService, IMovieService movieService)
        {
            _ticketService = ticketService;
            _movieService = movieService;
        }

        [HttpGet]
        public IEnumerable<TicketDto> Get([FromQuery] DateTime? date)
        {
            var allTickets = date == null ? 
                _ticketService.GetAllTicketAsList() : 
                _ticketService.GetAllTicketAsList(date);
            return allTickets.Select(ticket => new TicketDto()
            {
                Id = ticket.Id.ToString(),
                MovieTitle = _movieService.GetSpecificMovie(ticket.MovieId).Title,
                Seat = ticket.Seat,
                Price = ticket.Price.ToString(),
                Date = ticket.Date
            }).ToList();
        }

        [HttpGet("{id:guid}", Name = "Get")]
        public ActionResult<TicketDto> Get(Guid id)
        {
            var ticket = _ticketService.GetSpecificTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return new TicketDto()
            {
                Id = ticket.Id.ToString(),
                MovieTitle = _movieService.GetSpecificMovie(ticket.MovieId).Title,
                Seat = ticket.Seat,
                Price = ticket.Price.ToString(),
                Date = ticket.Date
            };
        }

        [HttpPost]
        public ActionResult Post([FromBody] TicketDto ticket)
        {
            var selectedMovie = _movieService.GetSpecificMovie(Guid.Parse(ticket.SelectedMovie));
            if (selectedMovie == null)
            {
                return BadRequest();
            }
            MovieTicket newTicket = new MovieTicket()
            {
                MovieId = selectedMovie.Id,
                Date = ticket.Date,
                Seat = ticket.Seat,
                Price = Int32.Parse(ticket.Price)
            };
             var movieTicket = _ticketService.CreateNewTicket(newTicket);
             if (movieTicket == null)
             {
                 return new BadRequestResult();
             }
             return new OkResult();
        }

        [HttpPut("{id:guid}")]
        public ActionResult<TicketDto> Put([FromBody] TicketDto ticket, Guid id)
        {
            var updatedTicket = _ticketService.GetSpecificTicket(id);
            if (updatedTicket == null)
            {
                return NotFound();
            }

            updatedTicket.Date = ticket.Date;
            updatedTicket.Seat = ticket.Seat;
            updatedTicket.Price = Int32.Parse(ticket.Price);
            updatedTicket.MovieId = Guid.Parse(ticket.SelectedMovie);
            var movieTicket = _ticketService.UpdateExistingTicket(updatedTicket);
            if (movieTicket == null)
            {
                return new BadRequestResult();
            }

            return new OkResult();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var movieTicket = _ticketService.DeleteTicket(id);
            if (movieTicket == null)
            {
                return NotFound();
            }

            return new OkResult();
        }
        
        [HttpGet("getAllMovies")]
        public IEnumerable<MovieDto> GetAllMovies()
        {
            return _movieService.GetAllMovies().Select(m => new MovieDto
            {
                Title = m.Title,
                Id = m.Id.ToString()
            });
        }
        
        [HttpPost("export")]
        public IActionResult ExportAllTickets([FromBody] string genre)
        {
            var result = _ticketService.GetAllTicketAsList().Where(t => t.Movie.Genre == genre);
            var fileName = "Tickets.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            using (var workBook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("Tickets");
                worksheet.Cell(1, 1).Value = "Ticket Id";
                worksheet.Cell(1, 2).Value = "Movie Title";
                worksheet.Cell(1, 3).Value = "Date";
                worksheet.Cell(1, 4).Value = "Seat";
                worksheet.Cell(1, 5).Value = "Price";

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result.ElementAt(i - 1);
                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.Movie.Title;
                    worksheet.Cell(i + 1, 3).Value = item.Date.ToString();
                    worksheet.Cell(i + 1, 4).Value = item.Seat;
                    worksheet.Cell(i + 1, 5).Value = item.Price;
                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }
    }
    
    
}
