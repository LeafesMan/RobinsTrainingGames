/*
 * Auth: Ian
 * 
 * Proj: Robins
 * 
 * Date: 3/6/24
 * 
 * Desc: Interface for handling camera movements
 */
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    Vector2 targetPos, defaultPos;
    float targetZoom, defaultZoom;
    Camera camRef;
    [SerializeField] float lerpPosSpeed;
    [SerializeField] float lerpZoomSpeed;

    private void Start()
    {
        targetPos = defaultPos = transform.position;
        targetZoom = defaultZoom = GetComponent<Camera>().orthographicSize;
        camRef = GetComponent<Camera>();
    }

    private void Update()
    {
        // Interpolate Camera to target
        transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * lerpPosSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10); // Maintain - Z position

        // Interpolate Zoom to target
        camRef.orthographicSize = Mathf.Lerp(camRef.orthographicSize, targetZoom, Time.deltaTime * lerpZoomSpeed);
    }

    public void SetTargetPos(Vector2 targetPos) => this.targetPos = targetPos;
    public void ResetTargetPos() => targetPos = defaultPos;
    public void SetTargetZoom(float zoom) => targetZoom = zoom;
    public void ResetTargetZoom() => targetZoom = defaultZoom;
}
