using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatFactory : MonoBehaviour
{
    public float Health = 100f;
    public float Defense = 10f;
    public float Protection = 5f;
    public float Speed = 10f;
    public float Strength = 10f;
    public float Dex = 10f;
    public float Accuracy = 10f;
    public float Luck = 5f;

    Stats m_stats;

    private void Start()
    {
        m_stats = new Stats(Health, Defense, Protection, Speed, Strength, Dex, Accuracy, Luck);
    }

    public Stats Get() { return m_stats; }

    public void MakeRandom(float H, float D, float P, float S, float St, float Dex, float A, float L, float RangeMult)
    {
        Health      = Random.Range(H - (H * RangeMult), H + (H * RangeMult));
        Defense     = Random.Range(D - (D * RangeMult), D + (D * RangeMult));
        Protection  = Random.Range(P - (P * RangeMult), P + (P * RangeMult));
        Speed       = Random.Range(S - (S * RangeMult), S + (S * RangeMult));
        Strength    = Random.Range(St - (St * RangeMult), St + (St * RangeMult));
        Dex         = Random.Range(Dex - (Dex * RangeMult), Dex + (Dex * RangeMult));
        Accuracy    = Random.Range(A - (A * RangeMult), A + (A * RangeMult));
        Luck        = Random.Range(L - (L * RangeMult), L + (L * RangeMult));
    }
}
