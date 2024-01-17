




using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;


namespace HEADLINEHUB
{
	public class NewsApiClient
	{
		private HttpClient _httpClient;
		private string _apiKey;
		private const string  BaseUrl = "https://newsapi.org";
		private const string UserAgent = "HEADlINEHUB/1.0";

		public NewsApiClient(string apikey) 
		{
			_apiKey = apikey;
			_httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Add("User-Agent",UserAgent);
		}

		public async Task<List<Article>> GetTopHeadLinesAsync() 
		{
			try
			{
				
				string endPoint = "/v2/top-headlines";
				string country = "us";
				string apiUrl = $"{BaseUrl}{endPoint}";
				DateTime dateTime = DateTime.Now;
				string fullUrl = $"{apiUrl}?country={country}&from={dateTime}&apiKey={_apiKey}";
				

				using (HttpResponseMessage response = await _httpClient.GetAsync(fullUrl))
				{
					if (response.IsSuccessStatusCode)
					{
						string json = await response.Content.ReadAsStringAsync();
						var newsData = JsonConvert.DeserializeObject<NewsResponse>(json);

						List<Article> article = newsData.articles;
						
						return article; 
					}
					else
					{
						Console.WriteLine($"Error: {response.StatusCode}");
						Console.WriteLine(await response.Content.ReadAsStringAsync());
						return null;
					}
				}

			}
			catch (HttpRequestException ex)
			{

				Console.WriteLine($"Http Request error:{ex.Message}");
				return null;
            }
			catch (Exception ex)
			{
				Console.WriteLine($"An error occured: {ex.Message}");
				return null;
			}
		}

		public async Task<List<Article>> GetArticlesByCategoryAsync(NewsCategory category)
		{
			try
			{
				string endpoint = "/v2/top-headlines";
				string country = "gb";
				string stringCategory = category.ToString().ToLower();
				DateTime dateTime = DateTime.Now;

				string apiUrl = $"{BaseUrl}{endpoint}";

				string fullUrl = $"{apiUrl}?country={country}&from={dateTime}&category={stringCategory}&apiKey={_apiKey}";

				using (HttpResponseMessage response = await _httpClient.GetAsync(fullUrl))
				{
					if (response.IsSuccessStatusCode)
					{
						string json = await response.Content.ReadAsStringAsync();
						var newsData = JsonConvert.DeserializeObject<NewsResponse>(json);

						List<Article> articles = newsData.articles;

						return articles;
					}
					else
					{
						Console.WriteLine($"Error: {response.StatusCode}");
						Console.WriteLine(await response.Content.ReadAsStringAsync()); 

						return null;
					}
					
				}

			}
			catch (HttpRequestException ex)
			{
				Console.WriteLine($"Http Request Error: {ex.Message}");

				return null;	
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occured {ex.Message}");

				return null;
			}
		

		}
		
		public async Task<List<Article>> SearchArticlesAsync(string search)
		{
			try
			{
				string endPoint = "/v2/top-headlines";
				
				string apiUrl = $"{BaseUrl}{endPoint}";
				DateTime dateTime = DateTime.Now;

				// full url of the request 
				string fullUrl = $"{apiUrl}?q={search}&from={dateTime}&apiKey={_apiKey}";
				using (HttpResponseMessage reponse = await _httpClient.GetAsync(fullUrl))
				{
					// Checking if the expected response is given
					if (reponse.IsSuccessStatusCode)
					{
						string json = await reponse.Content.ReadAsStringAsync();
						var newsData = JsonConvert.DeserializeObject<NewsResponse>(json);
						List<Article> article = newsData.articles;
						return article;
					}
					else
					{
						Console.WriteLine(reponse.StatusCode);
						Console.WriteLine(await reponse.Content.ReadAsStringAsync());
						return null;
					}
				}


			
			}
			catch(HttpRequestException ex) 
			{
				Console.WriteLine($"HttpRequest Error: {ex.Message}");
				
				return null; 
			}catch(Exception ex)
			{
				Console.WriteLine($"An Error occured: {ex.Message}");
				return null;
			}
		} 
		public async Task OpenContentInBrowser(Article selectedArticle)
		{
			try
			{
				if (string.IsNullOrEmpty(selectedArticle.Url))
				{
					Console.WriteLine("Invalid Article or Wrong Url");
					return;
				}
				await Task.Run(() => { Process.Start(selectedArticle.Url); });
			}

			catch (System.ComponentModel.Win32Exception ex)
			{
				Console.WriteLine($"An error occured: {ex.Message}");
			}
			return;
		}
		
		
	}
}
