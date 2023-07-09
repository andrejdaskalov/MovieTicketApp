using System;
using System.Collections.Generic;
using Domain;

namespace Service
{
    public interface ITicketService
    {
        public List<MovieTicket> GetAllTicketAsList();
        public MovieTicket GetSpecificTicket(Guid? id);
        public MovieTicket CreateNewTicket(MovieTicket newEntity);
        public MovieTicket UpdateExistingTicket(MovieTicket updatedTicket);
        public MovieTicket DeleteTicket(Guid? id);
        public Boolean TicketExist(Guid? id);
        public List<MovieTicket> GetAllTicketAsList(DateTime? date);
    }
}