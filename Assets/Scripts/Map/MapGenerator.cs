using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MapGenerator
{
    /// <summary>
    /// This class is for
    /// 1.Main class of out generator
    /// 2.call all helper methods to create rooms and corridors
    /// 3.divide spaces
    /// </summary>
    public static List<RoomNode> allNodesCollection = new List<RoomNode>();
    private int mapWidth;
    private int mapLength;

    public MapGenerator(int dungeonWidth, int dungeonLength)
    {
        this.mapWidth = dungeonWidth;
        this.mapLength = dungeonLength;
    }

    public List<BSPNode> CalculateMap(int maxIterations, int roomWidthMin, int roomLengthMin, float roomBottomCornerModifier, float roomTopCornerMidifier, int roomOffset, int corridorWidth)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(mapWidth, mapLength);
        allNodesCollection = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        List<BSPNode> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(bsp.RootNode);

        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, roomLengthMin, roomWidthMin);
        List<RoomNode> roomList = roomGenerator.GenerateRoomsInGivenSpaces(roomSpaces, roomBottomCornerModifier, roomTopCornerMidifier, roomOffset);

        CorridorsGenerator corridorGenerator = new CorridorsGenerator();
        List<BSPNode> corridorList = corridorGenerator.CreateCorridor(allNodesCollection, corridorWidth);
        
        return new List<BSPNode>(roomList).Concat(corridorList).ToList();
    }
}