using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum Type { Number, Mine, Flag, Exploded, Unknown }
    public Type type;

    public bool isRevealed;
    public bool isFlagged;
    public bool isExploded;
    public int index;

    public SpriteRenderer spriteRdr;
    void Awake()
    {
        spriteRdr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        isRevealed = isFlagged = isExploded = false;
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
