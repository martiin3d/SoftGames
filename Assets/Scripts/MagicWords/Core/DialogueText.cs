using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;

/// <summary>
/// Implements the visual presentation of a single line of dialogue. Handles per-character reveal, emoji substitution, and user interaction via the continue button view.
/// </summary>
public class DialogueText : IDialogueText
{
    private TMP_Text _textField;
    private readonly IContinueButtonView _continueButtonView;
    private float _charDelay;
    private StringBuilder _sb;

    /// <summary>
    /// Creates a new instance of <see cref="DialogueText"/>.
    /// </summary>
    /// <param name="uiTextField">TMP_Text component used to display the dialogue.</param>
    /// <param name="continueButtonView">Interface that reports when the continue button is pressed.</param>
    /// <param name="delayPerChar">
    /// Delay between revealing each character (seconds). Default is 0.05 s.
    /// </param>
    public DialogueText(TMP_Text uiTextField, IContinueButtonView continueButtonView, float delayPerChar = 0.05f)
    {
        _sb = new StringBuilder();
        _textField = uiTextField;
        _charDelay = delayPerChar;
        _continueButtonView = continueButtonView;
    }

    /// <summary>
    /// Replaces emoji placeholders (e.g. '{satisfied}') with TMP sprite tags.
    /// </summary>
    /// <param name="text">Raw dialogue text.</param>
    /// <returns>Text ready for rendering by TextMeshPro.</returns>
    private string ReplaceEmojis(string text)
    {
        return Regex.Replace(text, @"\{(.*?)\}", match =>
        {
            string emojiName = match.Groups[1].Value;
            return $"<sprite name=\"{emojiName}\">";
        });
    }

    /// <summary>
    /// Coroutine that gradually reveals the dialogue line and waits for user input before yielding control back to the caller.
    /// </summary>
    /// <param name="dialog">Dialogue data containing speaker name and text.</param>
    /// <returns>IEnumerator suitable for <see cref="MonoBehaviour.StartCoroutine"/>.</returns>
    public IEnumerator ShowDialogueCoroutine(DialogData dialog)
    {
        // Build the full line with speaker prefix and emoji substitution
        _sb.Clear();
        _sb.Append(dialog.name).Append(": ").Append(ReplaceEmojis(dialog.text));
        _textField.text = _sb.ToString();
        _textField.ForceMeshUpdate();

        TMP_TextInfo textInfo = _textField.textInfo;
        int totalVisible = 0;
        bool skipped = false;

        // Start with nothing visible
        _textField.maxVisibleCharacters = 0;

        // Reveal each character one by one, respecting user skip input
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            totalVisible++;
            
            _textField.maxVisibleCharacters = totalVisible;

            if (_continueButtonView.IsContinueButtonPressed)
            {
                skipped = true;
                break;
            }
            yield return new WaitForSeconds(_charDelay);
        }

        // If the user pressed skip, show the full line immediately
        if (skipped)
        {
            _textField.maxVisibleCharacters = int.MaxValue;
            _continueButtonView.ResetState();
        }

        // Await final press of the continue button before finishing
        while (!_continueButtonView.IsContinueButtonPressed)
        {
            yield return null;
        }
        _continueButtonView.ResetState();
    }
}