using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 8f;

    [Header("Map Bounds (Floor Edges)")]
    [SerializeField] private SpriteRenderer floorBounds;

    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = new Vector3(target.position.x, target.position.y, transform.position.z);

        if (floorBounds != null && _cam != null && _cam.orthographic)
        {
            float halfHeight = _cam.orthographicSize;
            float halfWidth = halfHeight * _cam.aspect;

            Bounds b = floorBounds.bounds;
            float minX = b.min.x + halfWidth;
            float maxX = b.max.x - halfWidth;
            float minY = b.min.y + halfHeight;
            float maxY = b.max.y - halfHeight;

            desiredPos.x = (minX <= maxX) ? Mathf.Clamp(desiredPos.x, minX, maxX) : b.center.x;
            desiredPos.y = (minY <= maxY) ? Mathf.Clamp(desiredPos.y, minY, maxY) : b.center.y;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
    }
}