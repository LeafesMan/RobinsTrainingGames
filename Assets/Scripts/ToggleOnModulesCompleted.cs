/*
 * Auth: Ian
 * 
 * Proj: Robins
 * 
 * Date: 3/6/24
 * 
 * Desc: Toggles the attached button when all modules in the array are completed
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOnModulesCompleted : MonoBehaviour
{
    public List<QuestionModule> modulesToComplete;

    private void Start()
    {   // Disable this button
        GetComponent<Button>().interactable = false;

        // Hook completion of modules to Check Completion
        foreach (var module in modulesToComplete)
        {
            module.ModuleCompleted += () => CheckCompletion(module);
        }
    }

    private void CheckCompletion(QuestionModule module)
    {   // When all required modules are completed reenable button
        modulesToComplete.Remove(module);
        if (modulesToComplete.Count == 0) GetComponent<Button>().interactable = true;
    }
}
