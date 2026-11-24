using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    [SerializeField] private TMP_Text counterText;
    [SerializeField] private GameObject promptObject;
    [SerializeField] private int totalItems = 3;

    private int _collectedCount;

    private void Awake()
    {
        Instance = this;
        SetPromptVisible(false);
        UpdateCounterUI();
    }

    public void SetPromptVisible(bool visible)
    {
        if (promptObject != null) promptObject.SetActive(visible);
    }

    public void RegisterCollectedItem()
    {
        _collectedCount++;
        UpdateCounterUI();
        SetPromptVisible(false);
    }

    private void UpdateCounterUI()
    {
        if (counterText != null)
            counterText.text = $"Собрано: {_collectedCount} / {totalItems}";
    }
}