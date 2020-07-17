using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impediments : MonoBehaviour
{
    [SerializeField] int dmg = 0;

    public int CallDmg() {
        return dmg;
    }
}
