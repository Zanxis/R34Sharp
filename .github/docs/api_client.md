<br/>
<h1 align="center"> ✨ ● API CLIENT ● ✨ </h1>

Welcome to the API client information guide! In this article, you'll find all the information you need to start using the client in your project, including its key features and capabilities. With comprehensive and rich content, you'll be able to dive deep into the client's functionality and unlock its full potential. Let's get started!

## `1.` INTRODUCTION
If you've successfully referenced the API in your project and imported the R34Sharp namespace into your code, you're ready to start using the R34ApiClient class. This class is responsible for providing you with access to the Rule34 API functionality you need to build your project.

To get started, simply instantiate the R34ApiClient class and you'll be ready to use its basic functionality. With this powerful tool at your fingertips, you'll be able to automate tedious tasks and streamline your development process like never before.

> ⚠ ➥ Keep in mind that the wrapper has an internally used HttpClient class for making API requests. Instantiating multiple clients can have a significant impact on the overall performance of your program. To ensure optimal performance, we recommend that you use a single instance of the R34ApiClient class throughout your program.

## `2.` GETTING INFORMATION
With your client instantiated, you can now make asynchronous requests for specific information on Rule34 using the following entities:

- **[Posts](./entities/posts.md)**
- **[Comments](./entities/comments.md)**
- **[Tags](./entities/tags.md)**

Keep in mind that depending on the amount of data you request, the API response time may vary and there may be packet loss. Therefore, it's important to design your program to handle these potential delays and errors gracefully. With careful management and optimization, you'll be able to get the data you need from Rule34 quickly and easily.


## `3.` PERFORMANCE
As the API is still under development, its operations include many nuances that may gradually affect the performance of your application and consume a lot of device memory. If you're experiencing excessive memory usage, consider reducing the usage of API features or optimizing your code to minimize memory consumption. It's also recommended to keep an eye on updates and improvements to the API, as they may address performance issues and help improve the overall stability of your application.

## `4.` LIMITS
It is important to note that even though the API is free, it has certain limits and restrictions in place to prevent abuse and overload of the server. These limits include the frequency and number of requests that can be made within a given time frame. If you exceed these limits, you may receive a temporary TimeOut from the website and your application may be affected. Therefore, it is advisable to control your API usage and balance your needs with the allowed usage. Additionally, using cache systems to store your requests can help reduce the number of requests made and improve performance.