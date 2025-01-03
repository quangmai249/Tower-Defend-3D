using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] float speed;
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.right * this.speed * Time.deltaTime);
    }
}
