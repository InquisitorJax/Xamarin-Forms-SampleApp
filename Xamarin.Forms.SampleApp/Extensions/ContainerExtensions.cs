using Prism.Ioc;

namespace Xamarin.Forms.SampleApp.Extensions
{
	public static class ContainerExtensions
	{
		public static T Resolve<T>(this IContainerProvider container, string key = null)
		{
			return string.IsNullOrEmpty(key) ? (T)container.Resolve(typeof(T)) : (T)container.Resolve(typeof(T), key);
		}
	}
}