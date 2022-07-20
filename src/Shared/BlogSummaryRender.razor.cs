namespace BlazorBlog.Shared
{
	using BlazorBlog.Models;
	using Microsoft.AspNetCore.Components;

	public partial class BlogSummaryRender
	{
		[Parameter]
		public EntryModel? Entry { get; set; }
	}
}