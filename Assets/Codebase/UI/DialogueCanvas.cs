using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// UI Canvas for the Dialogue
/// </summary>
public class DialogueCanvas : OpenableCanvas<DialogueCanvas>, IOpenable
{
    /// <summary> Container for the Dialogue UI </summary>
    [SerializeField] private Transform container = null;
    /// <summary> Text displaying the dialogue </summary>
    [SerializeField] private TextMeshProUGUI text = null;
    /// <summary> Duration of the containers animation </summary>
    [SerializeField] private float containerAnimateTime = 1f;
    /// <summary> Duration of each letter appearing </summary>
    [SerializeField] private float timeBetweenLetters = 0.025f;
    /// <summary> Container for the prompt object to continue dialogue </summary>
    [SerializeField] private GameObject prompt = null;

    /// <summary>
    /// Awake callback
    /// </summary>
    private void Awake()
    {
        Instance = this;
        container.localScale = Vector3.zero;
        text.text = string.Empty;
    }

    /// <summary>
    /// Opens the Canvas and displays the specified dialogue
    /// </summary>
    /// <param name="dialogue">The list of strings to display in order</param>
    public async void DisplayDialogue(List<string> dialogue)
    {
        if (IsOpen || IsTransitioning)
            return;
        
        Open();

        while (IsTransitioning)
            await UniTask.NextFrame();

        foreach (string line in dialogue)
        {
            text.text = line;
            prompt.SetActive(false);
            
            // Enable characters one at a time
            for (int j = 0; j < text.text.Length + 1; j++)
            {
                text.maxVisibleCharacters = j;
                await UniTask.WaitForSeconds(timeBetweenLetters);
            }
            
            prompt.SetActive(true);
            prompt.transform.DOLocalJump(prompt.transform.localPosition, 10, 1, 0.5f).SetLoops(100);

            // Await player input to continue dialogue
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                    break;
            
                await UniTask.NextFrame();
            }
        }
        
        // Dialogue has finished
        Close();
    }
    
    #region IOpenable
    /// <summary>
    /// Opens the canvas, making it visible and interactable
    /// </summary>
    public override void Open()
    {
        if (IsOpen)
            return;
        
        IsOpen = true;
        IsTransitioning = true;
        
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        
        // Actually open
        container.DOKill();
        container.DOScale(1f, containerAnimateTime).onComplete += () =>
        {
            canvasGroup.interactable = true;
            IsTransitioning = false;
        };
    }

    public virtual void Close()
    {
        if (!IsOpen)
            return;

        IsOpen = false;
        IsTransitioning = true;
        
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        // Actually close
        container.DOKill();
        container.DOScale(0f, containerAnimateTime).onComplete += () =>
        {
            canvasGroup.alpha = 0f;
            IsTransitioning = false;
        };
    }
    #endregion
}