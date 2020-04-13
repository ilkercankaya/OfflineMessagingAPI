using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace offlineMessagingAPI.Controllers
{

    public class AllMessageRequest
    {
        public string From { get; set; }
        public DateTime Date { get; set; }

    }
        public class MessageRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }

    }

    public class SentMessagesModel
    {
        public string SentTo { get; set; }
        public IEnumerable<AspNetUserMessage> Messages { get; set; }
    }

    [RoutePrefix("api/Message")]
    [Authorize]
    public class MessageController : ApiController
    {
        private IStoreAppContext db = new BlockedUsersAndMessages();

        public MessageController() { }
        public MessageController(IStoreAppContext db)
        {
            this.db = db;
        }

        private HttpResponseMessage CreateMessage(int code, String Message)
        {
            var response = Request.CreateResponse((HttpStatusCode)code);
            response.Content = new StringContent(Message);
            return response;
        }


        private void LogException(Exception e)
        {
            try
            {
                db = new BlockedUsersAndMessages();
                ExceptionLog eg = new ExceptionLog(e);

                db.ExceptionLogs.Add(eg);
                db.SaveChanges();
            }
            catch (Exception error)
            {
                // If an exception occured while trying to log the exception it probably means our database is down
                // we can not do any logging from here so we can not handle the exception
            }
        }

        // GET: api/Message/GetAllMessages
        [Route("GetAllMessages")]
        [HttpGet]
        public IEnumerable<AspNetUserMessage> GetAllMessages([FromBody]AllMessageRequest Request)
        {
            try
            {
                if (Request.Date == null)
                {
                    Request.Date = new DateTime();
                }

                // Get all the messages sent or received by the user
                var messages = db.AspNetUserMessages.Where(o => (o.From == Request.From || o.To == Request.From) && o.SentDate >= Request.Date).ToList();

                return messages;
            }
            catch (Exception e)
            {
                // Log the error to the database
                LogException(e);
                return null;
            }
        }

        // GET: api/Message/GetAllMessagesSentByAUser
        [Route("GetAllMessagesSentByAUser")]
        [HttpGet]
        public IEnumerable<SentMessagesModel> GetAllMessagesSentByAUser([FromBody]AllMessageRequest Request)
        {
            try
            {
                if (Request.Date == null)
                {
                    Request.Date = new DateTime();
                }

                // Get all the messages sent by the user to the given date
                var messages = db.AspNetUserMessages.Where(o => o.From == Request.From && o.SentDate >= Request.Date).ToList();
                var groupByTo = messages.GroupBy(m => m.To);
                List<SentMessagesModel> sentMessages = new List<SentMessagesModel>();

                foreach (var currentGroup in groupByTo)
                {
                    // Sort by sent date before appending to the response
                    sentMessages.Add(new SentMessagesModel() { SentTo = currentGroup.Key, Messages = currentGroup.OrderByDescending(t => t.SentDate).ToList() });
                }

                return sentMessages;
            }
            catch (Exception e)
            {
                // Log the error to the database
                LogException(e);
                return null;
            }
        }

        // GET: api/Message/GetAllMessagesSentToAUser
        [Route("GetAllMessagesSentToAUser")]
        [HttpGet]
        public IEnumerable<AspNetUserMessage> GetAllMessagesSentToAUser([FromBody]MessageRequest Request)
        {
            try
            {
                // if no query date is given that is as the smallest date possible to get all the messages sent to a user
                if(Request.Date == null)
                {
                    Request.Date = new DateTime();
                }

                // Get all the messages sent to a user
                // if a date is given the query finds all the messages sent from today to the given date INCLUSIVE
                var messages = db.AspNetUserMessages.Where(o => o.From == Request.From && o.To == Request.To && o.SentDate >= Request.Date).ToList();

                return messages;
            }
            catch (Exception e)
            {
                // Log the error to the database
                LogException(e);
                return null;
            }
        }

        // POST: api/Message/SendMessage
        [Route("SendMessage")]
        [HttpPost]
        public HttpResponseMessage SendMessage([FromBody]AspNetUserMessage message)
        {
            try
            {
                if (message.SentDate == DateTime.MinValue)
                {
                    message.SentDate = DateTime.Now;
                }

                // If the receiver user blocked the sender; abort the process
                if (db.AspNetBlockedUsers.Any(o => o.From == message.To && o.To == message.From)){
                    throw new Exception($"User {message.From} blocked {message.To}! The message can not be sent.");
                }

                // ReadDate should be enforced as null since the receiver hasnt read the msg yet
                message.ReadDate = null;
                // User is not blocked try to send the message
                db.AspNetUserMessages.Add(message);
                db.SaveChanges();

                return CreateMessage(200, "OK");
            }
            catch(Exception e)
            {
                // Log the error to the database
                LogException(e);
                return CreateMessage(500, " Internal Server Error");
            }

        }
    }
}
