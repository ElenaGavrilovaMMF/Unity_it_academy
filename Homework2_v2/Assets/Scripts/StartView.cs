using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartView : MonoBehaviour
{
    public Text subTitle;
    public Text mainMenu;
    private Button[] firstLevelScreenArray;
    private GameObject[] togglesArray;
    private GameObject dropdown;
    private GameObject[] inputArray;
    private GameObject scrollView;
   private List<string> tagsList = new List<string>() {"firstLevel", "secondLevelButtons","secondLevelToggles",
       "secondLevelDropd","secondLevelInput","secondLevelScrollView"};
   private List<Button> firstLevelList = new List<Button>();
   private List<Button> secondLevelButtonsList = new List<Button>();
  
   // Start is called before the first frame update
    void Start()
    {
        firstLevelScreenArray = GameObject.FindObjectsOfType<Button>();
        togglesArray = GameObject.FindGameObjectsWithTag(tagsList[2]);
        inputArray = GameObject.FindGameObjectsWithTag(tagsList[4]);
        dropdown = GameObject.FindGameObjectWithTag(tagsList[3]);
        scrollView = GameObject.FindGameObjectWithTag(tagsList[5]);
        convertArrayToListButton(firstLevelList, tagsList[0]);
        convertArrayToListButton(secondLevelButtonsList,tagsList[1]);
        
        showButtonList(true,firstLevelList);
        setActiveBackButton(false);
        showGameObjectArray(false, togglesArray);
        showButtonList(false, secondLevelButtonsList);
        dropdown.gameObject.SetActive(false);
        showGameObjectArray(false, inputArray);
        scrollView.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        firstLevelList[0].onClick.AddListener(() =>
        {
            showGameObjectArray(true, inputArray);
            showButtonList(false,firstLevelList);
            mainMenu.text = "Input";
            setActiveBackButton(true);
            clickButtonBack();
            subTitle.text = "";
        });
        firstLevelList[1].onClick.AddListener(() =>
        {
            scrollView.gameObject.SetActive(true);
            showButtonList(false,firstLevelList);
            mainMenu.text = "Scroll View";
            setActiveBackButton(true);
            clickButtonBack();
            subTitle.text = "";
        });
        firstLevelList[2].onClick.AddListener(() =>
        {
            showButtonList(false,firstLevelList);
            showButtonList(true,secondLevelButtonsList);
            mainMenu.text = "Buttons";
            setActiveBackButton(true);
            //fuctionOfSecondLevelButtons();
            clickButtonBack();
            
        });
        firstLevelList[3].onClick.AddListener(() =>
        {
            dropdown.gameObject.SetActive(true);
            showButtonList(false,firstLevelList);
            mainMenu.text = "Drops";
            setActiveBackButton(true);
            clickButtonBack();
            subTitle.color = Color.white;
            //subTitle.fontSize = 35;
            subTitle.text = "New Text";
            
        });
        firstLevelList[4].onClick.AddListener(() =>
        {
            showGameObjectArray(true, togglesArray);
            showButtonList(false,firstLevelList);
            mainMenu.text = "Toggles";
            setActiveBackButton(true);
            clickButtonBack();
            
        });
        
    }

    private void showButtonList(bool flag, List<Button> buttonsList)
    {
        for (int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].gameObject.SetActive(flag);
        }
    }

    private void showGameObjectArray(bool flag, GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].gameObject.SetActive(flag);
        }
    }

    private void convertArrayToListButton(List<Button> listButtons, string tag)
    {
        for (int i = 0; i < firstLevelScreenArray.Length; i++)
        {
            if (firstLevelScreenArray[i].tag == tag)
            {
                listButtons.Add(firstLevelScreenArray[i]);
            }
        }
    //    listButtons.Reverse();
    }
    
    private Button findButtonByName(List<Button> listButton, string name)
    {
        for (int i = 0; i < listButton.Count; i++)
        {
            if (listButton[i].name == name)
            {
                return listButton[i];
            }
        }

        return null;
    }

    private void clickButtonBack()
    {
        findButtonByName(firstLevelList, "Back").onClick.AddListener(() =>
        {
            showButtonList(true,firstLevelList);
            setActiveBackButton(false);
            showGameObjectArray(false, togglesArray);
            showButtonList(false, secondLevelButtonsList);
            showGameObjectArray(false, inputArray);
            dropdown.gameObject.SetActive(false);
            scrollView.gameObject.SetActive(false);
            mainMenu.text = "Main Menu";
            subTitle.text = " ";
            subTitle.color = Color.black;
            for (int i = 0; i < secondLevelButtonsList.Count; i++)
            {
                secondLevelButtonsList[i].enabled = true;
            }
            // subTitle.fontSize = 25;
        });
    }

    private void setActiveBackButton(bool flag)
    {
        findButtonByName(firstLevelList, "Back").gameObject.SetActive(flag);
    }

    private void fuctionOfSecondLevelButtons()
    {
         findButtonByName(secondLevelButtonsList, "One").onClick.AddListener(() =>
          {
             subTitle.text = "One Clicked";
         });
         findButtonByName(secondLevelButtonsList, "Two").onClick.AddListener(() =>
         {
             subTitle.text = "Two Clicked";
         });
          findButtonByName(secondLevelButtonsList, "Disable").onClick.AddListener(() =>
          {
              for (int i = 0; i < secondLevelButtonsList.Count-1; i++)
              {
                 secondLevelButtonsList[i].enabled = false;
              }
          });
        findButtonByName(secondLevelButtonsList, "Back").onClick.AddListener(() =>
        {
                
            showButtonList(true,firstLevelList);
            showButtonList(false, secondLevelButtonsList);
            subTitle.text = " ";
        });
    }
}
