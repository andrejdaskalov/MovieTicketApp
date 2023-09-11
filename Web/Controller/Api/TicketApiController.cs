using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller.Api
{
    [Route("api/tickets")]
    [ApiController]
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
                Id = ticket.Id,
                MovieTitle = _movieService.GetSpecificMovie(ticket.MovieId).Title,
                Seat = ticket.Seat,
                Price = ticket.Price,
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
                Id = ticket.Id,
                MovieTitle = _movieService.GetSpecificMovie(ticket.MovieId).Title,
                Seat = ticket.Seat,
                Price = ticket.Price,
                Date = ticket.Date
            };
        }

        [HttpPost]
        public ActionResult<TicketDto> Post([FromBody] TicketDto ticket)
        {
            var selectedMovie = _movieService.GetSpecificMovie(ticket.SelectedMovie);
            if (selectedMovie == null)
            {
                return BadRequest();
            }
            MovieTicket newTicket = new MovieTicket()
            {
                MovieId = selectedMovie.Id,
                Date = ticket.Date,
                Seat = ticket.Seat,
                Price = ticket.Price
            };
             _ticketService.CreateNewTicket(newTicket);
             return new TicketDto()
             {
                 Id = newTicket.Id,
                 MovieTitle = _movieService.GetSpecificMovie(newTicket.MovieId).Title,
                 Seat = newTicket.Seat,
                 Price = newTicket.Price,
                 Date = newTicket.Date
             };
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
            updatedTicket.Price = ticket.Price;
            updatedTicket.MovieId = ticket.SelectedMovie;
            _ticketService.UpdateExistingTicket(updatedTicket);

            return new TicketDto()
            {
                Id = updatedTicket.Id,
                MovieTitle = _movieService.GetSpecificMovie(updatedTicket.MovieId).Title,
                Seat = updatedTicket.Seat,
                Price = updatedTicket.Price,
                Date = updatedTicket.Date
            };
        }

        [HttpDelete("{id:guid}")]
        public void Delete(Guid id)
        {
            _ticketService.DeleteTicket(id);
        }
    }
}
