using System;
using Prism.Navigation;
using Xamarin.Forms.SampleApp.Models;

namespace Xamarin.Forms.SampleApp.ViewModels
{
	public class TodoItemViewModel : ViewModelBase, INavigationAware
	{
		public TodoItemViewModel(INavigationService navigation) : base(navigation)
		{
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			
		}

		private TodoItem _model;

		public TodoItem Model
		{
			get { return _model; }
			set { SetProperty(ref _model, value); }
		}


		public void OnNavigatingTo(NavigationParameters parameters)
		{
			//TODO: should be string ID, and get from the repo
			if (parameters.ContainsKey("Model"))
			{
				Console.WriteLine("w00t! You sent a model!");
				Model = (TodoItem)parameters["Model"];
			}
		}
	}
}