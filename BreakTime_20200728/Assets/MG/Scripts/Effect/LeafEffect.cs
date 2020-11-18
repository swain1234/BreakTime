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

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.A))
        //    ChangeLeaf("b", 1f, 2f);
    }

    public void ChangeLeaf(string s,float a = 0.3f, float b = 0.3f)
    {
        psr.material = Resources.Load(s, typeof(Material)) as Material;
        pMain.startSize = new ParticleSystem.MinMaxCurve(a, b);
    }
}
