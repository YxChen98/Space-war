using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("shooting game/enemy")]
public class enemy : MonoBehaviour {

    public float m_speed = 1;
    public float m_life = 10;
    protected float m_rotSpeed = 5;
    protected Transform m_transform;

    public AudioClip m_shootClip;
    public Transform m_explosionFX;
    public Transform m_hitFX;
    protected AudioSource m_audio;


    protected float m_fireTimer = 3;
    public Transform m_rocket;

    private int m_point = 10;

    // Use this for initialization
    void Start () {
        m_transform = this.transform;
        m_audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        float rot = m_transform.rotation.y;
        rot += m_rotSpeed * Time.deltaTime;
        m_transform.rotation = Quaternion.Euler(new Vector3(0, rot, 0));

        UpdateMove();
	}

    protected virtual void UpdateMove()
    {
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;

        m_transform.Translate(new Vector3(rx, 0, -m_speed * Time.deltaTime));

        m_fireTimer -= Time.deltaTime;

        if (m_fireTimer<=0)
        {
            m_fireTimer = 4;
            Instantiate(m_rocket,m_transform.position,m_transform.rotation);
            m_audio.PlayOneShot(m_shootClip);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("playerRocket")==0){
            rocket Rocket = other.GetComponent<rocket>();
            if(Rocket != null)
            {
                m_life -= Rocket.m_power;
                Instantiate(m_hitFX, m_transform.position, m_transform.rotation);

                if (m_life <= 0)
                {
                    GameManager.Instance.AddScore(m_point);
                    Instantiate(m_explosionFX, m_transform.position, m_transform.rotation);
                    Destroy(this.gameObject);
                }
            }
        }
        
        else if (other.tag.CompareTo("Player") == 0)
        {
            m_life = 0;
            Instantiate(m_explosionFX, m_transform.position, m_transform.rotation);
            Destroy(this.gameObject);
        }

        if (other.tag.CompareTo("Bound") == 0)
        {
            m_life = 0;
            Destroy(this.gameObject);
        }
    }
}
