using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Monstercraft : MonoBehaviour
{
    private MapManager mapManager;
    private Stage stage;
    private GameObject mapprefab;
    public GameObject monster;
    public GameObject targetObj; // 이동시킬 객체
    public GameObject toObj; // 이동시킬 좌표
    public GameManager gameManager;
    public int Stage_nb = 0;
    // Start is called before the first frame update

    void Start()
    {
        gameManager = GameManager.instance.GetComponent<GameManager>();
        targetObj = gameManager.Player;
        mapManager = MapManager.instance.GetComponent<MapManager>();
        Stage_nb = DateManager.instance.nowPlayer.Stage_number;
        mapprefab = mapManager.PrefabObject[Stage_nb];
        stage = mapManager.PrefabObject[Stage_nb].GetComponent<Stage>();
        toObj = stage.Setstart();

        if (Slotdata.instance.savefiles[Slotdata.instance.numbers])// 슬롯데이터에 데이터가 있을때 실행
        {
            targetObj.transform.position = toObj.transform.position;
            if (!DateManager.instance.nowPlayer.monster_clear)
            {
                if (stage.stage_Number != Stage.Stage_number.Box && stage.stage_Number != Stage.Stage_number.Store && stage.stage_Number != Stage.Stage_number.Stage0)
                {
                    monster=mapprefab.transform.GetChild(3).gameObject;
                    monster.SetActive(true);
                //stage.monster.SetActive(true);
                }
            }
        }
    }
}
