using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    public Joystick joystick;
    Animator _anim;
    Rigidbody2D rb;
    private CameraShake shakeCam;
    //la player speed era pubblica fino a poco fa, l'ho cambiata
    private float playerSpeed = 5f;
    public float paliCollected = 0;
    private bool isGrounded;
    private bool isJumping;
    private bool hasDoubleJumped;
    private bool isDoubleJumping;
    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private float moveInput;
    public Transform feetPosition;
    public float checkRadius;
    public LayerMask groundMask;
    private UImanager uiman;
    private AudioSource jumpSoundSource;
    public AudioClip[] jumpSounds;
    public AudioClip hitSound;
    public AudioClip gotObjectSound;
    public GameObject sayan;
    public AudioClip aureaSound;
    public AudioSource additionalAudio;
    private AudioSource shrinkAudio;
    public AudioClip[] punchSounds;
    public Sprite bodyMuscoloso;
    public AudioClip shrinkingSound;
    public AudioClip enlargingSound;

    public Sprite headMuscoloso;
    public AudioSource transformazioneAudio;

    public GameObject armSxMuscoloso;
    public GameObject armDxMuscoloso;

    public GameObject torcia;

    //For input
    bool getDownJump;
    bool getUpJump;
    bool getJump;
    bool getAccovacciato;
    bool getUpAccovacciato;
    bool wasAccovacciato = false;
    // Start is called before the first frame update
    void Start()
    {
        shrinkAudio = GameObject.Find("shrinkingAudio").GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        uiman = GameObject.Find("Canvas").GetComponent<UImanager>();
        shakeCam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        jumpSoundSource = GetComponent<AudioSource>();
        
    }

    private void FixedUpdate()
    {

#if UNITY_ANDROID || UNITY_IOS
        if (joystick.Horizontal > 0.2f)
            moveInput = 1f;
        else if (joystick.Horizontal < -0.2f)
            moveInput = -1f;
        else moveInput = 0f;
#else
        moveInput = Input.GetAxis("Horizontal");
#endif
        //Flip the character
        Vector3 characterScale = transform.localScale;
        if ((moveInput < 0 && characterScale.x >0)|| (moveInput > 0 && characterScale.x < 0))
        {
            characterScale.x = -characterScale.x;
        }
        
        transform.localScale = characterScale;


        
        if (moveInput != 0) _anim.SetBool("IsWalking", true); else _anim.SetBool("IsWalking", false);
        if (rb.position.x >= 8.5f && moveInput == 1) rb.velocity = new Vector2(-playerSpeed, rb.velocity.y);
        else if (rb.position.x <= -7.5f && moveInput == -1) rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        else rb.velocity = new Vector2(moveInput * playerSpeed, rb.velocity.y);
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, groundMask);


        //JUMPING

#if UNITY_ANDROID || UNITY_IOS
        /*
        getDownJump = Input.GetButtonDown("Jump"); 
        getUpJump = Input.GetButtonUp("Jump");
        getJump = Input.GetButton("Jump");
        */
        if (joystick.Vertical <= -0.5f && wasAccovacciato == false)
        {
            getAccovacciato = true;
            getUpAccovacciato = false;
            wasAccovacciato = true;
        }
        /*
        if (joystick.Vertical <= -0.5f && wasAccovacciato == true)
        {
            getAccovacciato = false;
        }*/
        if (joystick.Vertical > -0.3f  && wasAccovacciato == true)
        {
            wasAccovacciato = false;
            getAccovacciato = false;
            getUpAccovacciato = true;
        }
        /*
        if (joystick.Vertical > -0.3f && wasAccovacciato == false)
        {
            getUpAccovacciato = false;
        }
        */
#else
         getDownJump = Input.GetKeyDown(KeyCode.UpArrow);
        getUpJump = Input.GetKeyUp(KeyCode.UpArrow);
        getJump = Input.GetKey(KeyCode.UpArrow);
        getAccovacciato = Input.GetKey(KeyCode.DownArrow);
        getUpAccovacciato = Input.GetKeyUp(KeyCode.DownArrow);

