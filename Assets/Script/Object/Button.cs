using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Monster" || collision.collider.tag == "Player")
            WallKeyActive(false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Monster" || collision.collider.tag == "Player")
            WallKeyActive(true);
    }
}
