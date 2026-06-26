using UnityEngine;

public class Movimentação : MonoBehaviour
{
    [Header("Movimento")]

    [SerializeField]
    private float speed = 8f;

    [Header("Limites")]

    [SerializeField]
    private float leftLimit = -8f;

    [SerializeField]
    private float rightLimit = 8f;

    private void Update()
    {
        // Lê as teclas A/D ou Setas
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Cria o movimento
        Vector3 movement =
            Vector3.right *
            horizontal *
            speed *
            Time.deltaTime;

        // Move o barril
        transform.position += movement;

        // Limita a posição
        transform.position = new Vector3(
            Mathf.Clamp(
                transform.position.x,
                leftLimit,
                rightLimit),
            transform.position.y,
            transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Frutas"))
{
    Debug.Log("Pegou frutas");

    GameManager.Instance.AddScore(5);

    Destroy(other.gameObject);
}

if(other.CompareTag("Bomb"))
{
    Debug.Log("Pegou Bomb");

    Destroy(other.gameObject);

    GameManager.Instance.GameOver();
}
    }
}