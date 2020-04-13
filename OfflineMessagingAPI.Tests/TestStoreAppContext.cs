using offlineMessagingAPI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineMessagingAPI.Tests
{
    public class TestStoreAppContext : IStoreAppContext
    {
        public TestStoreAppContext()
        {
            this.AspNetUserMessages = new TestProductDbSet();
        }

        public DbSet<AspNetUserMessage> AspNetUserMessages { get; set; }

        public DbSet<AspNetBlockedUser> AspNetBlockedUsers => throw new NotImplementedException();

        public DbSet<ExceptionLog> ExceptionLogs => throw new NotImplementedException();

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(AspNetUserMessage item) { }
        public void Dispose() { }
    }
}
