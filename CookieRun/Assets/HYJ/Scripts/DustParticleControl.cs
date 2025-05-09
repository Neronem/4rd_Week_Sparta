using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    public ParticleSystem dustParticle; // ����Ʈ ��ƼŬ �ý���

    public void ReplaceParticle(ParticleSystem newDustParticle)
    {
        if (newDustParticle != null)
            Destroy(dustParticle.gameObject); // ���� ��ƼŬ �ý��� ����

        dustParticle = Instantiate(newDustParticle, transform);

    }

    public void CreateDust()
    {
        if (dustParticle == null) return;

        dustParticle.Stop();
        dustParticle.Play();
    }
    
}
