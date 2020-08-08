using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Button : MonoBehaviour
{
   [SerializeField] GameObject wall;
   

    void WallKeyActive(bool set)
    {
        if (wall != null)
        {
            wall.SetActive(set);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WallKeyActive(false);
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        WallKeyActive(true);
    }

}
