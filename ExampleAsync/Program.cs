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

			Console.WriteLine(MessageConstants.cancelMessage);
						
			Task cancelTask = Task.Run(() =>
			{
				
				while (Console.ReadKey().Key != ConsoleKey.Enter)
				{

					Console.WriteLine(MessageConstants.invalidKeyMessage);
				}


				Console.WriteLine(MessageConstants.validKeyMessage);

				
				cancellationTokenSource.Cancel();
			});

			IService downloadService = new Service();

			Task contentTask = downloadService.GetContentsLengthAsync(resources, userCancellationToken);

			await Task.WhenAny(new[] { cancelTask, contentTask });
		}

	}
}
