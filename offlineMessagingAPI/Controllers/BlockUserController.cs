using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace offlineMessagingAPI.Controllers
{
    [RoutePrefix("api/BlockUser")]
    [Authorize]
    public class BlockUserController : ApiController
    {
        private IStoreAppContext db = new BlockedUsersAndMessages();

        public BlockUserController() { }
        public BlockUserController(IStoreAppContext db)
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

        // POST: api/BlockUser/Block
        [Route("Block")]
        [HttpPost]
        public HttpResponseMessage Block([FromBody]AspNetBlockedUser UserBlock)
        {
            try
            {
                db.AspNetBlockedUsers.Add(UserBlock);
                db.SaveChanges();

                return CreateMessage(200, "OK");
            }
            catch (Exception e)
            {
                // Log the error to the database
                LogException(e);
                return CreateMessage(500, " Internal Server Error");

            }

        }
    }
}
