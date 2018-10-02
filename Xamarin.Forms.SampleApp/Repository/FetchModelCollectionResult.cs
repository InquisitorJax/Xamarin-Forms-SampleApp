using System.Collections.Generic;
using Wibci.LogicCommand;
using Xamarin.Forms.SampleApp.Models;

namespace Xamarin.Forms.SampleApp.Repository
{
	public class FetchModelCollectionResult<T> : CommandResult where T : ModelBase
	{
		public IList<T> ModelCollection { get; set; }
	}
}