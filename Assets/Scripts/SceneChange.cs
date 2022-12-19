using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Eflatun.SceneReference;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private SceneReference sceneToSwitchTo;

    public void SwitchScene() => LevelManager.I.SwitchLevel(sceneToSwitchTo);
}
