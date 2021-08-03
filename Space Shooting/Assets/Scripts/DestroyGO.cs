using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ���� ������Ʈ Destroy �� �ִϸ��̼�
public class DestroyGO : MonoBehaviour
{
    public GameManager GM;

    public AudioSource Bomb_BGM;
    public AudioClip BombBGM;

    void Start()
    {
        
    }
    void Update()
    {
        if (!Bomb_BGM.isPlaying)
        {
            Bomb_BGM.PlayOneShot(BombBGM);
            Destroy(Bomb_BGM, 0.3f);
        }
        Destroy(gameObject, 0.2f);
    }
}
