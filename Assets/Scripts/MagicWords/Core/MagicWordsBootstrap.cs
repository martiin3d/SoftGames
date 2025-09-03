using TMPro;
using UnityEngine;

/// <summary>
/// Bootstrapper for the *Magic Words* dialogue experience.
/// Sets up UI components, loads dialogue data from a remote URL, and orchestrates the dialogue flow via <see cref="DialogueManager"/>.
/// </summary>
public class MagicWordsBootstrap : MonoBehaviour
{
    [SerializeField] private string _dialogueUrl = "https://private-624120-softgamesassignment.apiary-mock.com/v3/magicwords";
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private UrlImage _avatarImageLeft;
    [SerializeField] private UrlImage _avatarImageRight;
    [SerializeField] private ContinueButtonView _continueButton;
    [SerializeField] private RestartButtonView _restartButton;
    [SerializeField] private float _typingSpeed = 0.1f;

    private ITextureCache _textureCache;
    private IDialogueManager _dialogueManager;

    /// <summary>
    /// Initializes dependencies, loads dialogue data and starts the first sequence.
    /// </summary>
    private void Start()
    {
        _textureCache = new TextureCache();
        _avatarImageLeft.Initialize(_textureCache);
        _avatarImageRight.Initialize(_textureCache);
        _restartButton.Initialize(OnRestartButtonClick);

        IDialogueLoader dialogLoader = new DialogueLoader(_dialogueUrl);
        IDialogueText dialogueText = new DialogueText(_dialogueText, _continueButton, _typingSpeed);
        _dialogueManager = new DialogueManager(dialogLoader, dialogueText, _avatarImageLeft, _avatarImageRight, _textureCache, this);

        // Kick off the first dialogue sequence asynchronously
        _dialogueManager.StartDialogueSequenceAsync(OnDialogueSequenceComplete, OnDialogueSequenceFail);
    }

    /// <summary>
    /// Called when the user clicks the restart button.
    /// Hides the restart UI and re‑initiates the dialogue sequence.
    /// </summary>
    private void OnRestartButtonClick()
    {
        _restartButton.gameObject.SetActive(false);
        _dialogueManager.StartDialogueSequenceAsync(OnDialogueSequenceComplete, OnDialogueSequenceFail);
        _continueButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Callback invoked when the dialogue sequence finishes successfully.
    /// Shows the restart button and hides the continue button.
    /// </summary>
    private void OnDialogueSequenceComplete()
    {
        _restartButton.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Callback invoked when the dialogue sequence fails.
    /// Hides the continue button to prevent further interaction.
    /// </summary>
    private void OnDialogueSequenceFail()
    {
        _continueButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Cleans up resources before the object is destroyed.
    /// </summary>
    private void OnDestroy()
    {
        _textureCache.Dispose();
        _textureCache = null;
    }
}
