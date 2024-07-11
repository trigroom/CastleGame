using UnityEngine;

public class AtackBuffToCharacters : BuffInfo
{
    [SerializeField] private float attackMultiplayer, buffDuration;
    [SerializeField] private int[] charactersToBuffNumbers;
    public override void TakeBuff(Collider2D[] characters)
    {
        Debug.Log(characters.Length);
        if (charactersToBuffNumbers.Length == 0 )
        {
                    Debug.Log(characters.Length);
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i].gameObject.TryGetComponent(out AttackMain attackMain))
                    attackMain.AttackBuff(attackMultiplayer, buffDuration);
            }
        }
        else
            for (int i = 0; i < characters.Length; i++)
                if (characters[i].gameObject.TryGetComponent(out AttackMain attackMain) && CheckCharNumber(characters[i].gameObject.GetComponent<HealthSystem>().characterNumber))
                    attackMain.AttackBuff(attackMultiplayer, buffDuration);
    }

    private bool CheckCharNumber(int number)
    {
        foreach (int num in charactersToBuffNumbers)
        {
            if(num == number)
                return true;
        }
        return false;
    }
}
