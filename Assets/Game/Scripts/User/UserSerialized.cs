using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserSerialized
{
    public string characterName;    
    public SerializableVector3 position;        
    public int experience;

    public int questComplete = 0;

    public UserSerialized(string characterName, SerializableVector3 position)
    {
        this.characterName = characterName;
        this.position = position;
        this.questComplete = 0;
    }      

    public string toString()
    {
        return MultiIdiomaManager.instance.GetTexto("MENUHUMANS_NAME") + ":" + characterName + "\n" +               
               "XP:" + experience;
    }

}

