using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [SerializeField] private float PanSpeed = 0.2f;
    [SerializeField] private float XBorder = 5;
    [SerializeField] private float ZBorder = 25;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PanMove());
    }

    private IEnumerator PanMove()
    {
        yield return null;

        while (this.enabled)
        {
            if (Input.GetKey(KeyCode.W) && transform.position.z < ZBorder)
            {
                transform.position += new Vector3(0, 0, PanSpeed);
            }
            else if(Input.GetKey(KeyCode.S) && transform.position.z > -ZBorder)
            {
                transform.position -= new Vector3(0, 0, PanSpeed);
            }

            if (Input.GetKey(KeyCode.A) && transform.position.x > -XBorder)
            {
                transform.position -= new Vector3(PanSpeed, 0, 0);
            }
            else if (Input.GetKey(KeyCode.D) && transform.position.x < XBorder)
            {
                transform.position += new Vector3(PanSpeed, 0, 0);
            }
            yield return null;
        }
    }
}
