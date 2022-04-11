namespace Microsoft.AspNetCore.Mvc.ModelBinding;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class FromQueryPagingAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource BindingSource { get; } = PagingBindingSource.FromQueryPaging;
}