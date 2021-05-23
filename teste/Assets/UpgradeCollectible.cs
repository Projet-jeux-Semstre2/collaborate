using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCollectible : MonoBehaviour
{
    public GameObject _ameliorationManager;
    private GameObject _player;
    private Player _playerCs;
    public float distanceUse;

    private void OnEnable()
    {
        
        _player = GameObject.FindWithTag("Player");
        _playerCs = _player.GetComponent<Player>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= distanceUse)
        {
            _playerCs.useButtonOn = true;
            if (Input.GetButtonDown("Use"))
            {
                _ameliorationManager.SetActive(true);
                StartCoroutine(_ameliorationManager.GetComponent<AmeliorationManager>().chooseUpgradeRandom());
                PauseMenu.cursorLock = false;
                PauseMenu.canLock = false;
                PauseMenu.pauseTime = true;
                Destroy(gameObject);
                _playerCs.useButtonOn = false;
            }
        }
        if(_playerCs.useButtonOn && Vector3.Distance(transform.position, _player.transform.position) > distanceUse && Vector3.Distance(transform.position, _player.transform.position) <= distanceUse * 2)
        {
            _playerCs.useButtonOn = false;
        }
    }
}
