using TMPro;
using UnityEngine;

public class FloatingTextObject : MonoBehaviour
{
    private float moveSpeed;
    private TMP_Text floatingText;
    public bool isMoving /*{ get; private set*/; //}

    private void Start()
    {
        floatingText = GetComponent<TMP_Text>();
        gameObject.SetActive(false);
    }

    public void MoveText(string text, float floatingTime,float floatingSpeed, Vector2 spawnPos, Color textColor)
    {
        gameObject.SetActive(true);
        moveSpeed = floatingSpeed;
        transform.position = new Vector2(Random.Range(spawnPos.x-0.1f, spawnPos.x + 0.1f), Random.Range(spawnPos.y - 0.1f, spawnPos.y + 0.1f));
        isMoving = true;
        floatingText.text = text;
        floatingText.color = textColor;
        Invoke("HideText", floatingTime);
    }

    private void HideText()
    {
        isMoving = false;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isMoving)
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }
}
