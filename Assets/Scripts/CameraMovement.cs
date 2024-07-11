using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float moveSpeed, maxX, minX, currentMoveSpeed;
    private float screenOffset = Screen.width / 8;

    void FixedUpdate()
    {
        if (Input.mousePosition.x < screenOffset && transform.position.x > minX)
            currentMoveSpeed = -moveSpeed * ((screenOffset - Input.mousePosition.x) / screenOffset);

        else if (Input.mousePosition.x > Screen.width - screenOffset && transform.position.x < maxX)
            currentMoveSpeed = moveSpeed * ((Input.mousePosition.x - Screen.width + screenOffset) * 6 / (Screen.width - screenOffset));
        else
            currentMoveSpeed = 0;
        transform.position += new Vector3(currentMoveSpeed * Time.deltaTime, 0, 0);
    }
}
