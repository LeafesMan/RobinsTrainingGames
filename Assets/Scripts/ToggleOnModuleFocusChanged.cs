/*
 * Auth: Ian
 * 
 * Proj: Robins
 * 
 * Date: 3/6/24
 * 
 * Desc: Toggles the buttons interactability when any modules focus changes
 */
using UnityEngine;
using UnityEngine.UI;

public class ToggleOnModuleFocusChanged : MonoBehaviour
{
    private void OnEnable()
    {   // Toggle the interactability of this button when any module is Un/Focused
        QuestionModule.ModuleFocused += DisableInteractability;
        QuestionModule.ModuleUnfocused += EnableInteractability;
    }

    // Unbind Events for new scene
    private void OnDisable()
    {   // Toggle the interactability of this button when any module is Un/Focused
        QuestionModule.ModuleFocused -= DisableInteractability;
        QuestionModule.ModuleUnfocused -= EnableInteractability;
    }

    void DisableInteractability() => GetComponent<Button>().interactable = false;
    void EnableInteractability() => GetComponent<Button>().interactable = true;

}
