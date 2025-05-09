using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticle; // 더스트 파티클 시스템
    [SerializeField] private bool createDustOnJump = true; // 점프 시 더스트 생성 여부

    public void CreateDust()
    {
        if (createDustOnJump)
        {
            dustParticle.Stop();
            dustParticle.Play();
        }
    }
}
