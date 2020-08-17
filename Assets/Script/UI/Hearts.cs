using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

 public class Hearts : MonoBehaviour
{
    [SerializeField] GameObject[] hpImg;
    [SerializeField] GameObject player;
    public int hp; 

     void Start()
    {
        hp = player.gameObject.GetComponent<PlayerBehaviour>().ReturnPlayerHp();
        DisActiveHpImage(hp);
    } 

    public void HPdown(int dmg)
    {
        hp -= dmg;
        DisActiveHpImage(hp);
    }

    public void DisActiveHpImage(int num)
    {
        for (int i = num; i < hpImg.Length; i++)
        {
            hpImg[i].gameObject.SetActive(false);
        }
    }

} 

