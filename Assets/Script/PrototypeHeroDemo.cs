using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharachterController : MonoBehaviour {

    [Header("Variables")]
    [SerializeField] float      m_maxSpeed = 5.5f;
    [SerializeField] float      m_jumpForce = 9f;
    [SerializeField] bool       m_hideSword = false;
    [Header("Effects")]
    [SerializeField] GameObject m_RunStopDust;
    [SerializeField] GameObject m_JumpDust;
    [SerializeField] GameObject m_LandingDust;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Prototype    m_groundSensor;
    public AudioSource         m_audioSource;
    private AudioManager_PrototypeHero m_audioManager;
    private bool                m_grounded = false;
    private bool                m_moving = false;
    private int                 m_facingDirection = 1;
    private float               m_disableMovementTimer = 0.0f;

    public bool HasKey;
    public GameObject Key;
    public GameObject Door2;
    public GameObject LevelController;
    public GameObject JumpBoost;

    public Vector3 PortalStart;
    public Vector3 PortalEnd;
    // Use this for initialization
    void Start ()
    {
        PortalStart = GameObject.Find("PortalStart").transform.position;
        PortalEnd = GameObject.Find("PortalEnd").transform.position;
        HasKey = false;
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_PrototypeHero.instance;
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
    }

    // Update is called once per frame
    void Update ()
    {
        if(HasKey == true)
        {
            Key.GetComponent<KeyFollowingScript>().enabled = true;
        }
        // Decrease timer that disables input movement. Used when attacking
        m_disableMovementTimer -= Time.deltaTime;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = 0.0f;

        if (m_disableMovementTimer < 0.0f)
            inputX = Input.GetAxis("Horizontal");

        // GetAxisRaw returns either -1, 0 or 1
        float inputRaw = Input.GetAxisRaw("Horizontal");
        // Check if current move input is larger than 0 and the move direction is equal to the characters facing direction
        if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
            m_moving = true;

        else
            m_moving = false;

        // Swap direction of sprite depending on move direction
        if (inputRaw > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputRaw < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }
     
        // SlowDownSpeed helps decelerate the characters when stopping
        float SlowDownSpeed = m_moving ? 1.0f : 0.5f;
        // Set movement
        m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);

        // Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // Set Animation layer for hiding sword
        int boolInt = m_hideSword ? 1 : 0;
        m_animator.SetLayerWeight(1, boolInt);

        // -- Handle Animations --
        //Jump
        if (Input.GetButtonDown("Jump") && m_grounded && m_disableMovementTimer < 0.0f)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if(m_moving)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "JumpTextController")
        {
            UIScript.instance.JumpTextOff();
        }
        if (collision.gameObject.name == "GuideController")
        {
            UIScript.instance.GuideTextOff();
        }
        if (collision.transform.tag == "Death")
        {
            SceneManager.LoadScene("DeathScene");
        }
        if (collision.gameObject.name == "JumpBoost")
        {
            UIScript.instance.JumpBoostTextOff();
            JumpBoost.SetActive(false);
            m_jumpForce = 15.5f;
        }

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PortalStart" && (Input.GetKeyDown(KeyCode.E)))
        {
            Debug.Log("TELEPORT");
            transform.position = PortalEnd;
        }
        if (collision.gameObject.name == "PortalEnd" && (Input.GetKeyDown(KeyCode.E)))
        {
            Debug.Log("TELEPORT");
            transform.position = PortalStart;
        }
        if (collision.gameObject.name == "Key" && (Input.GetKey(KeyCode.E)))
        {
            UIScript.instance.KeyTextOff();
            UIScript.instance.GuideText3On();
            Debug.Log("You got the key!");
            HasKey = true;
        }
        if (collision.gameObject.name == "KeyHole" && (Input.GetKey(KeyCode.E)) && HasKey == true)
        {
            Debug.Log("Unlocked!");
            Door2.SetActive(false);
            Key.SetActive(false);
        }
        if (collision.gameObject.name == "Exit" && (Input.GetKey(KeyCode.E)))
        {
            Debug.Log("Level Finishied!");
            SceneManager.LoadScene("LevelOneFininished");
        }
    }
    // Function used to spawn a dust effect
    // All dust effects spawns on the floor
    // dustXoffset controls how far from the player the effects spawns.
    // Default dustXoffset is zero
    void SpawnDustEffect(GameObject dust, float dustXOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, 0.0f, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
        }
    }

    // Animation Events
    // These functions are called inside the animation files
    public void AE_runStop()
    {
        m_audioManager.PlaySound("RunStop");
        // Spawn Dust
        float dustXOffset = 0.6f;
        SpawnDustEffect(m_RunStopDust, dustXOffset);
    }

    public void AE_footstep()
    {
        m_audioManager.PlaySound("Footstep");
    }

    public void AE_Jump()
    {
        m_audioManager.PlaySound("Jump");
        // Spawn Dust
        SpawnDustEffect(m_JumpDust);
    }

    public void AE_Landing()
    {
        m_audioManager.PlaySound("Landing");
        // Spawn Dust
        SpawnDustEffect(m_LandingDust);
    }
}
