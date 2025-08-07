using UnityEngine;
using System.Collections.Generic;
public class MarkerMaker : MonoBehaviour
{
     public static MarkerMaker Instance;
     public List<GameObject> markers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (Instance == null) Instance = this;
    }

    public static GameObject Marker(int index)
    {
         if (!Instance) return null;
         if (index > Instance.markers.Count) return null;
         if (index < 0) return null;

          if (Instance.markers[index]) return Instantiate(Instance.markers[index], Instance.transform);
          return null;
    }
}
