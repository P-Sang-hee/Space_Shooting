using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �� ���� ������Ʈ 1 ����. �̵� �ӵ� �� �÷��̾ Destroy �� ���� �߰�.
public class Enemy1Controll : MonoBehaviour
{
    public GameManager GM;

    public int Enemy1Life;
    public float MoveY;
    public float EnemyMoveSpeed;
    public int Score;

    public GameObject bomb;
    public Text GameScore;

    void Start()
    {
        Enemy1Life = 3;
        EnemyMoveSpeed = 2.0f;
    }

    void Update()
    {
        MoveY = EnemyMoveSpeed * -Time.deltaTime;
        transform.Translate(0, MoveY, 0);
        GameScore = GameObject.Find("GameManager").GetComponent<GameManager>().GameScore;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾��� �Ѿ˰� �浹 ��, �� ������ 1 ����. 0�� �� ���� ������Ʈ�� ��� Destroy.
        if(collision.gameObject.tag == "Bullet")
        {
            Enemy1Life -= 1;
            if (Enemy1Life == 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().Score += 10;
                
                Instantiate(bomb, transform.position, transform.rotation);
                Destroy(gameObject, 0.01f);
            }
        }

        // �÷��̾��� �Ѿ˰� �浹���� �ʰ�, ȭ�� ������ ���� ��� Destroy. 
        if (collision.gameObject.tag == "DestroyB")
        {
            Destroy(gameObject);
        }
    }
}
