using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(LineRenderer))]
public class LinearIndicator : IndicatorBase
{
    private Camera _cam;
    private LineRenderer _lineRenderer;
    private RaycastHit _raycastHit;
    private Ray _ray;
    private Vector3 tempPos;
    [SerializeField] private Vector3 offSet;

    private void Awake()
    {
        _cam = Camera.main;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0,transform.position);
    }

    private void Update()
    {
        //Çizginin ikinci noktasının pozisyonu belirlenir
        //Raycast yollanacak

        if (inputVariables.mouseDelta.magnitude > 0)
        {
            _ray = _cam.ScreenPointToRay(Input.mousePosition + offSet);
            if (Physics.Raycast(_ray, out _raycastHit))
            {
                tempPos = _raycastHit.point;
                tempPos.y = transform.position.y;
                _lineRenderer.SetPosition(1,tempPos);
            }
        }
    }
}