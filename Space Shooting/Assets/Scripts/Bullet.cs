using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 적 오브젝트를 Destroy 할 수 있는 총알 속도, 이동 및 미충돌 후 화면 밖으로 넘어갈 시 Destroy.
public class Bullet : MonoBehaviour
{
    // 플레이어 불릿
    public float BulletSpeed;
    public float moveY;

    void Start()
    {
        moveY = 0;
        BulletSpeed = 2f;
    }

    void Update()
    {
        // 총알 이동 
        moveY = BulletSpeed * Time.deltaTime;
        transform.Translate(0, moveY, 0);

    }

    // 플레이어 총알 Destroy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindWithTag("Bullet") && collision.gameObject.tag == "DestroyB")
        {
            Destroy(gameObject);
        }

        if (GameObject.FindWithTag("Bullet") && collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
