using UnityEngine;

public class SpawnObjectsAfterDeathHealthSystem : HealthSystem
{
    [SerializeField] private Transform[] spawnedPrefabs;
    [SerializeField] private float[] timeToMoveCharacters, moveSpeedCharacters;
    protected override void Death()
    {
        for (int i = 0; i < spawnedPrefabs.Length; i++)
        {
            if (timeToMoveCharacters.Length == 0)
                CharacterSelecter.instance.SpawnCharacter(spawnedPrefabs[i].gameObject, transform.position);
            else
            {
                CharacterSelecter.instance.SpawnCharacter(spawnedPrefabs[i].gameObject, transform.position).GetComponent<MoveOffsetOnSpawn>().SetMoveStats(timeToMoveCharacters[i], moveSpeedCharacters[i]);
            }
        }
        base.Death();
    }
}
