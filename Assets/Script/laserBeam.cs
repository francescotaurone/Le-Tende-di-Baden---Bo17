using UnityEngine;
using System.Collections;

public class laserBeam : MonoBehaviour
{
    [Header("Laser pieces")]
    
   
    public GameObject laserEnd;
    public GameObject imageClouds;
    public GameObject generalLight;
    public GameObject beam;
    public GameObject thunderSound;
    public GameObject beamButton;
    public LayerMask hittableObjectMask;
    public GameObject player;

    public GameObject spriteMask;
    private AudioSource objectHit;
    public GameObject uiman;
    private AudioSource backgroundSong;

    bool istruzioniPerBottoneBeamMostrate = false;
    public GameObject istruzioniButtonBeam;

    private float initialDimensions;
    private float timeToCount = 5f;
    private float timeCounter = 0f;
    private float radius;
    private float oldVolume;
    private Vector2 laserDirection;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) == true && beamButton.activeSelf == true)
        {
            Debug.Log("BeamSequence initiated");
            beamButton.SetActive(false);
            
            BeamActivated();
        }
    }

    public void BeamActivated()
    
    {
        backgroundSong = uiman.GetComponent<AudioSource>();
        oldVolume = backgroundSong.volume;
        backgroundSong.volume = 0f ;
        objectHit = GetComponent<AudioSource>();
        imageClouds.SetActive(true);
        generalLight.SetActive(false);
        thunderSound.SetActive(true);

        radius = beam.GetComponent<Renderer>().bounds.size.y * beam.transform.localScale.y;
        spriteMask.transform.localScale = new Vector3(1f, spriteMask.transform.localScale.y, spriteMask.transform.localScale.z);

        initialDimensions = spriteMask.GetComponent<Renderer>().bounds.size.x;
        StartCoroutine(BeamCoroutine());

    }
    IEnumerator BeamCoroutine()
    {
        float maxLaserSize = 100f;
        float currentLaserSize = maxLaserSize;
        yield return new WaitForSeconds(1f);

        beam.SetActive(true);
        timeCounter = timeToCount;

        while(timeCounter >= 0)
        {
            // Raycast at the right as our sprite has been design for that
            if (player.transform.localScale.x > 0) laserDirection = beam.transform.right;
            else laserDirection = - beam.transform.right ;
            //RaycastHit2D hit = Physics2D.Raycast(beam.transform.position, laserDirection, maxLaserSize);

            RaycastHit2D hit = Physics2D.CircleCast(beam.transform.position,radius,  laserDirection, maxLaserSize, hittableObjectMask);
            currentLaserSize = 20f;

            if (hit.collider != null)
            {

                currentLaserSize = Vector2.Distance(hit.point, beam.transform.position);

                // -- Create the end sprite
                if (laserEnd.activeSelf == false)
                {
                    laserEnd.SetActive(true);
                    //laserEnd.transform.parent = this.transform;
                    //laserEnd.transform.localPosition = Vector2.zero;
                }
            }
            else
            {

                if (laserEnd.activeSelf == true) laserEnd.SetActive(false);
            }


            spriteMask.transform.localScale = new Vector3(currentLaserSize / initialDimensions, spriteMask.transform.localScale.y, spriteMask.transform.localScale.z);

            if (laserEnd.activeSelf == true)
            {
                //laserEnd.transform.localPosition = new Vector2(currentLaserSize, 0f);
                laserEnd.transform.position = new Vector2(hit.point.x, beam.transform.position.y);
            }

            if (hit.collider != null)
            {
                //Destroy(hit.transform.gameObject, 2f);
                if(hit.transform.tag == "axe") hit.transform.gameObject.GetComponent<axeBehavior>().subtractLife();
                else hit.transform.gameObject.GetComponent<EnemyBehavior>().subtractLife();
                if (objectHit.isPlaying == false) objectHit.Play();
            }
            timeCounter -= Time.deltaTime;

            yield return null;
        }

        beam.SetActive(false);
        thunderSound.SetActive(false);
        imageClouds.SetActive(false);
        generalLight.SetActive(true);
        backgroundSong.volume = oldVolume;

    }
}