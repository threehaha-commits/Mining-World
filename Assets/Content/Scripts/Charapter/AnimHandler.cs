using System;
using UnityEngine;

public enum Anim
{
    Idle,
    Walk,
    Mining
};

[RequireComponent(typeof(Move))]
public class AnimHandler : MonoBehaviour
{
    private static Animator A_Anim;
    private const string A_Idle = "Idle";
    private const string A_Walk = "FWalk";
    private const string A_Mining = "Mining";

    private void Awake()
    {
        A_Anim = GetComponent<Animator>();
    }

    public static void SetMining(bool value)
    {
        A_Anim.SetBool(A_Mining, value);
    }
    
    public static void Play(Anim anim)
    {
        switch (anim)
        {
            case Anim.Idle:
                A_Anim.SetTrigger(A_Idle);
                break;
            case Anim.Mining:
                A_Anim.SetTrigger(A_Mining);
                break;
        }
    }

    public static void Direction(float direction)
    {
        var dir = Mathf.Abs(direction);
        A_Anim.SetFloat(A_Walk, dir);
    }
}