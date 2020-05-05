using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    public float degree;
    public float intesity;
    Rigidbody bod;
    Vector3 euler;
    Vector3 localup;
    Camera mainCam;

    GameObject fire;

    //canvas vars
    Vector3 pos;
    float s;
    Text speed;
    Text gameover;
    Text fuelT;
    public float fuel;
    Text windT;
    float wind;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        //degree = 1;
        bod = GetComponent<Rigidbody>();
        euler = new Vector3(0, 0, degree);
        localup = transform.up;

        fire = GameObject.Find("Ship").transform.Find("fire").gameObject;
        fire.SetActive(false);

        windT = GameObject.Find("Canvas").transform.Find("Wind").gameObject.GetComponent<Text>();
        fuelT = GameObject.Find("Canvas").transform.Find("Fuel").gameObject.GetComponent<Text>();
        speed = GameObject.Find("Canvas").transform.Find("Speed").gameObject.GetComponent<Text>();
        gameover = GameObject.Find("Canvas").transform.Find("End").gameObject.GetComponent<Text>();
        gameover.text = "";
        fuelT.text = "Feul: " + fuel;
        wind = Random.Range(-2f, 2f);
        bool t = true;
        string temp = "~> ";
        float f = wind;
        if (wind < 0)
        {
            f = wind * -1;
            temp = "<~ ";
        }
        windT.text = "Wind: " + temp + f.ToString("F1") + "mph";

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }

        mainCam.transform.position = new Vector3(GameObject.Find("Ship").transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);

        bool v = Input.GetKey("space");
        float h = Input.GetAxis("Horizontal");

        localup = transform.up;
        Quaternion rotation = Quaternion.Euler(euler * h);
        //Debug.Log("h: " + h);
        if (transform.position.y > 1.3f)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(wind , 0, 0));
        }
        bod.MoveRotation(bod.rotation * rotation);
        if (v&&fuel>0)
        {
            fire.SetActive(true);
            Debug.Log("fly");
            Vector3 t = new Vector3(intesity, intesity, intesity);

            bod.AddForce(Vector3.Scale(t, localup));
            fuel = fuel - 2;
        }
        else
        {
            fire.SetActive(false);
        }

        fuelT.text = "Fuel: " + fuel;
    }

    private void FixedUpdate()
    {
        s = (GameObject.Find("Ship").transform.position - pos).magnitude;
        pos = GameObject.Find("Ship").transform.position;

        speed.text = "Speed: " + (s*100).ToString("F2");

        if (s < 0.05)
        {
            speed.color = new Color(0, 255, 0);
        }else if(s < 0.10)
        {
            speed.color = new Color(100, 100,0);
        }else if(s > 0.10)
        {
            speed.color = new Color(255, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (s<0.10 && collision.gameObject.tag == "Landing")
        {
            gameover.text = "You Landed safely!";
            Time.timeScale = 0;
        }

        if (s > 0.10)
        {
            Time.timeScale = 0;
            gameover.text = "You crashed!";
        }

    }
}
