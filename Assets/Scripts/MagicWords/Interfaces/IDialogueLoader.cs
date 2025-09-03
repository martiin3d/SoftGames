using System.Threading.Tasks;

/// <summary>
/// Contract for asynchronously loading dialogue data.
/// </summary>
public interface IDialogueLoader
{
    /// <summary>
    /// Loads a <see cref="DialogModels"/> instance from an external source.
    /// The operation is performed asynchronously and returns the loaded model once completed.
    /// </summary>
    /// <returns>A task that completes with the loaded dialogue models.</returns>
    Task<DialogModels> LoadDialogueAsync();
}