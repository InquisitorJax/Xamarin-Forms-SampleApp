using Prism.Navigation;

namespace Xamarin.Forms.SampleApp.Views
{
	public partial class MainPage : MasterDetailPage, IMasterDetailPageOptions
	{
		public MainPage()
		{
			InitializeComponent();
		}

		public bool IsPresentedAfterNavigation => false;
	}
}