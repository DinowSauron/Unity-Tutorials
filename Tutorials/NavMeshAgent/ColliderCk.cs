using UnityEngine;

public class ColliderCk : MonoBehaviour {
    public ArtificialInteligence Script;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Script.Seguir = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Script.Seguir = false;
        }
    }
}
