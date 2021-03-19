using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    public float shootForce;
    public GameObject bullet;
    public GameObject grenade;
    public GameObject pingPong;
    public GameObject bulletLocation;
    public GameObject grenadeLocation;
    public GameObject pingPongLocation;
    public GameObject neutralLocation;
    
    private Vector3 throwSpeed = new Vector3(0.0f, 7.0f, 0.0f);
    private Rigidbody bodyRobot;
    private GameObject weaponOnRobot;
    private GameObject weaponInLocation;
    // Start is called before the first frame update
    void Start()
    {
        bodyRobot = GetComponent<Rigidbody>();
    }
    void Update()
    { 
        movingRobot();
        if (weaponOnRobot != null)
        {
            Vector3 positionWeapon = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + gameObject.transform.localScale.y, gameObject.transform.position.z);
            weaponOnRobot.gameObject.transform.rotation = gameObject.transform.rotation;
            weaponOnRobot.gameObject.transform.position = positionWeapon;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Shoot(positionWeapon));
            }
        }
    }
    
    private IEnumerator Shoot(Vector3 position)
    {
        weaponOnRobot.gameObject.GetComponent<Rigidbody>().AddForce(weaponOnRobot.gameObject.transform.forward * shootForce + throwSpeed,
            ForceMode.Impulse);
        weaponOnRobot = null;
        yield return new WaitForSeconds(1);
        weaponOnRobot = Instantiate(weaponInLocation, position, gameObject.transform.rotation);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(bulletLocation.gameObject))
        {
            changeWeaponForLocation(bullet);
        }
        else if (collision.gameObject.Equals(grenadeLocation.gameObject))
        {
            changeWeaponForLocation(grenade);
        } 
        else if (collision.gameObject.Equals(pingPongLocation.gameObject))
        {
            changeWeaponForLocation(pingPong);
        } 
        else if (collision.gameObject.Equals(neutralLocation.gameObject))
        {
            Destroy(weaponOnRobot);
            weaponInLocation = null;
        }
    }
    
    void changeWeaponForLocation(GameObject weapon)
    {
        Destroy(weaponOnRobot);
        Vector3 positionWeapon = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + gameObject.transform.localScale.y, gameObject.transform.position.z);
        weaponOnRobot = Instantiate(weapon, positionWeapon, gameObject.transform.rotation);
        weaponInLocation = weapon;
    }
    
    void movingRobot()
    {
        float sideForce = Input.GetAxis("Horizontal") * rotationSpeed;
        if (sideForce != 0.0f)
        {
            bodyRobot.angularVelocity = new Vector3(0.0f, sideForce, 0.0f);
        }

        float forwardForce = Input.GetAxis("Vertical") * movementSpeed;
        if (forwardForce != 0.0f)
        {
            bodyRobot.velocity = bodyRobot.transform.forward * forwardForce;
        }
    }

}
