using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NPCSavingState : MonoBehaviour, ISaveable
{
    public List<NPC> npcs;
    private List<NPCState> npcStates;
    private readonly string fileName = "NPC.json";
    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);

        npcStates = new List<NPCState>();

        if(File.Exists(filePath))
        {
            ReadFile();
        }
        else
        {
            InitNPCFile();
        }
    }

    public void ReadFile()
    {
        NPCStates npcFileStates = JsonArrayHandler<NPCStates>.ReadJsonFile(filePath);
        npcStates.Clear();
        foreach (var npcState in npcFileStates.items)
        {
            npcStates.Add(npcState);
        }

        for (int i = 0; i < npcStates.Count; i++)
        {
            npcs[i].hasTalked = npcStates[i].hasTalked;
        }

    }

    public void WriteFile()
    {
        for(int i = 0; i < npcs.Count; i++)
        {
            npcStates[i].hasTalked = npcs[i].hasTalked;
        }
        JsonArrayHandler<NPCState>.WriteJsonFile(filePath, npcStates);
    }

    public void InitNPCFile()
    {
        int index = 0;
        npcStates.Clear();
        foreach (NPC npc in npcs)
        {
            NPCState state = new NPCState
            {
                name = npcs[index].characterDialog.dialog.name,
                hasTalked = false
            };
            npcStates.Add(state);
            index++;
        }

        WriteFile();
    }

    public void Save()
    {
        WriteFile();
    }

    public void Clear()
    {
        InitNPCFile();
        ReadFile();
    }
}
