using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SMWebAPI.Models;

namespace SMWebAPI.Controllers
{
    [Route("api/ApplicationMessages")]
    [ApiController]
    public class ApplicationMessagesController : ControllerBase
    {
        private readonly SMDbContext _context;

        public ApplicationMessagesController(SMDbContext context)
        {
            _context = context;
        }

        public class ApplicationMessages
        {
            public List<Message_Subject> Message_Subjects { get; set; }

        }

        //Get ALL messages regardless of broker (used for colleague UI) 
        [HttpGet]
        public async Task<ActionResult<ApplicationMessages>> GetApplicationMessagesAsync()
        {
            var messSubject = await _context.Message_Subject
                                    .Include(m => m.Message_Chain)
                                    .ToListAsync();

            ApplicationMessages allAppAndMessage = new ApplicationMessages();
            allAppAndMessage.Message_Subjects = messSubject;

            return allAppAndMessage;
        }

        HttpGet("{rollNumber}")]
        public async Task<ActionResult<ApplicationMessages>> GetApplicationMessagesAsync(int rollNumber)
        {
            var messSubject = await _context.Message_Subject.Where(a => a.RollNumber == rollNumber)
                                    .Include(m => m.Message_Chain)
                                    .ToListAsync();

            ApplicationMessages allAppAndMessage = new ApplicationMessages();
            allAppAndMessage.Message_Subjects = messSubject;

            return allAppAndMessage;
        }

        //[HttpPost]
        //public async Task<ActionResult<Message_Chain>> PostApplicationMeesage(ApplicationMessages newSubject)
        //{
        //    _context.Message_Subject.Add(message_Subject);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (Message_SubjectExists(message_Subject.MessageSubjectId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetMessage_Subject", new { id = message_Subject.MessageSubjectId }, message_Subject);
        //}


    }

}
