/// <summary>
/// Defines the contract for a UI component that represents a "continue" button.
/// </summary>
public interface IContinueButtonView
{
    /// <summary>
    /// Gets a value indicating whether the continue button has been pressed.
    /// The implementing view should set this to true when the user taps or clicks the button,
    /// and reset it once the action has been handled.
    /// </summary>
    bool IsContinueButtonPressed { get; }

    /// <summary>
    /// Resets the internal state of the continue button.
    /// </summary>
    void ResetState();
}