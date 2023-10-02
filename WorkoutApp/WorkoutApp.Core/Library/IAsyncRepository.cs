using FluentResults;

namespace WorkoutApp.Core.Library;

/// <summary>
/// Contract for an asynchronous repository.
/// </summary>
/// <seealso cref="IRepository{TId,TItem}"/>
/// <typeparam name="TId">The ID type to use.</typeparam>
/// <typeparam name="TItem">The item type to use.</typeparam>
public interface IAsyncRepository<in TId, TItem>
{
    /// <summary>
    /// Add a <typeparamref name="TItem"/> to the repository.
    /// </summary>
    /// <param name="item">The <typeparamref name="TItem"/> to add.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>Task containing result indicating success or failure.</returns>
    Task<IResult> AddAsync(TItem item, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a specific item by ID from the repository.
    /// </summary>
    /// <param name="id">The ID of the item to get.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>Task containing result indicating success or failure and the matching item if successful.</returns>
    Task<IResult<TItem>> GetAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all items in the repository.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>Task containing result indicating success or failure and all the items if successful.</returns>
    Task<IResult<IEnumerable<TItem>>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete a specific item by ID.
    /// </summary>
    /// <param name="id">The ID of the item to delete.</param>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>Task containing result indicating success or failure.</returns>
    Task<IResult> DeleteAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete all items in the repository.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to use.</param>
    /// <returns>Task containing result indicating success or failure.</returns>
    Task<IResult> DeleteAllAsync(CancellationToken cancellationToken = default);
}