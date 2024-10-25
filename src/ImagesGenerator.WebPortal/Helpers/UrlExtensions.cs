namespace ImagesGenerator.WebPortal.Helpers
{
    public static class UrlExtensions
	{
		public static string Index(this IUrlHelper urlHelper)
			=> urlHelper.Page("/Index")!;

        public static string Payment(this IUrlHelper urlHelper)
            => urlHelper.Page("/Payment")!;

        public static string Result(this IUrlHelper urlHelper, string? orderUid)
            => urlHelper.Page("/Result", new { orderUid = orderUid })!;

        public static string Setup(this IUrlHelper urlHelper) // TODO: Rename to Create or Start
            => urlHelper.Page("/Setup")!;
    }
}