using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode 
{
	private string name;
	private int index; //index is used to map this Node's name with index of Adjacency Matrix' cell#
	private List<GraphNode> neighbors = new List<GraphNode>();
	private bool isVisited =false;
	private GraphNode parent;

	public Vector2Int BottomLeftAreaCorner { get; set; }
	public Vector2Int BottomRightAreaCorner { get; set; }
	public Vector2Int TopRightAreaCorner { get; set; }
	public Vector2Int TopLeftAreaCorner { get; set; }


	public GraphNode(string name, int index)
	{
		this.name = name;
		this.index = index;
	}

	public string GetName()
	{
		return name;
	}

	public void SetName(string name)
	{
		this.name = name;
	}

	public int GetIndex()
	{
		return index;
	}

	public void SetIndex(int index)
	{
		this.index = index;
	}

	public List<GraphNode> GetNeighbors()
	{
		return neighbors;
	}

	public void SetNeighbors(List<GraphNode> neighbors)
	{
		this.neighbors = neighbors;
	}

	public bool IsVisited()
	{
		return isVisited;
	}

	public void SetVisited(bool isVisited)
	{
		this.isVisited = isVisited;
	}

	public GraphNode GetParent()
	{
		return parent;
	}

	public void SetParent(GraphNode parent)
	{
		this.parent = parent;
	}

	public GraphNode(string name)
	{
		this.name = name;
	}

	public GraphNode(GraphNode parentNode)
	{
		neighbors = new List<GraphNode>();
		this.parent = parentNode;
		if (parentNode != null)
		{
			parent.AddChild(this);
		}
	}

	public void AddChild(GraphNode node)
	{
		neighbors.Add(node);

	}

	public void RemoveChild(GraphNode node)
	{
		neighbors.Remove(node);
	}

}
