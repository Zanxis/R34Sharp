<br/>
<h1 align="center"> ✨ ● API CLIENT ● ✨ </h1>

Welcome to the API client information guide, in this article you will see complete and rich content on how to use it in your project, including its main features.

## `1.` INTRODUCTION
With the API referenced in your project and the **R34Sharp** namespace imported into your code, you'll have access to the **R34ApiClient** class, responsible for providing you, the programmer, with Rule34 API functionality to you.

Now, you must instantiate this class so that we can use it for what we want. Once that's done, you should have access to the API's basic functionality.

> ⚠ ➥ Remember that the wrapper has an HttpClient class internally that is used to obtain and make API requests, instantiating many clients can significantly impact the overall performance of your program.

## `2.` GETTING INFORMATION.

With your client instantiated in code, you now have the ability to make asynchronous requests for specific information in Rule34, which are, respectively:

- [Posts](./entities/posts.md)

- [Comments]("")

- [Tags]("")

Remember that depending on the amount of things requested, the API may be slow and packet loss due to the amount of data being requested.


## `3.` PERFORMANCE.
As the API is still under development, it can consume a lot of device memory because its operations include many nuances that end up gradually affecting the application's performance. If you are experiencing excessive memory usage, try decreasing the usage of API features.

## `4.` LIMITS.

Despite being quite expansive and free, the API has limits and barriers that must not be exceeded thanks to Rule34, which are the frequency and constancy of requests made in a given time. If you are requesting a lot of things in a very short time that cost a lot, it is likely that you will receive a temporary TimeOut from the website and the application. Try to control the use of the API, interspersing your needs with what you are allowed to have.