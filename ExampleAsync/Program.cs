using ExampleAsync.Utility;
using ExampleAsync.Services;
namespace ExampleAsync
{
    internal class Program
    {
		/// <summary>
		/// Asynchronous Method to Download the resources and aggregates the content length. Handling cancellation request
		/// </summary>
		/// <returns></returns>
		static async Task Main()
		{
			
			IEnumerable<string> resources = ResourceConstant.resources;
						
			CancellationTokenSource cancellationTokenSource = new();
						
			CancellationToken userCancellationToken = cancellationTokenSource.Token;
						
			ShowMessage(MessageConstants.cancelMessage);
						
			Task cancelTask = Task.Run(() =>
			{
				
				while (Console.ReadKey().Key != ConsoleKey.Enter)
				{
					
					ShowMessage(MessageConstants.invalidKeyMessage);
				}

				
				ShowMessage(MessageConstants.validKeyMessage);

				
				cancellationTokenSource.Cancel();
			});

			IService downloadService = new Service();

			Task aggregateContentLengthsTask = downloadService.DownloadService(resources, userCancellationToken);

			await Task.WhenAny(new[] { cancelTask, aggregateContentLengthsTask });
		}

		/// <summary>
		/// ShowMessage
		/// </summary>
		/// <param name="cancelMessage">Cancel Message</param>
		private static void ShowMessage(string cancelMessage)
		{
			Console.WriteLine(cancelMessage);
		}
	}
}
