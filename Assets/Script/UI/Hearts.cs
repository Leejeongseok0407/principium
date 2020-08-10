using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

 public class Hearts : MonoBehaviour
{ 

     public static int hp = 5; 

     public GameObject life1; 
     public GameObject life2; 
     public GameObject life3;
     public GameObject life4;
     public GameObject life5;

   

     // Use this for initialization 

     void Start()
    {
         life1.GetComponent<Image>().enabled = true;
         life2.GetComponent<Image>().enabled = true;
         life3.GetComponent<Image>().enabled = true;
         life4.GetComponent<Image>().enabled = true;
         life5.GetComponent<Image>().enabled = true;
    } 

   

     // Update is called once per frame 

     void Update()
    {
        
        switch(hp)

        {
            case 4:
                life5.GetComponent<Image>().enabled = false;
                break;

            case 3:
                life4.GetComponent<Image>().enabled = false;
                break;

            case 2: 
                 life3.GetComponent<Image>().enabled = false;
                 break;
        
             case 1: 
                 life2.GetComponent<Image>().enabled = false;
                 break;
        
             case 0: 
                 life1.GetComponent<Image>().enabled = false;
                 //game over 
                 break;
        
         } 

     } 


    public void HPdown(int dmg)
    {
        Hearts.hp -= 1;
    }

 } 

