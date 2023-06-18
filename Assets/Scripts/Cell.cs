using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum Type { Invalid, Empty, Mine, Number }
    public Type type;
    public Vector3Int position;
    public int number;
    public bool revealed;
    public bool flagged;
    public bool exploded;


}
