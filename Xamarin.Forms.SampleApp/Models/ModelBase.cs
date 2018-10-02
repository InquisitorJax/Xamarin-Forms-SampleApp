using System;
using Prism.Mvvm;
using SQLite;

namespace Xamarin.Forms.SampleApp.Models
{
	public abstract class ModelBase : BindableBase
	{

		public ModelBase()
		{
			_id = Guid.NewGuid().ToString();
		}

		private string _id;

		[PrimaryKey, Unique]
		public string Id
		{
			get { return _id; }
			set { SetProperty(ref _id, value); }
		}
	}
}