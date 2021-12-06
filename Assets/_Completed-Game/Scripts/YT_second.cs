using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class YT_second : MonoBehaviour
{
    public GameObject Targetforsecond;
    public GameObject[] Positions;
    public GameObject Prefab;

    private Dictionary<GameObject, GameObject> Configure = new Dictionary<GameObject, GameObject>();
    public NavMeshAgent agent;
    
    [HideInInspector]
    //public Factory _factory;
    public bool _reachedTargetforsecond = false;


    // Start is called before the first frame update

    private void Start()
    {
        Targetforsecond = GameObject.FindWithTag("Targetforsecond");
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(Targetforsecond.transform.position);
        foreach (GameObject pos in Positions)
        {
            Configure.Add(pos, null);
        }
        GameObject going = Prefab;
    }
    // Update is called once per frame


    public void StartSecondConfigure(GameObject going)
    {
        Debug.Log("work");
        Debug.Log("Start secondConfiguration");
        //make sure there is a spot to configure the agent
        List<GameObject> keys = Configure.Keys.ToList();
        foreach (GameObject key in keys)
        {
            //guard statement
            if (Configure[key] != null) { continue; }
            Configure[key] = going;

            //disable agent, this agent is now the leader
            NavMeshAgent agent = going.GetComponentInParent<NavMeshAgent>();
            CollisionDetection detect = going.GetComponent<CollisionDetection>();
            YT_second yT_Second = going.GetComponentInParent<YT_second>();
            agent.enabled = false;
            detect.enabled = false;
            yT_Second.enabled = false;
            break;
        }
    }
    private void Update()
    {
        if (_reachedTargetforsecond)
        {
            //Debug.Log("Waiting");

            //put object into place
            bool allConfigured = true;
            foreach (KeyValuePair<GameObject, GameObject> kvp in Configure)
            {
                //guard statement
                if (kvp.Value == null) { allConfigured = false; continue; }
                if (kvp.Key.transform.position == kvp.Value.transform.position) { continue; }

                //move object into position
                GameObject cAgent = kvp.Value;
                Vector3 pos = kvp.Key.transform.position;
                Quaternion rot = kvp.Key.transform.rotation;

                cAgent.transform.position = Vector3.Lerp(cAgent.transform.position, pos, Time.deltaTime);
                cAgent.transform.rotation = Quaternion.Lerp(cAgent.transform.rotation, rot, Time.deltaTime);
                if (Vector3.Distance(cAgent.transform.position, pos) < 0.05f)
                {
                    //Debug.Log("Configured");
                    cAgent.transform.position = pos;
                    cAgent.transform.rotation = rot;
                }

                allConfigured = false;
            }

            if (allConfigured)
            {
                //destroy all agents
                List<GameObject> keys = Configure.Keys.ToList();
                foreach (GameObject key in keys)
                {
                    GameObject going = Configure[key];
                    Destroy(going);

                }
                

                //instantiate factory
                GameObject prefab = Instantiate(Prefab, transform.position, transform.rotation, null);
                float moveY = prefab.transform.localScale.y / 2;
                prefab.transform.position += new Vector3(0, 0, 0);
                prefab.transform.position += new Vector3(0, moveY, 0);

                Debug.Log("All Configuredsecond");
                //Destroy(gameObject); //destroy this last, because it will destroy this script
            }
            return;
        }
        //if (!agent.pathPending)
        //{
            //if (agent.remainingDistance <= agent.stoppingDistance)
            {
                //if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    //Debug.Log(gameObject.name + " has reached Target");
                    //_reachedTargetforsecond = true;
                    //agent.enabled = false;
                }
            }
        }


    }

