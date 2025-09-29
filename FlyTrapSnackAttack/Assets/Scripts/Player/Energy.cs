using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    //settings
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float drainRate = 1f;
    public Slider EnergyBar;

    void Start()
    {
        currentEnergy = maxEnergy;
        EnergyBar.maxValue = maxEnergy;
        EnergyBar.value = currentEnergy;
    }

    void Update()
    {
        //Losing energy over time
        currentEnergy -= drainRate * Time.deltaTime;

        //UI change
        EnergyBar.value = currentEnergy;

        //checking for game over
        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            GameOver();
        }
    }

    //Calculate Damage
    public void Damage(float amount)
    {
        currentEnergy -= amount;
        if (currentEnergy < 0)
        {
            currentEnergy = 0;
        }
    }

    //Calculate Heal
    public void Heal(float amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }


    //Activate Game Over
    private void GameOver()
    {
        GameManager.Instance.SetEndGame();
        SceneManager.LoadScene("GameOverScreen");
    }
}
