using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.SampleApp.Repository
{
    public interface IRepositoryPathProvider
    {
		string DatabasePath { get; }
    }
}
