using Prism;
using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Forms.SampleApp.ViewModels;
using Xamarin.Forms.SampleApp.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Xamarin.Forms.SampleApp
{
	public partial class App : PrismApplication
	{
		/*
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor.
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
		public App() : this(null)
		{
		}

		public App(IPlatformInitializer initializer) : base(initializer)
		{
		}

		public static IContainerProvider Ioc { get; private set; }

		public static IEventAggregator EventManager => Ioc.Resolve<IEventAggregator>();

		protected override async void OnInitialized()
		{
			InitializeComponent();

			Ioc = Container;

			//initialize the repo connection (create tables if necessary)
			var repo = Ioc.Resolve<IRepository>();
			await repo.InitializeAsync();

			await NavigationService.NavigateAsync("MainPage/NavigationPageBase/TodoItemsPage");
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterSingleton<IRepository, RepositoryInstance>();

			containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
			containerRegistry.RegisterForNavigation<TodoItemsPage>();
			containerRegistry.RegisterForNavigation<TodoItemPage, TodoItemViewModel>();

			containerRegistry.RegisterForNavigation<NavigationPageBase>();
			containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
			containerRegistry.RegisterForNavigation<SettingsPage, SettingsViewModel>();
			containerRegistry.RegisterForNavigation<ProfilePage, ProfileViewModel>();
		}

		protected override void OnSleep()
		{
			base.OnSleep();
		}

		protected override void OnResume()
		{
			base.OnResume();
		}

		protected override void OnStart()
		{
			base.OnStart();
		}
	}
}