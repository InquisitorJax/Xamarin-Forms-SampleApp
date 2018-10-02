using System.Windows.Input;

namespace Xamarin.Forms.SampleApp.Controls
{
	/// <summary>
	/// ListView with ItemTapCommand property
	/// TODO: look into attached properties vs this inheritance
	/// </summary>
	public class ListViewEx : ListView
	{
		
		public static BindableProperty ItemTapCommandProperty = BindableProperty.Create("ItemTapCommand", typeof(ICommand), typeof(ListView), null);

		public ListViewEx()
		{
			this.ItemTapped += this.OnItemTapped;			
		}

		public ICommand ItemTapCommand
		{
			get { return (ICommand)this.GetValue(ItemTapCommandProperty); }
			set { this.SetValue(ItemTapCommandProperty, value); }
		}

		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item != null && this.ItemTapCommand != null && this.ItemTapCommand.CanExecute(e.Item))
			{
				this.ItemTapCommand.Execute(e.Item);
				this.SelectedItem = null;
			}
		}
	}
}