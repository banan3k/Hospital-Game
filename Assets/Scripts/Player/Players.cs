using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Players
{
    static private Dictionary<int, PlayerProperties> _playersList;
    private static Vector3 _cameraOffset = new Vector3(0, 0, 0);

    static public PlayerProperties GetPlayer(int number)
    {
        CheckForPlayer(number);
        return _playersList[number];
    }

    static private void CheckForPlayer(int number)
    {
        if (_playersList == null)
            _playersList = new Dictionary<int, PlayerProperties>();

        if (!_playersList.ContainsKey(number))
            _playersList.Add(number, new PlayerProperties(number));
    }

    static public Vector3 GetCameraOffset()
    {
        return _cameraOffset;
    }

    static public void SetCameraOffset(Vector3 cameraOffset)
    {
        _cameraOffset = cameraOffset;
    }

    public static void ResetPlayers()
    {
        int i = 0;
        foreach (KeyValuePair<int, PlayerProperties> player in _playersList)
        {
            player.Value.ResetLocation();
        }
    }
}