using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    [Header("Sprite Sheet (25 frames: 5 rows x 5 cols)")]
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float frameRate = 8f;

    [Header("Row Indices (0..4)")]
    [SerializeField] private int downRow = 0;
    [SerializeField] private int downSideRow = 1;
    [SerializeField] private int sideRow = 2;
    [SerializeField] private int upSideRow = 3;
    [SerializeField] private int upRow = 4;

    private SpriteRenderer _sr;
    private PlayerMovement _movement;
    private int _currentRow;
    private int _currentFrame;
    private float _timer;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (frames == null || frames.Length < 25) return;

        Vector2 input = _movement.MoveInput;
        bool isMoving = input.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            UpdateDirection(input);
            _timer += Time.deltaTime;
            if (_timer >= 1f / frameRate)
            {
                _timer -= 1f / frameRate;
                _currentFrame = (_currentFrame + 1) % 5;
            }
        }
        else
        {
            _currentFrame = 0;
            _timer = 0f;
        }

        int index = _currentRow * 5 + _currentFrame;
        if (index >= 0 && index < frames.Length)
        {
            _sr.sprite = frames[index];
        }
    }

    private void UpdateDirection(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0f) angle += 360f;

        // Определяем 8 секторов по 45 градусов
        if (angle >= 337.5f || angle < 22.5f)
        {
            _currentRow = sideRow;
            _sr.flipX = false; // Вправо
        }
        else if (angle >= 22.5f && angle < 67.5f)
        {
            _currentRow = upSideRow;
            _sr.flipX = false; // Вверх-вправо
        }
        else if (angle >= 67.5f && angle < 112.5f)
        {
            _currentRow = upRow;
            _sr.flipX = false; // Вверх
        }
        else if (angle >= 112.5f && angle < 157.5f)
        {
            _currentRow = upSideRow;
            _sr.flipX = true; // Вверх-влево
        }
        else if (angle >= 157.5f && angle < 202.5f)
        {
            _currentRow = sideRow;
            _sr.flipX = true; // Влево
        }
        else if (angle >= 202.5f && angle < 247.5f)
        {
            _currentRow = downSideRow;
            _sr.flipX = true; // Вниз-влево
        }
        else if (angle >= 247.5f && angle < 292.5f)
        {
            _currentRow = downRow;
            _sr.flipX = false; // Вниз
        }
        else
        {
            _currentRow = downSideRow;
            _sr.flipX = false; // Вниз-вправо
        }
    }
}