namespace BlazorBlog.Shared
{
	using BlazorBlog.Models;
	using Microsoft.AspNetCore.Components;

	public partial class BlogRender
	{
		[Parameter]
		public EntryModel? Entry { get; set; }
	}
}