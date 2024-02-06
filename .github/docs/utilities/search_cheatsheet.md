<br/>
<h1 align="center"> ✨ ● CHEATSHEET ● ✨ </h1>

## Credits

You can check the original page [by clicking here](https://rule34.xxx/index.php?page=help&topic=cheatsheet).

## Settings

### Combining Tags

`tag1 tag2`
Search for posts that have tag1 and tag2.

### Logical OR

`( tag1 ~ tag2 )`
Search for posts that have tag1 or tag2. The parentheses are important to group the tags between which the "or" counts. The spaces between the parentheses and tags are also important because some tags end in parentheses!

### Fuzzy Search

`night~`
Fuzzy search for the tag night. This will return results such as night fight bright and so on according to the Levenshtein distance.

### Excluding Tags

`-tag1`
Search for posts that don't have tag1.

### Wildcard Search

`ta*1`
Search for posts with tags that start with "ta" and end with "1".

### User Specific

`user:bob`
Search for posts uploaded by the user Bob.

### MD5 Hash Search

`md5:foo`
Search for posts with the MD5 hash foo.

### MD5 Hash Prefix Search

`md5:foo*`
Search for posts whose MD5 starts with the MD5 hash foo.

### Questionable Rating

`rating:questionable`
Search for posts that are rated questionable.

### Exclude Questionable Rating

`-rating:questionable`
Search for posts that are not rated questionable.

### Parent Post Search

`parent:1234`
Search for posts that have 1234 as a parent (and include post 1234).

### Combined Rating Search

`rating:questionable rating:safe`
In general, combining the same metatags (the ones that have colons in them) will not work.

### Combined Metatags

`rating:questionable parent:100`
You can combine different metatags, however.

### Dimensional Search

`width:>=1000 height:>1000`
Find images with a width greater than or equal to 1000 and a height greater than 1000.

### Score Threshold

`score:>=10`
Find images with a score greater than or equal to 10. This value is updated once daily at 12AM CST.

### Sort by Updated Date

`sort:updated:desc`
Sort posts by their most recently updated order.

### Other Sortable Types

- id
- score
- rating
- user
- height
- width
- parent
- source
- updated
- 
Can be sorted by both asc or desc.