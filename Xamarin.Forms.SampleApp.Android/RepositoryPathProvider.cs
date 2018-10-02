using System.IO;
using SQLite;
using Xamarin.Forms.SampleApp.Repository;

namespace Xamarin.Forms.SampleApp.Droid
{
	public class RepositoryPathProvider : IRepositoryPathProvider
	{
		public string DatabasePath => GetDatabasePath();

		private string GetDatabasePath()
		{			
			string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder		
			return path;
		}
	}
}