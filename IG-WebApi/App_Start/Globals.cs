using IG_CoreLibrary;
using IG_CoreLibrary.Repository;
using IG_WebApi.App_Start;
using Microsoft.Extensions.DependencyModel;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class Globals {
    public static IBaseRepository BaseRepository => new StandardKernel().Get<BaseRepository>();
	
}
