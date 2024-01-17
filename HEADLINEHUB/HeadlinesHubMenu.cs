using System;
using System.Runtime.Remoting.Services;
using System.Threading.Tasks;

namespace HEADLINEHUB
{
	
	public class HeadlinesHubMenu
	{
		
		public async Task  DisplayMenu()
		{
			NewsControl newsControl = new NewsControl();
			//News Menu with Options
			Console.WriteLine("HeadLineHub Menu");
            Console.WriteLine("1. Top Headlines");
			Console.WriteLine("2. News by Category");
			Console.WriteLine("3. Search for News");
			Console.WriteLine("Enter a menu option");
			while (true)
			{
				string option = Console.ReadLine();
				if (!string.IsNullOrEmpty(option) && int.TryParse(option, out int result))
				{
					
					switch (result)
					{
						case 1:
							await newsControl.DisplayTopHeadLinesAsync();
							break;
						case 2:
							await newsControl.NewsByCategory();
							break;
						case 3:
							await newsControl.DisplaySearchNewsAsync(); 
							break;
						default:
							Console.WriteLine("Enter a valid Input as seen on the list");
							break;


					}

				}
				else
				{
					Console.WriteLine("Invalid input. Please enter a valid option");
				}
			}

			
			
        }
		
	}
}
