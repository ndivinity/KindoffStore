namespace KindoffStore.Models.Interfaces;


/// <summary>
/// Interface used as a contract for models that are based around enumerable collections
/// of an entity type fetched from a data source (sqlite in this case, but it should
/// work regardless of the data source as long as it implements IEnumerator).
/// </summary>
/// <typeparam name="EnumeratorListItem">The entity type that's wrapped around a collection.</typeparam>
public interface IEnumerativeModel<EnumeratorListItem> where EnumeratorListItem : class
{
    /// <summary>
    /// This method fetches the collection of items from the data source.
    /// </summary>
    /// <returns>True if the collection was fetched successfully or partial. False if failed.</returns>
    bool FetchEnumerable();

    /// <summary>
    /// This function checks if the collection of items comply with the requirements
    /// that the View expects.
    /// </summary>
    /// <returns>True if comply, False if sus.</returns>
    bool CheckEnumerable();
}