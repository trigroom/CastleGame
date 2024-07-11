using UnityEngine;

public class GhostRunner : MonoBehaviour
{
    [SerializeField] private float moveSpeed, timeBetweenSpawns;
    private Animator runnerAnimator;
    private bool isRunning;

    private void Awake()
    {
       // if (UpgradesManager.instance.unlockedCharacters[10])
       //     Destroy(gameObject);
    }
    void Start()
    {
        isRunning = true;
        InvokeRepeating("Spawn", timeBetweenSpawns, timeBetweenSpawns);
        runnerAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isRunning)
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }

    public void OnMouseDown()
    {
        CatchRunner();
    }
    private void Spawn()
    {
        transform.position = new Vector3(-2f, 0.279f, 0);
        isRunning = true;
        Invoke("ChangeRunningState", 10);
    }

    private void CatchRunner()
    {
        UpgradesManager.instance.unlockedCharacters[10] = true;

        runnerAnimator.SetTrigger("isDeath");
        Destroy(gameObject, 1f);
    }

    private void ChangeRunningState() => isRunning = false;
}
