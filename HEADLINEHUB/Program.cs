using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEADLINEHUB
{
	public class Program
	{
		static  async Task Main(string[] args)
		{
			HeadlinesHubMenu menu = new HeadlinesHubMenu();
			 await menu.DisplayMenu();
			Console.ReadLine();
			
		}
	}
}
