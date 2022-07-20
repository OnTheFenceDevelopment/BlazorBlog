namespace BlazorBlog.Shared
{
	using Microsoft.AspNetCore.Components;
	using System.Collections.Generic;

	public partial class Categories
	{
		[Parameter]
		public List<string>? CategoryArray { get; set; }
	}
}