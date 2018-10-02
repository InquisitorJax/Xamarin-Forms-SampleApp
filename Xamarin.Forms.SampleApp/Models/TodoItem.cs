using Prism.Mvvm;

namespace Xamarin.Forms.SampleApp.Models
{
	public class TodoItem : ModelBase
	{
		private string _text;

		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}

		private bool _isCompleted;

		public bool IsCompleted
		{
			get { return _isCompleted; }
			set { SetProperty(ref _isCompleted, value); }
		}

	}
}