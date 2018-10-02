using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.SampleApp.Extensions;
using Xamarin.Forms.SampleApp.Models;
using Xamarin.Forms.SampleApp.Views;

namespace Xamarin.Forms.SampleApp.ViewModels
{
	public class MainPageViewModel : ViewModelBase, INavigationAware
	{
		private readonly IRepository _repo;

		private ObservableCollection<TodoItem> _todoItems;

		public MainPageViewModel(IRepository repository, INavigationService navigation) : base(navigation)
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

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			//nothing
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			//nothing
		}

		public async void OnNavigatingTo(NavigationParameters parameters)
		{
			await LoadTodoItemsAsync();
		}

		private void OpenSelectedTodoItem()
		{
			NavigationParameters.Add("Model", SelectedItem);
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