using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ���� ������Ʈ 2�� �߻��ϴ� �Ѿ� ����.
// �̵�, �ӵ�, Destroy.
public class E_Bullet : MonoBehaviour
{
    // �� �Ҹ�
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
