using System;
using System.Collections.Generic;
using Domain;

namespace Service
{
    public interface ITicketService
    {
        public List<MovieTicket> GetAllTicketAsList();
        public MovieTicket GetSpecificTicket(int? id);
        public MovieTicket CreateNewTicket(MovieTicket newEntity);
        public MovieTicket UpdateExistingTicket(MovieTicket updatedTicket);
        public MovieTicket DeleteTicket(int? id);
        public Boolean TicketExist(int? id);
    }
}