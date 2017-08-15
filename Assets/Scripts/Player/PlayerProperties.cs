using UnityEngine;
using System.Collections;

public class PlayerProperties
{
    public readonly int Number;

    private Vector3 _location = new Vector3(0, 0, 0);
    private int _points = 0;
    private int _extraChancesMultiplier = 0;
    private float _movementResource = 9f;
    private float _movementMultiplayer = 1f;
    private float _cannonPowerMultiplayer = 1f;
    private float _cannonAccuracyMultiplayer = 1f;
    private bool _hasHelmet = false;
    private string _history;

    public PlayerProperties(int number)
    {
        this.Number = number;
    }

    public Vector3 GetLocation()
    {
        return _location;
    }

    public void SetLocation(Vector3 location)
    {
        this._location = location;
    }

    public void ResetLocation()
    {
        this._location = new Vector3(0, 0, 0);
    }

    public int GetPoints()
    {
        return _points;
    }

    public void AddPoints(int points)
    {
        this._points += points;
    }

    public int GetExtraChancesMultiplier()
    {
        return _extraChancesMultiplier;
    }

    public void AddExtraChancesMultiplier(int multiplier)
    {
        this._extraChancesMultiplier += multiplier;
    }

    public float GetMovementMultiplayer()
    {
        return _movementMultiplayer;
    }

    public void AddMovementMultiplayer(float multiplier)
    {
        this._movementMultiplayer += multiplier;
    }

    public float GetCannonPowerMultiplayer()
    {
        return _cannonPowerMultiplayer;
    }

    public void AddCannonPowerMultiplayer(float multiplier)
    {
        this._cannonPowerMultiplayer += multiplier;
    }

    public float GetCannonAccuracyMultiplayer()
    {
        return _cannonAccuracyMultiplayer;
    }

    public void AddCannonAccuracyMultiplayer(float multiplier)
    {
        this._cannonAccuracyMultiplayer += multiplier;
    }

    public bool HasHelmet()
    {
        return _hasHelmet;
    }

    public void SetHasHelmet(bool hasHelmet)
    {
        this._hasHelmet = hasHelmet;
    }

    public string GetHistory()
    {
        return _history;
    }

    public void AddHistory(string action)
    {
        this._history += action + "\n";
    }

    public float GetMovementResouce()
    {
        return _movementResource;
    }

    public void SetMovementResource(float movementResource)
    {
        this._movementResource = movementResource;
    }

    public void SubstractMovementResouce()
    {
        this._movementResource--;
    }

    public void AddMovementResource(float multiplier)
    {
        this._movementResource += multiplier;
    }
}
