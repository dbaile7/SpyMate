using UnityEngine;
using System.Collections;
using SocketIO;

public class tiltScript : MonoBehaviour {

    SocketIOComponent socket;

    public GameObject board;

	// Use this for initialization
	void Start () {

        socket = ((GameObject.FindGameObjectWithTag("Socket")).GetComponent<socket>()).getSocket();
        socket.On("TiltData", TiltChange);
    }
	
	void TiltChange(SocketIOEvent ev)
    {
        //float negY = -1;
        //if (float.Parse(ev.data.GetField("y").str) >= 0)
        /*  negY = 1;

      board.transform.rotation = (new Quaternion
          (float.Parse(ev.data.GetField("x").str) * -1,
          board.transform.rotation.y, 
          float.Parse(ev.data.GetField("z").str) * -1,
          board.transform.rotation.w)); //float.Parse(ev.data.GetField("w").str)));
          */
        board.transform.eulerAngles = (new Vector3
            (float.Parse(ev.data.GetField("y").str + 1.0f) * -60.0f, 
				90.0f, 
				float.Parse(ev.data.GetField("x").str + 1.0f) * 60.0f));


        Debug.Log(board.transform.rotation);
    }
}
