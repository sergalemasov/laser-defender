using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;

    Material myMaterial;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.SetTextureOffset("_MainTex", offset);

        offset.y += backgroundScrollSpeed * Time.deltaTime;

        if (offset.y >= 1)
        {
            offset.y -= 1;
        }
    }
}
