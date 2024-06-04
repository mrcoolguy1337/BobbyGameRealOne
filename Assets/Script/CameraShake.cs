using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    [SerializeField]
    public Vector2 freq;
    [SerializeField]
    public Vector2 amp;

    Vector2 time;

    [SerializeField]
    public bool ShouldShake;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        time += freq * Time.deltaTime;
        Vector3 ShakePos = transform.localPosition;

        if (ShouldShake)
        {
            ShakePos.x = Mathf.Sin(time.x) * amp.x;
            ShakePos.y = Mathf.Sin(time.y) * amp.y;
        }
        else
        {
            ShakePos = Vector3.zero;
        }

        transform.localPosition = ShakePos;

    }

    public IEnumerator ShakeOn()
    {
        ShouldShake = true;
        yield return new WaitForSeconds(0.05f);
        ShouldShake = false;

    }

    public void ShakeTrue()
    {
        StartCoroutine(ShakeOn());
    }
}
