using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingBFS : MonoBehaviour
{
	//BFS Traversal by a source node
	public static void BFSForSSSP(RoomNode node)
	{
		Queue<RoomNode> queue = new Queue<RoomNode>();
		queue.Enqueue(node);  //add source node to queue
		while (queue.Count>0 )
		{
			RoomNode presentNode =  queue.Dequeue();
			presentNode.Visted = true;
			print($"Printing path for node {presentNode.name}:{presentNode.ID}" );
			foreach (RoomNode neighbor in presentNode.Neighbors)
			{  //for each neighbor of present node
				if (!neighbor.Visted)
				{
					queue.Enqueue(neighbor);
					neighbor.Visted  = (true);
					neighbor.SetParent(presentNode);
				}//end of if
			}//end of for loop
		}//end of while loop
	}//end of method
}
