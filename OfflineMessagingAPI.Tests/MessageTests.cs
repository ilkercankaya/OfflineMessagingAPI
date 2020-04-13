using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using offlineMessagingAPI;
using offlineMessagingAPI.Controllers;
using Xunit;

namespace OfflineMessagingAPI.Tests
{

    public class MessageTests
    {
        [Fact]
        public void ShouldGetAllMessagesSentByAUser()
        {
            // Arrange Data ----
            AllMessageRequest Request = new AllMessageRequest();
            Request.From = "userVIP";

            var listOfMessages = new List<AspNetUserMessage>();
            listOfMessages.Add(new AspNetUserMessage {
                From = Request.From,
                To = "User1",
                Text = "DummyTxt"
            });

            listOfMessages.Add(new AspNetUserMessage
            {
                From = "User2",
                To = Request.From,
                Text = "txt2"
            });

            var newContext = new TestStoreAppContext();
            foreach (AspNetUserMessage msg in listOfMessages)
            {
                newContext.AspNetUserMessages.Add(msg);
            }

            // Setup database mocker
            var controller = new MessageController(newContext);

            // Act ---
            List<AspNetUserMessage> response = controller.GetAllMessages(Request).AsEnumerable().ToList();

            // Assert ---
            Assert.True(response.SequenceEqual(listOfMessages));
        }
    }
}
