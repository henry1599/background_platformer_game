using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grounded : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject obj;
    void Start()
    {
        obj = gameObject.transform.parent.gameObject;
        //obj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            obj.GetComponent<CharacterController>().isGrounded = true;
        }
        if (collision.collider.tag == "DeadZone")
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            obj.GetComponent<CharacterController>().isGrounded = false;
        }
    }
}
