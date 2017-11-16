using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("shooting game/Player")]
public class player : MonoBehaviour {
    public float m_speed = 1;
    public float m_life = 3;
    protected float m_rocketRate = 0;
    protected Transform m_transform;
    public Transform m_rocket;
    // Use this for initialization

    public AudioClip m_shootClip;
    protected AudioSource m_audio;
    public Transform m_explosionFX;
    public Transform m_hitFX;

    protected Vector3 m_targetPos;
    public LayerMask m_inputMask;

	void Start () {
        m_transform = this.transform;
        m_audio = this.GetComponent<AudioSource>();

        m_targetPos = this.m_transform.position;
	}

    void MoveTo()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 ms = Input.mousePosition;//get mouse position in the screen
            Ray ray = Camera.main.ScreenPointToRay(ms);//ScreenPointToRay changes to a ray
            RaycastHit hitinfo;
            bool iscast = Physics.Raycast(ray, out hitinfo, 1000, m_inputMask);//Raycast generates a ray, only hits the assigned inputMask

            if (iscast)
            {
                m_targetPos = hitinfo.point;
            }
        }
        Vector3 pos = Vector3.MoveTowards(this.m_transform.position, m_targetPos, m_speed * Time.deltaTime);
        this.m_transform.position = pos;
    }

    // Update is called once per frame
    void Update () {
        float movev = 0;
        float moveh = 0;

        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movev -= m_speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            movev += m_speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveh += m_speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveh -= m_speed * Time.deltaTime;
        }

        this.m_transform.Translate(new Vector3(moveh, 0, movev));
       MoveTo();

        m_rocketRate -= Time.deltaTime;

        if (m_rocketRate <= 0)
        {
            m_rocketRate =0.1f;

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Instantiate(m_rocket, m_transform.position, m_transform.transform.rotation);
                m_audio.PlayOneShot(m_shootClip);
            }

        }
       

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("playerRocket") != 0)
        {
            m_life -= 1;
            Instantiate(m_hitFX, m_transform.position, m_transform.rotation);

            if(m_life <= 0)
            {
                Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

}
