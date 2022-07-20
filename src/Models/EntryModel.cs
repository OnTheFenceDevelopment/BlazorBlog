namespace BlazorBlog.Models
{
	using System;
	using System.Collections.Generic;

	public class EntryModel
	{
		public string Author { get; set; } = "N/A";

		public List<string>? Categories { get; set; }

		public string[] Content { get; set; } = new List<string>().ToArray();

		public DateTimeOffset Created { get; set; }

		public int Id { get; set; }

		public string Permalink { get; set; } = string.Empty;

		public string Title { get; set; } = string.Empty;
	}
}