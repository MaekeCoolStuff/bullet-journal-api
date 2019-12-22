using BulletJournal.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace BulletJournal.Data
{
    public class BulletJournalContext : DbContext
    {
        public BulletJournalContext(DbContextOptions<BulletJournalContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
