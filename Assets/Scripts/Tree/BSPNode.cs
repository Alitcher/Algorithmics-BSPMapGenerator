using System;
using System.Collections.Generic;
using UnityEngine;

public class BSPNode
{
    private List<BSPNode> childrenNodeList;

    public List<BSPNode> ChildrenNodeList { get => childrenNodeList;}

    public bool Visted { get; set; }
    public Vector2Int BottomLeftAreaCorner { get; set; }
    public Vector2Int BottomRightAreaCorner { get; set; }
    public Vector2Int TopRightAreaCorner { get; set; }
    public Vector2Int TopLeftAreaCorner { get; set; }

    public BSPNode Parent { get; set; }


    public int TreeLayerIndex { get; set; }

    public BSPNode(BSPNode parentNode)
    {
        childrenNodeList = new List<BSPNode>();
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

    public void RemoveChild(BSPNode node)
    {
        childrenNodeList.Remove(node);
    }
}