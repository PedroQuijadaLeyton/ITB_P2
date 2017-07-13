using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{

    public string score;
    public int score_value;
    bool enter = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Block" && !enter)
        {
            if(score != string.Empty)
            {
                transform.GetChild(0).GetChild(0).GetComponent<Text>().text = score;
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                FindObjectOfType<Manager>().add_score(score_value);
                StartCoroutine(waitforsec_score());
                StartCoroutine(waitforsec_effect());
            }
            enter = true;
            FindObjectOfType<Manager>().impact_sound.Play();
        }
    }

    IEnumerator waitforsec_score()
    {
        yield return new WaitForSeconds(1.5f);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator waitforsec_effect()
    {
        yield return new WaitForSeconds(0.15f);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
}
