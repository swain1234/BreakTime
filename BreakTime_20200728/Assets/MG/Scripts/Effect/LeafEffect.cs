using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafEffect : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystemRenderer psr;

    ParticleSystem.MainModule pMain;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        psr = GetComponent<ParticleSystemRenderer>();
        pMain = ps.main;
    }

    public void ChangeLeaf(string s,float a = 0.3f, float b = 0.3f)
    {
        psr.material = Resources.Load("Leaves/"+s, typeof(Material)) as Material;
        pMain.startSize = new ParticleSystem.MinMaxCurve(a, b);
    }
}
