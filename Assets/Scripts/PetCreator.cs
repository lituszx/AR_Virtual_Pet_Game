using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCreator : MonoBehaviour
{
    [System.Serializable]
    public class PetProperties
    {
        public string targetName;
        public string name;
        public int lvl = 0, life = 100, exp = 0, hungry = 100, happy = 100, cleanness = 100;
    }
    static public List<PetProperties> pets = new List<PetProperties>
    {
        new PetProperties(){ targetName = "Max", name="Max"},
        new PetProperties(){ targetName = "Rex", name="Rex"},
        new PetProperties(){ targetName = "Flapy", name="Flapy"},
        new PetProperties(){ targetName = "Dragon", name="Dragon"},
    };
    static public PetProperties ClonePet(PetProperties _pet)
    {
        PetProperties temPet = new PetProperties()
        {
            targetName = _pet.targetName,
            name = _pet.name,
            lvl = _pet.lvl,
            life = _pet.life,
            exp = _pet.exp,
            hungry = _pet.hungry,
            happy = _pet.happy,
            cleanness = _pet.cleanness,
        };
        return temPet;
    }
    static public PetProperties GetPetByTargetName(string _targetName)
    {
        PetProperties loadPet = new PetProperties();
        if(PlayerPrefs.HasKey(_targetName)== true)
        {
            loadPet = LoadInfoPet(_targetName);
        }
        else
        {
            for (int i = 0; i < pets.Count; i++)
            {
                if (pets[i].targetName == _targetName)
                {
                    loadPet = ClonePet(pets[i]);
                    break;
                }
            }
        }       
        return loadPet;
    }
    static public void SavePetInfo(PetProperties _pet)
    {
        string key = _pet.targetName;
        List<string> petInfo = new List<string>
        {
            _pet.targetName,
            _pet.name,
            _pet.lvl.ToString(),
            _pet.life.ToString(),
            _pet.exp.ToString(),
            _pet.hungry.ToString(),
            _pet.happy.ToString(),
            _pet.cleanness.ToString()
        };
        PlayerPrefsX.SetStringList(key, petInfo);
    }
    static public PetProperties LoadInfoPet(string _key)
    {
        List<string> petInfo = PlayerPrefsX.GetStringList(_key);
        PetProperties loadPet = new PetProperties
        {
            targetName = petInfo[0],
            name = petInfo[1],
            lvl = int.Parse(petInfo[2]),
            life = int.Parse(petInfo[3]),
            exp = int.Parse(petInfo[4]),
            hungry = int.Parse(petInfo[5]),
            happy = int.Parse(petInfo[6]),
            cleanness = int.Parse(petInfo[7]),
        };
        return loadPet;
    }
}
