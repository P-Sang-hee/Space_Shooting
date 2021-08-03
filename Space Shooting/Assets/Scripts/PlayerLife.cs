using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 Life 이미지 출력 관리 및 Life 0일 시 게임 오버 bool 값 true로 변경
public class PlayerLife : MonoBehaviour
{
    public GameManager GM;

    public int Lifes;

    public GameObject AllLifes;
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;

    void Start()
    {
        Lifes = 3;
    }

    void Update()
    {
        if (GM.GameStart == true)
        {
            AllLifes.SetActive(true);
            LifeManager();
        }
    }

    public void LifeManager()
    {
        if (Lifes == 3)
        {
            Life3.SetActive(true);
            Life2.SetActive(true);
            Life1.SetActive(true);
        } else if (Lifes == 2)
        {
            Life3.SetActive(true);
            Life2.SetActive(true);
            Life1.SetActive(false);
        } else if (Lifes == 1)
        {
            Life3.SetActive(true);
            Life2.SetActive(false);
            Life1.SetActive(false);
        } else if (Lifes == 0)
        {
            Life3.SetActive(false);
            Life2.SetActive(false);
            Life1.SetActive(false);
            GM.GameOver = true;
        }
    }

}
