namespace BlazorBlog.Shared
{
	using Microsoft.AspNetCore.Components;
	using System;

	public partial class BlogPager
	{
		private int pageNumber;

		[Parameter]
		public int Count { get; set; }

		[Parameter]
		public int Day { get; set; }

		[Parameter]
		public int Month { get; set; }

		public int NumberOfPages => (int)Math.Ceiling(Count / (double)PageSize);

		[Parameter]
		public int PageNumber
		{
			get => pageNumber;
			set
			{
				if (value < 1)
				{
					pageNumber = 1;
				}
				else
				{
					pageNumber = value;
				}
			}
		}

		[Parameter]
		public int PageSize { get; set; }

		[Parameter]
		public int Year { get; set; }

		private bool DisplayNewer => PageNumber > 1;

		private bool DisplayOlder => PageNumber < NumberOfPages;

		private string GetNewerHref
		{
			get
			{
				string href = string.Empty;
				if (Year != 0)
				{
					href = string.Format("/{0}", Year);
				}

				if (Month != 0)
				{
					href = string.Format("{0}/{1}", href, Month);
				}

				if (Day != 0)
				{
					href = string.Format("{0}/{1}", href, Day);
				}

				return string.Format("{0}/Page/{1}", href, PageNumber - 1);
			}
		}

		private string GetOlderHref
		{
			get
			{
				string href = string.Empty;
				if (Year != 0)
				{
					href = string.Format("/{0}", Year);
				}

				if (Month != 0)
				{
					href = string.Format("{0}/{1}", href, Month);
				}

				if (Day != 0)
				{
					href = string.Format("{0}/{1}", href, Day);
				}

				return string.Format("{0}/Page/{1}", href, PageNumber + 1);
			}
		}
	}
}