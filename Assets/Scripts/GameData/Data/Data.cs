using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class Data 
{
    public AttributesData playerAttributesData;

    public AttributesData shamanAttributesData;

    public Data()
    {
        playerAttributesData = new AttributesData(true);
        shamanAttributesData = new AttributesData(false);
    }   

    public String toString()
    {
        return playerAttributesData.toString();
    }

}
