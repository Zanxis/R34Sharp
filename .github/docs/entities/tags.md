<h1 align="center"> ✨ ● RULE34 - TAGS ● ✨ </h1>

The Rule34 API tags system is an important feature that allows users to search for posts based on the tags that they have been associated with. Tags are essentially keywords or labels that have been manually added to posts by the community. They describe the content of the post and make it easier for users to find specific types of content.

## `1.` INTRODUCTION
To begin with, you must have imported the API namespace in your code: **R34Sharp**, this will give you access to use the **R34ApiClient** client. After that, instantiate a new client in your code and that way, you will be able to obtain tools that will help us to obtain and manipulate tags.

See the examples below:

- _Importing namespace;_
```cs
using R34Sharp;
```

- _Creating a client instance;_
```cs
R34ApiClient client = new R34ApiClient();
```

## `2.` GETTING TAGS
With that done, we are now able to use the asynchronous method **GetTagsAsync**, which, like **GetPostsAsync**, requires a special search constructor to search for Tags.

The constructor used by **GetTagsAsync** works in a slightly different way from the others previously shown, being capable of performing three search functions that are different from each other, namely:

- **Search by Name** - This type of search will strictly return a single required tag by name.

- **Search for Pattern** - This type of search returns all tags that have the pattern placed anywhere in the tag. For example: If the selected pattern was _"presenting"_ this search would also return _"presenting_anus"_ and so on.

- **Search by ID** - This type of search strictly searches for a single tag with a selected ID number.

This gives you a complete way to search for tags in Rule34 and get information related to them.

Below is an example of a Tag request:
```cs
// Instantiating API client
R34ApiClient client = new R34ApiClient();

// Making the Tag request
await client.Tags.GetTagsAsync(new R34TagsSearchBuilder()
{
    Limit = 1,
    Search = "presenting",
    SearchType = R34TagSearchType.Name
});
```

In the example above, the search would only return information from the "presenting" tag, as the search is performed based on name.

## `3.` VIEWING TAGS INFORMATION
After using the **GetTagsAsync** method, it will return an object of type **R34Tags** that will contain all the tags returned by the request.

See the example below using a ForEach method to see the information of each returned Tag:

```cs
// Instantiating API client
R34ApiClient client = new R34ApiClient();

// Making the Tag request
R34Tags tags = await client.Tags.GetTagsAsync(new R34TagsSearchBuilder()
{
    Limit = 1,
    Search = "presenting",
    SearchType = R34TagSearchType.Pattern
});

// Reading the name of all Tags found
foreach (R34Tag tag in tags.Data)
{
    Console.WriteLine(tag.Name);
}
```