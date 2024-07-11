using UnityEngine;

public class HeroMovementSystem : MovementSystem
{
    public bool isPlayerControl;
    private Vector2 moveVector;
    // private HealthSystem healthSystem;

    private void Awake()
    {
        // healthSystem = GetComponent<HealthSystem>();
    }
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isPlayerControl = !isPlayerControl;
            if (isPlayerControl)
                ChangeMoveSpeed();
        }

        //  if(healthSystem.isDiscarding)
        if (!isPlayerControl)
            base.Update();
        else
        {
            moveVector.x = Input.GetAxis("Horizontal");
            moveVector.y = Input.GetAxis("Vertical");
            transform.Translate(moveVector * Time.deltaTime * currentMoveSpeed);
        }
    }
}
