using SocketIO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playermove : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private Camera cam;

    public float Speed = 10.0f;
    public float TurnSpeed = 50.0f;
    public float SprintFactor = 2.0f;
    public float Gravity = 20.0f;
	public GameObject key;
	public Text instructions;

    private Vector3 moveDirection = Vector3.zero;

    private SocketIOComponent socket;

	// Use this for initialization
	void Start ()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        controller = GetComponentInChildren<CharacterController>();
        cam = GetComponentInChildren<Camera>();

		instructions.text = "Find a way inside";

        socket = ((GameObject.FindGameObjectWithTag("Socket")).GetComponent<socket>()).getSocket();
    }
	
	// Update is called once per frame
	void Update ()
    {

//		if(Input.GetKeyDown(KeyCode.S)){
//			SceneManager.LoadScene("huntergame");
//
//		}

        //Walking?
        if (Input.GetKey("left") || Input.GetKey("right") || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("MoveParam", 1);
        }
        else if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            anim.SetInteger("MoveParam", 1);
        }
        else {
            anim.SetInteger("MoveParam", 0);
            anim.SetInteger("RunParam", 0);
        }

        //Running?
        if (anim.GetInteger("MoveParam") != 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetInteger("RunParam", 1);
            }
            else {
                anim.SetInteger("RunParam", 0);
            }
        }

        if (controller.isGrounded)
        {
            moveDirection = this.transform.forward * Input.GetAxis("Vertical") * (Speed +
                (Speed * (anim.GetInteger("RunParam") * (SprintFactor - 1.0f))));
        }

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * TurnSpeed * Time.deltaTime, 0);
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= Gravity * Time.deltaTime;
    }
		
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.name == "key"){
			((GameObject.FindGameObjectWithTag ("Socket")).GetComponent<socket> ()).hunt ();
			Destroy(col.gameObject);
			SceneManager.LoadScene("huntergame");
		}
	}
}
