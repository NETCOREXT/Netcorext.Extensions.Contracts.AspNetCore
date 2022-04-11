namespace Microsoft.AspNetCore.Mvc.ModelBinding;

public class PagingBindingSource : BindingSource
{
    public static readonly BindingSource FromQueryPaging = new PagingBindingSource("FromQueryPaging", "FromQueryPaging", true, true);

    public PagingBindingSource(string id, string displayName, bool isGreedy, bool isFromRequest) : base(id, displayName, isGreedy, isFromRequest)
    { }

    public override bool CanAcceptDataFrom(BindingSource bindingSource)
    {
        return bindingSource == this;
    }
}