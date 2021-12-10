using UnityEngine;

public class Vase : MonoBehaviour
{
    [SerializeField] private GameObject vaseModel;
    [SerializeField] private GameObject brokenVaseModel;
    [SerializeField] private GameObject keyCollectible;

    private Material _vaseMaterial;
    private float _shaderProgress = 1;
    private bool _isAppearing;
    private readonly float smashForce = 9f;
    private bool _alreadyAppeared;
    private static readonly int Progress = Shader.PropertyToID("_Progress");

    private void Update()
    {
        if (_isAppearing)
        {
            _shaderProgress = Mathf.Clamp01(_shaderProgress - Time.deltaTime * 0.5f);
            if (_shaderProgress <= 0)
            {
                _isAppearing = false;
            }
            _vaseMaterial.SetFloat(Progress,_shaderProgress);
        }
    }

    private void OnEnable()
    {
        if (!_alreadyAppeared)
        {
            _vaseMaterial = vaseModel.GetComponentInChildren<Renderer>().material;
            _shaderProgress = 1;
            _vaseMaterial.SetFloat(Progress,_shaderProgress);
            _isAppearing = true;
            _alreadyAppeared = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) return;
        if (other.relativeVelocity.x > smashForce || other.relativeVelocity.y > smashForce || other.relativeVelocity.z > smashForce)
        {
            SmashVase();
        }
    }

    private void SmashVase()
    {
        GetComponent<AudioSource>().Play();
        vaseModel.SetActive(false);
        GetComponent<Collider>().enabled = false;
        brokenVaseModel.SetActive(true);
        keyCollectible.SetActive(true);
        Invoke(nameof(RemoveVelocity),0.2f);
    }

    private void RemoveVelocity()
    {
        foreach (Rigidbody rb in GetComponents<Rigidbody>())
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }
    }
    
}
