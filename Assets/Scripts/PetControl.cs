using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PetControl : MonoBehaviour
{
    [System.Serializable]
    public class PetInfoProperties
    {
        public PetCreator.PetProperties pet;
        public GameObject petObj, canvasPet;
    }
    public List<PetInfoProperties> allPets = new List<PetInfoProperties>();
    public float countdownLife, countdownHungry, countdownHappines, countdownCleanness;
    public Transform gridCanvasPet;
    public GameObject baseCanvasPet;
    public GameObject playPet;
    public GameObject particleToy, particleSpone, particleFood;
    private GameObject activeParticles;
    public float contador;
    public bool isplaying = false;
    void Start()
    {
        //PlayerPrefs.DeleteAll();
    }
    void Update()
    {
        if (isplaying == true)
            contador += Time.deltaTime;
        if (allPets.Count > 0)
        {
            countdownCleanness += Time.deltaTime;
            countdownHappines += Time.deltaTime;
            countdownHungry += Time.deltaTime;
            countdownLife += Time.deltaTime;
            if (countdownLife >= 1)
            {
                countdownLife = 0;
            }
            if (countdownHungry >= 5)
            {
                countdownHungry = 0;
                RemoveHungry();
            }
            if (countdownHappines >= 10)
            {
                countdownHappines = 0;
                RemoveHappines();
            }
            if (countdownCleanness >= 20)
            {
                countdownCleanness = 0;
                RemoveCleanness();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadPetByTarget(null, "Max");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadPetByTarget(null, "Rex");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadPetByTarget(null, "Flapy");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadPetByTarget(null, "Dragon");
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnloadPetByTarget("Max");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            UnloadPetByTarget("Rex");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            UnloadPetByTarget("Flapy");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            UnloadPetByTarget("Dragon");
        }
    }
    public void AddLife()
    {
        for (int i = 0; i < allPets.Count; i++)
        {
            if (allPets[i].pet.hungry >= 100 && allPets[i].pet.life < 100)
            {
                allPets[i].pet.life++;
                PetCreator.SavePetInfo(allPets[i].pet);
                UpdateCanvasPet(allPets[i]);
            }
        }
    }
    public void RemoveLife(PetInfoProperties _petInfo)
    {
        _petInfo.pet.life--;
        if (_petInfo.pet.life < 0)
        {
            _petInfo.pet.life = 0;
        }
        PetCreator.SavePetInfo(_petInfo.pet);
        UpdateCanvasPet(_petInfo);
    }
    private void RemoveCleanness()
    {
        for (int i = 0; i < allPets.Count; i++)
        {
            allPets[i].pet.cleanness--;
            if (allPets[i].pet.cleanness < 0)
            {
                allPets[i].pet.cleanness = 0;
                RemoveLife(allPets[i]);
            }
            PetCreator.SavePetInfo(allPets[i].pet);
            UpdateCanvasPet(allPets[i]);
        }
    }
    private void RemoveHappines()
    {
        for (int i = 0; i < allPets.Count; i++)
        {
            allPets[i].pet.happy--;
            if (allPets[i].pet.happy < 0)
            {
                allPets[i].pet.happy = 0;
                RemoveLife(allPets[i]);
            }
            PetCreator.SavePetInfo(allPets[i].pet);
            UpdateCanvasPet(allPets[i]);
        }
    }
    private void RemoveHungry()
    {
        for (int i = 0; i < allPets.Count; i++)
        {
            allPets[i].pet.hungry--;
            if (allPets[i].pet.hungry < 0)
            {
                allPets[i].pet.hungry = 0;
                RemoveLife(allPets[i]);
            }
            PetCreator.SavePetInfo(allPets[i].pet);
            UpdateCanvasPet(allPets[i]);
        }
    }
    bool HasPet(string _targetName)
    {
        for (int i = 0; i < allPets.Count; i++)
        {
            if (allPets[i].pet.targetName == _targetName)
            {
                return true;
            }
        }
        return false;
    }
    public void LoadPetByTarget(GameObject _obj, string _targetName)
    {
        if (HasPet(_targetName) == false)
        {
            PetInfoProperties newPet = new PetInfoProperties();
            newPet.pet = PetCreator.GetPetByTargetName(_targetName);
            newPet.petObj = _obj;
            newPet.canvasPet = CreateCanvasPet(newPet);
            allPets.Add(newPet);
            UpdateCanvasPet(newPet);
        }
    }
    public void UpdateCanvasPet(PetInfoProperties _petInfo)
    {
        _petInfo.canvasPet.transform.Find("PetName").GetComponent<Text>().text = _petInfo.pet.name;
        _petInfo.canvasPet.transform.Find("LevelInfo").GetComponent<Text>().text = _petInfo.pet.lvl.ToString();
        _petInfo.canvasPet.transform.Find("ExpInfo").GetComponent<Text>().text = _petInfo.pet.exp.ToString();
        _petInfo.canvasPet.transform.Find("LifeInfo").GetComponent<Text>().text = _petInfo.pet.life.ToString();
        _petInfo.canvasPet.transform.Find("HungryInfo").GetComponent<Text>().text = _petInfo.pet.hungry.ToString();
        _petInfo.canvasPet.transform.Find("HappyInfo").GetComponent<Text>().text = _petInfo.pet.happy.ToString();
        _petInfo.canvasPet.transform.Find("CleanInfo").GetComponent<Text>().text = _petInfo.pet.cleanness.ToString();
    }
    public GameObject CreateCanvasPet(PetInfoProperties _petInfo)
    {
        GameObject newCanvas = Instantiate(baseCanvasPet, gridCanvasPet);
        newCanvas.transform.Find("Food").GetComponent<Button>().onClick.AddListener(delegate { AddFood(_petInfo); });
        newCanvas.transform.Find("Toy").GetComponent<Button>().onClick.AddListener(delegate { AddHappinees(_petInfo); });
        newCanvas.transform.Find("Sponge").GetComponent<Button>().onClick.AddListener(delegate { AddCleanness(_petInfo); });
        newCanvas.transform.Find("Play").GetComponent<Button>().onClick.AddListener(delegate { AddPlay(_petInfo); });
        newCanvas.transform.Find("Upgrade").GetComponent<Button>().onClick.AddListener(delegate { AddLevel(_petInfo); });
        return newCanvas;
    }
    public void UnloadPetByTarget(string _targetName)
    {
        if (HasPet(_targetName))
        {
            for (int i = 0; i < allPets.Count; i++)
            {
                if (allPets[i].pet.targetName == _targetName)
                {
                    Destroy(allPets[i].canvasPet);
                    allPets.RemoveAt(i);
                    break;
                }
            }
        }
    }
    void AddPlay(PetInfoProperties _petInfo)
    {
        /*
        playPet.SetActive(true);
        isplaying = true;
        if(contador > 3)
        {
            _petInfo.pet.happy++;
            _petInfo.pet.exp++;
            contador = 0;
        }   */
        playPet.SetActive(true);
        PetController petPlayer = _petInfo.petObj.GetComponent<PetController>();
        petPlayer.enabled = true;
        petPlayer.InitGame(_petInfo);
    }
    void AddFood(PetInfoProperties _petInfo)
    {
        if(_petInfo.pet.hungry < 100)
        {
            if (activeParticles == null)
            {
                GameObject newParticles = Instantiate(particleFood, Vector3.zero, Quaternion.identity);
                activeParticles = newParticles;
                Destroy(newParticles, 2);
            }
            _petInfo.pet.hungry++;
            _petInfo.pet.exp++;
            UpdateCanvasPet(_petInfo);
        }
    }
    void AddHappinees(PetInfoProperties _petInfo)
    {
        if (_petInfo.pet.happy < 100)
        {
            if (activeParticles == null)
            {
                GameObject newParticles = Instantiate(particleToy, Vector3.zero, Quaternion.identity);
                activeParticles = newParticles;
                Destroy(newParticles, 2);
            }
            _petInfo.pet.happy++;
            _petInfo.pet.exp++;
            UpdateCanvasPet(_petInfo);
        }
    }
    void AddCleanness(PetInfoProperties _petInfo)
    {
        if (_petInfo.pet.cleanness < 100)
        {
            if (activeParticles == null)
            {
                GameObject newParticles = Instantiate(particleSpone, Vector3.zero, Quaternion.identity);
                activeParticles = newParticles;
                Destroy(newParticles, 2);
            }
            _petInfo.pet.cleanness++;
            _petInfo.pet.exp++;
            UpdateCanvasPet(_petInfo);
        }
    }
    void AddLevel(PetInfoProperties _petInfo)
    {
        if(_petInfo.pet.exp >= 100)
        {
            _petInfo.pet.exp = _petInfo.pet.exp - 100;
            _petInfo.pet.lvl++;           
        }
    }
}
