using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
   
    public Player_Move Move;
    public GameObject targetObj; // 이동시킬 객체
    public GameObject toObj; // 이동시킬 좌표
    private Stage stage; // 외부 Stage 스크립트에 연결할 변수
    private FadeEffect fadeEffect;
    private MapManager mapManager; // 외부  mapManager 스크립트에 연결할 변수
    public GameManager gameManager;// 외부 gameManager 스크립트에 연결할 변수
//   private FadeEffect fadeEffect;
    public static int Stage_number = 0;
    
//
    public void Start()
    {
        Move = GameManager.instance.Player.GetComponent<Player_Move>();
        gameManager = GameManager.instance.GetComponent<GameManager>();
        mapManager = MapManager.instance.GetComponent<MapManager>();
        fadeEffect = FadeEffect.instance.GetComponent<FadeEffect>();
        targetObj = gameManager.Player;
        
        if(Slotdata.instance.savefiles[Slotdata.instance.numbers])// 슬롯데이터에 데이터가 있을때 실행
        {
            Stage_number = DateManager.instance.nowPlayer.Stage_number;// 플레이어의 방 시작 번호를 받아옴
            stage = mapManager.PrefabObject[Stage_number].GetComponent<Stage>();// 플레이어의 방 시작 프리팹의 스크립트를 연결
            /*toObj = stage.Setstart(); // 프리팹의 시작 위치받음
            if (!DateManager.instance.nowPlayer.monster_clear)//방을 클리어 못했을때 시작지점으로 플레이어가 이동하는걸 막기 위해 사용
            {
                targetObj.transform.position = toObj.transform.position;// 시작위치로 이동
            }*/
        }else{
            stage = mapManager.PrefabObject[Stage_number].GetComponent<Stage>();
            toObj = stage.Setstart();
            if (stage.monster_count > 0)//방에 몬스터가 있을때
            {
                targetObj.transform.position = toObj.transform.position;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            targetObj=collision.gameObject;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&Move.Ischeck)
        {
            Move.Ischeck=false;
            Stage_number += 1;
            stage = mapManager.PrefabObject[Stage_number].GetComponent<Stage>();
            stage.monster_clear=false;// 방 이동시 몬스터 클리어를 false로 변경
            DateManager.instance.nowPlayer.monster_clear = false;
            stage.teleport.SetActive(false);// 이동한 방의 텔레포트를 비활성화 시킴
            if (stage.stage_Number == Stage.Stage_number.Box || stage.stage_Number == Stage.Stage_number.Store || stage.stage_Number == Stage.Stage_number.Stage0){// 텔레포트할 방이 몬스터가 없는 방이면 포탈 활성화.
                stage.teleport.SetActive(true);// 몬스터가 없는 방이면 텔레포트를 활성화
            }
            toObj = stage.Setstart();
            Invoke("TeleportRoutine",0.1f);
        }
    }
    public void TeleportRoutine()
    {   
        targetObj.transform.position = toObj.transform.position;
        fadeEffect.Fadeset();
        Invoke("Monster_spon",2f);
        DateManager.instance.nowPlayer.Stage_number=Stage_number;
    }
    public void Monster_spon(){
        stage.monster.SetActive(true);// 페이드 효과 끝나면 몬스터 활성화
    }
}
