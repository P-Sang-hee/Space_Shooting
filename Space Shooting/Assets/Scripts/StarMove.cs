using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 배경 움직임.
public class StarMove : MonoBehaviour
{
    public GameManager GM;

    private float StarOffset;
    private MeshRenderer StarRd;

    public float StarMoveSpeed;
    
    void Start()
    {
        StarRd = GetComponent<MeshRenderer>();
        StarMoveSpeed = 0.05f;
    }

    void Update()
    {
        if (GM.GameStart == true)
        {
            StarOffset += Time.deltaTime * StarMoveSpeed;
            StarRd.material.mainTextureOffset = new Vector2(0, StarOffset);

        }
    }
}
