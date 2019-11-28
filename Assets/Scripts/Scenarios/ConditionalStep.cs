﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalStep : Step
{
    public override void OnActivate()
    {
        Update();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stepDescription = $"{Time.time}";
        if (Time.time > 10)
            state = State.COMPLETED;
    }
}
