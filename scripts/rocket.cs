using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("shooting game/rocket")]
public class rocket : MonoBehaviour {
    public float m_speed = 10;
    public float m_liveTime = 1;
    public float m_power = 1.0f;
    protected Transform m_transform;

	// Use this for initialization
	void Start () {
        m_transform = this.transform;
        Destroy(this.gameObject, m_liveTime);
	}
	
	// Update is called once per frame
	void Update () {
        m_transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Enemy") != 0)
            return;
        
        Destroy(this.gameObject);
    }
}
