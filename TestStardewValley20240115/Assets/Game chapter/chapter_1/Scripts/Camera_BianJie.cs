using UnityEngine;

public class CameraFollowWithBounds : MonoBehaviour
{
    public Transform target; // ���ǵ�Transform
    public SpriteRenderer mapRenderer; // ��ͼ��SpriteRenderer

    private Vector2 cameraMinBounds;
    private Vector2 cameraMaxBounds;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
        CalculateBounds();
    }

    void LateUpdate()
    {
        FollowTarget();
    }

    void CalculateBounds()
    {
        if (mapRenderer == null)
            return;

        // ��ȡ��ͼ�ı߽�
        Bounds mapBounds = mapRenderer.bounds;

        cameraMinBounds = (Vector2)mapBounds.min + new Vector2(halfWidth, halfHeight);
        cameraMaxBounds = (Vector2)mapBounds.max - new Vector2(halfWidth, halfHeight);
    }

    void FollowTarget()
    {
        if (target == null)
            return;

        // ��ȡĿ���λ��
        Vector3 targetPos = target.position;

        // Clamp the camera position to keep it within the bounds
        targetPos.x = Mathf.Clamp(targetPos.x, cameraMinBounds.x, cameraMaxBounds.x);
        targetPos.y = Mathf.Clamp(targetPos.y, cameraMinBounds.y, cameraMaxBounds.y);

        // Set the camera's position to the target position with the same z coordinate
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
    }
}
