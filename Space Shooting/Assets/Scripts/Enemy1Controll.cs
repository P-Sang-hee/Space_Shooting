using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 적 게임 오브젝트 1 관리. 이동 속도 및 플레이어가 Destroy 시 점수 추가.
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
        // 플레이어의 총알과 충돌 시, 적 라이프 1 감소. 0이 된 게임 오브젝트의 경우 Destroy.
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

        // 플레이어의 총알과 충돌하지 않고, 화면 밖으로 나갈 경우 Destroy. 
        if (collision.gameObject.tag == "DestroyB")
        {
            Destroy(gameObject);
        }
    }
}
