using UnityEngine;

public class LinesDrawer : Singleton<LinesDrawer>
{
    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    public float DrawAmount = 50f;
    int cantDrawOverLayerIndex;
    [SerializeField] private LineRenderer _redLine;
    


    [Space(30f)] public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;
    Line currentLine;
    Camera cam;
    private bool _allow = true;

    void Start()
    {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
        GameplayManager.StartStage += StartStage;
    }

    void Update()
    {
        if (!_allow) return;
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (currentLine != null)
            Draw();

        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    // Begin Draw ----------------------------------------------
    void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        //Set line properties
        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }

    // Draw ----------------------------------------------------
    void Draw()
    {
        if (DrawAmount <= 0)
        {
            // Invoke
            GameplayManager.StartStage?.Invoke();
            return;
        }

        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        bool h = false;
        if (currentLine.pointsCount > 2)
        {
            var a = Physics2D.Linecast(currentLine.GetLastPoint(), mousePosition, cantDrawOverLayer);
            if (a)
            {
                // print(123);
                h = true;
            }
        }


        //Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw( )
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);

        if (h)
        {
            _redLine.SetPosition(0, currentLine.GetLastPoint());
            _redLine.SetPosition(1, mousePosition);
        }
        else
        {
            _redLine.SetPosition(0, Vector3.zero);
            _redLine.SetPosition(1, Vector3.zero);
        }
        currentLine.AddPoint(h ? currentLine.GetLastPoint() : mousePosition);
    }

    // End Draw ------------------------------------------------
    void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                //If line has one point
                Destroy(currentLine.gameObject);
            }
            else
            {
                //Add the line to "CantDrawOver" layer
                // currentLine.gameObject.layer = cantDrawOverLayerIndex;

                //Activate Physics on the line
                currentLine.UsePhysics(true);

                currentLine = null;
                GameplayManager.StartStage?.Invoke();
            }
            _redLine.SetPosition(0, Vector3.zero);
            _redLine.SetPosition(1, Vector3.zero);
        }
    }

    private void StartStage()
    {
        _allow = false;
    }

    private void OnEnable()
    {
        
    }
}