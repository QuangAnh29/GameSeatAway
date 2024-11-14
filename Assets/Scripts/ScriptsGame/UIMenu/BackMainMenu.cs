using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMainMenu : BaseButton
{
    [SerializeField] private int _senceMainMenu;
    protected override void OnClick()
    {
        SceneManager.LoadScene(_senceMainMenu);
    }
}
