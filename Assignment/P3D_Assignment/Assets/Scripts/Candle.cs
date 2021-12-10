using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] private GameObject keyObjectToAppear;
    
    private readonly Vector3 _targetScale = new Vector3(0.75f,0.75f,0.1f);
    private bool _burningDown;

    private void Update()
    {
        if (_burningDown)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.deltaTime);
            if (transform.localScale.z <= 0.15f)
            {
                keyObjectToAppear.SetActive(true);
                _burningDown = false;
            }
        }
    }

    public void StartBurning()
    {
        _burningDown = true;
    }
}
