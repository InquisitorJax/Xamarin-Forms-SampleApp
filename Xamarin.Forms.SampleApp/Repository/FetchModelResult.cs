using Wibci.LogicCommand;
using Xamarin.Forms.SampleApp.Models;

namespace Xamarin.Forms.SampleApp.Repository
{
	public class FetchModelResult<T> : CommandResult where T : ModelBase
	{
		public T Model { get; set; }
	}
}