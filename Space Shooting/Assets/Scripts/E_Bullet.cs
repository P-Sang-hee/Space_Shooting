using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 게임 오브젝트 2가 발사하는 총알 관리.
// 이동, 속도, Destroy.
public class E_Bullet : MonoBehaviour
{
    // 적 불릿
    public float E_BulletSpeed;
    public float E_moveY;

    void Start()
    {
        E_moveY = 0;
        E_BulletSpeed = 5f;
    }
    
    void Update()
    {
        E_moveY = E_BulletSpeed * -Time.deltaTime;
        transform.Translate(0, E_moveY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindWithTag("EnemyB") && collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (GameObject.FindWithTag("EnemyB") && collision.gameObject.tag == "DestroyB")
        {
            Destroy(gameObject);
        }
    }
}
