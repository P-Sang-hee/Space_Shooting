using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾ �� ������Ʈ�� Destroy �� �� �ִ� �Ѿ� �ӵ�, �̵� �� ���浹 �� ȭ�� ������ �Ѿ �� Destroy.
public class Bullet : MonoBehaviour
{
    // �÷��̾� �Ҹ�
    public float BulletSpeed;
    public float moveY;

    void Start()
    {
        moveY = 0;
        BulletSpeed = 2f;
    }

    void Update()
    {
        // �Ѿ� �̵� 
        moveY = BulletSpeed * Time.deltaTime;
        transform.Translate(0, moveY, 0);

    }

    // �÷��̾� �Ѿ� Destroy
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
