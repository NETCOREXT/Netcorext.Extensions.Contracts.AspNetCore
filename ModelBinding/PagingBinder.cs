using Netcorext.Contracts;

namespace Microsoft.AspNetCore.Mvc.ModelBinding;

public class PagingBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

        var modelName = bindingContext.ModelName ?? bindingContext.FieldName;

        var model = new Paging();

        var p = GetQuery(bindingContext.ActionContext.HttpContext.Request.Query, nameof(Paging.Offset), nameof(Paging.Offset));

        if (!string.IsNullOrWhiteSpace(p) && int.TryParse(p, out var offset))
        {
            model.Offset = offset;
        }

        var ps = GetQuery(bindingContext.ActionContext.HttpContext.Request.Query, nameof(Paging.Limit), nameof(Paging.Limit));

        if (!string.IsNullOrWhiteSpace(ps) && int.TryParse(ps, out var limit))
        {
            model.Limit = limit;
        }

        bindingContext.Result = ModelBindingResult.Success(model);

        return Task.CompletedTask;
    }

    private string GetQuery(IQueryCollection queryCollection, params string[] keys)
    {
        foreach (var key in keys)
        {
            if (queryCollection.TryGetValue(key, out var query))
            {
                return query;
            }
        }

        return null;
    }
}