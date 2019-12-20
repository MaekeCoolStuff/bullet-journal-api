using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulletJournalApi.Models
{
    public class BulletJournalDatabaseSettings : IBulletJournalDatabaseSettings
    {
        public string TasksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBulletJournalDatabaseSettings
    {
        string TasksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
