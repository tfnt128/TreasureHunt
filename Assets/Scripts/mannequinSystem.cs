using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class mannequinSystem : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody rb;
    private Animator anim;
    public float stepDistance;
    public float rotationSpeed = 5f;
    public float movementSpeed;
    public ItensManager item;
    private AudioSource audio;
    
    
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (item.collectibleCount == 1)
        {
            stepDistance = 1.3f;
            anim.SetTrigger("Start");
        }
        else if(item.collectibleCount == 3)
        {
            stepDistance = 1.1f;
            anim.SetTrigger("Run");
        }
    }

    public void Steps()
    {
        float random = Random.Range(.8f, 1.2f);
        audio.pitch = random;
        audio.Play();
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0f; 
        
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        Vector3 movement = direction.normalized * stepDistance;
        movement.y = 0f;
        
        rb.MovePosition(transform.position + movement * Time.deltaTime * movementSpeed);
    }
    
}