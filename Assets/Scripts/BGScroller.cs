using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField]    private float _scrollSpeed;
    [SerializeField]    private float _tileSizeY;
    private Vector2 startPos;

    void Start()
    {
        startPos = new Vector2(0, 24.9f);
    }

    // Update is called once per frame
    void Update()
    {
        float NewPosition = Mathf.Repeat(Time.time * _scrollSpeed, _tileSizeY);
        transform.position = startPos + Vector2.down * NewPosition;
    }
}
