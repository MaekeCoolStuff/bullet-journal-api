using System;
using System.Collections.Generic;
using System.Text;

namespace BulletJournal.Domain
{
    public enum ItemType
    {
        Task,
        Event,
        Note
    }

    public enum ItemStatus
    {
        Done,
        Cancelled,
        Migrated
    }

    public class Item
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ItemType Type { get; set; }
        public ItemStatus Status { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
