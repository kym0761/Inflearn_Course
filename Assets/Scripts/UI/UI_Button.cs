using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
   
    enum Texts
    { 
    
        PointText,
        ScoreText
    
    }

    enum Buttons
    {
        PointButton
    }

    enum GameObjects
    {
        TestObject
    }

    enum Images
    {
        ItemIcon
    }


    int _Score = 0;


    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //Text text = GetText((int)Texts.ScoreText);
        //if (text != null)
        //{
        //    text.text = "bind Text";
        //}

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;

        AddUIEvent(go, (PointerEventData eventData) =>
       { go.transform.position = eventData.position; }, Define.UIEvent.Drag);

        //Bind With Extension.
        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);


    }


    public void OnButtonClicked(PointerEventData eventData)
    {
        //Debug.Log("ButtonClicked");
        _Score++;
        // _Text.text = $"Á¡¼ö : {_Score}";
        GetText((int)Texts.ScoreText).text = $"Score : {_Score}";

    }




}
