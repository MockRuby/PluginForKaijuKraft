using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class KaijuStats : MonoBehaviour
{
    // Stats for the Kaiju
    public int health; // Health points of the Kaiju
    public int attack; // Attack points of the Kaiju
    public int defence; // Defence points of the Kaiju
    public int speed; // Speed of the Kaiju

    // Enum to represent different stages of life for the Kaiju
    public enum StagesOfLife
    {
        Egg,
        Juvenile,
        Adult
    }

    public StagesOfLife stageOfLife; // Current stage of life for the Kaiju
    public float growth; // Growth progress of the Kaiju

    public float prevSystemTime = 0; // Previous system time for growth calculation

    // Food stats for the Kaiju
    public int generalFood = 0; // General food collected
    public int foodAttack = 0; // Food for increasing attack
    public int foodDefence = 0; // Food for increasing defence
    public int foodHealth = 0; // Food for increasing health
    public int foodSpeed = 0; // Food for increasing speed

    public GameObject egg; // GameObject representing the egg stage
    public GameObject juv; // GameObject representing the juvenile stage
    public GameObject adult; // GameObject representing the adult stage

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Kaiju stats at the Egg stage
        stageOfLife = StagesOfLife.Egg;
        health = 10;
        attack = 10;
        defence = 10;
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        // Check the current stage of life for the Kaiju and update accordingly
        if (stageOfLife == StagesOfLife.Egg)
        {
            EggGrowing();
            egg.SetActive(true);
            juv.SetActive(false);
            adult.SetActive(false);
        }
        else if (stageOfLife == StagesOfLife.Juvenile)
        {
            JuvenileGrowing();
            egg.SetActive(false);
            juv.SetActive(true);
            adult.SetActive(false);
        }
        else
        {
            egg.SetActive(false);
            juv.SetActive(false);
            adult.SetActive(true);
        }

        // Check for key inputs to feed the Kaiju with different types of food
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            growth += 5;
            generalFood += 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            growth += 5;
            foodHealth += 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            growth += 5;
            foodAttack += 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            growth += 5;
            foodDefence += 1;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            growth += 5;
            foodSpeed += 1;
        }
    }

    // Function to handle growth logic for the Egg stage
    void EggGrowing()
    {
        float currentSystemTime  = (float)DateTime.Now.TimeOfDay.TotalSeconds;
        if (prevSystemTime == 0)
        {
            growth += 1 * Time.deltaTime;
        }
        else
        {
            growth += (currentSystemTime - prevSystemTime);
        }
        growth = Mathf.Clamp(growth, 0, 100);

        // Check if the Kaiju has reached the Juvenile stage
        if (growth >= 100)
        {
            stageOfLife = StagesOfLife.Juvenile;
            growth = 0;
            // Increase stats based on collected food
            health += 10 + generalFood + foodHealth * 5;
            attack += 10 + generalFood + foodHealth * 5 ;
            defence += 10 + generalFood + foodDefence * 5;
            speed += 10 + generalFood + foodSpeed * 5;

            // Reset food stats
            generalFood = 0;
            foodAttack = 0;
            foodHealth = 0;
            foodDefence = 0;
            foodSpeed = 0;
        }

        prevSystemTime = currentSystemTime;
    }
    
    // Function to handle growth logic for the Juvenile stage
    void JuvenileGrowing()
    {
        float currentSystemTime  = (float)DateTime.Now.TimeOfDay.TotalSeconds;
        if (prevSystemTime == 0)
        {
            growth += 1 * Time.deltaTime;
        }
        else
        {
            growth += (currentSystemTime - prevSystemTime);
        }
        growth = Mathf.Clamp(growth, 0, 100);

        // Check if the Kaiju has reached the Adult stage
        if (growth >= 100)
        {
            stageOfLife = StagesOfLife.Adult;
            growth = 0;
            // Increase stats based on collected food
            health += 10 + generalFood + foodHealth * 5;
            attack += 10 + generalFood + foodHealth * 5 ;
            defence += 10 + generalFood + foodDefence * 5;
            speed += 10 + generalFood + foodSpeed * 5;

            // Reset food stats
            generalFood = 0;
            foodAttack = 0;
            foodHealth = 0;
            foodDefence = 0;
            foodSpeed = 0;
            
        }

        prevSystemTime = currentSystemTime;
    }
}