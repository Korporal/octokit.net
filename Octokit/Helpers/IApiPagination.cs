using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// Used to paginate through API response results.
    /// </summary>
    /// <remarks>
    /// This is meant to be internal, but I factored it out so we can change our mind more easily later.
    /// </remarks>
    public interface IApiPagination
    {
        /// <summary>
        /// Paginate a request to asynchronous fetch the results until no more are returned
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="getFirstPage">A function which generates the first request</param>
        /// <param name="uri">The original URI (used only for raising an exception)</param>
        Task<IReadOnlyList<T>> GetAllPages<T>(Func<Task<IReadOnlyPagedCollection<T>>> getFirstPage, Uri uri);

        // Changing the above to:
        // Task<IReadOnlyList<T>> GetAllPages<T>(Func<Tuple<bool,Task<IReadOnlyPagedCollection<T>>>> getFirstPage, Uri uri);
        // Would enable the implementation of 'getFirstPage' to return an indicator as to whether to run lazy or not.
        // That could be deduced by the lambda 'getFirstPage' examint the ApiOptions for some hitherto unused value,
        // Perhaps a start page of specifically 0 could be used here.
        // GetAllPages could then return an instance of either ReadOnlyCollection or LazyReadOnlyCollection which we would define.
        // Consumers would see only a IReadOnlyList...
    }
}