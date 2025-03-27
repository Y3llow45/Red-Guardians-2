using UnityEngine;

public class finish : MonoBehaviour
{
    public GameController gameController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            gameController.Win();
        }   
    }
}