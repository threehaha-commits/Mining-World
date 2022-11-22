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
    private const string A_Walk = "Walk";
    private const string A_Mining = "Mining";

    private void Awake()
    {
        A_Anim = GetComponent<Animator>();
    }

    public static void SetMining(bool value)
    {
        A_Anim.SetBool(A_Mining, value);
    }

    public static void Play(bool value)
    {
        A_Anim.SetBool(A_Walk, value);
    }
}