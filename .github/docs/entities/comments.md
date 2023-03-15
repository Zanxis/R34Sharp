<br/>
<h1 align="center"> ✨ ● RULE34 - COMMENTS ● ✨ </h1>

Welcome to the Rule34 post comments article, throughout this article you will see how to get comments from specific posts and how to manage them.

## `1.` INTRODUCTION

To begin with, you need to import the **R34Sharp** namespace into your code to gain access to the **R34ApiClient** class which will give you access to features that allow you to grab comments.

Note in the example below how to import the namespace and instantiate the class:

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

## `2.` GETTING COMMENTS

There are currently two ways to get comments for certain posts, respectively:

- Get comments by type **R34Post**;
- Get comments from the client with the **GetCommentsAsync()** method.

Both modes described above work towards the same goal, just see which fits best in the current context of your project.

### 2.1. Getting comments by R34Post
To get comments directly from a post, just make a post request and use **GetCommentsAsync()** (located in R34Post) to get all the comments made in that post, see the following example:

<br/>

- _Making a request and getting the comments of the first post;_
```cs
// Instantiating the API client
R34ApiClient client = new R34ApiClient();

// Making a request
R34Posts posts = await client.GetPostsAsync(new()
{
	Limit = 1,
	Tags = new R34TagModel[]
	{
		new R34TagModel("Danganronpa"),
	}
});

// Getting comments from the first post of the request
R34Comments comments = posts.Data[0].GetCommentsAsync();

// All comments are read through a ForEach loop, where the name of the author of the comment and its content are printed on the screen.
foreach(R34Comment comment in comments.Data)
{
	Console.WriteLine($"{comment.Creator}: {comment.Content}");
}
```

<br/>

To avoid possible errors in your application, you can use the **HasComments** property (which is located in R34Post) to check in advance the existence of comments in that post.

### 2.1. Getting feedback from the API client
The other existing way of getting feedback is using the API client. Once your client is instantiated, you will have access to the async method **GetCommentsAsync** , which like the **GetPostsAsync** method also has a custom lookup constructor.

In this custom constructor (in **GetCommentsAsync** ), you must instantiate a new object and give it the ID of the post you want to get the comments from, in the **PostId** field.

See the example below on how to get the comments through this method:

<br/>

- _Getting comments from a post via API client;_
```cs
// Instantiating the API client
R34ApiClient client = new R34ApiClient();

// Search the post comments below
await client.GetCommentsAsync(new R34CommentsSearchBuilder()
{
	PostId = 0,
});
```

<br/>

In the example above, an example ID is provided, but you could get a post ID through a post request or by manually allocating an ID.