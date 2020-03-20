using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HelloWorld.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
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
            catch (Exception ex)
            {
                //handles null exception rethrow a custom exception
                throw new BusinessException("This Works");
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
