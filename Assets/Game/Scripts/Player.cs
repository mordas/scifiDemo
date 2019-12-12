using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField] private float _speed = 2f;
    private float _gravity = 9.81f;
    [SerializeField] private ParticleSystem _shoot;
    [SerializeField] private GameObject _hitMarker;
    [SerializeField] private AudioSource _shootSound;
    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;
    private bool reloading = false;
    [SerializeField] private GameObject _uiManager;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAmmo = maxAmmo;
                           _uiManager.GetComponent<UIManager>().UpdateAmmo(maxAmmo);
    }

    void Update()
    {
        CalculateMovement();
      ShootMethod();
      if (Input.GetKey(KeyCode.R) && !reloading)
      {
          reloading = true;
          StartCoroutine("Reload");
      }
        
    }

    void ShootMethod()
    {
        if (Input.GetMouseButton(0) && currentAmmo > 0)
                       {
                           Vector3 target = new Vector3(0.5f,0.5f, 0); 
                           Ray rayOrigin = Camera.main.ViewportPointToRay(target);
                           RaycastHit hitinfo;
               
                           if (Physics.Raycast(rayOrigin, out hitinfo,Mathf.Infinity))
                           {
                            GameObject h_marker = (GameObject)Instantiate(_hitMarker, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                            Destroy(h_marker,1f);
                           }
               
                           currentAmmo -= 1;
                           _uiManager.GetComponent<UIManager>().UpdateAmmo(currentAmmo);
                           _shoot.gameObject.SetActive(true);
                           if (_shoot.isPlaying == false)
                           {
                            _shootSound.Play();
                           }
               //            _shoot.Play(true);
                       }
                       else
                       {
               //            _shoot.Stop(true);
               _shoot.gameObject.SetActive(false);
                            _shootSound.Stop();
                       }     
    }

    void CalculateMovement()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float vericalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, vericalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        // Переводим координаты из локальных в глобальные
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
                           _uiManager.GetComponent<UIManager>().UpdateAmmo(maxAmmo);
        reloading = false;
    }
}