using offlineMessagingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineMessagingAPI.Tests
{
    class TestProductDbSet : TestDbSet<AspNetUserMessage>
    {
        public override AspNetUserMessage Find(params object[] keyValues)
        {
            return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
        }
    }
}
