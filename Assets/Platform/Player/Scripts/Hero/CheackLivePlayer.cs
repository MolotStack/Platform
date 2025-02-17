using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheackLivePlayer : MonoBehaviour
{
    public void RestartLevel()
    {
        Debug.Log("Перезапуск после смерти");
        ResetLevelZone.instance.ResetLevel();
    }
}
