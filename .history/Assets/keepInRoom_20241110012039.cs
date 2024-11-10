using UnityEngine;

public class keepInRoom : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if (transform.position.y <= -10)
        {
            // Reset the object's position to (0, 2, 0)
            transform.position = new Vector3(0, 2, 0);
        }
    }
}
