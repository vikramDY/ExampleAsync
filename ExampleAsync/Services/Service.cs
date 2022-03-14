using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleAsync.Services
{
    public class Service : IService
    {
		/// <summary>
		/// Downloads data or File and aggregates the content length. 
		/// </summary>
		/// <param name="resources">Pass the URLs list.</param>
		/// <param name="cancellationToken">Pass the cancellation token.</param>
		/// <returns></returns>
		public async Task GetContentsLengthAsync(IEnumerable<string> resources, CancellationToken cancellationToken)
		{

			List<int> contentLengths = new ();

			foreach (string url in resources)
			{
				int contentLength = await GetUrlContentLengthAsync(url, cancellationToken);

				contentLengths.Add(contentLength);
			}

			var totalContentsLength = contentLengths.Sum();
			Console.WriteLine($"\nTotal Contents Length:  {totalContentsLength}");
		}

		/// <summary>
		/// Method to process URL asynchronously.
		/// </summary>
		/// <param name="resourceURL"></param>
		/// <param name="cancellationToken"></param>
		/// <returns>Content length for a specific URL.</returns>
		public static async Task<int> GetUrlContentLengthAsync(string resourceURL, CancellationToken cancellationToken)
		{
			var client = new HttpClient();

			Task<string> getStringTask =
				client.GetStringAsync(resourceURL, cancellationToken);

			string contents = await getStringTask;

			return contents.Length;

		}
	}
}
