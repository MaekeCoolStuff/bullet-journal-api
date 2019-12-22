using BulletJournal.Data;
using BulletJournal.Domain;
using BulletJournalApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BulletJournalApi.Services
{
    public class ItemService
    {
        private readonly BulletJournalContext _context;

        public ItemService(BulletJournalContext context)
        {
            _context = context;
        }

        public List<Item> Get() => _context.Items.Where(item => true).ToList();

        public Item Get(int id) => _context.Items.FirstOrDefault(item => item.Id == id);

        public Item Create(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            return item;
        }

        public void Update(Item itemIn)
        {
            _context.Items.Update(itemIn);
            _context.SaveChanges();
        }            

        public void Remove(Item itemIn)
        {
            _context.Items.Remove(itemIn);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var itemToRemove = _context.Items.FirstOrDefault(item => item.Id == id);
            _context.Items.Remove(itemToRemove);
            _context.SaveChanges();
        }
    }
}
