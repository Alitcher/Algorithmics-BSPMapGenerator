using System;
using System.Collections.Generic;
using System.Linq;

public class CorridorsGenerator
{
    public List<BSPNode> CreateCorridor(List<RoomNode> allNodesCollection, int corridorWidth)
    {
        List<BSPNode> corridorList = new List<BSPNode>();
        Queue<RoomNode> structuresToCheck = new Queue<RoomNode>(
            allNodesCollection.OrderByDescending(node => node.TreeLayerIndex).ToList());
        while (structuresToCheck.Count > 0)
        {
            var node = structuresToCheck.Dequeue();
            if (node.ChildrenNodeList.Count == 0)
            {
                continue;
            }

            node.ChildrenNodeList[0].AddNeighbors(node.ChildrenNodeList[1]);
            node.ChildrenNodeList[1].AddNeighbors(node.ChildrenNodeList[0]);

            CorridorNode corridor = new CorridorNode(node.ChildrenNodeList[0], node.ChildrenNodeList[1], corridorWidth);

            corridorList.Add(corridor);
        }
        return corridorList;
    }
}