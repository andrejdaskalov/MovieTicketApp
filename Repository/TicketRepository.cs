using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<MovieTicket> _entities;
        private DbSet<OrderItem> _orderItemEntities;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<MovieTicket>();
            _orderItemEntities = _context.Set<OrderItem>();
        }

        public IEnumerable<MovieTicket> GetAll()
        {
            return _entities.Include(e => e.Movie).AsEnumerable();
        }

        public MovieTicket Get(Guid? id)
        {
            return _entities.Include(e => e.Movie).First(e => e.Id.Equals(id));
        }

        public MovieTicket Insert(MovieTicket entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException("entity");
            }

            _entities.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public MovieTicket Update(MovieTicket entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException("entity");
            }

            _entities.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public MovieTicket Delete(MovieTicket entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException("entity");
            }

            _orderItemEntities.Where(item => item.MovieTicket.Id == entity.Id).ToList().ForEach(
                item => { item.MovieTicket = null; }
            );

            _entities.Remove(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}