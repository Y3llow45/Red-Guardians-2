using UnityEngine;

public class SharpTriangle : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "SoldierClone")
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            gameController.GameOver();
        }
    }
}