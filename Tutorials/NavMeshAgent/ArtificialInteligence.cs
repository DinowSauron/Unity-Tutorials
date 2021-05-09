using UnityEngine;
using UnityEngine.AI;

public class ArtificialInteligence : MonoBehaviour {

    public NavMeshAgent Agent;
    public bool Seguir;
    public float WaitTime;
    public Transform VisãoLocal;
    public LayerMask Layoso; // coloque como "Everything"
    private Vector3 LastPos;
	
	void Update () {
        if (Seguir == true)
        {
            WaitTime = 0;
        }
        else
        {
            if (WaitTime <= 2)
                WaitTime += 1 * Time.deltaTime;
            if (Agent.destination != LastPos)
                Agent.destination = LastPos;
        }
        
        if (WaitTime <= 2)
        {
            RaycastHit Hit = new RaycastHit();
            VisãoLocal.LookAt(GameObject.FindWithTag("Player").transform.position);
            if (Physics.Raycast(VisãoLocal.position, VisãoLocal.forward, out Hit, 500, Layoso, QueryTriggerInteraction.Ignore))
            {
                Debug.DrawLine(VisãoLocal.position, Hit.point, Color.green);
                if (Hit.transform.gameObject.tag == "Player")
                {
                    Agent.destination = GameObject.FindWithTag("Player").transform.position;
                    LastPos = GameObject.FindWithTag("Player").transform.position;
                }
                else
                {
                    if (Agent.destination != LastPos)
                        Agent.destination = LastPos;
                }
            }
            else
            {
                if (Agent.destination != LastPos)
                    Agent.destination = LastPos;
            }
        }
	}
}
