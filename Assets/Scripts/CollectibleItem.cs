using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class CollectibleItem : MonoBehaviour
{
    private bool _playerInRange;

    private void Update()
    {
        if (_playerInRange && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (GameUIManager.Instance != null)
                GameUIManager.Instance.RegisterCollectedItem();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            _playerInRange = true;
            if (GameUIManager.Instance != null)
                GameUIManager.Instance.SetPromptVisible(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            _playerInRange = false;
            if (GameUIManager.Instance != null)
                GameUIManager.Instance.SetPromptVisible(false);
        }
    }
}