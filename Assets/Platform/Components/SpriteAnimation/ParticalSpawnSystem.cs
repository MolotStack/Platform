using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSpawnSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject _prefabsParticls;

    public void CreatePatricls()
    {
        var spawnParticls = Instantiate(_prefabsParticls, _player.transform.position, _player.transform.rotation);
        spawnParticls.transform.localScale = _player.transform.lossyScale;
    }
}
