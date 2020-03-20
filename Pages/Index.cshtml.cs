using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HelloWorld.Pages
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            :base(message)
        {

        }

        //
        // Summary:
        //     Initializes a new instance of the System.Exception class with a specified error
        //     message and a reference to the inner exception that is the cause of this exception.
        //
        // Parameters:
        //   message:
        //     The error message that explains the reason for the exception.
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
        //     (Nothing in Visual Basic) if no inner exception is specified.
        public BusinessException(string message, Exception innerException)
            :base (message, innerException)
        {

        }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        private void SomeWork()
        {
            try
            {
                //try to do some work here 
                object notExist = null;
                notExist.ToString(); //intentionally get null exception 
            }
            catch(Exception ex)
            {
                //handles null exception rethrow a custom exception
                throw new BusinessException("Business logic", ex);
            }
        }

        public void OnGet()
        {
            try
            {
                SomeWork();
            }
            catch (Exception e)
            {
                NewRelic.Api.Agent.NewRelic.NoticeError(e);
                throw;
            }
        }
    }
}
