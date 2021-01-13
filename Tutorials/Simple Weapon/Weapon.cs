using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePosition;
    public float fireRate, range;
    public enum Mode {  Semi, Auto, Brust };
    public Mode state;
    public int ammo = 30;
    public int magazine = 3;

    [Space(35)]
    public LayerMask Layers;
    public GameObject damageLog;
    public GameObject sparksLog;
    public int maxAmmoCapacity = 30;

    private LineRenderer line;
    private int brustCount;
    private float rate;
    private bool Loading;


    void Start()
    {
        if (gameObject.GetComponent<LineRenderer>())
        {
            line = gameObject.GetComponent<LineRenderer>();
        }
        else
        {
            line = gameObject.AddComponent<LineRenderer>();
        }
    }

    void Update()
    {
        if (rate < fireRate)
            rate += 1 * Time.deltaTime;
        else
            Loading = false;

        FadeOut();


        if (ammo > 0 && rate >= fireRate && !Loading)
        {
            //Modos de disparos

            if (state == Mode.Semi)
            {
                if (Input.GetMouseButtonDown(0))
                    Fire();
            }
            if(state == Mode.Auto)
            {
                if (Input.GetMouseButton(0))
                    Fire();
            }
            if (state == Mode.Brust)
            {
                if (brustCount != 0)
                {
                    brustCount -= 1;
                    Fire();
                }

                if (Input.GetMouseButtonDown(0) && brustCount == 0)
                {
                    Fire();
                    brustCount = 2;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !Loading && magazine > 0)
        {
            rate = -2;
            Loading = true;

            ammo = maxAmmoCapacity;
            magazine -= 1;
        }
    }

    void Fire()
    {
        rate = 0;
        ammo -= 1;
        line.SetPosition(0, FirePosition.position);

        sparksLog.GetComponent<ParticleSystem>().Play();

        if (gameObject.GetComponentInParent<SmoothCam>())
        { //smooth system
            gameObject.GetComponentInParent<SmoothCam>().fired();
        }

        RaycastHit hit;
        if(Physics.Raycast(FirePosition.position, FirePosition.forward, out hit, range, Layers, QueryTriggerInteraction.Ignore))
        {
            line.SetPosition(1, hit.point); //Rastro
                                 
            Damage(hit.transform.gameObject); //Informa que um dano foi sofrido

            if (!hit.transform.gameObject.GetComponent<Rigidbody>())
            {
                //Queima do disparo nas paredes
                GameObject dano = Instantiate(damageLog, hit.point, Quaternion.Euler(0,0,0));
                dano.transform.LookAt(FirePosition.position);
                Destroy(dano, 10);
            }
            else
            {
                //Força aplicada nos rigidbory
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-(FirePosition.position - hit.point).normalized * 5,hit.point, ForceMode.Impulse);
            }
        }
        else
        {
            //Rastro caso o tiro não pegue em nenhum objeto
            line.SetPosition(1, FirePosition.position + (FirePosition.forward * 100));
        }
        line.material.color += new Color(0, 0, 0, (1 - line.material.color.a));

    }

    void FadeOut()
    {
        // Rastro Do Disparo
        line.material.color -= new Color(0, 0, 0, 0.03f);
        line.SetPosition(0, Vector3.Lerp(line.GetPosition(0), line.GetPosition(1), 30f * Time.deltaTime / Vector3.Distance(line.GetPosition(0), line.GetPosition(1))));
    }

    void Damage(GameObject Alvo)
    {
        if(Alvo.tag == "TagDoSeuInimigo")
        {
            //Retira a vida do seu inimgo(ou qualquer entidade), sendo ele o objeto "Alvo"
        }

    }
}
