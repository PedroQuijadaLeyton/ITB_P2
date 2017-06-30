using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public GameObject tower_block;
    GameObject tower_block_reference;
    int number_block = 0;
    public Transform main_camera;
    public Transform spawn_position;
    float factor_of_height = 1.3f;

    public Sprite good_sprite;
    public Sprite medium_sprite;
    public Sprite bad_sprite;

    public Sprite[] image_words;
    public Image button_good;
    public Image button_medium;
    public Image button_bad;
    int index_imagenes = 0;

    bool is_lerping = false;
    float starting_time_lerp;
    Vector3 start_position;
    Vector3 end_position;

    public AudioSource impact;
    public AudioSource music;

    public Text score_text;

    public GameObject rec;

    // Use this for initialization
    void Start ()
    {
        impact = GetComponent<AudioSource>();
        music = GetComponents<AudioSource>()[1];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (is_lerping)
        {
            float timeSinceStarted = Time.time - starting_time_lerp;
            float percentageComplete = timeSinceStarted / 1f;

            main_camera.position = Vector3.Lerp(start_position, end_position, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                is_lerping = false;
            }
        }

        if (index_imagenes == 5)
            index_imagenes = 0;
    }

    public void spawn_block_100()
    {
        number_block++;
        tower_block_reference = Instantiate<GameObject>(tower_block);
        //set acore to spawn
        tower_block_reference.GetComponent<Block>().score = "100%";
        tower_block_reference.GetComponent<Block>().score_value = 200;
        //change sprite
        tower_block_reference.GetComponent<SpriteRenderer>().sprite = good_sprite;
        tower_block_reference.transform.position = spawn_position.position;
        move_spawn_position();

        //cambio images botn
        button_good.sprite = image_words[index_imagenes];
        index_imagenes++;
        music.mute = false;
        rec.SetActive(false);
        button_good.transform.GetChild(0).gameObject.SetActive(false);

        StartCoroutine(waitforsec());
    }

    public void spawn_block_80()
    {
        number_block++;
        tower_block_reference = Instantiate<GameObject>(tower_block);
        //set acore to spawn
        tower_block_reference.GetComponent<Block>().score = "80%";
        tower_block_reference.GetComponent<Block>().score_value = 100;
        //change sprite
        tower_block_reference.GetComponent<SpriteRenderer>().sprite = medium_sprite;
        tower_block_reference.transform.position = new Vector3(-0.3f, spawn_position.position.y, 0);
        move_spawn_position();
        
        //cambio images botn
        button_medium.sprite = image_words[index_imagenes];
        index_imagenes++;
        music.mute = false;
        rec.SetActive(false);
        button_medium.transform.GetChild(0).gameObject.SetActive(false);

        StartCoroutine(waitforsec());
    }

    public void spawn_block_30()
    {
        tower_block_reference = Instantiate<GameObject>(tower_block);
        //change sprite
        tower_block_reference.GetComponent<SpriteRenderer>().sprite = bad_sprite;
        tower_block_reference.transform.position = new Vector3(1, spawn_position.position.y, 0);
        //cambio images botn
        button_bad.sprite = image_words[index_imagenes];
        index_imagenes++;
        music.mute = false;
        rec.SetActive(false);
        button_bad.transform.GetChild(0).gameObject.SetActive(false);
    }


    public void move_camera()
    {
        if(number_block > 2)
        {
            is_lerping = true;
            starting_time_lerp = Time.time;

            //We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
            start_position = main_camera.position;
            end_position = new Vector3(0, (main_camera.position.y + factor_of_height), -10);

            //main_camera.position = Vector3.Lerp(main_camera.position, new Vector3(0, (main_camera.position.y + factor_of_height * number_block), -10), 1f);
        }

    }

    public void move_spawn_position()
    {
        if (number_block > 2)
        {
            spawn_position.position = new Vector3(0, spawn_position.position.y + factor_of_height, 0);
        }
    }

    IEnumerator waitforsec()
    {
        yield return new WaitForSeconds(1.5f);
        move_camera();
    }

    public void recording(int boton)
    {
        rec.SetActive(true);
        music.mute = true;

        if (boton == 0)
            button_bad.transform.GetChild(0).gameObject.SetActive(true);
        else if(boton == 1)
            button_medium.transform.GetChild(0).gameObject.SetActive(true);
        else
            button_good.transform.GetChild(0).gameObject.SetActive(true);

    }

    public void add_score(int score)
    {
        score_text.text = (int.Parse(score_text.text) + score).ToString();
    }

}
