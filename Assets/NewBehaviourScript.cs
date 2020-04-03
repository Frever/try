using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;
    public Transform leftbounds;
    public Transform rightbounds;

    public float smoothDampTime = 0.15f;
    private Vector3 smoothDampVelocity = Vector3.zero;

    private float camwidth, camheight, levelminX, levelmaxX;
    // Start is called before the first frame update
    void Start()
    {
        camheight = Camera.main.orthographicSize * 2;
        camwidth = camheight * Camera.main.aspect;

        float leftboundsWidth = leftbounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;
        float rightboundsWidth = rightbounds.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 2;

        levelminX = leftbounds.position.x + leftboundsWidth + (camwidth / 2);
        levelmaxX = rightbounds.position.x + rightboundsWidth + (camwidth / 2);

    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            float targetX = Mathf.Max(levelminX, Mathf.Min(levelmaxX, target.position.x));

            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);

            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}