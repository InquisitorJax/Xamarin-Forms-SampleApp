using Prism;
using Prism.Ioc;
using Xamarin.Forms.SampleApp.Repository;

namespace Xamarin.Forms.SampleApp.Droid
{
	public class ApplicationInitializer : IPlatformInitializer
	{
		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.Register<IRepositoryPathProvider, RepositoryPathProvider>();
		}
	}
}