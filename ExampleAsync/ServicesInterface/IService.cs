using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleAsync.Services
{
    public interface IService
    {
        abstract Task GetContentsLengthAsync(IEnumerable<string> resourcePaths, CancellationToken userCancellationToken);
    }
}
