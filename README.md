Dynamic Caching
==============

Dynamic Function Caching allows you to call a caching method that accepts a function and it's paramaters as paramaters and then checks the cache for the result of the function, if not found it executes the function and stores it's result in the cache. This allows you to implement your caching once and just call the cachers methods.

To use this, clone the repository and include the DLL in your project. From there you can inherit the BaseCacher and override the abstract caching methods to suit your needs.

See the TestCacher and Caching projects in the solution for how to use this
