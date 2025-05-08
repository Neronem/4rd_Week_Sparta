using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticle; // ����Ʈ ��ƼŬ �ý���
    [SerializeField] private bool createDustOnJump = true; // ���� �� ����Ʈ ���� ����

    public void CreateDust()
    {
        if (createDustOnJump)
        {
            dustParticle.Stop();
            dustParticle.Play();
        }
    }
}
