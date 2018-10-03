using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Xamarin.Forms.SampleApp.ViewModels
{
	public abstract class ViewModelBase : BindableBase, INavigationAware
	{
		protected ViewModelBase()
		{
			NavigateCommand = new DelegateCommand<string>(NavigateAsync);
			NavigationParameters = new NavigationParameters();
		}

		public DelegateCommand<string> NavigateCommand { get; private set; }
		protected NavigationParameters NavigationParameters { get; private set; }

		public virtual void OnNavigatedFrom(NavigationParameters parameters)
		{
		}

		public virtual void OnNavigatedTo(NavigationParameters parameters)
		{
		}

		public virtual void OnNavigatingTo(NavigationParameters parameters)
		{
		}

		private async void NavigateAsync(string page)
		{
			Console.WriteLine($"Navigating to page {page}");
			//{MasterDetailPage}/{NavigationPage}/{Page you want to navigate}.
			//MainPage/NavigationPageBase/{page}
			await App.Navigation.NavigateAsync(new Uri(page, UriKind.Relative), NavigationParameters);
			NavigationParameters = new NavigationParameters();
		}
	}
}