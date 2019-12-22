using BulletJournal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulletJournalApi.Models
{
    public class ItemModel {
        public string Text { get; set; }
        public ItemType Type { get; set; }
        public ItemStatus Status { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
    }
}
