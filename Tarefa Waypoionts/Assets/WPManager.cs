using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// estrutura basica de coordernada e direçao apontada para o tank
public struct Link
{
    public enum direction { UNI,BI}
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class WPManager : MonoBehaviour
{
    // instanciando todas as parametrizaçoes de variaveis
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    void Start()
    {
        //definindo que precisa existir pelo menos 1 waypoint
        if (waypoints.Length>0)
        {
            //processo de repetição para entrada dos waypoints no node
            foreach(GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }
            //processo de repetiçao para todos pontos que formam a rota nos nodes1 e node2 do link 
            foreach(Link l in links)
            {
                graph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.direction.BI)
                    graph.AddEdge(l.node2, l.node1);
            }

        }
        
    }

    
    void Update()
    {
        //linhas mapeadas da rota em modo de Debug
        graph.debugDraw();
    }
}
