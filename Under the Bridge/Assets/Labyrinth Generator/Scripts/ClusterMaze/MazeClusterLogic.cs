using System.Collections.Generic;
using UnityEngine;

public static class MazeClusterLogic
{
    public static void WriteClusters(float gapFrequency, out List<List<List<MazeSquare[]>>> gridClusters, MazeSquare[,,] squares)
    {
        gridClusters = new List<List<List<MazeSquare[]>>>();
        int length = squares.GetLength(0);
        int width = squares.GetLength(1);
        int height = squares.GetLength(2);

        for (int z = 0; z < height; z++) {

            gridClusters.Add(new List<List<MazeSquare[]>>());

            for (int i = 0; i < length; i++) {
                List<MazeSquare[]> rowClusters = new List<MazeSquare[]>();
                int clusterSize = 0;

                for (int j = 0; j < width; j++) {
                    MazeSquare[] cluster;

                    //Only 1 cluster for the origin
                    if (i == 0 && j == 0)
                    {
                        squares[i, j, z] = new MazeSquare(i, j, z);
                        clusterSize++;
                        continue;
                    }

                    //Store clusters
                    float rand = Random.Range(0, 1f);
                    if (rand < gapFrequency)
                    {
                        if (clusterSize > 0)
                        {
                            //Cluster excluding current (null) square
                            cluster = SetCluster(clusterSize, i, j, z, squares);
                            rowClusters.Add(cluster);
                            clusterSize = 0;
                        }

                        if (i == 0)
                            break;
                        continue;
                    }

                    squares[i, j, z] = new MazeSquare(i, j, z);

                    clusterSize++;
                    if (j == width - 1)
                    {
                        //Cluster including current (!null) square
                        cluster = SetCluster(clusterSize, i, j + 1, z, squares);
                        rowClusters.Add(cluster);
                    }
                }

                gridClusters[z].Add(rowClusters);
            }
        }

        LinkByClusters(gridClusters, squares);
    }

    static MazeSquare[] SetCluster(int size, int col, int row, int layer, MazeSquare[,,] grid)
    {
        MazeSquare[] cluster = new MazeSquare[size];

        for (int i = 0; i < size; i++)
            cluster[i] = grid[col, row - (size - i), layer];

        return cluster;
    }

    static void LinkByClusters(List<List<List<MazeSquare[]>>> gridClusters, MazeSquare[,,] squares)
    {
        int length = squares.GetLength(0);
        int height = squares.GetLength(2);

        for (int z = 0; z < height; z++) {
            for (int i = 1; i < length; i++) {
                //Iterate through clusters
                for (int j = 0; j < gridClusters[z][i].Count; j++) {
                    int connections = 0;

                    for (int k = 0; k < gridClusters[z][i][j].Length; k++) {
                        MazeSquare current = gridClusters[z][i][j][k];

                        if (squares[i - 1, current.yCoord, current.zCoord] != null)
                            connections++;
                    }

                    if (connections == 0) {
                        //Scout out to the first row to find where this cluster can connect
                        bool hit = false;

                        for (int k = 0; k < gridClusters[z][i][j].Length; k++) {
                            MazeSquare current = gridClusters[z][i][j][k];

                            for (int l = i - 1; l >= 0; l--) {
                                if (squares[l, current.yCoord, current.zCoord] != null) {
                                    hit = true;
                                    for (int m = i - 1; m > l; m--)
                                        squares[m, current.yCoord, current.zCoord] = new MazeSquare(m, current.yCoord, current.zCoord);

                                    break;
                                }
                            }

                            if (hit == true)
                                break;
                        }

                        //If there's nowhere it can connect, draw a line down, then to the first row, then to the origin
                        if (hit == false) {
                            MazeSquare sq = gridClusters[z][i][j][0];

                            for (int k = i - 1; k >= 0; k--)
                                squares[k, sq.yCoord, sq.zCoord] = new MazeSquare(k, sq.yCoord, sq.zCoord);

                            for (int k = sq.yCoord; k >= gridClusters[0][0][0].Length; k--)
                                if (squares[0, k, 0] == null)
                                    squares[0, k, 0] = new MazeSquare(0, k, 0);
                        }
                    }
                }
            }
        }
    }
}