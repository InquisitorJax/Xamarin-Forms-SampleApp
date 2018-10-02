using SQLite;

namespace Xamarin.Forms.SampleApp.Repository
{
	public interface IDatabaseConnectionFactory
	{
		SQLiteAsyncConnection CreateConnection();
	}
}