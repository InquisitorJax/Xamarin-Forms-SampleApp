using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Xamarin.Forms.SampleApp.ViewModels
{
	public abstract class ViewModelBase : BindableBase, INavigationAware
	{
		protected INavigationService Navigation { get; private set; }

		protected ViewModelBase(INavigationService navigationService)
		{
			//NOTE: Navigation must be injected per view model :( https://github.com/PrismLibrary/Prism/issues/1366
			NavigateCommand = new DelegateCommand<string>(NavigateAsync);
			NavigationParameters = new NavigationParameters();
			Navigation = navigationService;
		}

		public DelegateCommand<string> NavigateCommand { get; private set; }
		protected NavigationParameters NavigationParameters { get; private set; }

		public virtual void OnNavigatedFrom(INavigationParameters parameters)
		{
		}

		public virtual void OnNavigatedTo(INavigationParameters parameters)
		{
		}

		public virtual void OnNavigatingTo(INavigationParameters parameters)
		{
		}

		private async void NavigateAsync(string page)
		{
			Console.WriteLine($"Navigating to page {page}");
			//{MasterDetailPage}/{NavigationPage}/{Page you want to navigate}.
			//MainPage/NavigationPageBase/{page}
			await Navigation.NavigateAsync(new Uri(page, UriKind.Relative), NavigationParameters);
			NavigationParameters = new NavigationParameters();
		}
	}
}