using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public AudioClip windUpSound, launchSound;

    public float maxLaunchSpeed;
    public float currentLaunchSpeed;

    private AudioSource audioSource;
    private float increaseRate;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = windUpSound;
        increaseRate = (maxLaunchSpeed * Time.fixedDeltaTime) / audioSource.clip.length;
    }

    private void OnMouseDown()
    {
        currentLaunchSpeed = 0f;
        InvokeRepeating("IncreaseLaunchSpeed", 0.5f, Time.fixedDeltaTime);
        audioSource.clip = windUpSound;
        audioSource.Play();
    }

    private void OnMouseUp()
    {
        CancelInvoke("IncreaseLaunchSpeed");
        audioSource.Stop();
        audioSource.clip = launchSound;
        audioSource.Play();

        GameObject projectile = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity, gameObject.transform);

        Vector3 launchVelocity = new Vector3(1f, 1f, 0.0f).normalized * currentLaunchSpeed;

        projectile.GetComponent<PhysicsObject>().velocity = launchVelocity;


    }

    void IncreaseLaunchSpeed()
    {
        if (currentLaunchSpeed <= maxLaunchSpeed)
        {
            currentLaunchSpeed += increaseRate;
        }
        
        //if (currentLaunchSpeed > maxLaunchSpeed)
        //{
        //    currentLaunchSpeed = maxLaunchSpeed;
        //    currentLaunchPercentage = 1f;
        //    CancelInvoke("IncreaseLaunchSpeed");
        //}
    }
}
