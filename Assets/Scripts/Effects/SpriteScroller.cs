using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    Vector2 offSet;
    Material material;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offSet = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offSet;
    }
}
