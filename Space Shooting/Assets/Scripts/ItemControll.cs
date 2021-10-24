using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 내 아이템 Destroy
public class ItemControll : MonoBehaviour
{
    public GameManager GM;

    public AudioSource GetItem_BGM;
    public AudioClip GetItemBGM;

    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindWithTag("LifeItem") && collision.gameObject.tag == "Player")
        {
            if (!GetItem_BGM.isPlaying)
            {
                GetItem_BGM.PlayOneShot(GetItemBGM);
            }
            GM.Item_Get = true;

            if(GM.Item_Get == true)
            {
                GM.Item_Get = false;
                GM.Item_Time = 0f;
                GameObject.Find("GamePlaying").GetComponent<PlayerLife>().Lifes += 1;
                Destroy(gameObject, 0.15f);
            }
        }

    }
}
