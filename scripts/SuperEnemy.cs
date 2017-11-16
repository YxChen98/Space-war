using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("shooting game/SuperEnemy")]
public class SuperEnemy : enemy {

    private int m_Spoint = 25;

    protected Transform m_player;

    private void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if(obj != null)
        {
            m_player = obj.transform;
        }
    }


 
    
    protected override void UpdateMove()
    {

        m_fireTimer -= Time.deltaTime;
        if (m_fireTimer <= 0)
        {
            m_fireTimer = 2;

            if (m_player != null)
            {
                Vector3 relativePos = m_transform.position - m_player.position;
                Instantiate(m_rocket, m_transform.position, Quaternion.LookRotation(relativePos));
                m_audio.PlayOneShot(m_shootClip);
            }
        }

        base.UpdateMove();
        m_transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("playerRocket") == 0)
        {
            rocket Rocket = other.GetComponent<rocket>();
            if (Rocket != null)
            {
                m_life -= Rocket.m_power;
                Instantiate(m_hitFX, m_transform.position, m_transform.rotation);

                if (m_life <= 0)
                {
                    GameManager.Instance.AddScore(m_Spoint);
                    Instantiate(m_explosionFX, m_transform.position, m_transform.rotation);
                    Destroy(this.gameObject);
                }
            }
        }


        if (other.tag.CompareTo("Bound") == 0)
        {
            m_life = 0;
            Destroy(this.gameObject);
        }
    }
}

