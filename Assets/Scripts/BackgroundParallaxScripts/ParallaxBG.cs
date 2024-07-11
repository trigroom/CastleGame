using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private float speed;

    private void Start()
    {
        if(TryGetComponent(out Renderer renderer))
        mat = renderer.material;
        else if(TryGetComponent(out SpriteRenderer renderer_))
        mat = renderer_.material;
    }

    private void Update()
    {
        float distance = Mathf.Repeat(Time.time *  speed, 1);
        Vector2 offset = new Vector2(distance, 0);
        mat.SetTextureOffset("_MainTex", offset);
    }
}
