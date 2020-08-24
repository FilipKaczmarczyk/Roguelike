using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;

    public float speed = 40f;

    public Camera mainCamera, bigMapCamera;

    private bool bigMapActive;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!bigMapActive)
            {
                BigMapActivate();
            }
            else
            {
                BigMapDeactivate();
            }
        }
    }

    public void ChangeRoom(Transform room)
    {
        target = room;
    }

    public void BigMapActivate()
    {
        if (!LevelManager.instance.isPause)
        {
            bigMapActive = true;

            bigMapCamera.enabled = true;
            mainCamera.enabled = false;

            PlayerController.instance.canMove = false;

            Time.timeScale = 0f;

            UIController.instance.mapDisplay.SetActive(false);
            UIController.instance.bigMapText.SetActive(true);
        }
    }

    public void BigMapDeactivate()
    {
        if (!LevelManager.instance.isPause)
        {
            bigMapActive = false;

            bigMapCamera.enabled = false;
            mainCamera.enabled = true;

            PlayerController.instance.canMove = true;

            Time.timeScale = 1f;

            UIController.instance.mapDisplay.SetActive(true);
            UIController.instance.bigMapText.SetActive(false);
        }    
    }
}
