using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController charControl;
    private CapsuleCollider coll;
    private PlayerAnimator anim;
    private PlayerHealth health;
    [SerializeField] private Vector3 playerStart;
    private Vector3 playerVel;
    [SerializeField] private float playerSpeed = 4.5f;
    [SerializeField] private float jumpHeight = 2.5f;
    private float gravity = 9.81f;
    [SerializeField] private bool grounded;

    #region Getters
    public Vector3 PlayerStart() { return playerStart; }
    public Vector3 PlayerVel() { return playerVel; }
    public float PlayerSpeed() {  return playerSpeed; }
    public float PlayerJumpHeight() { return jumpHeight; }
    public float PlayerGravity() { return gravity; }
    #endregion
    #region Setters
    public void PlayerStart(Vector3 v) { playerStart = v; }
    public void PlayerVel(Vector3 v) { playerVel = v; }
    public void PlayerSpeed(float f) { playerSpeed = f; }
    public void PlayerJumpHeight(float f) { jumpHeight = f; }
    public void PlayerGravity(float f) { gravity = f; }
    #endregion

    void Awake()
    {
        charControl = GetComponent<CharacterController>();
        coll = GetComponent<CapsuleCollider>();
        anim = GetComponentInChildren<PlayerAnimator>();
        health = GetComponent<PlayerHealth>();
        playerStart = transform.position;
    }

    void Update()
    {
        grounded = IsGrounded();

        if(playerVel.y < 0 && grounded)
        {
            playerVel.y = -gravity * Time.deltaTime;
        }
        else
        {
            playerVel.y -= gravity * Time.deltaTime;
        }

        float xDir = Input.GetAxis("Horizontal");
        float yDir = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(xDir, 0.0f, yDir);

        charControl.Move(moveDir * Time.deltaTime * playerSpeed);

        if(moveDir != Vector3.zero)
        {
            gameObject.transform.forward = moveDir;
        }


        anim.UpdateValues(charControl.velocity.magnitude);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Debug.Log("Jump button pressed");
            playerVel.y += Mathf.Sqrt(jumpHeight * -3.0f * -gravity);
        }

        playerVel.y -= 0.01f;
        charControl.Move(playerVel * Time.deltaTime);
    }

    /// <summary>
    /// Checks for if player collider is on the ground
    /// </summary>
    bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        bool g = Physics.Raycast(ray, out RaycastHit hit, coll.bounds.extents.y - (coll.height / 2) + 0.05f);
        if(g == true)
        {     
            if (hit.collider.gameObject.CompareTag("Lava"))
            {
                return false;
            }
        }

        return g;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            GameController.GameCon.UpdateScore(other.gameObject.GetComponent<Pickup>().GetPickedUp());
            
        }
        else if (other.gameObject.CompareTag("Lava"))
        {
            Transform lavaHiss = other.gameObject.transform.GetChild(0);
            lavaHiss.position = transform.position;
            lavaHiss.GetComponent<AudioSource>().Play();
        }
        else if(other.gameObject.name == "EndZone")
        {
            if(GameController.GameCon.CheckFinishAllowed())
            {
                other.gameObject.GetComponent<AudioSource>().Play();
            }
            GameController.GameCon.TryWin();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {

            health.UpdateHealth(PlayerHealth.damageSources.lava);
        }
    }
}
