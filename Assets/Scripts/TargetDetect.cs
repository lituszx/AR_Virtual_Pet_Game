using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class TargetDetect : MonoBehaviour
{
    private TrackableBehaviour theTrackable;
    private TrackableBehaviour.StatusInfo lastStatus;
    void Start()
    {
        theTrackable = GetComponent<TrackableBehaviour>();
        lastStatus = theTrackable.CurrentStatusInfo;
    }
    void Update()
    {
        if(theTrackable.CurrentStatusInfo != lastStatus)
        {
            if(theTrackable.CurrentStatusInfo == TrackableBehaviour.StatusInfo.NORMAL)
            {
                transform.parent.GetComponent<PetControl>().LoadPetByTarget(transform.GetChild(0).gameObject, theTrackable.TrackableName);
                lastStatus = theTrackable.CurrentStatusInfo;
            }
            if (theTrackable.CurrentStatusInfo == TrackableBehaviour.StatusInfo.UNKNOWN)
            {
                transform.parent.GetComponent<PetControl>().UnloadPetByTarget(theTrackable.TrackableName);
                lastStatus = theTrackable.CurrentStatusInfo;
            }
        }
    }
}
