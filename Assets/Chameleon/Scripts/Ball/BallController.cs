using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public const float maxUsualVelocity = 800;
    public const float maxSuperVelocity = 1000;
    public const float forceXUsual = 4000;
    public const float forceXAcceleration = 6000;
    public const float xAcceleration = 1.5f;
    public const int accelerationSeconds = 5;

    //references
    public UIController uiController;
    public AirDetector ad;
    public GamePlay gp;
    public Renderer myRenderer;
    public ParticleSystem trailEffect;
    public ParticleSystem switcherEffect;
    private Rigidbody2D rb;
    public Vector3 beginPosition;

    //variables
    public float maxVelocity;
    public bool isGrounded = false;
    public bool isAccelerated = false;
    public int accelCount = 0;
    public bool addForce = false;
    public Colors myColor = Colors.First;
    public float forceX;
    public float gravityStrong;
    public float gravitySlow;

    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.beginPosition = transform.position;
        ChangeColor(Colors.First);
        maxVelocity = maxUsualVelocity;
    }

    void FixedUpdate()
    {
        if (this.addForce)
        {
            if (this.isGrounded)
            {
                this.rb.AddForce(new Vector2(this.forceX, 0));
            }
            else
            {
                this.rb.AddForce(new Vector2(this.forceX/2 , -this.gravityStrong));
            }
           
        }
        if (! this.isGrounded)
        {
            this.rb.AddForce( new Vector2(0, -this.gravitySlow) ); 
        }

        this.rb.velocity = Vector2.ClampMagnitude(this.rb.velocity, maxVelocity);
    }

    void OnCollisionEnter2D (Collision2D collider)
    {
        if(collider.gameObject.tag == "Ground")
        {
            GroundModel GM = collider.gameObject.GetComponent<GroundModel>();
            if(GM.myColor != this.myColor)
            {
                gp.GameOver();
            }
            ContactPoint2D point = collider.GetContact(0);
            Vector2 dist = -point.point + (Vector2)transform.position;
            float a = Vector2.Angle(rb.velocity, point.normal);
        

            if ( a <= 90 && a >= 80 && rb.velocity.magnitude > 520 && ad.inAir == true)
            {
                Accelaration();
                #if ! UNITY_EDITOR
                Handheld.Vibrate();
                #endif
                ad.inAir = false; 
            }
          
            this.isGrounded = true;
            ad.inAir = false;  
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Ground")
        {
            this.isGrounded = false; 
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (rb.velocity.magnitude > 799 && ad.inAir == true)
            {
                #if ! UNITY_EDITOR
                Handheld.Vibrate();
                #endif
                rb.velocity = Vector2.zero;
                gp.GameOver();
            }
        }
        else if (col.gameObject.tag == "Coin")
        {
            gp.AddCoin();
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Switcher")
        {
           SwitchColor();
        }
    }

    public void Restart ()
    {
        transform.position = this.beginPosition;
        this.rb.velocity = Vector2.zero;
        ChangeColor(Colors.First);
        gp.ToStation(GameStation.MainMenu);
    }

    public void AddForce ()
    {
        addForce = true;
    }

    public void RemoveForce ()
    {
        addForce = false;
    }

    public void ChangeColor (Colors color)
    {
        this.myRenderer.material.color = this.gp.curentColors[(int)color];
       // this.myTrailRenderer.startColor = this.gp.curentColors[(int)color];
        //this.myTrailRenderer.endColor = this.gp.curentColors[(int)color];
        trailEffect.startColor = this.gp.curentColors[(int)color];;
        this.myColor = color;

        switcherEffect.startColor = this.gp.curentColors[(int)color];
        switcherEffect.Play();
    }

    public void SwitchColor ()
    { 
        if (this.myColor == Colors.Second)
        {
            switcherEffect.startColor = this.gp.curentColors[(int)Colors.First]; 
            myRenderer.material.color = this.gp.curentColors[(int)Colors.First];
            trailEffect.startColor = this.gp.curentColors[(int)Colors.First];
            myColor = Colors.First;
        }
        else
        {
            switcherEffect.startColor = this.gp.curentColors[(int)Colors.Second];
            myRenderer.material.color = this.gp.curentColors[(int)Colors.Second];
            trailEffect.startColor = this.gp.curentColors[(int)Colors.Second];
            myColor = Colors.Second;
        }
        switcherEffect.Play();
    }

    public void Accelaration ()
    {
        accelCount++;
        Debug.Log("call");
        if (isAccelerated)
        {
            StopCoroutine("StopAcceleration");
        }

        uiController.SuperMode(true, accelCount);
        maxVelocity = maxSuperVelocity;
        isAccelerated = true;
        trailEffect.startSize = 80;
        trailEffect.startLifetime = .7f;
        forceX = forceXAcceleration;
        rb.velocity *= xAcceleration;
        trailEffect.startColor = new Color32(255, 255, 255, 255);
        StartCoroutine("StopAcceleration");
    }

    IEnumerator StopAcceleration ()
    {
        yield return new WaitForSeconds(5);

        trailEffect.startSize = 50;
        trailEffect.startLifetime = .3f;
        trailEffect.startColor = gp.curentColors[(int)myColor];
        forceX = forceXUsual;
        maxVelocity = maxUsualVelocity;
        isAccelerated = false;
        accelCount = 0;
        uiController.SuperMode(false);
    }

    public void StopRightNow ()
    {
        StopCoroutine("StopAcceleration");
        trailEffect.startSize = 50;
        trailEffect.startLifetime = .3f;
        trailEffect.startColor = gp.curentColors[(int)myColor];
        forceX = forceXUsual;
        maxVelocity = maxUsualVelocity;
        isAccelerated = false;
        accelCount = 0;
        uiController.SuperMode(false);
    }


}
