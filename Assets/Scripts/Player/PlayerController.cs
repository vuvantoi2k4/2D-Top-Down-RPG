using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public PlayerMovement Movement { get; private set; }

    [SerializeField] private Transform weaponCollider;

    protected override void Awake()
    {
        base.Awake();

        Movement = GetComponent<PlayerMovement>();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }
    public bool FacingLeft => Movement.FacingLeft;
}