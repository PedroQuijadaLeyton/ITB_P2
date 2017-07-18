﻿using System.Collections;
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
    int index_imagenes = 0;
    public SpriteRenderer image_to_name;

    bool is_lerping = false;
    float starting_time_lerp;
    Vector3 start_position;
    Vector3 end_position;

    public Text score_text;

    int current_bottom_pressed;
    public GameObject the_claw;
    Animator the_claw_animator;
    GameObject the_claw_open;
    GameObject the_claw_close;

    public GameObject rec_overlay;
    public GameObject analysing_overlay;

    public CanvasGroup recording_menu;
    public GameObject release_button;

    public GameObject pedazo_de_mierda;

    public AudioSource music_sound;
    public AudioSource impact_sound;
    public AudioSource hint_1_sound;
    public AudioSource hint_2_sound;
    public AudioSource hint_3_sound;
    public AudioSource bad_sound;
    public AudioSource good_sound;

    // Use this for initialization
    void Start ()
    {
        the_claw_animator = the_claw.GetComponent<Animator>();
        the_claw_open = the_claw.transform.GetChild(1).gameObject;
        the_claw_close = the_claw.transform.GetChild(0).gameObject;
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

    //public void spawn_block_100()
    //{
    //    number_block++;
    //    tower_block_reference = Instantiate<GameObject>(tower_block);
    //    //set acore to spawn
    //    tower_block_reference.GetComponent<Block>().score = "100%";
    //    tower_block_reference.GetComponent<Block>().score_value = 200;
    //    //change sprite
    //    tower_block_reference.GetComponent<SpriteRenderer>().sprite = good_sprite;
    //    tower_block_reference.transform.position = spawn_position.position;
    //    move_spawn_position();

    //    //cambio images botn
    //    button_good.sprite = image_words[index_imagenes];
    //    index_imagenes++;
    //    music.mute = false;
    //    rec.SetActive(false);
    //    button_good.transform.GetChild(0).gameObject.SetActive(false);

    //    StartCoroutine(waitforsec());
    //}

    //public void spawn_block_80()
    //{
    //    number_block++;
    //    tower_block_reference = Instantiate<GameObject>(tower_block);
    //    //set acore to spawn
    //    tower_block_reference.GetComponent<Block>().score = "80%";
    //    tower_block_reference.GetComponent<Block>().score_value = 100;
    //    //change sprite
    //    tower_block_reference.GetComponent<SpriteRenderer>().sprite = medium_sprite;
    //    tower_block_reference.transform.position = new Vector3(-0.3f, spawn_position.position.y, 0);
    //    move_spawn_position();
        
    //    //cambio images botn
    //    button_medium.sprite = image_words[index_imagenes];
    //    index_imagenes++;
    //    music.mute = false;
    //    rec.SetActive(false);
    //    button_medium.transform.GetChild(0).gameObject.SetActive(false);

    //    StartCoroutine(waitforsec());
    //}

    //public void spawn_block_30()
    //{
        
    //}


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
            spawn_position.parent.transform.parent.transform.parent.position = new Vector3(0, spawn_position.parent.transform.parent.transform.parent.position.y + factor_of_height, 0);
        }
    }

    IEnumerator wait_to_move_camera()
    {
        yield return new WaitForSeconds(1.5f);
        move_camera();
    }

    public void recording(int boton_id)
    {
        rec_overlay.SetActive(true);
        music_sound.mute = true;

        if (boton_id == 0)
        {
            hint_1_sound.PlayDelayed(1);
            //button_bad.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if(boton_id == 1)
        {
            hint_2_sound.PlayDelayed(1);
            //button_medium.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            hint_3_sound.PlayDelayed(1);
            //button_good.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void stop_recording(int boton_id)
    {
        current_bottom_pressed = boton_id;

        rec_overlay.SetActive(false);
        analysing_overlay.SetActive(true);

        if (current_bottom_pressed == 0)
        {
            //tower_block_reference = Instantiate(tower_block);
            //change sprite
            //tower_block_reference.GetComponent<SpriteRenderer>().sprite = bad_sprite;
           // tower_block_reference.transform.position = new Vector3(1, spawn_position.position.y, 0);
            //cambio images botn
            //button_bad.sprite = image_words[index_imagenes];
            //index_imagenes++;
           
            //button_bad.transform.GetChild(0).gameObject.SetActive(false);
        }
        //else if (current_bottom_pressed == 1)
        //{
        //    //number_block++;
        //    //tower_block_reference = Instantiate(tower_block);
        //    //set acore to spawn
        //    //tower_block_reference.GetComponent<Block>().score = "80%";
        //    //tower_block_reference.GetComponent<Block>().score_value = 100;
        //    //change sprite
        //    //tower_block_reference.GetComponent<SpriteRenderer>().sprite = medium_sprite;
        //    //tower_block_reference.transform.position = new Vector3(-0.3f, spawn_position.position.y, 0);
        //    //move_spawn_position();

        //    //cambio images botn
        //    //button_medium.sprite = image_words[index_imagenes];
        //    //index_imagenes++;
        //    button_medium.transform.GetChild(0).gameObject.SetActive(false);

        //    //StartCoroutine(wait_to_move_camera());
        //}
        //else
        //{
        //    //number_block++;
        //   // tower_block_reference = Instantiate(tower_block);
        //    //set acore to spawn
        //   // tower_block_reference.GetComponent<Block>().score = "100%";
        //   // tower_block_reference.GetComponent<Block>().score_value = 200;
        //    //change sprite
        //   // tower_block_reference.GetComponent<SpriteRenderer>().sprite = good_sprite;
        //   // tower_block_reference.transform.position = spawn_position.position;
        //   // move_spawn_position();

        //    //cambio images botn
        //   /// button_good.sprite = image_words[index_imagenes];
        //   // index_imagenes++;
        //    button_good.transform.GetChild(0).gameObject.SetActive(false);

        //    //StartCoroutine(wait_to_move_camera());
        //}
        StartCoroutine(wait_for_analysing());
    }

    public void add_score(int score)
    {
        score_text.text = (int.Parse(score_text.text) + score).ToString();
    }


    IEnumerator wait_for_analysing()
    {
        yield return new WaitForSeconds(3.0f);
        prepare_to_release_block();
    }

    void prepare_to_release_block()
    {
        if(current_bottom_pressed != 0)
        {
            recording_menu.alpha = 0;
            recording_menu.interactable = false;
            recording_menu.blocksRaycasts = false;

            release_button.SetActive(true);
            the_claw.SetActive(true);

            //button_good.transform.GetChild(0).gameObject.SetActive(false);
            //button_medium.transform.GetChild(0).gameObject.SetActive(false);
            //button_bad.transform.GetChild(0).gameObject.SetActive(false);

            index_imagenes++;
            image_to_name.sprite = image_words[index_imagenes];

            pedazo_de_mierda.SetActive(true);
            StartCoroutine(wait_to_hide_pedazo());
            good_sound.Play();

            the_claw_animator.SetBool("move", true);
        }
        else
        {
            bad_sound.Play();
        }
        
        music_sound.mute = false;
        analysing_overlay.SetActive(false);
    }

    public void release_block()
    {
        if (current_bottom_pressed == 1)
        {
            number_block++;
            tower_block_reference = Instantiate(tower_block);
            //set acore to spawn
            tower_block_reference.GetComponent<Block>().score = "+100";
            tower_block_reference.GetComponent<Block>().score_value = 100;
            //change sprite
            tower_block_reference.GetComponent<SpriteRenderer>().sprite = medium_sprite;
            //tower_block_reference.transform.position = new Vector3(-0.3f, spawn_position.localPosition.y, 0);
            tower_block_reference.transform.position = spawn_position.position;
            move_spawn_position();

            //cambio images botn
            //button_medium.sprite = image_words[index_imagenes];
            //index_imagenes++;
            //button_medium.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            number_block++;
            tower_block_reference = Instantiate(tower_block);
            //set acore to spawn
            tower_block_reference.GetComponent<Block>().score = "+200";
            tower_block_reference.GetComponent<Block>().score_value = 200;
            //change sprite
            tower_block_reference.GetComponent<SpriteRenderer>().sprite = good_sprite;
            tower_block_reference.transform.position = spawn_position.position;
            move_spawn_position();

            //cambio images botn
            //button_good.sprite = image_words[index_imagenes];
            //button_good.transform.GetChild(0).gameObject.SetActive(false);

            //StartCoroutine(wait_to_move_camera());
        }

        release_button.SetActive(false);
        the_claw_animator.SetBool("move", false);
        StartCoroutine(wait_for_show_recording_menu());
        StartCoroutine(wait_to_move_camera());
    }

    IEnumerator wait_for_show_recording_menu()
    {
        yield return new WaitForSeconds(1.5f);

        recording_menu.alpha = 1;
        recording_menu.interactable = true;
        recording_menu.blocksRaycasts = true;
    }

    IEnumerator wait_to_hide_pedazo()
    {
        yield return new WaitForSeconds(3.0f);
        pedazo_de_mierda.SetActive(false);
    }

}
