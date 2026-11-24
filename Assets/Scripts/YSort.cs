using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    [SerializeField] private int sortingOffset = 0;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        _sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f) + sortingOffset;
    }
}