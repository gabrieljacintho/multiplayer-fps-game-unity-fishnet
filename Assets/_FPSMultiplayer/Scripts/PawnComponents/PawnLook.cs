using FishNet.Object;
using UnityEngine;

public class PawnLook : NetworkBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _xMin;
    [SerializeField] private float _xMax;

    private PawnInput _input;
    private Vector3 _eulerAngles;


    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        _input = GetComponent<PawnInput>();
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        _eulerAngles.x -= _input.MouseY;
        _eulerAngles.x = Mathf.Clamp(_eulerAngles.x, _xMin, _xMax);

        _cameraTransform.eulerAngles = _eulerAngles;

        transform.Rotate(0f, _input.MouseX, 0f, Space.World);
    }
}
