using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MultiMain : MonoBehaviour
{
    public static int MultiWidth = 5;
    public static int MultiHeight = 5;

    public float EdgeBoundsSize = 0.2f;
    public float boxSize = 1.0f;
    float halfBoxSize;
    public string Gameresult;
    public GameOverScreen GameOverScreen;

    public Color blankColor = Color.white;
    public Color playerColor = Color.yellow;
    public Color enemyColor = Color.black;

    public EdgeObject edgeObjectSrc;
    public BoxObject boxObjectSrc;
    public GameObject playerInputInfo;
    public GameObject replayScreen;
    public Icon_Glow Icon_Glow;
    public Icon_GlowCPU Icon_GlowCPU;

    RuntimeData runtimeData = new RuntimeData();
    Camera mainCam;
    Vector3 tableNormal = Vector3.up;

    [Header("玩家先手：")]
    public bool playerBegin = true;

    /// <summary>
    /// 主循环委托，根据需要从不同的方法间切换，充当状态机
    /// </summary>
    Action mainLoop;

    void Start()
    {
        mainLoop = Init;
        ScoreEnemy.enemyScore = 0;
        Score.playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mainLoop?.Invoke();
    }

    #region 初始化

    /// <summary>
    /// 初始化数据
    /// </summary>
    void InitData()
    {
        halfBoxSize = boxSize * 0.5f;
        mainCam = Camera.main;
    }

    enum EdgeSetType
    {
        H,
        V
    }
    /// <summary>
    /// 初始化全部边
    /// </summary>
    /// <param name="edgeCount"></param>
    void InitEdge(int edgeCount)
    {
        runtimeData.edges = new List<Edge>(edgeCount);
        //设置边
        EdgeSetType edgeSetType = EdgeSetType.H;
        int currentEdgeX = 0;
        float currentEdgeY = -halfBoxSize;
        for (int i = 0; i < edgeCount; i++)
        {
            Edge e = new Edge();
            Vector3 pos = new Vector3(0, 0, currentEdgeY * boxSize);
            Vector3 size = new Vector3(EdgeBoundsSize, EdgeBoundsSize, EdgeBoundsSize);

            //创建显示对象
            e.edgeObject = Instantiate(edgeObjectSrc);           
            e.edgeObject.Init();

            switch (edgeSetType)
            {
                case EdgeSetType.H:
                    pos.x = currentEdgeX * boxSize;
                    size.x = boxSize - EdgeBoundsSize;
                    e.edgeObject.transform.eulerAngles = new Vector3(0, 90, 0);
                    break;
                case EdgeSetType.V:
                    pos.x = currentEdgeX * boxSize - halfBoxSize;
                    size.z = boxSize - EdgeBoundsSize;
                    e.edgeObject.transform.eulerAngles = Vector3.zero;
                    break;
            }
            e.bounds.center = pos;
            e.bounds.size = size;
            e.edgeObject.transform.position = pos;
            e.edgeObject.SetColor(blankColor);

            //当前边+1
            //并且当当前边大于等于预期值的时候重置，当前Y增加半个box长度，同时切换行
            currentEdgeX++;
            switch (edgeSetType)
            {
                case EdgeSetType.H:
                    if (currentEdgeX >= MultiWidth)
                    {
                        currentEdgeX = 0;
                        currentEdgeY += halfBoxSize;
                        edgeSetType = EdgeSetType.V;
                    }
                    break;
                case EdgeSetType.V:
                    if (currentEdgeX >= (MultiWidth + 1))
                    {
                        currentEdgeX = 0;
                        edgeSetType = EdgeSetType.H;
                        currentEdgeY += halfBoxSize;
                    }

                    break;
            }


            runtimeData.edges.Add(e);
        }
    }

    /// <summary>
    /// 初始化格子
    /// </summary>
    /// <param name="boxCount"></param>

    void InitBox(int boxCount)
    {
        runtimeData.boxes = new List<GameBox>(boxCount);

        //设置Box
        for (int i = 0; i < boxCount; i++)
        {
            GameBox b = new GameBox();
            int x = i % MultiWidth;
            int y = i / MultiWidth;

            b.position = new Vector3(x * boxSize, 0, y * boxSize);

            //从下向上
            int downY = y * 2;
            int leftY = downY + 1;
            int upY = downY + 2;

            //参考项目文件夹下的 棋盘格分配算法 文件
            int ed = downY * MultiWidth + y + x;
            int eu = upY * MultiWidth + y + x + 1;
            int el = leftY * MultiWidth + y + x;
            int er = el + 1;

            //设置边缘
            b.SetEdge(eu, EdgeType.Up);
            b.SetEdge(ed, EdgeType.Down);
            b.SetEdge(el, EdgeType.Left);
            b.SetEdge(er, EdgeType.Right);

            b.boxObject = Instantiate(boxObjectSrc);
            b.boxObject.Init();
            b.boxObject.SetColor(blankColor);
            b.boxObject.transform.position = b.position;

            runtimeData.boxes.Add(b);
        }
    }

    void Init()
    {
        InitData();
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        //初始化场景
        int edgeCount = MultiWidth * (2 * MultiHeight + 1) + MultiHeight;
        InitEdge(edgeCount);

        int boxCount = MultiWidth * MultiHeight;
        InitBox(boxCount);


        //完成初始化，判断玩家或者敌方先手
        //总之先玩家先手
        if (playerBegin)
            mainLoop = PlayerLoop;
        else
        {
            if (UnityEngine.Random.Range(0, 100) >= 50)
            {
                mainLoop = PlayerLoop;
            }
            else
            {
                mainLoop = EnemyLoop;
            }
        }

    }
    #endregion

    /// <summary>
    /// 调试绘制用方法
    /// </summary>
    void DebugLoop()
    {
        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);

        //射线与平面求交
        //求出鼠标指向的，位于世界原点，面向上的桌面的点
        Vector3 castPoint = MathEx.RayCastPlane(cameraRay, tableNormal, Vector3.zero);

        Debug.DrawRay(castPoint, Vector3.up, Color.green);

        for (int i = 0; i < runtimeData.edges.Count; i++)
        {
            if (runtimeData.edges[i].bounds.Contains(castPoint))
            {
                DebugDraw.DrawBounds(runtimeData.edges[i].bounds, Color.blue);

                for (int b = 0; b < runtimeData.boxes.Count; b++)
                {
                    if (runtimeData.boxes[b].edges.Contains(i))
                    {
                        Debug.DrawRay(runtimeData.boxes[b].position, Vector3.forward, Color.blue);
                    }
                }

            }
            else
            {
                DebugDraw.DrawBounds(runtimeData.edges[i].bounds, Color.red);
            }
        }
    }

    #region 主逻辑

    /// <summary>
    /// 临时变量 - 被选择激活的线条
    /// </summary>
    int perActiveEdge = -1;

    /// <summary>
    /// 检查是否已经填满格子
    /// </summary>
    /// <returns></returns>
    bool CheckFinish()
    {
        for (int i = 0; i < runtimeData.boxes.Count; i++)
        {
            if (runtimeData.boxes[i].activeType == 0)
            {
                return false;
            }
        }
        return true;
    }

    void PlayerLoop()
    {
        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);
        Vector3 castPoint = MathEx.RayCastPlane(cameraRay, tableNormal, Vector3.zero);

        Vector3 infoFloatPoint = castPoint;

        for (int i = 0; i < runtimeData.edges.Count; i++)
        {
            //当鼠标指向的地方未被选择，则悬停
            if (runtimeData.edges[i].bounds.Contains(castPoint) && runtimeData.edges[i].activeType == 0)
            {
                infoFloatPoint = runtimeData.edges[i].bounds.center;

                //当点击
                if (Input.GetMouseButtonUp(0))
                {
                    //设置玩家选中的线条
                    perActiveEdge = i;
                    mainLoop = PlayerSetLine;
                    break;
                }
            }
        }
        playerInputInfo.transform.position = infoFloatPoint;
    }

    void PlayerSetLine()
    {
        //Debug.Log($"Player Active {perActiveEdge}");

        //当玩家设置的线条激活了一个Box，则继续回到玩家操作
        bool successActiveABox = false;
        runtimeData.edges[perActiveEdge].activeType = 1;
        runtimeData.edges[perActiveEdge].edgeObject.SetColor(playerColor);
        var connectBoxes = runtimeData.boxes.FindAll(s => s.edges.Contains(perActiveEdge));
        for(int i = 0; i < connectBoxes.Count; i++)
        {
            if(connectBoxes[i].activeType == 0 && connectBoxes[i].AllEdgeSet(runtimeData.edges))
            {
                connectBoxes[i].activeType = 1;
                connectBoxes[i].boxObject.SetColor(playerColor);
                successActiveABox = true;
            }
        }
        //如果已经完成，则直接进入结算
        if(CheckFinish())
        {
            mainLoop = Finish;
            return;
        }

        if(successActiveABox)
        {
            mainLoop = PlayerLoop;
            Score.playerScore += 1;
        }
        else
        {
            Icon_Glow.TurnP1_Off();
            Icon_GlowCPU.TurnCPU_On();
            mainLoop = EnemyLoop;
        }
    }

  

    List<Edge> CopyEdges(List<Edge> edge)
    {
        List<Edge> results = new List<Edge>(edge.Count);
        for(int i = 0; i < edge.Count; i++)
        {
            results.Add(new Edge(edge[i]));
        }
        return results;
    }

    List<GameBox> CopyBoxes(List<GameBox> boxes)
    {
        List<GameBox> result = new List<GameBox>(boxes.Count);
        for(int i = 0; i < boxes.Count; i++)
        {
            result.Add(new GameBox(boxes[i]));
        }
        return result;
    }

    /// <summary>
    /// 从数组取出若干个最大值数据
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    List<int> GetMaxValuesIndex(int[] nums)
    {
        List<int> result = new List<int>(nums.Length);
        int max = int.MinValue;

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] > max)
            {
                max = nums[i];
            }
        }
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == max)
            {
                result.Add(i);
            }
        }

        return result;
    }

    /// <summary>
    /// 尝试勾线
    /// </summary>
    /// <param name="aiEdges">边列表 </param>
    /// <param name="aiBoxes">Box列表 </param>
    /// <param name="currentCtrl">当前操作者 0 = Player， 1 = Enemy </param>
    /// <param name="scores">积分列表</param>
    /// <param name="cid">当前操作积分</param>
    /// <param name="depth">遍历深度</param>
    void TryLine(List<Edge> aiEdges, List<GameBox> aiBoxes, int currentCtrl, int[] scores, int cid, int depth)
    {
        if (depth <= 0) return;

        for (int i = 0; i < aiEdges.Count; i++)
        {
            if (aiEdges[i].activeType > 0) continue;


            //以下的操作在Try完一次之后都要恢复原状
            //因为玩家激活编号为1 AI激活编号为2，所以currentCtrl + 1操作全部为设置对应激活ID
            aiEdges[i].activeType = currentCtrl + 1;

            //找出与当前边共边的全部Box
            //与当前操作边共边的Box里面包含了尚未被填充且顺利包围的Box
            //则将其设置为Enemy激活模式
            //当ctrlBoxes不为空的时候，说明至少获得一分，因此下一个回合都是自己，否则下个回合将交换操作
            var connectBoxes = aiBoxes.FindAll(s => s.edges.Contains(i));
            var ctrlBoxes = connectBoxes.FindAll(s => (s.activeType == 0 && s.AllEdgeSet(aiEdges)));
            if (ctrlBoxes.Count > 0)
            {
                for (int j = 0; j < ctrlBoxes.Count; j++)
                {
                    ctrlBoxes[j].activeType = currentCtrl + 1;
                }
                //增加积分：因为找出了全部可被激活的情况，所以数组长度代表了成功激活的个数，因此数组长度 = 分数
                //如果操作者为玩家，则玩家得分，所以AI判定扣分，如果操作者为AI，则AI判定加分
                //score += (currentCtrl == 0) ? -ctrlBoxes.Count: ctrlBoxes.Count;
                switch(currentCtrl)
                {
                    case 0:
                        scores[cid] -= ctrlBoxes.Count;
                        break;
                    case 1:
                        scores[cid] += ctrlBoxes.Count;
                        break;
                }
                TryLine(aiEdges, aiBoxes, currentCtrl, scores, cid, --depth);
            }
            else //没有可以被激活的Box的时候反转回合操作者
            {
                TryLine(aiEdges, aiBoxes, 1 - currentCtrl, scores, cid, --depth);
            }

            //恢复操作前状态
            aiEdges[i].activeType = 0;
            for (int j = 0; j < ctrlBoxes.Count; j++)
            {
                ctrlBoxes[j].activeType = 0;
            }

        }
    }

    public int Depth = 0;
    void EnemyLoop()
    {
        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);
        Vector3 castPoint = MathEx.RayCastPlane(cameraRay, tableNormal, Vector3.zero);

        Vector3 infoFloatPoint = castPoint;

        for (int i = 0; i < runtimeData.edges.Count; i++)
        {
            //当鼠标指向的地方未被选择，则悬停
            if (runtimeData.edges[i].bounds.Contains(castPoint) && runtimeData.edges[i].activeType == 0)
            {
                infoFloatPoint = runtimeData.edges[i].bounds.center;

                //当点击
                if (Input.GetMouseButtonUp(0))
                {
                    //设置玩家选中的线条
                    perActiveEdge = i;
                    mainLoop = EnemySetLine;
                    break;
                }
            }
        }
        playerInputInfo.transform.position = infoFloatPoint;
    }

    void EnemySetLine()
    {
        bool successActiveABox = false;
        runtimeData.edges[perActiveEdge].activeType = 2;
        runtimeData.edges[perActiveEdge].edgeObject.SetColor(enemyColor);
        var connectBoxes = runtimeData.boxes.FindAll(s => s.edges.Contains(perActiveEdge));
        for (int i = 0; i < connectBoxes.Count; i++)
        {
            if (connectBoxes[i].activeType == 0 && connectBoxes[i].AllEdgeSet(runtimeData.edges))
            {
                connectBoxes[i].activeType = 2;
                connectBoxes[i].boxObject.SetColor(enemyColor);
                successActiveABox = true;
            }
        }

        //如果已经完成，则直接进入结算
        if (CheckFinish())
        {
            mainLoop = Finish;
            return;
        }

        if (successActiveABox)
        {
            mainLoop = EnemyLoop;
            ScoreEnemy.enemyScore += 1;
        }
        else
        {
            Icon_Glow.TurnP1_On();
            Icon_GlowCPU.TurnCPU_Off();
            mainLoop = PlayerLoop;
        }
    }
    #endregion

    void Finish()
    {   

        Debug.Log($"Player : {runtimeData.playerScore} | Enemy : {runtimeData.enemyScroe}");

        if (ScoreEnemy.enemyScore > Score.playerScore)
        {
            Gameresult = "The Enemy ";
            Debug.Log($"Gameresult = {Gameresult}");

        }
        else if (ScoreEnemy.enemyScore < Score.playerScore)
        {
            Gameresult = "The Player ";
            Debug.Log($"Gameresult = {Gameresult}");

        }

        mainLoop = DisplayEndingUI;
   
    }

    void DisplayEndingUI()
    {
        replayScreen.SetActive(true);
        Debug.Log($"Gameresult = {Gameresult}");
        GameOverScreen.Setup(Gameresult);
    }

}
