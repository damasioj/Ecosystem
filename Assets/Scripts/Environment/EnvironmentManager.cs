﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Responsible for managing a single environment in the scene.
/// Each environment only manages information of the agents, targets, sources and structures in its children or that are associated through the inspector.
/// </summary>
public class EnvironmentManager : MonoBehaviour
{
    public List<BasicAgent> agents;

    #region Properties
    private List<BaseTarget> Targets { get; set; }
    private List<BaseSource> Sources { get; set; }
    private List<BaseStructure> Structures { get; set; }
    #endregion

    void Awake()
    {
        Targets = GetComponentsInChildren<BaseTarget>().ToList();
        Sources = GetComponentsInChildren<BaseSource>().ToList();
        Structures = GetComponentsInChildren<BaseStructure>().ToList();
        
        foreach (var agent in agents)
        {
            agent.TaskDone += OnTaskDone;
            agent.UpdateTarget(Targets);
            agent.UpdateGoal(GetPendingStructures());
        }
    }

    /// <summary>
    /// Returns structures that have yet to be completed.
    /// </summary>
    private IEnumerable<BaseStructure> GetPendingStructures()
    {
        return Structures.Where(s => !s.IsComplete);
    }

    /// <summary>
    /// Informs the environment when the task has finished and updates the finished agent's target and goal.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnTaskDone(object sender, EventArgs e)
    {
        if (sender is BasicAgent callingAgent)
        {
            callingAgent.UpdateTarget(Targets);
            callingAgent.UpdateGoal(GetPendingStructures());
        }
    }
}
