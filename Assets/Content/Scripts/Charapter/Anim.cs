using UnityEngine;

[RequireComponent(typeof(Move))]
public class Anim : MonoBehaviour
{
    private Animator A_Anim;
    private const string A_Idle = "Idle";
    private const string A_Walk = "Walk";
    private const string A_Mining = "Mining";

    private void Awake()
    {
        A_Anim = GetComponent<Animator>();
    }

    public Animator Get()
    {
        return A_Anim;
    }
}