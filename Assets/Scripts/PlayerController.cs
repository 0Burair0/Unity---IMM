using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int turnSpeed;
    private float zBound = -7;
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem explosionEndParticle;
    public AudioClip damageSound;
    public AudioClip keySound;
    public AudioClip gameEnd;
    public AudioClip notEnd;
    private AudioSource playerAudio;
    private GameManager gameManager;
    
   

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        MapLimit();
        
        //starting timer
        StartCoroutine(GameTimeCountDown());

    }
    //moving player from user input
    void MovePlayer()
    {
        if (gameManager.isGameActive){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up,turnSpeed * horizontalInput *Time.deltaTime );

        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
        }
    }
    //restricting back movement to keep player in map
    void MapLimit()
    {
        if(transform.position.z < zBound){
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle")) 
        {
        
            playerAudio.PlayOneShot(damageSound, 1.0f);
            gameManager.UpdateLives(1);
            
        }

    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(keySound, 1.0f);
            gameManager.UpdateKeys(1);
        }

        if(other.gameObject.CompareTag("Exit")  && gameManager.keys != 3)
        {
 
            playerAudio.PlayOneShot(notEnd, 1.0f);
        }
        else if (other.gameObject.CompareTag("Exit")  && gameManager.keys == 3)
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(gameEnd, 1.0f); 
            gameManager.GameWon();
        }

    }

    //game timer
    IEnumerator GameTimeCountDown()
    {
        yield return new WaitForSeconds(120);
      

    }

}
