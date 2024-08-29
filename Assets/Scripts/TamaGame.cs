using UnityEngine;

public class TamaGame : MonoBehaviour
{
    public GameObject loveParticles;
    private GameObject activeParticles;

    public void InstantiatePArticles()
    {
        if (activeParticles == null)
        {
            GameObject newParticles = Instantiate(loveParticles, Vector3.zero, Quaternion.identity);
            activeParticles = newParticles;
            Destroy(newParticles, 5);
        }
    }
    public void Desactive(GameObject canvas)
    {
        canvas.SetActive(false);
        GameObject.FindObjectOfType<PetControl>().isplaying = false;
    }
}
