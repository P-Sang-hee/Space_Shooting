using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 이동 및 이동 속도, Life 관리.
public class PlayerControll : MonoBehaviour
{
    public GameManager GM;
    
    public int Lifes;
    public float moveSpeed;

    public AudioSource Life_BGM;
    public AudioClip LifeBGM;

    Rigidbody2D DestroyRigid;

    void Start()
    {
        moveSpeed = 0.11f;
        
        DestroyRigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GM.GameStart == true)
        {
            Player_Move();
        }
    }

    // 플레이어 이동. 키보드를 이용한 상하좌우 이동.
    public void Player_Move()
    {
        float UpDown = Input.GetAxis("Horizontal");
        float LeftRight = Input.GetAxis("Vertical");

        transform.position += new Vector3(UpDown, LeftRight, 0) * moveSpeed;

        Vector3 viewPosition = Camera.main.WorldToViewportPoint(transform.position);
        viewPosition.x = Mathf.Clamp01(viewPosition.x);
        viewPosition.y = Mathf.Clamp01(viewPosition.y);

        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(viewPosition);
        transform.position = worldPosition;
    }

    // 적 및 적 총알에 충돌 시 Life 감소.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyB")
        {
            GameObject.Find("GamePlaying").GetComponent<PlayerLife>().Lifes -= 1;
            Life_BGM.PlayOneShot(LifeBGM);
        }
    }

}
