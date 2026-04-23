using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _velocity;
    private Vector3 _dir;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(DialogueManager.Instance.GetDialogueVariables().GetString("MissionId"));
        }
    }


    private void FixedUpdate()
    {
        if (DialogueManager.Instance.DialogueIsPlaying) return;
        _rb.MovePosition(_rb.position + _dir * (Time.deltaTime * _velocity));
    }
}