#endif

            if (isGrounded && getDownJump)
        {
            jumpTimeCounter = jumpTime;
            _anim.SetTrigger("IsTakingOff");
            jumpSoundSource.clip = jumpSounds[Random.Range(0, jumpSounds.Length)];
            jumpSoundSource.Play();
            isJumping = true;
            hasDoubleJumped = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (isGrounded == false) _anim.SetBool("IsJumping", true);
        else
        {
            _anim.SetBool("IsJumping", false);
            _anim.SetBool("IsDoubleJumping", false);
        }
        if (getJump && isJumping == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }

        if (getUpJump || jumpTimeCounter <= 0)
        {
            isJumping = false;
            isDoubleJumping = false;
        }

        if(isGrounded == false && isJumping ==false && isDoubleJumping ==false && getDownJump && hasDoubleJumped ==false)
        {
            jumpSoundSource.clip = jumpSounds[Random.Range(0, jumpSounds.Length)];
            jumpSoundSource.Play();
            isDoubleJumping = true;
            hasDoubleJumped = true;
            _anim.SetBool("IsDoubleJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter = jumpTime;

        }
        if (getJump && isDoubleJumping == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
            hasDoubleJumped = true;
        }
        //ACCOVACCIATO
        if (getAccovacciato && isGrounded) _anim.SetBool("IsAccovacciato", true);
        if (getUpAccovacciato && isGrounded) _anim.SetBool("IsAccovacciato", false);

       
    }



    public void objectCollected (string objectTag)
    {
        jumpSoundSource.clip = gotObjectSound;
        jumpSoundSource.Play();
        if (objectTag.CompareTo("Palo") == 0)
        {
            uiman.poleCollected();
        }
        else if(objectTag.CompareTo("Catino") ==0){

            uiman.catinoCollected();
        }
        else if (objectTag.CompareTo("sovrattelo") == 0)
        {

            uiman.sovratteloCollected();
        }
        else if (objectTag.CompareTo("saluto") == 0)
        {
            PowerUp("saluto");
        }
        else if (objectTag.CompareTo("lives") == 0)
        {
            PowerUp("lives");
        }
        else if (objectTag.CompareTo("energyBall") == 0)
        {
            PowerUp("energyBall");
        }
        else if (objectTag.CompareTo("shrink") == 0)
        {
            PowerUp("shrink");
        }
        else if (objectTag.CompareTo("flashlightObject") == 0)
        {
            PowerUp("flashlightObject");
        }
        else if (objectTag.CompareTo("lampoObject") == 0)
        {
            PowerUp("lampoObject");
        }
    }

    public void playerHit ()
    {   
        if (sayan.activeSelf == false)
        {
            jumpSoundSource.clip = hitSound;
            jumpSoundSource.Play();
            uiman.subtractLife();
            shakeCam.enabled = true;
        }
        else
        {
            jumpSoundSource.clip = punchSounds[Random.Range(0, punchSounds.Length)];
            jumpSoundSource.Play();
        }
        
    }
#if UNITY_ANDROID || UNITY_IOS
    public void JumpClickDown()
    {
        getDownJump = true;
        getJump = true;
        getUpJump = false;
        StartCoroutine(resetDownJump());
    }

    public void JumpClickUp()
    {
        getDownJump = false;
        getUpJump = true;
        getJump = false;
        StartCoroutine(resetUpJump());
    }

    IEnumerator resetDownJump()
    {
        yield return new WaitForSeconds(0.2f);
        getDownJump = false;
    }
    IEnumerator resetUpJump()
    {
        yield return new WaitForSeconds(0.2f);
        getUpJump = false;
    }
#endif

    private void PowerUp (string tipo)
    {
        if (tipo.CompareTo("saluto") == 0)
        {
            StartCoroutine(SalutoPowerUp());
        }

        if (tipo.CompareTo("lives") == 0)
        {
            uiman.AddLives();
        }
        if (tipo.CompareTo("energyBall") == 0)
        {
            uiman.ActivateBeamButton();
        }
        if (tipo.CompareTo("shrink") == 0)
        {
            StartCoroutine(ShrinkPowerUp());
        }
        if (tipo.CompareTo("flashlightObject") == 0)
        {
            uiman.ActivateLightButton();
        }
        if (tipo.CompareTo("lampoObject") == 0)
        {
            uiman.ActivateLampoButton();
        }
    }

    IEnumerator SalutoPowerUp()
    {
        additionalAudio.Play(); 
        sayan.SetActive(true);
        yield return new WaitForSeconds(7f);
        additionalAudio.Stop();
        sayan.SetActive(false);
    }

    IEnumerator ShrinkPowerUp()
    {
        shrinkAudio.clip = shrinkingSound;
        shrinkAudio.Play();
        Debug.Log("Scale = " + transform.localScale);
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(0.3f, 0.3f, 0.3f));
        Debug.Log("Scale After = " + transform.localScale);
        yield return new WaitForSeconds(7f);
        shrinkAudio.clip = enlargingSound;
        shrinkAudio.Play();
        gameObject.transform.localScale = 1/0.3f * gameObject.transform.localScale;
    }

    public void trasformazione1()
    {
        GameObject.Find("Body").GetComponent<SpriteRenderer>().sprite = bodyMuscoloso;
        transformazioneAudio.Play();

    }
    public void trasformazione2()
    {
        
        GameObject.Find("ArmSx").SetActive(false);
        GameObject.Find("ArmDx").SetActive(false);
        
        armSxMuscoloso.SetActive(true);
        armDxMuscoloso.SetActive(true);
        torcia.transform.parent = armDxMuscoloso.transform;
        transformazioneAudio.Play();
    }
    public void trasformazione3()
    {
        GameObject.Find("Head").GetComponent<SpriteRenderer>().sprite = headMuscoloso;
        transformazioneAudio.Play();

    }

    public void increasePlayerSpeed()
    {
        playerSpeed *= 2f;
        _anim.SetFloat("speedMultiplier", 2f);
    }

    public void decreasePlayerSpeed()
    {
        playerSpeed /= 2f;
        _anim.SetFloat("speedMultiplier", 1f);
    }
}
