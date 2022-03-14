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
		public async Task DownloadService(IEnumerable<string> resources, CancellationToken cancellationToken)
		{
			HttpClient httpClient = new HttpClient();

			List<int> contentLengths = new List<int>();

			foreach (string url in resources)
			{
				int contentLength = await DownloadContent(url, httpClient, cancellationToken);

				contentLengths.Add(contentLength);
			}

			var totalContentsLength = contentLengths.Sum();
			Console.WriteLine($"\nTotal Contents Length:  {totalContentsLength}");
		}

		/// <summary>
		/// Method to process URL asynchronously.
		/// </summary>
		/// <param name="resourceURL"></param>
		/// <param name="httpClient"></param>
		/// <param name="cancellationToken"></param>
		/// <returns>Content length for a specific URL.</returns>
		public static async Task<int> DownloadContent(string resourceURL, HttpClient httpClient, CancellationToken cancellationToken)
		{
			HttpResponseMessage httpResponse = await httpClient.GetAsync(resourceURL, cancellationToken);

			byte[] contentBytesArray = await httpResponse.Content.ReadAsByteArrayAsync(cancellationToken);

			Console.WriteLine($"{resourceURL,-60} {contentBytesArray.Length,10:#,#}");

			return contentBytesArray.Length;
		}
	}
}
