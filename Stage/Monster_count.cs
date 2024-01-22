using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_count : MonoBehaviour
{
    public Stage stage;
    
    // Start is called before the first frame update
    void Start()
    {
        stage=transform.parent.GetComponent<Stage>();  
    }

    public void monster_count(){
        stage.monster_count-=1;
    }
}
