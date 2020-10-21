using System.IO;
using System.Linq;

namespace asp.net.core.angular.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFiletypes { get; set; }

        public bool IsSupported(string fileName)
        {
            return AcceptedFiletypes.Any(s => s == Path.GetExtension(fileName));
        }

    }

}
