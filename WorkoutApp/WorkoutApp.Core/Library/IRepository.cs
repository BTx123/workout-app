using FluentResults;

namespace WorkoutApp.Core.Library;

/// <summary>
/// Contract for a repository.
/// </summary>
/// <typeparam name="TId">The ID type to use.</typeparam>
/// <typeparam name="TItem">The item type to use.</typeparam>
public interface IRepository<in TId, TItem>
{
    /// <summary>
    /// Add a <typeparamref name="TItem"/> to the repository.
    /// </summary>
    /// <param name="item">The <typeparamref name="TItem"/> to add.</param>
    /// <returns>Result indicating success or failure.</returns>
    IResult Add(TItem item);

    /// <summary>
    /// Get a specific <typeparamref name="TItem"/> by ID from the repository.
    /// </summary>
    /// <param name="id">The ID of the <typeparamref name="TItem"/> to get.</param>
    /// <returns>Result indicating success or failure and the matching <typeparamref name="TItem"/> if successful.</returns>
    IResult<TItem> Get(TId id);

    /// <summary>
    /// Get all <typeparamref name="TItem"/>s in the repository.
    /// </summary>
    /// <returns>Result indicating success or failure and all the <typeparamref name="TItem"/>s if successful.</returns>
    IResult<IEnumerable<TItem>> GetAll();

    /// <summary>
    /// Update the <typeparamref name="TItem"/> by ID.
    /// </summary>
    /// <param name="id">The ID of the <typeparamref name="TItem"/> to update.</param>
    /// <param name="newItem">The item to update for ID <paramref name="id"/>.</param>
    /// <returns>Result indicating success or failure.</returns>
    IResult<TItem> Update(TId id, TItem newItem);

    /// <summary>
    /// Delete specific <typeparamref name="TItem"/> by ID.
    /// </summary>
    /// <param name="id">The ID of the <typeparamref name="TItem"/> to delete.</param>
    /// <returns>Result indicating success or failure.</returns>
    IResult Delete(TId id);

    /// <summary>
    /// Delete all <typeparamref name="TItem"/>s in the repository.
    /// </summary>
    /// <returns>Result indicating success or failure.</returns>
    IResult DeleteAll();
}