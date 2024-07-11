using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public float currentMoveSpeed { get; private set; }
    [SerializeField] private float defaultMoveSpeed, moveSpeedMultiplayer = 1;

    private void Start()
    {
        ChangeMoveSpeed();
    }
    protected virtual void Update()
    {
        if (currentMoveSpeed != 0)
            transform.position += new Vector3(currentMoveSpeed * Time.deltaTime, 0, 0);
    }

    public void ChangeMoveSpeed() => currentMoveSpeed = defaultMoveSpeed * moveSpeedMultiplayer;

    public void ChangeMoveSpeed(float multiplayer)
    {
        moveSpeedMultiplayer = multiplayer;
        currentMoveSpeed = defaultMoveSpeed * moveSpeedMultiplayer;
    }

    public void StopMovement() => currentMoveSpeed = 0;
}
