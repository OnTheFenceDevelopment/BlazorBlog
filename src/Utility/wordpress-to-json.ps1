$Entries = @()
$Content = [xml](Get-Content .\dave039splace.wordpress.2022-05-07.000.xml)
foreach($item in $Content.rss.channel.item)
{
	 if ($Item.post_type -eq "post") {
	
		$PostPath = $Item.link
		
		$Title = [string]([System.Web.HttpUtility]::HtmlEncode($item.Title.InnerText))
		$RawContent = $Item.encoded.InnerText
		$Content = [string]([System.Web.HttpUtility]::HtmlEncode($RawContent[0]))
		$CreatedDate = ([datetimeoffset]$Item.PubDate).ToString("o")
		$Id = $Item.post_id

		# Categories
		[string]$OutCategories = ""
		[bool]$added = $false
		$Cats = $Item.Category
		foreach($Cat in $Cats)
		{
			if ($added)
			{
				$OutCategories = $OutCategories + ",`"$($Cat.InnerText)`""
			} else {
				$OutCategories = $OutCategories + "`"$($Cat.InnerText)`""
				$added = $true
			}
		}

		# Categories
		[string]$OutCategories = ""
		[bool]$added = $false
		$Cats = $Item.Category
		foreach($Cat in $Cats)
		{
			if ($Cat.InnerText -eq "Uncategorised")
			{
				continue;
			}

			if ($added)
			{
				$OutCategories = $OutCategories + ";" + $($Cat.InnerText)
			} else {
				$OutCategories = $OutCategories + $($Cat.InnerText)
				$added = $true
			}
		}

		# Generate JSON for data feed
		[HashTable]$Entry = @{
			Content = $Content
			Created = $CreatedDate
			Id = $Id
			Title = $Title
			Permalink = $PostPath
			Categories = [array]$OutCategories.split(";")
			Author = [string]"Dave Carson"
		} # HashTable for Entry
		
		$Entries += $Entry
	
	}

}

$jsonEntries = $Entries | ConvertTo-Json

Return $jsonEntries
