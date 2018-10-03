using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Xamarin.Forms.SampleApp.Extensions;
using Xamarin.Forms.SampleApp.Messages;
using Xamarin.Forms.SampleApp.Models;
using Xamarin.Forms.SampleApp.Views;
using static Xamarin.Forms.SampleApp.Constants;

namespace Xamarin.Forms.SampleApp.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		private readonly IRepository _repo;
		private SubscriptionToken _modelUpdatedEventToken;

		private ObservableCollection<TodoItem> _todoItems;

		public MainPageViewModel(IRepository repository)
		{
			_repo = repository;
			OpenSelectedTodoItemCommand = new DelegateCommand(OpenSelectedTodoItem);
			FetchTodoItemsCommand = new DelegateCommand(FetchTodoItems);
		}

		public ICommand OpenSelectedTodoItemCommand { get; private set; }		
		public ICommand FetchTodoItemsCommand { get; private set; }

		public ObservableCollection<TodoItem> TodoItems
		{
			get { return _todoItems; }
			set { SetProperty(ref _todoItems, value); }
		}

		private TodoItem _selectedItem;

		public TodoItem SelectedItem
		{
			get { return _selectedItem; }
			set { SetProperty(ref _selectedItem, value); }
		}

		public async override void OnNavigatingTo(NavigationParameters parameters)
		{
			_modelUpdatedEventToken = App.EventManager.GetEvent<ModelUpdatedMessageEvent<TodoItem>>().Subscribe(OnTodoItemUpdated);
			await LoadTodoItemsAsync();
		}

		public override void OnNavigatedFrom(NavigationParameters parameters)
		{
			App.EventManager.GetEvent<ModelUpdatedMessageEvent<TodoItem>>().Unsubscribe(_modelUpdatedEventToken);
		}

		private void OnTodoItemUpdated(ModelUpdatedMessageResult<TodoItem> updateResult)
		{
			TodoItems.UpdateCollection(updateResult.UpdatedModel, updateResult.UpdateEvent);
		}

		private void OpenSelectedTodoItem()
		{
			//NOTE: ID passed as string value to support url query navigation as well
			NavigationParameters.Add(NavigationParameterName.ModelId, SelectedItem.Id);
			NavigateCommand.Execute($"{ViewKeys.Prefix}{ViewKeys.TodoItemPage}");
		}

		private async void FetchTodoItems()
		{
			await LoadTodoItemsAsync();
		}

		private async Task LoadTodoItemsAsync()
		{
			var itemsResult = await _repo.FetchItemsAsync<TodoItem>();
			if (itemsResult.IsValid())
			{
				TodoItems = itemsResult.ModelCollection.AsObservableCollection();

				if (!TodoItems.Any())
				{
					TodoItems.Add(new TodoItem { Text = "Ghost Item" });
				}
			}
		}
	}
}