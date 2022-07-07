using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Microsoft.Extensions.DependencyInjection;

public static class MvcOptionsExtensions
{
    public static MvcOptions AddPagingBinderProvider(this MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new PagingBinderProvider());

        return options;
    }
}