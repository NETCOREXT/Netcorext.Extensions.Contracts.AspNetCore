using Netcorext.Contracts;

namespace Microsoft.AspNetCore.Mvc.ModelBinding;

public class PagingBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType != typeof(Paging))
            return null;

        if (context.BindingInfo.BindingSource != PagingBindingSource.FromQueryPaging)
            return null;

        return new PagingBinder();
    }
}