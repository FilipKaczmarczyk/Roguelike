using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{
    public GunPickup[] potencialGuns;

    public SpriteRenderer sr;
    public Sprite unlockedChest;

    public GameObject message;

    public Transform spawnPoint;

    private bool canOpen, isOpen = false;

    public float scaleSpeed = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                int randomGun = Random.Range(0, potencialGuns.Length);

                Instantiate(potencialGuns[randomGun], spawnPoint.position, transform.rotation);

                sr.sprite = unlockedChest;

                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

                isOpen = true;

                message.SetActive(false);
            }
        }

        if (isOpen)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !isOpen)
        {
            message.SetActive(true);
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            message.SetActive(false);
            canOpen = false;
        }
    }
}
