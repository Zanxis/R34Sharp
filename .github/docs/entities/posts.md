<br/>
<h1 align="center"> ✨ ● RULE34 - POSTS ● ✨ </h1>

Welcome to the posts section of the R34Sharp API. Here you'll find a wealth of information and tools to help you search for and manage posts in your applications. In this article, we'll provide you with the necessary information to get started, as well as some general development ideas to help you utilize the API to its fullest potential.

## `1.` SEARCHING FOR POSTS

We will start by introducing you to simple and complete concepts related to searching and looking for posts in a simple and basic way. In your code, import the **R34Sharp** namespace so that you have access to the **R34ApiClient** class, as follows:

<br/>

- _Importing Namespace;_
```cs
using R34Sharp;
```

<br/>

- _Creating a client instance;_
```cs
R34ApiClient client = new R34ApiClient();
```

<br/>

After that, you will now have access to the asynchronous search method **GetPostsAsync**, responsible for obtaining posts based on a search builder called **R34PostsSearchBuilder**, responsible for delimiting the search rules for posts.

<br/>

> All search methods presented in the class are asynchronous.

See a step-by-step example below on how to perform searches using this method:

<br/>

- _Creating a client instance and performing searches;_
```cs
R34ApiClient client = new R34ApiClient();

// With the class instantiated, reference it and call the GetPostsAsync method;
await client.Posts.GetPostsAsync();

// The method described above receives a class called "R34PostsSearchBuilder", create a new instance of this class in the method so that it receives it as a parameter.
await client.Posts.GetPostsAsync(new R34PostsSearchBuilder());

// Once that's done, you can now define search rules.
await client.Posts.GetPostsAsync(new R34PostsSearchBuilder()
{
	Limit = 1000,
	Tags = new R34TagModel[] {
		new R34TagModel("Bowser"),
	},
});
```

<br/>

Note that R34PostsSearchBuilder has several fields that can be filled in to delimit and get a better search result, feel free to explore and define them as you wish.

## `2.` POST INFORMATION
In the previous topic, notice that a request was made using the asynchronous method **GetPostsAsync**, however, how can we get the result of the request? Simple!

The method responsible for the request returns an object of type **R34Posts**, which contains general information about the returned posts, the main one (the posts) being stored in **Data**.

In order to obtain information from each post, we can place the **Data** field in a ForEach loop, which will go through each element of the collection, as in the example below:


<br/>

- _Example of code that obtains the results of the request;_
```cs
// Make the request
R34Posts posts = await client.Posts.GetPostsAsync(new R34PostsSearchBuilder()
{
	Limit = 1000,
	Tags = new R34TagModel[] {
		new R34TagModel("Bowser"),
	},
});

// Go through the 1000 posts found in the last request
foreach(R34Post post in posts.Data)
{
	Console.WriteLine(post.FileName);
}
```

<br/>

**R34Post** type has a lot of useful information related to Rule34 posts, having ID information, Authors, Comments, Likes and many others. Feel free to check them out and get information related to them.

## `3.` FILTERING POSTS

Eventually, it may be useful to filter posts based on specific conditions, for example, you only want videos instead of images, and for that, you can use the system library **System.Linq** to filter all types of posts.

Observe the example below and see that, using the **Where** method, I filter all the contents of the request to only have videos.

<br/>

- _Import of the system library namespace;_
```cs
using System.Linq;
```

<br/>

- _Example of post filtering;_
```cs
// Execution of the request.
R34Posts posts = await client.Posts.GetPostsAsync(new R34PostsSearchBuilder()
{
	Limit = 1000,
	Tags = new R34TagModel[] {
		new R34TagModel("Bowser"),
	},
});

// Filter the collection to only hold videos.
IEnumerable<R34Post> filteredPosts = posts.Data.Where(x => x.FileMediaType == FileMediaType.Video);

// Reads the name of all posts from the enumeration.
foreach (R34Post post in filteredPosts)
{
	Console.WriteLine(post.FileName);
}
```

<br/>

With this example, you can observe the amount of possibilities that are provided to you for your own manipulation of posts.

## `4.` SEARCHING FOR POST OFFSET
As you develop and create new things, it might be interesting to go beyond 1000 posts, let's assume you want to request 2000 posts this time, how could you do that if the allowed limit is 1000? Simple, using **Search Offsets**.

Offsets is a simple way to fetch a bunch of posts beyond the established limit without incurring API overhead and in a simpler way to happen. You can define a offset when instantiating the search, as in the example below:

<br/>

- _Defining search offsets._
```cs
// Define a polling loop.
for(int i = 0; i < 2; i++)
{
	R34Posts posts1 = await client.Posts.GetPostsAsync(new R34PostsSearchBuilder()
	{
		Limit = 1000,
		Tags = new R34TagModel[] {
			new R34TagModel("Bowser"),
		},

		// Defines the offset to be searched.
		Offset = new Optional<int>(i),
	});
}
```

<br/>

See that in the example above, the search takes place within a loop that, through the iterator (i), merges other offsets in the search, resulting in:

- When offsets is set to 0 (default value), the search fetches the first Xth posts requested, when offsets is set to 1, the search fetches the Xth posts after the previous search, and so on.

That way, you might get more posts than the allowed limit.

## `5.` DOWNLOADING A POST
And finally, you also have the ability to download the media content available in each Post, for that you must use the asynchronous method **DownloadFileAsync()** present in **R34Post** . This method returns a **MemoryStream** containing all the bytes of the respective post file, which can be read and copied to your device.

If you want a faster download, you can use the **DownloadFilePreviewAsync()** method that downloads the thumbnail file available in each Rule34 post. Remember that this post has a much lower quality than the original post and in the case of videos, only the Thumbnail of the video is downloaded.

See the example below, which downloads all posts made in one request:

<br/>

- _Downloading media content in requested Posts._
```cs
// Execution of the request.
R34Posts posts = await client.Posts.GetPostsAsync(new R34PostsSearchBuilder()
{
	Limit = 10,
	Tags = new R34TagModel[] {
		new R34TagModel("Bowser"),
	},
});

// Downloads all required posts and creates files with their bytes in an images folder.
foreach (R34Post post in filteredPosts)
{
	using MemoryStream ms = await post.DownloadFileAsync();
	using FileStream file = File.Create(Path.Combine(directoryImagesPath, $"{post.FileName}{post.FileExtension}"));

	await file.WriteAsync(ms.ToArray(), token);
	Console.WriteLine($"[ Download Completed: {post.FileName}{post.FileExtension} ]");
}
```

<br/>

It's important to note that downloading large files from the Rule34 API can take a significant amount of time and may also consume a lot of bandwidth. Therefore, it's recommended to have a stable and fast internet connection before attempting to download any large files.

Additionally, it's advisable to implement some sort of progress monitoring feature to let the user know the current status of the download process, and also to implement error handling mechanisms in case the download fails or gets interrupted.

## `6.` BEST PRACTICES
### `1.` Save all information in a cache.

It is essential to emphasize that all methods that receive information and return collections make requests to the Rule34 API. In this context, it is highly recommended to store the obtained values in any form of cache of your preference. By doing so, we avoid redundant requests and the waste of memory resources with lists containing a large number of elements that are automatically created by the system.