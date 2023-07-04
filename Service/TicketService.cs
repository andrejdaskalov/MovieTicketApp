using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Repository;

namespace Service
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<MovieTicket> _repository;

        public TicketService(IRepository<MovieTicket> repository)
        {
            _repository = repository;
        }

        public List<MovieTicket> GetAllTicketAsList()
        {
            return _repository.GetAll().ToList();
        }

        public MovieTicket GetSpecificTicket(int? id)
        {
            return _repository.Get(id);
        }

        public MovieTicket CreateNewTicket(MovieTicket newEntity)
        {
            return _repository.Insert(newEntity);
        }

        public MovieTicket UpdateExistingTicket(MovieTicket updatedTicket)
        {
            return _repository.Update(updatedTicket);
        }

        public MovieTicket DeleteTicket(int? id)
        {
            var ticket = _repository.Get(id);
            if (ticket == null)
            {
                throw new ArgumentException("Ticket not found");
            }
            return _repository.Delete(ticket);
        }

        public bool TicketExist(int? id)
        {
            return _repository.Get(id) != null;
        }
    }
}