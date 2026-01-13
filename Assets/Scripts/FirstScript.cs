using UnityEngine;

public class FirstScript : MonoBehaviour
{
    [SerializeField] int i = 0;
    [SerializeField] string userName = "dsadsad";
    void Awake()
    {
        Debug.Log("FirstScript has awoken");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("FirstScript has started");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"FirstScript has updated: {i} hi {userName}");
        //i++;
    }
}
