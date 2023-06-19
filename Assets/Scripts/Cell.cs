using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum Type { Number, Mine, Flag, Exploded, Unknown }
    public Type type;

    bool isRevealed;
    bool isFlagged;
    bool isExploded;

    public SpriteRenderer spriteRdr;
    void Awake()
    {
        spriteRdr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetCell(Vector2 position, Type type)
    {
        transform.position = position;
        this.type = type;
    }
}
