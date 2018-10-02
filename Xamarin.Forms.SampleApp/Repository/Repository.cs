using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Wibci.LogicCommand;
using Xamarin.Forms.SampleApp.Extensions;
using Xamarin.Forms.SampleApp.Models;
using Xamarin.Forms.SampleApp.Repository;

namespace Xamarin.Forms.SampleApp
{
	public interface IRepository
	{
		Task InitializeAsync();

		Task<FetchModelResult<T>> FetchItemAsync<T>(string id) where T : ModelBase, new();

		Task<FetchModelCollectionResult<T>> FetchItemsAsync<T>() where T : ModelBase, new();

		Task<Notification> SaveItemAsync<T>(T item, ModelUpdateEvent updateEvent);
	}

	public class RepositoryInstance : IRepository
	{
		private bool _isInitialized = false;
		private SQLiteAsyncConnection _database;

		public async Task<FetchModelResult<T>> FetchItemAsync<T>(string id) where T : ModelBase, new()
		{
			FetchModelResult<T> retResult = new FetchModelResult<T>();

			var item = await _database.FindAsync<T>(id);
			retResult.Model = item;

			return retResult;
		}

		public async Task<FetchModelCollectionResult<T>> FetchItemsAsync<T>() where T : ModelBase, new()
		{
			FetchModelCollectionResult<T> retResult = new FetchModelCollectionResult<T>();
			var items = await _database.Table<T>().ToListAsync();
			retResult.ModelCollection = items;
			return retResult;
		}

		public async Task InitializeAsync()
		{
			if (_isInitialized)
				return;

			_isInitialized = true;

			var pathProvider = App.Ioc.Resolve<IRepositoryPathProvider>();
			var path = Path.Combine(pathProvider.DatabasePath, "TodoItemsDatabase.db");

			try
			{
				_database = new SQLiteAsyncConnection(path, false);
			}
			catch (SQLiteException)
			{
				//TODO: Log
				Console.WriteLine("Error Creating Database Connection");
			}

			var connection = _database.GetConnection();
			connection.CreateTable<TodoItem>();
		}

		public async Task<Notification> SaveItemAsync<T>(T item, ModelUpdateEvent updateEvent)
		{
			Notification retNotification = Notification.Success();
			try
			{
				if (updateEvent == ModelUpdateEvent.Created)
				{
					await _database.InsertAsync(item);
				}
				else
				{
					await _database.UpdateAsync(item);
				}
			}
			catch (SQLiteException)
			{
				//LOG:
				retNotification.Add(new NotificationItem("Save Failed"));
			}

			return retNotification;
		}
	}
}