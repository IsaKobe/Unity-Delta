using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class newInput : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject cubePref;
    InputAction move;
    InputAction fire;

    Vector3 newMove;
    bool jump = false;

    [SerializeField] float speed = 1;
    internal bool canJump;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        move = inputActions.FindAction("Move");
        fire = inputActions.FindAction("Fire");
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Spawn");
        }
    }

    private void OnEnable()
    {
        move.Enable();
        fire.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        newMove = move.ReadValue<Vector2>();
        newMove.z = newMove.y;
        newMove.y = 0;
        if(canJump && fire.triggered)
            jump = true;
        /*
        //transform.localPosition += newMove * Time.deltaTime;
        transform.Translate(newMove * Time.deltaTime * speed);

        Debug.Log($"Move: {newMove * Time.deltaTime}");

        if(fire.triggered)
        {
            Debug.Log("Fire action triggered");
        }*/
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + newMove * speed);
        if(jump)
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            jump = false;


            //Instantite(prefab, position, rotation, parent)
            GameObject cube = Instantiate(cubePref, transform.position, transform.rotation, transform); 
            /*GameObject game = new GameObject("copy");
            game.transform.position = transform.position;
            Rigidbody rbCopy = game.AddComponent<Rigidbody>();
            game.AddComponent<MeshRenderer>();
            MeshFilter filter = game.AddComponent<MeshFilter>();
            filter.mesh = GetComponent<MeshFilter>().mesh;

            game.SetActive(false);
            newInput input = game.AddComponent<newInput>();
            input.inputActions = inputActions;
            game.SetActive(true);*/
        }
    }

}
