using UnityEngine;
using System.Collections;

public class MinigameResults : MonoBehaviour
{
    static private MinigameResults results = null;

    private PatientStory _patient;
    bool _hasSucceded = false;

    private MinigameResults() { }

    private void Awake()
    {
        if (results == null)
            results = new MinigameResults();

        DontDestroyOnLoad(results);
    }

    static public MinigameResults GetMinigameResults()
    {
        if (results == null)
            results = new MinigameResults();

        return results;
    }

    public void SetPatient(PatientStory patient)
    {
        this._patient = patient;
    }

    public PatientStory GetPatient()
    {
        return _patient;
    }

    public bool HasSucceded()
    {
        return _hasSucceded;
    }

    public void SetHasSucceded(bool isSaved)
    {
        this._hasSucceded = isSaved;
    }
}
