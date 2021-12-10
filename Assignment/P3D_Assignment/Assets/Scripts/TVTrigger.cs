using UnityEngine;

public class TVTrigger : MonoBehaviour
{
    [SerializeField] private GameObject screen;
   
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        screen.SetActive(true);
    }
}
