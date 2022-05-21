namespace BlazorBlog.Pages
{
	using BlazorBlog.Models;
	using Microsoft.AspNetCore.Components;
	using System.Collections.Generic;
	using System.Linq;

	public partial class Index
	{
		private List<EntryModel>? blogEntries;
		private string? category;
		private int day;
		private int month;
		private List<EntryModel>? pagedBlogEntries;
		private int pageNumber = 1;
		private string permaLink;
		private int year;

		/// <summary>Gets the page size.</summary>
		public static int PageSize => 5;

		/// <summary>Gets or sets the category filter.</summary>
		[Parameter]
		public string Category
		{
			get => category;
			set
			{
				category = value;
				SetPagedFilteredBlog();
			}
		}

		/// <summary>Gets or sets the day filter.</summary>
		[Parameter]
		public int Day
		{
			get => day;
			set
			{
				day = value;
				SetPagedFilteredBlog();
			}
		}

		/// <summary>Gets or sets the debug string.</summary>
		public string? Debug { get; set; }

		/// <summary>Gets or sets the month filter.</summary>
		[Parameter]
		public int Month
		{
			get => month;
			set
			{
				month = value;
				SetPagedFilteredBlog();
			}
		}

		/// <summary>Gets or sets the pagination page number.</summary>
		[Parameter]
		public int PageNumber
		{
			get => pageNumber;
			set
			{
				pageNumber = value;
				SetPagedFilteredBlog();
			}
		}

		/// <summary>Gets or sets the page route.</summary>
		[Parameter]
		public string? PageRoute { get; set; }

		/// <summary>Gets or sets the permalink filter.</summary>
		[Parameter]
		public string PermaLink
		{
			get => permaLink;
			set
			{
				permaLink = value;
				SetPagedFilteredBlog();
			}
		}

		/// <summary>Gets or sets the total number of blog pages for the applied filter(s).</summary>
		public int TotalPages { get; set; }

		/// <summary>gets or sets the year filter.</summary>
		[Parameter]
		public int Year
		{
			get => year;
			set
			{
				year = value;
				SetPagedFilteredBlog();
			}
		}

		/// <summary>Set the paged filtered blog entries.</summary>
		private void SetPagedFilteredBlog()
		{
#if DEBUG
			Debug = string.Format("PageNumber: {0}, Year:{1}, Month:{2}, Day:{3}, PermaLink:'{4}', Category:'{5}'", PageNumber, Year, Month, Day, PermaLink, Category);
#endif

			TotalPages = 0;
			if (blogEntries == null)
			{
				return;
			}

			List<EntryModel> allBlogEntries = blogEntries;
			if (!string.IsNullOrWhiteSpace(PermaLink))
			{
				// Filter to single entry
				TotalPages = 1;
				pagedBlogEntries = allBlogEntries.Where(abe => abe.Permalink.EndsWith(PermaLink, StringComparison.OrdinalIgnoreCase)).ToList(); // Should be a single, but we using a list to display.
				return;
			}

			if (Year > 0)
			{
				allBlogEntries = allBlogEntries.Where(abe => abe.Created.Year == Year).ToList(); // Filter by year
			}

			if (allBlogEntries.Count == 0)
			{
				return;
			}

			if (Month > 0)
			{
				allBlogEntries = allBlogEntries.Where(abe => abe.Created.Month == Month).ToList(); // Filter by month
			}

			if (allBlogEntries.Count == 0)
			{
				return;
			}

			if (Day > 0)
			{
				allBlogEntries = allBlogEntries.Where(abe => abe.Created.Day == Day).ToList(); // Filter by day
			}

			if (allBlogEntries.Count == 0)
			{
				return;
			}

			if (!string.IsNullOrWhiteSpace(Category))
			{
				allBlogEntries = allBlogEntries.Where(abe => abe.Categories.Contains(Category)).ToList(); // Filter by category
			}

			if (allBlogEntries.Count == 0)
			{
				return;
			}

			TotalPages = allBlogEntries.Count;

			if (PageNumber > 0)
			{
				pagedBlogEntries = allBlogEntries.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
			}
			else
			{
				pagedBlogEntries = allBlogEntries.Take(PageSize).ToList();
			}
		}
	}
}