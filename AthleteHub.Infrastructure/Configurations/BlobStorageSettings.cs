using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Infrastructure.Configurations
{
    public class BlobStorageSettings
    {
        public const string BlobStorage = "BlobStorage";
        public string ConnectionString { get; set; }
        public string CertificatesContainerName { get; set; }
        public string PicturesContainerName { get; set; }
        public string VideosContainerName { get; set; }
        public string PDFsContainerName { get; set; }
        public Dictionary<string, string> ContainerMappings { get; set; }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
    }
}
