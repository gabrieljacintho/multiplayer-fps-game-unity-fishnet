using FishNet.Object;
using UnityEngine;

public sealed class PawnInput : NetworkBehaviour
{
    public float Horizontal;
    public float Vertical;

    public float MouseX;
    public float MouseY;

    public float Sensitivity = 1f;

    public bool Jump;

    private Pawn _pawn;


    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        _pawn = GetComponent<Pawn>();
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        MouseX = Input.GetAxis("Mouse X") * Sensitivity;
        MouseY = Input.GetAxis("Mouse Y") * Sensitivity;

        Jump = Input.GetButton("Jump");
    }
}
