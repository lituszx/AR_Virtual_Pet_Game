using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{

    public PetControl.PetInfoProperties myPet;

    private void Awake()
    {
        this.enabled = false;
    }

    private float _timer;
    public void InitGame(PetControl.PetInfoProperties _petInfo)
    {
        myPet = _petInfo;
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (myPet.petObj == null)
            EndGame();

        _timer += Time.deltaTime;
        if (_timer > 4)
        {
            if (myPet.pet.happy < 100)
                myPet.pet.happy++;
            myPet.pet.exp++;
            _timer = 0;
        }
    }

    public void EndGame()
    {
        this.enabled = false;
    }
}
