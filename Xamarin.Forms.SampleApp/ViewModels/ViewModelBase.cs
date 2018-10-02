using System;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Xamarin.Forms.SampleApp.ViewModels
{
	public abstract class ViewModelBase : BindableBase
	{
		private readonly INavigationService _navigation;
		protected ViewModelBase(INavigationService navigation)
		{
			NavigateCommand = new DelegateCommand<string>(NavigateAsync);
			_navigation = navigation;
			NavigationParameters = new NavigationParameters();
		}

		protected NavigationParameters NavigationParameters { get; private set; }

		public DelegateCommand<string> NavigateCommand { get; private set; }

		private async void NavigateAsync(string page)
		{
			Console.WriteLine($"Navigating to page {page}");
			//{MasterDetailPage}/{NavigationPage}/{Page you want to navigate}.
			//MainPage/NavigationPageBase/{page}
			await _navigation.NavigateAsync(new Uri(page, UriKind.Relative), NavigationParameters);
			NavigationParameters = new NavigationParameters();
		}
	}
}