using Prism.Navigation;

namespace Xamarin.Forms.SampleApp.Views
{
	public class NavigationPageBase : NavigationPage, INavigationPageOptions
	{
		public bool ClearNavigationStackOnNavigation => false;
	}
}