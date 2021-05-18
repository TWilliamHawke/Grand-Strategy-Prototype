using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIScreensManager : ScriptableObject
{
    UIScreen _activeScreen;
    public event UnityAction<UIScreen> OnScreenOpen;
    public event UnityAction<UIScreen> OnScreenClose;

    public bool isAnyScreenOpen => _activeScreen != null;
    

    public void ToggleScreen(UIScreen screen)
    {
        CloseActiveScreen();
        if(_activeScreen != screen)
        {
            ShowScreen(screen);
        }
        else
        {
            _activeScreen = null;
        }
    }

    public void CloseActiveScreen()
    {
        if(_activeScreen != null)
        {
            _activeScreen.Close();
            OnScreenClose?.Invoke(_activeScreen);
            _activeScreen = null;
        }
    }

    void ShowScreen(UIScreen screen)
    {
        _activeScreen = screen;
        screen.Show();
        OnScreenOpen?.Invoke(screen);
    }

    public void OpenScreen(UIScreen screen)
    {
        CloseActiveScreen();
        ShowScreen(screen);
    }

    void OnEnable()
    {
        _activeScreen = null;
    }

    

}
