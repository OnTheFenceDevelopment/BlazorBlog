namespace BlazorBlog.Shared
{
	using BlazorBlog.Models;
	using Microsoft.AspNetCore.Components;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public partial class TagCloud
	{
		[Parameter]
		public List<EntryModel>? BlogItems { get; set; }

		public SortedDictionary<string, WeightAndPostCount> TagCould
		{
			get
			{
				Dictionary<string, int> categoryDictionary = new();
				foreach (string cat in from EntryModel item in BlogItems
									   from string cat in item.Categories
									   select cat)
				{
					if (categoryDictionary.ContainsKey(cat))
					{
						categoryDictionary[cat] = categoryDictionary[cat] + 1;
					}
					else
					{
						categoryDictionary.Add(cat, 1);
					}
				}

				int totalcategories = categoryDictionary.Count;
				int grouping = (int)Math.Ceiling(totalcategories / 9D);
				int weight = 9;
				int count = 0;

				IOrderedEnumerable<KeyValuePair<string, int>> orderdDictionary = categoryDictionary.Where(cd => cd.Key != string.Empty).OrderByDescending(cd => cd.Value);
				SortedDictionary<string, WeightAndPostCount> weightedCategoryDictionary = new();
				foreach (KeyValuePair<string, int> item in orderdDictionary)
				{
					count++;
					if (count >= grouping)
					{
						count = 0;
						weight--;
					}

					WeightAndPostCount itemWeightAndPostCount = new()
					{
						Weight = weight,
						PostCount = item.Value,
					};

					weightedCategoryDictionary.Add(item.Key, itemWeightAndPostCount);
				}

				return weightedCategoryDictionary;
			}
		}
	}
}