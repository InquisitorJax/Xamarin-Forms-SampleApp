using System;
using System.Collections.Generic;
using System.Text;
using Prism.Ioc;

namespace Prism.TinyIoc.Forms
{
	public static class PrismIocExtensions
	{
		public static IUnityContainer GetContainer(this IContainerProvider containerProvider)
		{
			return ((IContainerExtension<IUnityContainer>)containerProvider).Instance;
		}

		public static IUnityContainer GetContainer(this IContainerRegistry containerRegistry)
		{
			return ((IContainerExtension<IUnityContainer>)containerRegistry).Instance;
		}
	}
}
