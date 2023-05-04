using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TemporaryProjectiles
{
    public GameObject ProjectilePref;
    public float ProjectilesTime;
    public TemporaryProjectiles()
    {
        UpdateReelapSystem.Run.AddListener(SubtractDeltaTime);

        void SubtractDeltaTime()
        {
            ProjectilesTime -= Time.deltaTime;
            if (ProjectilesTime <= 0)
            {
                ProjectilePref = null;
                UpdateReelapSystem.Run.RemoveListener(SubtractDeltaTime);
            }
        }
    }
    public TemporaryProjectiles(TemporaryProjectiles temporaryProjectiles) : this()
    {
        ProjectilePref = temporaryProjectiles.ProjectilePref;
        ProjectilesTime = temporaryProjectiles.ProjectilesTime;
    }
    public TemporaryProjectiles(GameObject ProjectilePref, float ProjectilesTime) : this()
    {
        this.ProjectilePref = ProjectilePref;
        this.ProjectilesTime = ProjectilesTime;
    }
}
