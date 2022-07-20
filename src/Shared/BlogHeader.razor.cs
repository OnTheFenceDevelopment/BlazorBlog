namespace BlazorBlog.Shared
{
	using Microsoft.AspNetCore.Components;
	using System;
	using System.Globalization;

	public partial class BlogHeader
	{
		[Parameter]
		public string? Category { get; set; }

		[Parameter]
		public int Day { get; set; }

		[Parameter]
		public int Month { get; set; }

		[Parameter]
		public string PermaLink { get; set; }

		[Parameter]
		public int Year { get; set; }

		private string DisplayHeader
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(Category))
				{
					return string.Format(CultureInfo.InvariantCulture, "Category: {0}", Category);
				}

				if (Year == 0 || !string.IsNullOrEmpty(PermaLink))
				{
					return string.Empty;
				}

				if (Day != 0)
				{
					DateTime fakeDateTime = new(Year, Month, Day);
					return string.Format(CultureInfo.InvariantCulture, "Day: {0}", fakeDateTime.ToString("dddd dd MMMM yyyy", CultureInfo.InvariantCulture));
				}
				else
				{
					if (Month != 0)
					{
						DateTime fakeDateTime = new(Year, Month, 1);
						return string.Format(CultureInfo.InvariantCulture, "Month: {0}", fakeDateTime.ToString("MMMM yyyy", CultureInfo.InvariantCulture));
					}
					else
					{
						return string.Format(CultureInfo.InvariantCulture, "Year: {0}", Year);
					}
				}
			}
		}
	}
}