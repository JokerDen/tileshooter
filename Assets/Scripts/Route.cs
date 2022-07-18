using UnityEngine;

public class Route : MonoBehaviour
{
    public RoutePoint[] path;
    
    public int GetNextIndex(int routeIndex)
    {
        routeIndex++;
        if (routeIndex >= path.Length)
            routeIndex = 0;
        return routeIndex;
    }

    public RoutePoint GetPath(int routeIndex)
    {
        if (routeIndex >= 0 && routeIndex < path.Length)
            return path[routeIndex];
        return null;
    }
}
