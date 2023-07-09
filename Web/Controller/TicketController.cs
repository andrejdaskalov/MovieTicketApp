using System;
using System.Collections;
using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Web.Controller
{
    [Route("")]
    public class TicketController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IMovieService _movieService;

        public TicketController(ITicketService ticketService, IMovieService movieService)
        {
            _ticketService = ticketService;
            _movieService = movieService;
        }

        // GET
        [HttpGet]
        public IActionResult Index([FromQuery] DateTime? date)
        {
            if (date == null)
            {
                var allTickets = _ticketService.GetAllTicketAsList();
                return View(allTickets);
            }
            else
            {
                var allTickets = _ticketService.GetAllTicketAsList(date);
                return View(allTickets);
            }
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            IEnumerable<Movie> movies = _movieService.GetAllMovies();
            return View(movies);
        }

        [HttpPost("Create")]
        public IActionResult Create(Domain.DTO.TicketDto ticket)
        {
            var selectedMovie = _movieService.GetSpecificMovie(ticket.SelectedMovie);
            MovieTicket newTicket = new MovieTicket()
            {
                MovieId = selectedMovie.Id,
                Date = ticket.Date,
                Seat = ticket.Seat,
                Price = ticket.Price
            };
            _ticketService.CreateNewTicket(newTicket);
            return RedirectToAction("Index");
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(Guid? id)
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

            IEnumerable<Movie> movies = _movieService.GetAllMovies();
            var ticketDto = new Domain.DTO.TicketDto()
            {
                SelectedMovie = ticket.Movie.Id,
                Date = ticket.Date,
                Seat = ticket.Seat,
                Price = ticket.Price,
                MovieOptions = movies
            };

            return View(ticketDto);
        }

        [HttpPost("Edit/{id}")]
        public IActionResult Edit(Guid id, Domain.DTO.TicketDto ticket)
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

            return RedirectToAction("Index");
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(Guid? id)
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

            var ticketDto = new Domain.DTO.TicketDto()
            {
                MovieTitle = ticket.Movie.Title,
                Date = ticket.Date,
                Seat = ticket.Seat,
                Price = ticket.Price,
                Id = ticket.Id
            };

            return View(ticketDto);
        }

        [HttpPost("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _ticketService.DeleteTicket(id);
            return RedirectToAction("Index");
        }
    }
}