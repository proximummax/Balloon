using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _value;
    private float _time = 0;
    public float CurrentTime
    {
        get => _time;
    }


    private void Start()
    {
        _value.text = "0.00";
    }

    void Update()
    {
        _time += Time.deltaTime;
        _value.text = _time.ToString("0.00");
    }
}
