using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;


namespace HEADLINEHUB
{
	public enum NewsCategory
	{
		General,
		Business,
		Technology,
		Health,
		Science,
		Sport
	}
	public class NewsControl
	{
		
		public async Task DisplayTopHeadLinesAsync()
		{
			NewsApiClient newsApiClient = new NewsApiClient("cda9fa1e7ae44a998857026e84763013");
			Console.WriteLine("Displaying Today's HeadLines");
			var topHeadLines = await newsApiClient.GetTopHeadLinesAsync();
			Console.WriteLine(topHeadLines.Count);
			for (var i = 0; i < topHeadLines.Count; i++)
			{
				var article = topHeadLines[i];
				Console.WriteLine($"{i + 1} {article.Title}");
			}

			//Displaying article in Browser
			Console.WriteLine("Enter an article number to view full article");
			string input = Console.ReadLine();
			if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int index))
			{
				int selectedArticleIndex = index - 1;
				if (selectedArticleIndex >= 0 && selectedArticleIndex < topHeadLines.Count)
				{
					var selectedArticle = topHeadLines[selectedArticleIndex];

					await newsApiClient.OpenContentInBrowser(selectedArticle);
				}
			}
			else
			{
				Console.WriteLine("Please Enter a correct article number");
			}
			


		}
		public async  Task NewsByCategory()
		{
			NewsApiClient apiClient = new NewsApiClient("cda9fa1e7ae44a998857026e84763013");
            //News by Category
            Console.WriteLine("News Category");
			Console.WriteLine("1. General");
			Console.WriteLine("2. Business");
			Console.WriteLine("3. Technology");
			Console.WriteLine("4. Health");
			Console.WriteLine("5. Science");
            Console.WriteLine("6. Sport");
            Console.WriteLine();
			Console.WriteLine("Select a news Category");
			while (true) 
			{ 
				string inputCategory = Console.ReadLine();

				if (inputCategory != null && int.TryParse(inputCategory, out  int category)) 
				{
					try
					{
						List<Article> articleCategory = new List<Article>();
						switch (category)
						{
							case 1:
								articleCategory = await apiClient.GetArticlesByCategoryAsync(NewsCategory.General);
								break;
							case 2:
								articleCategory = await apiClient.GetArticlesByCategoryAsync(NewsCategory.Business);
								break;
							case 3:
								articleCategory = await apiClient.GetArticlesByCategoryAsync(NewsCategory.Technology);
								break;
							case 4:
								articleCategory = await apiClient.GetArticlesByCategoryAsync(NewsCategory.Health);
								break;
							case 5:
								articleCategory = await apiClient.GetArticlesByCategoryAsync(NewsCategory.Science);
								break;
							case 6:
								articleCategory = await apiClient.GetArticlesByCategoryAsync(NewsCategory.Sport);
								break;
							default:
								Console.WriteLine("Please select a valid category");
								continue;
						}

						// Working with selected Category
						for(int i = 0; i < articleCategory.Count; i++) 
						{
							var article = articleCategory[i];
							Console.WriteLine($"{i + 1} {article.Title}");
							
						}

						//Displaying Article in Browser
						Console.WriteLine("Enter an article number to view full article");
						string input = Console.ReadLine();
						if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int index))
						{
							int selectedArticleIndex = index - 1;
							if(selectedArticleIndex >= 0 && selectedArticleIndex < articleCategory.Count) 
							{
								var selectedArticle = articleCategory[selectedArticleIndex];

								await apiClient.OpenContentInBrowser(selectedArticle);
							}
						}
						else
						{
							Console.WriteLine("Please Enter a correct article number");
						}

						
						
						
					}
					catch (Exception ex)
					{

						Console.WriteLine($"Error: {ex.Message}");
					}
					
				}
				else
				{
					Console.WriteLine("Invalid Input please try again");
                }
			}
			
			
        }
		public async Task DisplaySearchNewsAsync()
		{
			
			// Displaying searched news
			Console.WriteLine("Enter KeyWord to search for news");
			string searchString = Console.ReadLine().ToLower();
			NewsApiClient searchApiClient = new NewsApiClient("cda9fa1e7ae44a998857026e84763013");
			var searchResults = await searchApiClient.SearchArticlesAsync(searchString);
			if(searchResults.Count > 0)
			{
				for (var i = 0; i < searchResults.Count; i++)
				{
					var article = searchResults[i];
					Console.WriteLine($"{i + 1} {article.Title}");
				}
	
				//Displaying article in browser
				Console.WriteLine("Enter an article number to view full article");
				string input = Console.ReadLine();
				if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int index))
				{
					int selectedArticleIndex = index - 1;
					if (selectedArticleIndex >= 0 && selectedArticleIndex < searchResults.Count)
					{
						var selectedArticle = searchResults[selectedArticleIndex];

						await searchApiClient.OpenContentInBrowser(selectedArticle);
					}
				}
				else
				{
					Console.WriteLine("Please Enter a correct article number");
				}
			}
			else
			{
                Console.WriteLine("we are yet to receive news updates on the information you seek");
            }	 		
			
		}
		

		

			
	}
}
