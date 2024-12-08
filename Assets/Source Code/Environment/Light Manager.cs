using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.up * this.speed * Time.deltaTime);
    }
}
