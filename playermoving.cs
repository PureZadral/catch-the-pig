using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playermoving : MonoBehaviour
{
    Transform pBody;
    CharacterController control;
    public GameObject image;
    public TextMeshProUGUI text;
    float vertical;
    float horizontal;
    float mouseX;
    float gravityValue = -9.81f;
    public float speed = 5f;
    public float jumpHeight = 5f;
    bool isGrounded = false;
    static int pigs = 0;
    bool isCaptured = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pBody = GetComponent<Transform>();
        control = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        control.Move(pBody.forward * vertical * speed * Time.deltaTime);

        horizontal = Input.GetAxis("Horizontal");
        control.Move(pBody.right * horizontal * speed * Time.deltaTime);

        mouseX = Input.GetAxis("Mouse X") * 8;
        pBody.Rotate(0, mouseX, 0);

        control.Move(pBody.up * gravityValue * Time.deltaTime);

        if(Input.GetKeyDown("space") && isGrounded == true)
        {
            control.Move(pBody.up * jumpHeight);
        }

        isGrounded = false;
        isCaptured = false;
    }

    void OnControllerColliderHit(ControllerColliderHit coll)
    {
        if(coll.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
        if(coll.gameObject.tag == "Pig" && isCaptured == false)
        {
            Destroy(coll.gameObject);
            pigs = pigs + 1;
            isCaptured = true;
            text.text = "Поймано свиней " + pigs;
        }
        if(pigs == 4)
        {
            image.SetActive(true);
        }
    }
}
