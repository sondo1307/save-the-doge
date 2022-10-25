using UnityEngine;

public class LinesDrawer : Singleton<LinesDrawer>
{
    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;
    [SerializeField] private LineRenderer _redLine;
    


    [Space(30f)] public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;
    Line currentLine;
    Camera cam;
    private bool _allow = true;

    public bool AllowDraw = true;

    void Start()
    {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
        GameplayManager.Instance.EndDraw += StartStage;
    }

    void Update()
    {
        var a = Input.mousePosition;
        a.z = 10f;
        a = Camera.main.ScreenToWorldPoint(a);
        if (!_allow || a.y >= 2.5f) return;
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
        GameplayManager.Instance.BeginDraw?.Invoke();
    }

    // Draw ----------------------------------------------------
    void Draw()
    {
        if (GameplayManager.Instance.DrawAmount <= 0)
        {
            // Invoke
            GameplayManager.Instance.EndDraw?.Invoke();
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
            currentLine.AddPoint(currentLine.GetLastPoint());
        }
        else
        {
            _redLine.SetPosition(0, Vector3.zero);
            _redLine.SetPosition(1, Vector3.zero);
            currentLine.AddPoint(mousePosition);
        }
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
                GameplayManager.Instance.EndDraw?.Invoke();
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