using System;
using System.Collections.Generic;
using UnityEngine;

public class BSPNode
{
    public int ID { get; set; }
    public string name { get; set; }

    private List<BSPNode> childrenNodeList, neighborList;

    public List<BSPNode> ChildrenNodeList { get => childrenNodeList; }

    public List<BSPNode> Neighbors { get => neighborList; }

    public bool Visted { get; set; }

    /// <summary>
    /// store each corner of our mesh, which will be in rectangle
    /// </summary>
    public Vector2Int BottomLeftAreaCorner { get; set; }
    public Vector2Int BottomRightAreaCorner { get; set; }
    public Vector2Int TopRightAreaCorner { get; set; }
    public Vector2Int TopLeftAreaCorner { get; set; }

    public BSPNode Parent { get; set; }


    public int TreeLayerIndex { get; set; }

    public BSPNode(BSPNode parentNode)
    {
        childrenNodeList = new List<BSPNode>();
        neighborList = new List<BSPNode>();
        this.Parent = parentNode;
        if (parentNode != null)
        {
            parentNode.AddChild(this);
        }
    }

    public void AddChild(BSPNode node)
    {
        childrenNodeList.Add(node);

    }

    public void AddNeighbors(BSPNode node)
    {
        neighborList.Add(node);
    }

    public void RemoveChild(BSPNode node)
    {
        childrenNodeList.Remove(node);
    }
}