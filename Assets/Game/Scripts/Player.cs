﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    private CharacterController _controller;

    //private UIManager _uiManager;

    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private GameObject _hitMarkerPreFab;

    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private float _gravity = 9.81f;

    [SerializeField]
    private int currentArmmo;

    private int maxArmmo = 1000;

    public bool possuiMoeda = false;
    public bool weaponAtivo = false;

    public AudioClip footstepSound;
    public float footsFrequency = 0.7f;

    float fr = 0f;
    bool podeMoverParaCima = true;

    [SerializeField]
    private GameObject _weapon;

	// Use this for initialization
	void Start () {
        _controller = GetComponent<CharacterController>();
        //_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentArmmo = 0;
        //_uiManager.UpdateAmmo(currentArmmo);
    }
	
	// Update is called once per frame
	void Update () {

        //Recarregando a arma
        if( Input.GetKeyDown(KeyCode.R) )
        {
            StartCoroutine(CoroutineRecarregando());
        }

        //Código para disparo da arma, a partir da camera principal
        if( Input.GetMouseButton(0) && currentArmmo > 0 && weaponAtivo)
        {
            currentArmmo--;
            //_uiManager.UpdateAmmo(currentArmmo);
            Shoot();
        }
        else
        {
            _muzzleFlash.SetActive(false);
        }

        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;        
            SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        }

        CalculateMovement();
    }



    public void Shoot()
    {
        if( this.weaponAtivo )
        {
            _muzzleFlash.SetActive(true);

            Vector3 point = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);

            /*
            Ray rayPoint = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(rayPoint, out hitInfo))
            {
                Debug.Log("HitInfo: " + hitInfo.transform.name);
                Instantiate(_hitMarkerPreFab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

                Destructable wooden_Crate = hitInfo.transform.GetComponent<Destructable>();

                if(wooden_Crate != null)
                {
                    wooden_Crate.DestruirCaixa();
                }                
            }
            */
        }              
    }

    public void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;

        //Aplicando gravidade
        velocity.y -= _gravity;

        //Transformando de localSpacte para GlobalSpace
        velocity = transform.TransformDirection(velocity);

        _controller.Move(velocity * Time.deltaTime);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            fr += Time.deltaTime;

            while (fr >= footsFrequency)
            {
                fr = 0f;

                playFootstepSound();
            }

            if(podeMoverParaCima)
            {
                StartCoroutine(MoverParaCima());                
            }
            
        }

    }

    IEnumerator CoroutineRecarregando()
    {
        yield return new WaitForSeconds(1.5f);

        currentArmmo = maxArmmo;

        //_uiManager.UpdateAmmo(currentArmmo);
    }

    public void AtivaArma()
    {
        weaponAtivo = true;
        currentArmmo = maxArmmo;
        //_uiManager.UpdateAmmo(currentArmmo);
        _weapon.SetActive(weaponAtivo);
    }

    public void playFootstepSound()
    {
        GetComponent<AudioSource>().PlayOneShot(footstepSound);
    }

    IEnumerator MoverParaCima()
    {
        podeMoverParaCima = false;
        yield return new WaitForSeconds(0.2f);
        transform.position += Vector3.up * 1.5f * Time.deltaTime;
        yield return new WaitForSeconds(0.35f);
        podeMoverParaCima = true;
    }
}
