/*
 * Auth: Ian
 * 
 * Proj: Robins
 * 
 * Date: /24
 * 
 * Desc: 
 */
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveSelected : MonoBehaviour
{
    EventSystem eventSystem;
    private void Start()
    {
        eventSystem = GetComponent<EventSystem>();
    }
    private void Update()
    {
        eventSystem.SetSelectedGameObject(null);        
    }
}
