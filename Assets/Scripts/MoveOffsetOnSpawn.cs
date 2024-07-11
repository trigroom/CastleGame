using UnityEngine;

public class MoveOffsetOnSpawn : MonoBehaviour
{
    [SerializeField] private float timeToMove;
    [SerializeField] private float moveSpeed;
    private bool isMoved = true;

    private void Start()
    {
        if (timeToMove != 0)
            Invoke("StopMove", timeToMove);
    }

    private void Update()
    {
        if (timeToMove != 0 && isMoved)
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    private void StopMove() => isMoved = false;

    public void SetMoveStats(float timeToMove, float moveSpeed)
    {
        this.timeToMove = timeToMove;
        this.moveSpeed = moveSpeed;
    }
}
