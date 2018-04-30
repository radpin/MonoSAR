using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MonoSAR.Models;
using MonoSAR.Models.AccountViewModels;
using MonoSAR.Services;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.RetryPolicies;

namespace MonoSAR.Controllers
{
    public class BlobController : Controller
    {


        private Models.DB.monosarsqlContext _context;
        private IConfiguration _config;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private IOptions<ApplicationSettings> _applicationOptions;


        public BlobController(IConfiguration config, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> usermanager, IOptions<ApplicationSettings> options)
        {
            this._context = new Models.DB.monosarsqlContext(config);
            this._config = config;
            this._userManager = usermanager;
            this._applicationOptions = options;
        }
        
        public async Task<IActionResult> Index()
        {            
            CloudStorageAccount account = CloudStorageAccount.Parse(_config["azure-blob-connectionstring"]);
            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("docs");
            List<IListBlobItem> blobList = new List<IListBlobItem>();
            
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var results = await container.ListBlobsSegmentedAsync(null, blobContinuationToken);
                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    blobList.Add(item);
                    //Console.WriteLine(item.Uri);
                }
                blobContinuationToken = results.ContinuationToken;
            } while (blobContinuationToken != null); // Loop while the continuation token is not null. 

            return View(blobList);
        }
    }
}