using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager instance = null;

    public KeyCode[] inputs = new KeyCode[20];



    private void Awake() {
        if (null == instance) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
        
        inputs[0] = KeyCode.D; // 오른쪽
        inputs[1] = KeyCode.A; // 왼쪽이동
        inputs[2] = KeyCode.Space; //점프
        inputs[3] = KeyCode.LeftShift; // 대쉬
        inputs[4] = KeyCode.E; //상호작용
        inputs[5] = KeyCode.W; //포탈타기
        inputs[6] = KeyCode.I; //인벤토리
        inputs[7] = KeyCode.R; // 개임 재시작
        inputs[8] = KeyCode.Escape; // esc 입력
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
