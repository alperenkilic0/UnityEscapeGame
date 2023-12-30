using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public Text zaman, situation;
    public Button btn;

    bool gameResume = true;
    bool gameEnd = false;

    float zamanSayaci = 20;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb= GetComponent<Rigidbody>();
        


    }

    // Update is called once per frame
    void Update()
    {
        if(gameResume && !gameEnd)
        {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";

        }
        else if(!gameEnd)
        {
            situation.text = "You got caught.";
            btn.gameObject.SetActive(true);
        }
        

        if(zamanSayaci<=0)
        {
            gameResume = false;
        }


        
    }

    private void FixedUpdate()
    {
        if ((gameResume && !gameEnd))
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isWalking", true);
                transform.Translate(new Vector3(0, 0, 10f) * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isRunning", true);
                transform.Translate(new Vector3(0, 0, 16f) * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isWalking", true);
                transform.Rotate(0, -60 * Time.deltaTime, 0);
                transform.Translate(new Vector3(0, 0, 10f) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isWalking", true);
                transform.Rotate(0, 60 * Time.deltaTime, 0);
                transform.Translate(new Vector3(0, 0, 10f) * Time.deltaTime);
            }

        }
        else 
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);

        }

    }
    public void tryAgain()
    {
        SceneManager.LoadScene(0);
    }
    private void OnCollisionEnter(Collision other)
    {
        string objIsmi =other.gameObject.name;
        if (objIsmi.Equals("FinishPoint"))
        {
            print("You Won");
            gameEnd = true;
            situation.text = "UH..! You tried to escape, but they still can catch you. Continue..";
        }
    }
}
