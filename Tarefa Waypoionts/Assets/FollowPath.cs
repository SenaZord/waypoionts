using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPath : MonoBehaviour
{
    //Variaveis Globais do Tank

    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    void Start()
    {
        //wps variavel de gerenciamento dos pontos de locomoçao do tank
        wps = wpManager.GetComponent<WPManager>().waypoints;
        //g a variavel de valores de posiçoes(x, y, z) no mapa
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];

        
    }


    void Update()
    {
        
    }

    void LateUpdate()
    {
        
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;

        currentNode = g.getPathPoint(currentWP);
        //condição de seguimento dos waypoints
        if(Vector3.Distance(
            g.getPathPoint(currentWP).transform.position,
            transform.position) < accuracy)
        {
            currentWP++;
        }
        //condição da direção que o tank esta olhando e giro do seu eixo frontal
        if(currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        }
        //adicinando a velocidade para o tank 
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }

    //funçao de botao para movimentar o tank para a Heliporto no wp 1
    public void GoToHeli()
    {
        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    //funçao de botao para movimentar o tank para a ruina no wp 6
    public void GoToRuin()
    {
        g.AStar(currentNode, wps[6]);
        currentWP = 0;
    }
    //função de botão para movimentar o tank para a fabrica no wp 10
    public void GoToFactory()
    {
        g.AStar(currentNode, wps[10]);
        currentWP = 0;
    }
}
