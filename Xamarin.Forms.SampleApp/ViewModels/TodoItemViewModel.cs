using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Wibci.LogicCommand;
using Xamarin.Forms.SampleApp.Messages;
using Xamarin.Forms.SampleApp.Models;
using static Xamarin.Forms.SampleApp.Constants;

namespace Xamarin.Forms.SampleApp.ViewModels
{
	public class TodoItemViewModel : ViewModelBase
	{
		private readonly IRepository _repository;
		private bool _isNewModel;
		private TodoItem _model;

		public TodoItemViewModel(IRepository repository, INavigationService navigation) : base(navigation)
		{
			_repository = repository;
			SaveItemCommand = new DelegateCommand(SaveItem);
		}

		public ICommand SaveItemCommand { get; private set; }

		public TodoItem Model
		{
			get { return _model; }
			set { SetProperty(ref _model, value); }
		}

		public override async void OnNavigatingTo(NavigationParameters parameters)
		{
			//TODO: should be string ID, and get from the repo
			if (parameters.ContainsKey(NavigationParameterName.ModelId))
			{
				string id = (string)parameters[NavigationParameterName.ModelId];
				var fetchResult = await _repository.FetchItemAsync<TodoItem>(id);
				if (fetchResult.IsValid())
				{
					Console.WriteLine($"w00t! You sent a model! {Model.Text}");
					Model = fetchResult.Model;
				}
				else
				{
					Console.WriteLine($"Booooo! Couldn't find model with id! {id}");
					//TODO: Notification.Show(fetchResult.Notification.ToString());
				}
			}
			else
			{
				Model = new TodoItem { Text = "New Item" };
				_isNewModel = true;
			}
		}

		private async void SaveItem()
		{
			Notification result = Notification.Success();
			ModelUpdateEvent updateEvent = _isNewModel ? ModelUpdateEvent.Created : ModelUpdateEvent.Updated;

			//TODO: model validation

			var saveResult = await _repository.SaveItemAsync(Model, updateEvent);
			result.AddRange(saveResult);

			if (result.IsValid())
			{
				ModelUpdatedMessageResult<TodoItem> eventResult = new ModelUpdatedMessageResult<TodoItem> { UpdatedModel = Model, UpdateEvent = updateEvent };
				App.EventManager.GetEvent<ModelUpdatedMessageEvent<TodoItem>>().Publish(eventResult);
				await Navigation.GoBackAsync(); //NOTE: fails :( will work if INavigationService injected in constructor
			}
			else
			{
				//TODO: show user failure message
				//await UserNotifier.ShowMessageAsync(result.ToString(), "Save Failed");
			}
		}
	}
}