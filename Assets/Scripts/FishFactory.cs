using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FishFactory : ScriptableObject
{
    public Fish[] fishes;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Fish Get(int id)
    {
        Fish fish = Instantiate(fishes[id]);
        fish.SetLevel(id);
        return fish;
    }
}
