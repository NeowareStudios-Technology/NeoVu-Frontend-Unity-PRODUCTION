using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShoot : MonoBehaviour
{
    Vector2 touchStart;
    Vector2 touchEnd;
    float flickTime = 5;
    float flickLength = 0;
    float ballVelocity;
    float ballSpeed = 0;
    Vector3 worldAngle;
    public GameObject ballPrefab;
    private bool GetVelocity = false;
   
    public float comfortZone;
    bool couldbeswipe;
    float startCountdownLength = 0.0f;
    bool startTheTimer = false;
    static bool globalGameStart = false;
static bool shootEnable = false;
private float startGameTimer = 0.0f;
 
void Start()
    {
        startTheTimer = true;
        Time.timeScale = 1;
        if (Application.isEditor)
            Time.fixedDeltaTime = 0.01f;
    }

    void Update()
    {
        if (startTheTimer)
        {
            startGameTimer += Time.deltaTime;
        }
        if (startGameTimer > startCountdownLength)
        {
            globalGameStart = true;
            shootEnable = true;
            startTheTimer = false;
            startGameTimer = 0;
        }

        if (shootEnable)
        {
            //Debug.Log("enabled!");
            if (Input.touchCount > 0)
            {
                var touch = Input.touches[0];
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        Debug.Log("Touch Begin");
                        flickTime = 5;
                        timeIncrease();
                        couldbeswipe = true;
                        GetVelocity = true;
                        touchStart = touch.position;
                        break;
                    case TouchPhase.Moved:
                        if (Mathf.Abs(touch.position.y - touchStart.y) < comfortZone)
                        {
                            couldbeswipe = false;
                        }
                        else
                        {
                            couldbeswipe = true;
                        }
                        break;
                    case TouchPhase.Stationary:
                        if (Mathf.Abs(touch.position.y - touchStart.y) < comfortZone)
                        {
                            couldbeswipe = false;
                        }
                        break;
                    case TouchPhase.Ended:
                        var swipeDist = (touch.position - touchStart).magnitude;
                        if ( swipeDist > comfortZone) {
                            GetVelocity = false;
                            touchEnd = touch.position;
                            var ball = Instantiate(ballPrefab, new Vector3(0, 2.6f, -11), Quaternion.identity) as GameObject;
                            GetSpeed();
                            GetAngle();
                            ball.GetComponent<Rigidbody>().AddForce(new Vector3((worldAngle.x * ballSpeed), (worldAngle.y * ballSpeed), (worldAngle.z * ballSpeed)));
                            

                        }
                        break;
                }
                if (GetVelocity)
                {
                    flickTime++;
                }
            }
        }
        if (!shootEnable)
        {
            Debug.Log("shot disabled!");
        }
    }

    void timeIncrease()
    {
        if (GetVelocity)
        {
            flickTime++;
        }
    }

    void GetSpeed()
    {
        flickLength = 90;
        if (flickTime > 0)
        {
            ballVelocity = flickLength / (flickLength - flickTime);
        }
        ballSpeed = ballVelocity * 30;
        ballSpeed = ballSpeed - (ballSpeed * 1.65f);
        if (ballSpeed <= -33)
        {
            ballSpeed = -33;
        }
        Debug.Log("flick was" + flickTime);
        flickTime = 5;
    }

    void GetAngle()
    {
        Camera camera = this.GetComponent<Camera>();
        worldAngle = camera.ScreenToWorldPoint(new Vector3(touchEnd.x, touchEnd.y + 800, ((camera.nearClipPlane - 100) * 1.8f)));
    }
}
