namespace Xamarin.Forms.SampleApp.Models
{
	public class Profile : ModelBase
	{
		private byte[] _image;

		private string _name;

		public byte[] Image
		{
			get { return _image; }
			set { SetProperty(ref _image, value); }
		}

		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
	}
}