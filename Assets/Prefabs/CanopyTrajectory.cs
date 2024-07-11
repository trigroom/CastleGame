using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanopyTrajectory : MonoBehaviour
{
    private Vector2 targetPosition, startPosition;
    [SerializeField] private Transform objectToSpawn;
    [SerializeField] private float  moveSpeed;
    private float  dist, nextX, baseY, height;
    private int damage;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    { 
       dist = Mathf.Abs(targetPosition.x - startPosition.x);
        nextX = Mathf.MoveTowards(transform.position.x, targetPosition.x, moveSpeed * Time.deltaTime);
        baseY = Mathf.Lerp(startPosition.y, targetPosition.y, (nextX - startPosition.x) / dist);
        height = 0.2f * (nextX - startPosition.x) * (nextX - targetPosition.x) / (-0.25f * dist * dist);
    
    Vector3 movePosition = new Vector3(nextX, baseY + height, transform.position.z);
   transform.position = movePosition;

        if((Vector2)transform.position == targetPosition)
        {
            SpawnObjectAfterDesroy();
        }
    }

    public void SetValues(Vector2 targetPos, Vector2 startPos,int damage)
    {
        targetPosition = targetPos;
        startPosition = startPos;
        this.damage = damage;
    }

    private void SpawnObjectAfterDesroy()
    {
        Debug.Log("bottleBrocking");
        Instantiate(objectToSpawn, transform.position, Quaternion.identity).GetComponent<DamagedDurable>().changedHealth = damage;
        Destroy(gameObject);
    }
}
