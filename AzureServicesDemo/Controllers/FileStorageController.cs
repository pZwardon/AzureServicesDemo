using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AzureServicesDemo.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.WindowsAzure.Storage;

namespace AzureServicesDemo.Controllers
{
    public class FileStorageController : Controller
    {
        private readonly AzureStorageManager _azureStorageManager;
        private readonly TelemetryClient _telemetry = new TelemetryClient();

        public FileStorageController()
        {
            CloudStorageAccount cloudStorageAccount;
            if (CloudStorageAccount.TryParse(
                ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString,
                out cloudStorageAccount))
            {
                _azureStorageManager = new AzureStorageManager(cloudStorageAccount);
            }

            _telemetry.InstrumentationKey = TelemetryConfiguration.Active.InstrumentationKey;
        }

        public ActionResult Index()
        {
            if (_azureStorageManager == null)
            {
                return View("NoStorageConfigured");
            }

            var listBlobItems = _azureStorageManager.GetBlobList();
            return View(listBlobItems.Select(l => l.Uri.AbsoluteUri).ToList());
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    _azureStorageManager.UploadBlob(fileName, file);

                    _telemetry.TrackTrace(string.Format(CultureInfo.InvariantCulture, "Uploaded file {0}", fileName), SeverityLevel.Information);
                }
            }

            return RedirectToAction("Index");
        }
    }
}